#include "ActivationPageModel.hpp"

namespace SmartLock
{
    void ActivationPageModel::sendActivateMessage()
    {
        JsonDocument message;
        message["HardwareId"] = _config.thingName;
        message["ActivationResponse"] = 0;

        if (!_mqttService.publish(_config.activationResponsesPolicy, message))
        {
            _logger.logError("Error was occured while publishing message");
            return;
        }

        _deviceState.isActivated(true);
    }

    void ActivationPageModel::sendRejectMessage()
    {
        JsonDocument message;
        message["HardwareId"] = _config.thingName;
        message["ActivationResponse"] = 1;

        if (!_mqttService.publish(_config.activationResponsesPolicy, message))
        {
            _logger.logError("Error was occured while publishing message");
            return;
        }
    }
}
