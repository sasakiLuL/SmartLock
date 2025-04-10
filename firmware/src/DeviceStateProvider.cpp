#include "DeviceStateProvider.h"
#include "LittleFS.h"
#include "FS.h"

using fs::File;

DeviceState DeviceStateProvider::readDeviceStateFromFile()
{
    File file = LittleFS.open(Device_State_File_Path, "r");

    if (!file)
    {
        _logger->logError("Failed to open state state file for writing");
        return DeviceState();
    }

    DeviceState state;

    file.read((uint8_t *)&state, sizeof(DeviceState));
    file.close();

    return state;
}

void DeviceStateProvider::writeDeviceStateToFile(DeviceState state)
{
    File file = LittleFS.open(Device_State_File_Path, "w", true);

    if (!file)
    {
        _logger->logError("Failed to open devie state file for reading");
        return;
    }

    file.write((uint8_t *)&state, sizeof(DeviceState));
    file.close();
}

DeviceStateProvider::DeviceStateProvider(Logger *logger)
{
    _logger = logger;
}

void DeviceStateProvider::init()
{
    if (LittleFS.exists(Device_State_File_Path))
    {
        _deviceState = readDeviceStateFromFile();

        return;
    }

    writeDeviceStateToFile({false, true});
    _deviceState = {false, true};
}

DeviceState DeviceStateProvider::getDeviceState()
{
    return _deviceState;
}

void DeviceStateProvider::setDeviceState(DeviceState state)
{
    if (!LittleFS.begin())
    {
        _logger->logError("Failed to mount file system");
        return;
    }

    writeDeviceStateToFile(state);
    _deviceState = state;

    LittleFS.end();
}
