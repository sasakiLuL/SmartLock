#include "MqttService.h"

MqttService::MqttService(Configuration *configuration, Logger *logger)
{
    _configuration = configuration;
    _logger = logger;
}

void MqttService::init()
{
    _logger->logInfo("Initializing MQTT Service...");
    _logger->logInfo(String("CA Cert length: ") + _configuration->AmazonRootCA1.length());
    _logger->logInfo(String("Device Cert length: ") + _configuration->DeviceCertificate.length());

    _logger->logInfo(String("Private Key length: ") + _configuration->DevicePrivateKey.length());
    _logger->logInfo(String("AWS Endpoint: ") + _configuration->AwsEndpoint);
    _logger->logInfo(String("AWS Port: ") + _configuration->AwsPort);
    _logger->logInfo(String("Thing Name: ") + _configuration->ThingName);

    _networkClient.setCACert(_configuration->AmazonRootCA1.c_str());
    _networkClient.setCertificate(_configuration->DeviceCertificate.c_str());
    _networkClient.setPrivateKey(_configuration->DevicePrivateKey.c_str());

    _mqttClient.setClient(_networkClient);
    _mqttClient.setServer(_configuration->AwsEndpoint.c_str(), _configuration->AwsPort);
}

bool MqttService::connect()
{
    if (_mqttClient.connected())
    {
        return true;
    }

    if (_mqttClient.connect(_configuration->ThingName.c_str()))
    {
        _logger->logInfo("Connected to AWS IoT!");

        _mqttClient.subscribe(_configuration->ActionsPolicy.c_str());
        _logger->logInfo(String("Subscribed: ") + _configuration->ActionsPolicy);

        _mqttClient.subscribe(_configuration->ActivationRequestsPolicy.c_str());
        _logger->logInfo(String("Subscribed: ") + _configuration->ActivationRequestsPolicy);

        return true;
    }

    return false;
}

void MqttService::loop()
{
    if (!_mqttClient.connected())
    {
        if (!connect())
        {
            _logger->logError(String("Failed to connect, rc=") + _mqttClient.state());
            _logger->logError("Retrying in 5 seconds...");
            delay(5000);
        }
    }
    _mqttClient.loop();
}

bool MqttService::publish(String policy, JsonDocument message)
{
    if (_mqttClient.connected())
    {
        String jsonString;

        serializeJsonPretty(message, jsonString);

        _mqttClient.publish(policy.c_str(), jsonString.c_str());

        return true;
    }

    return false;
}

void MqttService::setCallback(function<void(char *, uint8_t *, unsigned int)> callback)
{
    _mqttClient.setCallback(callback);
}

String MqttService::getCurrentTimeUtc()
{

    struct tm timeinfo;
    if (!getLocalTime(&timeinfo))
    {
        Serial.println("Failed to obtain time");
        return "0000-00-00T00:00:00Z";
    }

    char buffer[25];
    strftime(buffer, sizeof(buffer), "%Y-%m-%dT%H:%M:%SZ", &timeinfo);

    return String(buffer);
}

String MqttService::generateGuid()
{
    char guid[37];
    snprintf(guid, sizeof(guid),
             "%08X-%04X-%04X-%04X-%08X%04X",
             esp_random(), esp_random() & 0xFFFF, (esp_random() & 0x0FFF) | 0x4000,
             (esp_random() & 0x3FFF) | 0x8000, esp_random(), esp_random() & 0xFFFF);

    return String(guid);
}
