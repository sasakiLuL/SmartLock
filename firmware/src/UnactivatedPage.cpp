#include "UnactivatedPage.h"

UnactivatedPage::UnactivatedPage(UIRender *render, Configuration *config)
{
    _render = render;
    _config = config;
}

PageName UnactivatedPage::getPageName()
{
    return PageName::Unactivated;
}

void UnactivatedPage::render()
{
    _render->deleteUIElements();

    auto logoLabel = _render->createLabel();

    logoLabel->text = "SmartLock";
    logoLabel->textSize = 2;
    logoLabel->x = Screen_Width / 2;
    logoLabel->y = Screen_Height / 2 - Letter_Height * logoLabel->textSize;
    logoLabel->color = TFT_WHITE;

    auto idLabel = _render->createLabel();

    idLabel->text = _config->ThingName;
    idLabel->textSize = 1;
    idLabel->x = Screen_Width / 2;
    idLabel->y = Screen_Height / 2 - Letter_Height * logoLabel->textSize + Letter_Height * logoLabel->textSize;
    idLabel->color = TFT_WHITE;
}
