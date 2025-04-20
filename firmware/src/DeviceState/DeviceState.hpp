#pragma once

#include "../Logging/Logger.hpp"

#define DeviceStateFilePath "/State"

namespace SmartLock
{
    class DeviceState
    {
    private:
        struct Data
        {
            bool isActivated;
            bool isOpened;
        };

        Logger &_logger;
        Data _data;

        Data readDeviceStateFromFile();
        void writeDeviceStateToFile(Data data);

    public:
        DeviceState(Logger &logger) : _logger(logger) {}

        void initialize();

        bool isActivated() const { return _data.isActivated; }
        bool isOpened() const { return _data.isOpened; }

        void isActivated(bool value);
        void isOpened(bool value);
    };
}
