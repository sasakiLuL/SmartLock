#include "WiFiProvider.hpp"


namespace SmartLock
{
    void WiFiProvider::checkStatus()
    {
        auto status = WiFi.status();

        if (status == WL_CONNECTED)
        {
            _logger.logInfo(std::string("WiFi is connected, IP: ") + WiFi.localIP().toString().c_str());
            _connected = true;
            _reconnectAttempts = 0;
            _onConnectedCallback();
            _disconnectedCallbackInvoked = false;
            return;
        }

        switch (status)
        {
        case WL_NO_SSID_AVAIL:
            _logger.logError("SSID not found: " + _ssid);
            break;
        case WL_CONNECT_FAILED:
            _logger.logError("WiFi connection failed (wrong password?)");
            break;
        case WL_DISCONNECTED:
            _logger.logInfo("WiFi is disconnected");
            break;
        default:
            break;
        }
        _connected = false;
    }

    void WiFiProvider::resetWiFi()
    {
        _logger.logInfo("Resetting WiFi module");
        WiFi.disconnect(true);
        _connected = false;
        _reconnectAttempts = 0;
    }

    void WiFiProvider::initialize()
    {
        _preferences.begin("wifi");
        _ssid = _preferences.getString("ssid", "").c_str();
        _password = _preferences.getString("pass", "").c_str();
        _preferences.end();
    } 

    void WiFiProvider::reconnect()
    {
        initialize();

        _logger.logInfo("Attempting to connect to WiFi");
        _logger.logInfo("SSID: " + _ssid);
        _logger.logInfo("Password: [HIDDEN]");
        WiFi.begin(_ssid.c_str(), _password.c_str());
        _reconnectAttempts++;

        checkStatus();
    }

    void WiFiProvider::loop()
    {
        if (!_connected)
        {
            if (!_disconnectedCallbackInvoked)
            {
                _logger.logError("WiFi is disconnected");
                _onDisconnectedCallback();
                _disconnectedCallbackInvoked = true;
            }

            auto now = std::chrono::steady_clock::now();

            if (now - _lastTryToConnectPoint >= _reconnectTime)
            {
                _lastTryToConnectPoint = now;

                checkStatus();

                if (!_connected)
                {
                    if (_reconnectAttempts >= 3)
                    {
                        resetWiFi();
                    }
                    else
                    {
                        _logger.logInfo("Reconnection attempt #" + std::to_string(_reconnectAttempts));
                        reconnect();
                    }
                }
            }
        }
    }
}