#include "ActionsPageModel.hpp"

namespace SmartLock
{
    void ActionsPageModel::toggle()
    {
        _deviceState.locked(!_deviceState.locked());

        JsonDocument message;

        message["state"]["reported"]["state"]["locked"] = _deviceState.locked();
        message["state"]["reported"]["state"]["status"] = (uint32_t)_deviceState.status();

        if (!_mqttService.publish(_config.updateTopic, message))
        {
            _logger.logError("Error was occured while publishing message");
        }
    }

    bool ActionsPageModel::locked()
    {
        return _deviceState.locked();
    }
}
