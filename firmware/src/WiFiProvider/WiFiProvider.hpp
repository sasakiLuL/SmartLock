#pragma once
#include <chrono>
#include <Preferences.h>
#include <WiFiClientSecure.h>
#include "../Logging/Logger.hpp"

namespace SmartLock
{
    class WiFiProvider
    {
    private:
        Logger &_logger;
        Preferences _preferences;

        std::chrono::milliseconds _reconnectTime;
        std::chrono::steady_clock::time_point _lastTryToConnectPoint;
        std::function<void()> _onConnectedCallback;
        std::function<void()> _onDisconnectedCallback;
        bool _disconnectedCallbackInvoked;
        bool _connected;
        uint8_t _reconnectAttempts;

        std::string _ssid;
        std::string _password;

        void checkStatus();
        void resetWiFi();
    public:
        WiFiProvider(Logger& logger) : 
            _logger(logger),
            _reconnectTime(2000),
            _lastTryToConnectPoint(std::chrono::steady_clock::now()),
            _connected(false),
            _disconnectedCallbackInvoked(false),
            _reconnectAttempts(0) {}

        bool connected() { return _connected; }
        void onConnected(std::function<void()> callback) { _onConnectedCallback = callback; }
        void onDisconnected(std::function<void()> callback) { _onDisconnectedCallback = callback; }

        std::string ssid() const { return _ssid; }
        std::string password() const { return _password; }

        void initialize();
        void reconnect();
        void loop();
    };
}