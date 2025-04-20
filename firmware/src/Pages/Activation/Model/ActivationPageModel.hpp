#pragma once

#include "../../Core/ViewModel/ViewModel.hpp"
#include "../../../Configurations/Configuration.hpp"
#include "../../../DeviceState/DeviceState.hpp"
#include "../../../Mqtt/MqttService.hpp"

namespace SmartLock
{
    class ActivationPageModel : public ViewModel
    {
    private:
        Configuration &_config;
        DeviceState &_deviceState;
        MqttService &_mqttService;

        std::string _username;

    public:
        ActivationPageModel(Logger &logger, Configuration &config, DeviceState &deviceState, MqttService &mqttService)
            : ViewModel(logger), _config(config), _deviceState(deviceState), _mqttService(mqttService) {}

        std::string username() const { return _username; }
        void username(const std::string& value) { _username = value; }

        void sendActivateMessage();
        void sendRejectMessage();
    };
}