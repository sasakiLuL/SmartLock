#include "ActionsPageModel.hpp"

namespace SmartLock
{
    void ActionsPageModel::toggle()
    {
        _deviceState.isOpened(!_deviceState.isOpened());
    }

    bool ActionsPageModel::isOpened()
    {
        return _deviceState.isOpened();
    }
}
