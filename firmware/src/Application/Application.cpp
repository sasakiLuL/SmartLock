#include "Application.hpp"
#include "../Pages/Core/Page/Page.hpp"
#include <LittleFS.h>

namespace SmartLock
{
    Application::Application() 
        : _render(),
        _logger(_render),
        _controller(),
        _configuration(_logger),
        _deviceState(_logger),
        _mqttService(_configuration, _logger),

        _actionsPageModel(_logger, _configuration, _deviceState, _mqttService),
        _activationPageModel(_logger, _configuration, _deviceState, _mqttService),
        _deactivationPageModel(_logger, _configuration, _deviceState, _mqttService),
        _unactivatedPageModel(_logger, _configuration),

        _initializationPage(_controller, _render, _logger),
        _unactivatedPage(_controller, _render, _logger, _unactivatedPageModel),
        _actionsPage(_controller, _render, _logger, _actionsPageModel),
        _activationPage(_controller, _render, _logger, _activationPageModel),
        _deactivationPage(_controller, _render, _logger, _deactivationPageModel) {}

    void Application::initialize()
    {
        if (!LittleFS.begin())
        {
            _logger.logError("failed to mount file system");
        }

        _render.initialize();

        _controller.addPage(Path::Initialization, &_initializationPage);
        _controller.addPage(Path::Unactivated, &_unactivatedPage);
        _controller.addPage(Path::Actions, &_actionsPage);
        _controller.addPage(Path::Activation, &_activationPage);
        _controller.addPage(Path::Deactivation, &_deactivationPage);

        connectToWifi();

        try
        {
            _configuration.initialize();
            _deviceState.initialize();
        }
        catch (const std::exception &e)
        {
            _logger.logError(e.what());
        }

        try
        {
            _mqttService.initialize();
        }
        catch (const std::exception &e)
        {
            _logger.logError(e.what());
        }

        _mqttService.setCallback([this](const char *topic, byte *payload, unsigned int length)
        { 
            mqttCallback(topic, payload, length); 
        });

        _mqttService.onConnected([this]()
        {
            if (_deviceState.isActivated())
            {
                _controller.changePage(Path::Actions);
            }
            else
            {
                _controller.changePage(Path::Unactivated);
            }
        });

        _mqttService.onDisconnected([this]()
        {
            _controller.changePage(Path::Initialization);
        });
    }

    void Application::loop()
    {
        _controller.loop();

        _mqttService.loop();
    }

    void Application::connectToWifi()
    {
        WiFi.begin(ssid, password);
        _logger.logInfo("WiFi connection started");
    }

    void Application::mqttCallback(const char *topic, uint8_t *payload, unsigned int length)
    {
        _logger.logInfo("Received message");

        String messageBuffer;

        for (int i = 0; i < length; i++)
            messageBuffer += (char)payload[i];

        JsonDocument messageJson;
        DeserializationError error = deserializeJson(messageJson, messageBuffer);

        _logger.logInfo(topic);
        _logger.logInfo(messageBuffer.c_str());

        if (error)
        {
            _logger.logError("Failed to deserialize message file");
            return;
        }

        if (String(topic).equalsConstantTime(_configuration.activationRequestsPolicy.c_str()))
        {
            if (!_deviceState.isActivated())
            {
                _activationPageModel.username(messageJson["Username"].as<const char *>());
                _controller.changePage(Path::Activation);
            }
        }
        else if (String(topic).equalsConstantTime(_configuration.actionsPolicy.c_str()))
        {
            if (_deviceState.isActivated())
            {
                int32_t commandType = messageJson["CommandType"];

                switch (commandType)
                {
                case CommandType::Open:
                    _deviceState.isOpened(true);
                    _controller.render();
                    break;
                case CommandType::Close:
                    _deviceState.isOpened(false);
                    _controller.render();
                    break;
                case CommandType::Deactivate:
                    _deviceState.isActivated(false);
                    _deviceState.isOpened(true);
                    _controller.changePage(Path::Unactivated);
                    break;
                default:
                    _logger.logError("Invalid CommandType: " + std::to_string(commandType));
                    break;
                }
            }
        }
        else
        {
            _logger.logError("Unknown topic");
        }
    }
}