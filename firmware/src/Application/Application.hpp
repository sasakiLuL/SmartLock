#pragma once

#include "../Ui/Render/Render.hpp"
#include "../Logging/Logger.hpp"
#include "../Pages/Core/Controller/Controller.hpp"
#include "../Pages/Actions/ActionsPage.hpp"
#include "../Pages/Activation/ActivationPage.hpp"
#include "../Pages/Initialization/InitializationPage.hpp"
#include "../Pages/Unactivated/UnactivatedPage.hpp"
#include "../Configurations/Configuration.hpp"
#include "../DeviceState/DeviceState.hpp"
#include "../Mqtt/MqttService.hpp"
#include "../Server/Server.hpp"
#include "../WiFiProvider/WiFiProvider.hpp"
#include <Preferences.h>

namespace SmartLock
{
    class Application
    {
    private:
        Server _server;
        Render _render;
        Logger _logger;
        Controller _controller;

        Preferences _preferences;
        Configuration _configuration;
        DeviceState _deviceState;
        WiFiProvider _wifiProvider;
        MqttService _mqttService;

        ActionsPageModel _actionsPageModel;
        ActivationPageModel _activationPageModel;
        UnactivatedPageModel _unactivatedPageModel;

        ActionsPage _actionsPage;
        ActivationPage _activationPage;
        InitializationPage _initializationPage;
        UnactivatedPage _unactivatedPage;

        void mqttCallback(std::string topic, JsonDocument message);

    public:
        Application();

        void initialize();
        void loop();
    };
}
