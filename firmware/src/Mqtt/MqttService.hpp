#pragma once

#include "../Configurations/Configuration.hpp"
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include "../Logging/Logger.hpp"
#include "../WiFiProvider/WiFiProvider.hpp"
#include <ArduinoJson.h>
#include <chrono>

namespace SmartLock
{
    class MqttService
    {
    private:
        WiFiProvider &_wifiProvider;
        Configuration &_configuration;
        Logger &_logger;
        PubSubClient _mqttClient;
        WiFiClientSecure _networkClient;

        std::chrono::milliseconds _reconnectTime;
        std::chrono::steady_clock::time_point _lastTryToConnectPoint;
        std::function<void()> _onConnectedCallback;
        std::function<void()> _onDisconnectedCallback;
        std::function<void(std::string, JsonDocument)> _mqttCallback;
        bool _connected;
        bool _disconnectedCallbackInvoked;

        void reconnect();

    public:
        MqttService(WiFiProvider& provider, Configuration &configuration, Logger &logger) : 
            _wifiProvider(provider),
            _logger(logger),
            _configuration(configuration),
            _reconnectTime(5000),
            _lastTryToConnectPoint(std::chrono::steady_clock::now()),
            _connected(false),
            _disconnectedCallbackInvoked(false) {}

        void onConnected(std::function<void()> callback) { _onConnectedCallback = callback; }
        void onDisconnected(std::function<void()> callback) { _onDisconnectedCallback = callback; }
        std::function<void(std::string, JsonDocument)> callback() const { return _mqttCallback; }

        void initialize(std::function<void(std::string, JsonDocument)> callback);
        void loop();
        bool publish(std::string topic, JsonDocument message);
        bool publish(std::string topic);


    };
}