#pragma once

#include "../Logging/Logger.hpp"
#include "DeviceStatus.hpp"

#define DeviceStateFilePath "/State"

namespace SmartLock
{
    class DeviceState
    {
    private:
        struct Data
        {
            DeviceStatus status;
            bool locked;
        };

        Logger &_logger;
        Data _data;

        Data readDeviceStateFromFile();
        void writeDeviceStateToFile(Data data);

    public:
        DeviceState(Logger &logger) : _logger(logger) {}

        void initialize();

        DeviceStatus status() const { return _data.status; }
        bool locked() const { return _data.locked; }

        void status(DeviceStatus value);
        void locked(bool value);
    };
}
