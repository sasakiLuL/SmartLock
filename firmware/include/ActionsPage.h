#ifndef ACTIONS_PAGE_H
#define ACTIONS_PAGE_H

#include "UIRender.h"
#include "Page.h"
#include "Configuration.h"
#include "MqttService.h"
#include "DeviceStateProvider.h"

class DeactivationPage;

class ActionsPage : public Page
{
private:
    UIRender *_render;
    Configuration *_config;
    MqttService *_mqttService;
    DeviceStateProvider *_deviceStateProvider;
    DeactivationPage *_deactivationPage;

public:
    ActionsPage(UIRender *render, Configuration *config, MqttService *mqttService, DeviceStateProvider *deviceStateProvider, DeactivationPage *deactivationPage);
    PageName getPageName() override;
    void render() override;
};

#endif