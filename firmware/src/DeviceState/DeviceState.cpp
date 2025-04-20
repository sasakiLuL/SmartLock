#include "DeviceState.hpp"
#include "LittleFS.h"
#include "FS.h"

namespace SmartLock
{
    void DeviceState::initialize()
    {
        if (LittleFS.exists(DeviceStateFilePath))
        {
            _data = readDeviceStateFromFile();
            return;
        }

        _data = {false, true};
        writeDeviceStateToFile(_data);
    }

    DeviceState::Data DeviceState::readDeviceStateFromFile()
    {
        fs::File file = LittleFS.open(DeviceStateFilePath, "r");

        if (!file)
        {
            throw std::runtime_error("Failed to open state state file for writing");
        }

        Data data;

        file.read((uint8_t *)&data, sizeof(Data));
        file.close();

        return data;
    }

    void DeviceState::writeDeviceStateToFile(Data data)
    {
        fs::File file = LittleFS.open(DeviceStateFilePath, "w", true);

        if (!file)
        {
            throw std::runtime_error("Failed to open devie state file for reading");
        }

        file.write((uint8_t *)&data, sizeof(Data));
        file.close();
    }

    void DeviceState::isActivated(bool value)
    {
        _data.isActivated = value;
        writeDeviceStateToFile(_data);
    }

    void DeviceState::isOpened(bool value)
    {
        _data.isOpened = value;
        writeDeviceStateToFile(_data);
    }
}
