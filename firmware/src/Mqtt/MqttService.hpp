#pragma once

#include "../Configurations/Configuration.hpp"
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include "../Logging/Logger.hpp"
#include <ArduinoJson.h>
#include <chrono>

namespace SmartLock
{
    class MqttService
    {
    private:
        Configuration &_configuration;
        Logger &_logger;
        PubSubClient _mqttClient;
        WiFiClientSecure _networkClient;

        std::chrono::milliseconds _reconnectTime;
        std::chrono::steady_clock::time_point _lastTryToConnectMqttPoint;
        std::chrono::steady_clock::time_point _lastTryToConnectWifiPoint;

        void reconnect();
        std::string getCurrentTimeUtc();

        std::function<void()> _onConnectedCallback;
        bool _isDisconnectedCallbackInvoked;
        std::function<void()> _onDisconnectedCallback;

        bool _isConnected;

    public:
        MqttService(Configuration &configuration, Logger &logger) : _logger(logger),
                                                                    _configuration(configuration),
                                                                    _reconnectTime(2000),
                                                                    _lastTryToConnectMqttPoint(std::chrono::steady_clock::now()),
                                                                    _lastTryToConnectWifiPoint(std::chrono::steady_clock::now()),
                                                                    _isConnected(false),
                                                                    _isDisconnectedCallbackInvoked(false) {}

        void onConnected(std::function<void()> callback) { _onConnectedCallback = callback; }
        void onDisconnected(std::function<void()> callback) { _onDisconnectedCallback = callback; }
        void initialize();
        void loop();
        bool publish(std::string, JsonDocument message);
        void setCallback(std::function<void(const char *, uint8_t *, unsigned int)> callback);
    };
}