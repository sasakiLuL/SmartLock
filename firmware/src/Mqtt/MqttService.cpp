#include "MqttService.hpp"

namespace SmartLock
{
    void MqttService::initialize(std::function<void(std::string, JsonDocument)> callback)
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
        _mqttClient.setBufferSize(4096);

        _mqttCallback = callback;
        _mqttClient.setCallback([this](const char *topic, uint8_t *payload, unsigned int length)
        {
            _logger.logInfo("Received message");
            _logger.logInfo(topic);
            
            String messageBuffer;

            for (int i = 0; i < length; i++)
                messageBuffer += (char)payload[i];
                
            JsonDocument message;
            DeserializationError error = deserializeJson(message, messageBuffer);

            if (error)
            {
                _logger.logError("Failed to deserialize message file");
                return;
            }

            _mqttCallback(std::string(topic), message);
        });
    }

    void MqttService::reconnect()
    {
        _logger.logInfo("Attempting MQTT connection");

        if (!_mqttClient.connect(_configuration.thingName.c_str()))
        {
            _logger.logError("Failed to connect, rc=" + std::to_string(_mqttClient.state()));
            _logger.logError("Retrying in 5 seconds...");
            _connected = false;
            return;
        }

        _logger.logInfo("Connected to AWS IoT!");

        if (!_mqttClient.subscribe(_configuration.deltaTopic.c_str(), 1))
        {
            _logger.logError("Failed to subscribe: " + _configuration.deltaTopic);
            _connected = false;
            return;
        }

        _logger.logInfo("Subscribed: " + _configuration.deltaTopic);

        if (!_mqttClient.subscribe(_configuration.getAcceptedTopic.c_str(), 1))
        {
            _logger.logError("Failed to subscribe: " + _configuration.getAcceptedTopic);
            _connected = false;
            return;
        }

        _logger.logInfo("Subscribed: " + _configuration.getAcceptedTopic);

        delay(500);

        _connected = true;
        _onConnectedCallback();
        _disconnectedCallbackInvoked = false;
    }

    void MqttService::loop()
    {
        if (!_wifiProvider.connected())
        {
            return;
        }

        if (!_connected)
        {
            if (!_disconnectedCallbackInvoked)
            {
                _onDisconnectedCallback();
                _disconnectedCallbackInvoked = true;
            }

            auto now = std::chrono::steady_clock::now();

            if (now - _lastTryToConnectPoint >= _reconnectTime)
            {
                _lastTryToConnectPoint = now;
                reconnect();
            }
        }

        if (_connected)
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

        _logger.logInfo("Published: " + policy);
        return _mqttClient.publish(policy.c_str(), jsonString.c_str());
    }

    bool MqttService::publish(std::string policy)
    {
        if (!_mqttClient.connected())
        {
            return false;
        }

        _logger.logInfo("Published: " + policy);

        return _mqttClient.publish(policy.c_str(), "");
    }
}