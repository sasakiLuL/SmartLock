#ifndef ACTIVATION_CONFIRMATION_PAGE_H
#define ACTIVATION_CONFIRMATION_PAGE_H

#include "UIRender.h"
#include "Page.h"
#include "Configuration.h"
#include "MqttService.h"
#include "DeviceStateProvider.h"
#include "ActionsPage.h"
#include "UnactivatedPage.h"

class ActivationConfirmationPage : public Page
{
private:
    UIRender *_render;
    Configuration *_config;
    DeviceStateProvider *_stateProvider;
    MqttService *_mqttService;
    String _username;
    ActionsPage *_actionPage;
    UnactivatedPage *_unactivatedPage;

public:
    ActivationConfirmationPage(
        UIRender *render,
        Configuration *config,
        DeviceStateProvider *stateProvider,
        MqttService *mqttService,
        ActionsPage *actionPage,
        UnactivatedPage *unactivatedPage);
    void setUserName(String username);
    PageName getPageName() override;
    void render() override;
};

#endif