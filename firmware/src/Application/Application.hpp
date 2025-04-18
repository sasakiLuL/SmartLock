#pragma once

#include "../Ui/Render/Render.hpp"
#include "../Logging/Logger.hpp"
#include "../Pages/Core/Controller/Controller.hpp"
#include "../Pages/Actions/ActionsPage.hpp"
#include "../Pages/Activation/ActivationPage.hpp"
#include "../Pages/Deactivation/DeactivationPage.hpp"
#include "../Pages/Initialization/InitializationPage.hpp"
#include "../Pages/Unactivated/UnactivatedPage.hpp"
#include "../Configurations/Configuration.hpp"
#include "../DeviceState/DeviceState.hpp"
#include "../Mqtt/MqttService.hpp"

namespace SmartLock
{
    class Application
    {
    private:
        enum CommandType
        {
            Open,
            Close,
            Deactivate
        };

        const char *ssid = "WestaedtWG";
        const char *password = "inWest2_zuhause";

        Render _render;
        Logger _logger;
        Controller _controller;

        Configuration _configuration;
        DeviceState _deviceState;
        MqttService _mqttService;

        ActionsPageModel _actionsPageModel;
        ActivationPageModel _activationPageModel;
        DeactivationPageModel _deactivationPageModel;
        UnactivatedPageModel _unactivatedPageModel;

        ActionsPage _actionsPage;
        ActivationPage _activationPage;
        DeactivationPage _deactivationPage;
        InitializationPage _initializationPage;
        UnactivatedPage _unactivatedPage;

        void connectToWifi();

        void mqttCallback(const char *topic, uint8_t *payload, unsigned int length);

    public:
        Application();

        void initialize();
        void loop();
    };
}
