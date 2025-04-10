#include "Logger.h"
#include "DeviceState.h"
#ifndef DEVICE_STATE_PROVIDER_H
#define DEVICE_STATE_PROVIDER_H

#define Device_State_File_Path "/State"

class DeviceStateProvider
{
private:
    DeviceState _deviceState;
    Logger *_logger;

    DeviceState readDeviceStateFromFile();
    void writeDeviceStateToFile(DeviceState state);

public:
    DeviceStateProvider(Logger *logger);

    void init();

    DeviceState getDeviceState();
    void setDeviceState(DeviceState state);
};

#endif