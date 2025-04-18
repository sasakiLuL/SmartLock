#include "Button.hpp"

namespace SmartLock
{
    void Button::render(RenderBuffer& buffer)
    {
        auto tft = buffer.getRender().getTFT();

        tft.setTextSize(2);
        tft.fillRoundRect(_x, _y, _width, _height, 10, _color);
        tft.setTextColor(_textColor);
        tft.setTextDatum(MC_DATUM);
        tft.drawString(_text.c_str(), _x + _width / 2, _y + _height / 2);
    }

    void Button::clear(RenderBuffer& buffer)
    {
        auto tft = buffer.getRender().getTFT();

        tft.fillRect(_x, _y, _width, _height, TFT_BLACK);
    }

    bool Button::isWithin(uint32_t x, uint32_t y)
    {
        return x > _x && x < (_x + _width) && y > _y && y < (_y + _height);
    }
}