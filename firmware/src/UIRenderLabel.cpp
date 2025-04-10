#include "UIRenderLabel.h"

void UIRenderLabel::render()
{
    _tft->setTextColor(color, TFT_BLACK);
    _tft->setTextDatum(MC_DATUM);
    _tft->setTextSize(textSize);
    _tft->drawString(text.c_str(), x, y);
}