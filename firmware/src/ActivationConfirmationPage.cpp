#include "ActivationConfirmationPage.h"
#include <time.h>

ActivationConfirmationPage::ActivationConfirmationPage(
    UIRender *render,
    Configuration *config,
    DeviceStateProvider *stateProvider,
    MqttService *mqttService,
    ActionsPage *actionPage,
    UnactivatedPage *unactivatedPage)
{
    _render = render;
    _config = config;
    _stateProvider = stateProvider;
    _mqttService = mqttService;
    _actionPage = actionPage;
    _unactivatedPage = unactivatedPage;
}

void ActivationConfirmationPage::setUserName(String username)
{
    _username = username;
}

PageName ActivationConfirmationPage::getPageName()
{
    return PageName::ActivationConfirmation;
}

void ActivationConfirmationPage::render()
{
    _render->deleteUIElements();

    auto usernameLabel = _render->createLabel();

    usernameLabel->text = _username;
    usernameLabel->textSize = 2;
    usernameLabel->x = Screen_Width / 2;
    usernameLabel->y = Screen_Height / 2 - Letter_Height * usernameLabel->textSize;
    usernameLabel->color = TFT_WHITE;

    auto infoLabel = _render->createLabel();

    infoLabel->text = "tries to a activate device. Activate?";
    infoLabel->textSize = 1;
    infoLabel->x = Screen_Width / 2;
    infoLabel->y = Screen_Height / 2 - Letter_Height * infoLabel->textSize + Letter_Height * usernameLabel->textSize;
    infoLabel->color = TFT_WHITE;

    auto acceptButton = _render->createButton();

    acceptButton->text = "Accept";
    acceptButton->textColor = TFT_WHITE;
    acceptButton->color = TFT_DARKGREEN;
    acceptButton->height = 40;
    acceptButton->width = 100;
    acceptButton->x = (Screen_Width - acceptButton->width) / 4;
    acceptButton->y = Screen_Height / 2 - Letter_Height * usernameLabel->textSize + Letter_Height * 5;
    acceptButton->onClick = [this]()
    {
        JsonDocument message;
        message["HardwareId"] = this->_config->ThingName;
        message["ActivationResponse"] = 0;

        if (!this->_mqttService->publish(this->_config->ActivationResponsesPolicy, message))
        {
            this->_render->writeDebug("[ERROR] Error while publishing message");
            return;
        }

        auto newState = DeviceState();
        newState.isDeviceActivated = true;
        newState.isOpened = this->_stateProvider->getDeviceState().isOpened;

        this->_stateProvider->setDeviceState(newState);

        _actionPage->render();
        this->_render->draw();
    };

    auto rejectButton = _render->createButton();

    rejectButton->text = "Reject";
    rejectButton->textColor = TFT_WHITE;
    rejectButton->color = TFT_DARKGREEN;
    rejectButton->height = 40;
    rejectButton->width = 100;
    rejectButton->x = (Screen_Width - rejectButton->width) / 4 * 3;
    rejectButton->y = Screen_Height / 2 - Letter_Height * usernameLabel->textSize + Letter_Height * 5;
    rejectButton->onClick = [this]()
    {
        JsonDocument message;
        message["HardwareId"] = this->_config->ThingName;
        message["ActivationResponse"] = 1;

        if (!this->_mqttService->publish(this->_config->ActivationResponsesPolicy, message))
        {
            this->_render->writeDebug("[ERROR] Error while publishing message");
            return;
        }

        _unactivatedPage->render();
        this->_render->draw();
    };
}
