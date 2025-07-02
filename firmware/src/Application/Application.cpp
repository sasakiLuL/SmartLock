#include "Application.hpp"
#include "../Pages/Core/Page/Page.hpp"
#include "../Mqtt/ActionStatus.hpp"
#include "../Mqtt/ActionType.hpp"
#include <LittleFS.h>

namespace SmartLock
{
    Application::Application() : 
        _render(),
        _logger(_render),
        _controller(),
        _configuration(_logger),
        _server(_logger, _configuration),
        _deviceState(_logger),
        _wifiProvider(_logger),
        _mqttService(_wifiProvider, _configuration, _logger),
        
        _actionsPageModel(_logger, _configuration, _deviceState, _mqttService),
        _activationPageModel(_logger, _configuration, _deviceState, _mqttService),
        _unactivatedPageModel(_logger, _configuration),

        _initializationPage(_controller, _render, _logger),
        _unactivatedPage(_controller, _render, _logger, _unactivatedPageModel),
        _actionsPage(_controller, _render, _logger, _actionsPageModel),
        _activationPage(_controller, _render, _logger, _activationPageModel) {}

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

        try
        {
            _configuration.initialize();
            _deviceState.initialize();
        }
        catch (const std::exception &e)
        {
            _logger.logError(e.what());
        }

        _server.initialize();

        _wifiProvider.initialize();

        _server.onCredentialsSave([this]()
        {
            _wifiProvider.reconnect();
        });

        _wifiProvider.onDisconnected([this]()
        {
            std::string message;
            std::string tippMessage;

            if (_wifiProvider.ssid().empty())
            {
                message = "";
                tippMessage = "Connect to \'" + _configuration.serverSSID + "\' to configure WIFI";
            }
            else
            {
                message = "Connecting to the " + _wifiProvider.ssid() + "...";
                tippMessage = "Connect to \'" + _configuration.serverSSID + "\' to configure WIFI";
            }

            _initializationPage.message(message);
            _initializationPage.tipp(tippMessage);
            _controller.changePage(Path::Initialization);
        });

        _wifiProvider.onConnected([this]()
        {
            _initializationPage.message("");
            _initializationPage.tipp("Connected!");
            _controller.changePage(Path::Initialization);
        });

        try
        {
            _mqttService.initialize([this](std::string topic, JsonDocument message)
            {
                mqttCallback(topic, message);
            });
        }
        catch (const std::exception &e)
        {
            _logger.logError(e.what());
        }

        _mqttService.onConnected([this]()
        {
            _mqttService.publish(_configuration.getTopic);

            if (_deviceState.status() == DeviceStatus::Activated)
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
            _initializationPage.message("");
            _initializationPage.tipp("Connecting to the server...");
            _controller.changePage(Path::Initialization);
        });
    }

    void Application::loop()
    {
        _server.loop();

        _wifiProvider.loop();

        _controller.loop();

        _mqttService.loop();
    }

    void Application::mqttCallback(std::string topic, JsonDocument message)
    {
        int32_t rawActionType;
        const char* rawActionId;
        const char* rawParameters;

        if (topic == _configuration.deltaTopic)
        {
            rawActionType = message["state"]["action"]["actionType"] | -1;
            rawActionId = message["state"]["action"]["actionId"] | "";
            rawParameters = message["state"]["action"]["actionArguments"] | "";
        }
        else if (topic == _configuration.getAcceptedTopic)
        {
            rawActionType = message["state"]["desired"]["action"]["actionType"] | -1;
            rawActionId = message["state"]["desired"]["action"]["actionId"] | "";
            rawParameters = message["state"]["desired"]["action"]["actionArguments"] | "";
        }
        else
        {
            _logger.logError("No topic handler was found");
            return;
        }

        if (rawActionType == -1 || rawActionId == "")
        {
            _logger.logWarning("No action to execute was found");
            return;
        }

        ActionType actionType = static_cast<ActionType>(rawActionType);
        std::string actionId(rawActionId);
        std::string parameters(rawParameters);
               
        JsonDocument reportedMessage;

        reportedMessage["state"]["desired"]["action"] = nullptr;
        reportedMessage["state"]["reported"]["action"]["lastExecutedActionId"] = actionId.c_str();
        reportedMessage["state"]["reported"]["action"]["lastExecutedActionStatus"] = static_cast<uint32_t>(ActionStatus::Success);

        reportedMessage["state"]["reported"]["state"]["locked"] = _deviceState.locked();
        reportedMessage["state"]["reported"]["state"]["status"] = static_cast<uint32_t>(_deviceState.status());
        
        switch (actionType)
        {
            case ActionType::Activate:
            {
                switch (_deviceState.status())
                {
                    case DeviceStatus::Activated:
                    {
                        reportedMessage["state"]["reported"]["action"]["lastActionStatus"] = static_cast<uint32_t>(ActionStatus::Failure);

                        if (!_mqttService.publish(_configuration.updateTopic, reportedMessage))
                        {
                            _logger.logError("Failed to publish activation message");
                        }
                        break;
                    }
                    
                    case DeviceStatus::Unactivated:
                    {
                        _activationPageModel.username(parameters);
                        _activationPageModel.actionId(actionId);
                        _controller.changePage(Path::Activation);
                        break;
                    }
                    
                    default:
                    {
                        _logger.logWarning("No device state found");
                        break;
                    }
                }                
                break;
            }
        
            case ActionType::Deactivate:
            {
                _deviceState.locked(false);
                _deviceState.status(DeviceStatus::Unactivated);

                reportedMessage["state"]["reported"]["state"]["locked"] = false;
                reportedMessage["state"]["reported"]["state"]["status"] = static_cast<uint32_t>(DeviceStatus::Unactivated);

                if (!_mqttService.publish(_configuration.updateTopic, reportedMessage))
                {
                    _logger.logError("Failed to publish deactivation message");
                    break;
                }

                _controller.changePage(Path::Unactivated);
                break;
            }

            case ActionType::Lock:
            {
                _deviceState.locked(true);

                reportedMessage["state"]["reported"]["state"]["locked"] = true;

                if (!_mqttService.publish(_configuration.updateTopic, reportedMessage))
                {
                    _logger.logError("Failed to publish lock message");
                }                
                break;
            }

            case ActionType::Unlock:
            {
                _deviceState.locked(false);

                reportedMessage["state"]["reported"]["state"]["locked"] = false;

                if (!_mqttService.publish(_configuration.updateTopic, reportedMessage))
                {
                    _logger.logError("Failed to publish lock message");
                }
                break;
            }
        
            default:
            {
                _logger.logWarning("No action type was found");
                break;
            }
        }

        _controller.render();
    }
}