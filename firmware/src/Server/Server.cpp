#include "Server.h"
#include "Server.hpp"

namespace SmartLock
{
    Server::Server(Logger& logger, Configuration& config) : _server(80), _logger(logger), _configuration(config), _dns()
    {
        _rootCallback = [this]()
        {
            _server.send(200, "text/html", _configuration.configurationPage.c_str());                              
        };

        _saveCredentialsCallback = [this]()
        {
            _preferences.begin("wifi", false);
            _preferences.putString("ssid", _server.arg("ssid"));
            _preferences.putString("pass", _server.arg("pass"));
            _preferences.end();

            _logger.logInfo("Wifi credentials were set");

            _server.send(200, "text/html", "OK");

            _onCredentialsSaveCallback();
        };
    }

    void Server::initialize()
    {
        WiFi.softAP(_configuration.serverSSID.c_str());

        _dns.start(53, "*", WiFi.softAPIP());

        _logger.logInfo(std::string("SoftAP started at:") + WiFi.softAPIP().toString().c_str());

        _server.on("/", HTTP_GET, _rootCallback);
        _server.on("/save", HTTP_POST, _saveCredentialsCallback);
        _server.onNotFound([this]()
        {
            _server.sendHeader("Location", "/", true);
            _server.send(302, "text/plain", "");
        });

        _server.begin();
    }

    void Server::loop()
    {
        _dns.processNextRequest();
        _server.handleClient();
    }
}