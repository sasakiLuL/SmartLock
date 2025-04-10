#ifndef DEACTIVATION_PAGE_H
#define DEACTIVATION_PAGE_H

#include "UIRender.h"
#include "Page.h"
#include "Configuration.h"
#include "MqttService.h"
#include "DeviceStateProvider.h"
#include "UnactivatedPage.h"
#include "ActionsPage.h"

class DeactivationPage : public Page
{
private:
    UIRender *_render;
    Configuration *_config;
    MqttService *_mqttService;
    DeviceStateProvider *_deviceStateProvider;
    UnactivatedPage *_unactivatedPage;
    ActionsPage *_actionsPage;

public:
    DeactivationPage(
        UIRender *render,
        Configuration *config,
        MqttService *mqttService,
        DeviceStateProvider *deviceStateProvider,
        UnactivatedPage *unactivatedPage);
    void setActionsPage(ActionsPage *actionsPage);
    PageName getPageName() override;
    void render() override;
};

#endif