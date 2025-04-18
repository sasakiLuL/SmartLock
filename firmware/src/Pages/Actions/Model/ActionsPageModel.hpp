#include "../../Core/ViewModel/ViewModel.hpp"
#include "../../../Configurations/Configuration.hpp"
#include "../../../DeviceState/DeviceState.hpp"
#include "../../../Mqtt/MqttService.hpp"

namespace SmartLock
{
    class ActionsPageModel : public ViewModel
    {
    private:
        Configuration &_config;
        DeviceState &_deviceState;
        MqttService &_mqttService;

    public:
        ActionsPageModel(Logger &logger, Configuration &config, DeviceState &deviceState, MqttService &mqttService)
            : ViewModel(logger), _config(config), _deviceState(deviceState), _mqttService(mqttService) {}

        void toggle();
        bool isOpened();
    };
}