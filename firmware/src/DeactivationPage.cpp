#include "DeactivationPage.h"

DeactivationPage::DeactivationPage(UIRender *render, Configuration *config, MqttService *mqttService, DeviceStateProvider *deviceStateProvider, UnactivatedPage *unactivatedPage)
{
    _render = render;
    _config = config;
    _mqttService = mqttService;
    _deviceStateProvider = deviceStateProvider;
    _unactivatedPage = unactivatedPage;
}

void DeactivationPage::setActionsPage(ActionsPage *actionsPage)
{
    _actionsPage = actionsPage;
}

PageName DeactivationPage::getPageName()
{
    return PageName::DeactivationConfirmation;
}

void DeactivationPage::render()
{
    _render->deleteUIElements();

    auto infoLabel = _render->createLabel();

    infoLabel->text = "Do you want to deactivateDevice?";
    infoLabel->textSize = 1;
    infoLabel->x = Screen_Width / 2;
    infoLabel->y = Screen_Height / 2 - Letter_Height * infoLabel->textSize;
    infoLabel->color = TFT_WHITE;

    auto acceptButton = _render->createButton();

    acceptButton->text = "Accept";
    acceptButton->textColor = TFT_WHITE;
    acceptButton->color = TFT_DARKGREEN;
    acceptButton->height = 40;
    acceptButton->width = 100;
    acceptButton->x = (Screen_Width - acceptButton->width) / 4;
    acceptButton->y = Screen_Height / 2 - Letter_Height * infoLabel->textSize + Letter_Height * 5;
    acceptButton->onClick = [this]()
    {
        JsonDocument message;
        message["HardwareId"] = this->_config->ThingName;

        this->_render->writeDebug(String("[INFO]: ") + this->_config->DeactivationsPolicy);

        if (!this->_mqttService->publish(this->_config->DeactivationsPolicy, message))
        {
            this->_render->writeDebug("[ERROR] Error while publishing message");
            return;
        }

        this->_deviceStateProvider->setDeviceState({false, true});

        _unactivatedPage->render();
        this->_render->draw();
    };

    auto rejectButton = _render->createButton();

    rejectButton->text = "Reject";
    rejectButton->textColor = TFT_WHITE;
    rejectButton->color = TFT_DARKGREEN;
    rejectButton->height = 40;
    rejectButton->width = 100;
    rejectButton->x = (Screen_Width - rejectButton->width) / 4 * 3;
    rejectButton->y = Screen_Height / 2 - Letter_Height * infoLabel->textSize + Letter_Height * 5;
    rejectButton->onClick = [this]()
    {
        _actionsPage->render();
        this->_render->draw();
    };
}
