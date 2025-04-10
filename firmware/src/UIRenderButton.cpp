#include "UIRenderButton.h"

void UIRenderButton::render()
{
    _tft->setTextSize(2);
    _tft->fillRoundRect(x, y, width, height, 10, color);
    _tft->setTextColor(textColor);
    _tft->setTextDatum(MC_DATUM);
    _tft->drawString(text.c_str(), x + width / 2, y + height / 2);
}

bool UIRenderButton::isWithin(uint16_t x, uint16_t y)
{
    return x > this->x && x < (this->x + width) && y > this->y && y < (this->y + height);
}