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

        _data = {DeviceStatus::Unactivated, false};
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

    void DeviceState::status(DeviceStatus value)
    {
        _data.status = value;
        writeDeviceStateToFile(_data);
    }

    void DeviceState::locked(bool value)
    {
        _data.locked = value;
        writeDeviceStateToFile(_data);
    }
}
