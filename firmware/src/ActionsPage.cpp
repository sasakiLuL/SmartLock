#include "ActionsPage.h"
#include "DeactivationPage.h"

ActionsPage::ActionsPage(UIRender *render, Configuration *config, MqttService *mqttService, DeviceStateProvider *deviceStateProvider, DeactivationPage *deactivationPage)
{
    _render = render;
    _config = config;
    _mqttService = mqttService;
    _deviceStateProvider = deviceStateProvider;
    _deactivationPage = deactivationPage;
}

PageName ActionsPage::getPageName()
{
    return PageName::Actions;
}

void ActionsPage::render()
{
    _render->deleteUIElements();

    auto openButton = _render->createButton();

    openButton->text = _deviceStateProvider->getDeviceState().isOpened ? "Close" : "Open";
    openButton->textColor = TFT_BLACK;
    openButton->color = _deviceStateProvider->getDeviceState().isOpened ? TFT_DARKGREEN : TFT_RED;
    openButton->height = 40;
    openButton->width = 150;
    openButton->x = (Screen_Width - openButton->width) / 2;
    openButton->y = (Screen_Height - openButton->height) / 2;
    openButton->onClick = [this, openButton]()
    {
        if (_deviceStateProvider->getDeviceState().isOpened)
        {
            _deviceStateProvider->setDeviceState({true, false});
            openButton->text = "Close";
            openButton->color = TFT_DARKGREEN;
        }
        else
        {
            _deviceStateProvider->setDeviceState({true, true});
            openButton->text = "Open";
            openButton->color = TFT_RED;
        }

        this->render();
        this->_render->draw();
    };

    auto deactivateButton = _render->createButton();

    deactivateButton->text = "Deactivate";
    deactivateButton->textColor = TFT_BLACK;
    deactivateButton->color = TFT_RED;
    deactivateButton->height = 40;
    deactivateButton->width = 150;
    deactivateButton->x = (Screen_Width - openButton->width) / 2;
    deactivateButton->y = Screen_Height / 2 + 50;
    deactivateButton->onClick = [this]()
    {
        this->_deactivationPage->render();
        this->_render->draw();
    };
}
