#include "DeactivationPageModel.hpp"

namespace SmartLock
{
    void DeactivationPageModel::sendDeactivationMessage()
    {
        JsonDocument message;
        message["HardwareId"] = _config.thingName.c_str();

        if (!_mqttService.publish(_config.deactivationsPolicy, message))
        {
            _logger.logError("Error was occured while publishing message");
            return;
        }

        _deviceState.isActivated(false);
        _deviceState.isOpened(true);
    }
}