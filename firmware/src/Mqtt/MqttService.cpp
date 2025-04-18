#include "MqttService.hpp"

namespace SmartLock
{
    void MqttService::initialize()
    {
        _logger.logInfo("Initializing MQTT Service...");
        _logger.logInfo("CA Cert length: " + std::to_string(_configuration.amazonRootCA1.length()));
        _logger.logInfo("Device Cert length: " + std::to_string(_configuration.deviceCertificate.length()));

        _logger.logInfo("Private Key length: " + std::to_string(_configuration.devicePrivateKey.length()));
        _logger.logInfo("AWS Endpoint: " + _configuration.awsEndpoint);
        _logger.logInfo("AWS Port: " + std::to_string(_configuration.awsPort));
        _logger.logInfo("Thing Name: " + _configuration.thingName);

        _networkClient.setCACert(_configuration.amazonRootCA1.c_str());
        _networkClient.setCertificate(_configuration.deviceCertificate.c_str());
        _networkClient.setPrivateKey(_configuration.devicePrivateKey.c_str());

        _mqttClient.setClient(_networkClient);
        _mqttClient.setServer(_configuration.awsEndpoint.c_str(), _configuration.awsPort);
    }

    void MqttService::reconnect()
    {
        if (!_mqttClient.connected())
        {
            _logger.logInfo("Attempting MQTT connection");

            if (!_mqttClient.connect(_configuration.thingName.c_str()))
            {
                _logger.logError("Failed to connect, rc=" + std::to_string(_mqttClient.state()));
                _logger.logError("Retrying in 5 seconds...");
                _isConnected = false;
                return;
            }

            _logger.logInfo("Connected to AWS IoT!");
        }

        if (!_mqttClient.subscribe(_configuration.actionsPolicy.c_str()))
        {
            _logger.logError("Failed to subscribe: " + _configuration.actionsPolicy);
            _isConnected = false;
            return;
        }
        else
        {
            _logger.logInfo("Subscribed: " + _configuration.actionsPolicy);
        }

        if (!_mqttClient.subscribe(_configuration.activationRequestsPolicy.c_str()))
        {
            _logger.logError("Failed to subscribe: " + _configuration.activationRequestsPolicy);
            _isConnected = false;
            return;
        }
        else
        {
            _logger.logInfo("Subscribed: " + _configuration.activationRequestsPolicy);
        }

        _isConnected = true;
        _onConnectedCallback();
        _isDisconnectedCallbackInvoked = false;
    }

    void MqttService::loop()
    {
        if (WiFi.status() != WL_CONNECTED)
        {
            if (!_isDisconnectedCallbackInvoked)
            {
                _onDisconnectedCallback();
                _isDisconnectedCallbackInvoked = true;
            }

            auto now = std::chrono::steady_clock::now();

            if (now - _lastTryToConnectMqttPoint >= _reconnectTime)
            {
                _lastTryToConnectMqttPoint = now;
                _logger.logWarning("Wifi is disconnected. Retrying after reconnecting");
            } 

            return;
        }

        if (!_isConnected)
        {
            if (!_isDisconnectedCallbackInvoked)
            {
                _onDisconnectedCallback();
                _isDisconnectedCallbackInvoked = true;
            }

            auto now = std::chrono::steady_clock::now();

            if (now - _lastTryToConnectMqttPoint >= _reconnectTime)
            {
                _lastTryToConnectMqttPoint = now;
                reconnect();
            }
        }

        if (_isConnected)
        {
            _mqttClient.loop();
        }        
    }

    bool MqttService::publish(std::string policy, JsonDocument message)
    {
        if (!_mqttClient.connected())
        {
            return false;
        }

        String jsonString;

        serializeJsonPretty(message, jsonString);

        return _mqttClient.publish(policy.c_str(), jsonString.c_str());
    }

    void MqttService::setCallback(std::function<void(const char *, uint8_t *, unsigned int)> callback)
    {
        _mqttClient.setCallback(callback);
    }

    std::string MqttService::getCurrentTimeUtc()
    {

        struct tm timeinfo;
        if (!getLocalTime(&timeinfo))
        {
            Serial.println("Failed to obtain time");
            return "0000-00-00T00:00:00Z";
        }

        char buffer[25];
        strftime(buffer, sizeof(buffer), "%Y-%m-%dT%H:%M:%SZ", &timeinfo);

        return std::string(buffer);
    }
}