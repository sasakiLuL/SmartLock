#include "ActivationPageModel.hpp"
#include "../../../Mqtt/ActionStatus.hpp"
#include "../../../Mqtt/ActionType.hpp"

namespace SmartLock
{
    void ActivationPageModel::sendActivateMessage()
    {
        _deviceState.status(DeviceStatus::Activated);

        JsonDocument message;

        message["state"]["desired"]["action"] = nullptr;
        message["state"]["reported"]["state"]["locked"] = _deviceState.locked();
        message["state"]["reported"]["state"]["status"] = static_cast<int>(_deviceState.status());
        message["state"]["reported"]["action"]["lastExecutedActionId"] = _actionid.c_str();
        message["state"]["reported"]["action"]["lastExecutedActionStatus"] = static_cast<uint32_t>(ActionStatus::Success);
        message["state"]["reported"]["action"]["lastExecutedAt"] = millis();

        if (!_mqttService.publish(_config.updateTopic, message))
        {
            _logger.logError("Error was occured while publishing message");
        }
    }

    void ActivationPageModel::sendRejectMessage()
    {
        _deviceState.status(DeviceStatus::Unactivated);

        JsonDocument message;

        message["state"]["desired"]["action"] = nullptr;
        message["state"]["reported"]["state"]["locked"] = _deviceState.locked();
        message["state"]["reported"]["state"]["status"] = static_cast<int>(_deviceState.status());
        message["state"]["reported"]["action"]["lastExecutedActionId"] = _actionid.c_str();
        message["state"]["reported"]["action"]["lastExecutedActionStatus"] = static_cast<uint32_t>(ActionStatus::Failure);
        message["state"]["reported"]["action"]["lastExecutedAt"] = millis();

        if (!_mqttService.publish(_config.updateTopic, message))
        {
            _logger.logError("Error was occured while publishing message");
        }
    }
}
