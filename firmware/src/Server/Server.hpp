#pragma once

#include "../Logging/Logger.hpp"
#include "../Configurations/Configuration.hpp"
#include <WebServer.h>
#include <DNSServer.h>
#include <Preferences.h>

namespace SmartLock
{
    class Server
    {
    private:
        IPAddress _serverAddress;
        WebServer _server;
        DNSServer _dns;
        Logger& _logger;
        Configuration& _configuration;
        Preferences _preferences;

        std::string _targetSSID;
        std::string _targetPassword;

        std::function<void()> _rootCallback;
        std::function<void()> _saveCredentialsCallback;
        std::function<void()> _onCredentialsSaveCallback;

    public:
        Server(Logger& logger, Configuration& config);
        void initialize();
        void onCredentialsSave(std::function<void()> callback) { _onCredentialsSaveCallback = callback; }
        std::string getIP() const { return WiFi.softAPIP().toString().c_str(); }
        void loop();
    };
}

