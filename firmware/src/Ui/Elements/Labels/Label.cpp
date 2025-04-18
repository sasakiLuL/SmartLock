#include "Label.hpp"

namespace SmartLock
{
    void Label::render(RenderBuffer& buffer)
    {
        auto tft = buffer.getRender().getTFT();

        tft.setTextColor(_color, TFT_BLACK);
        tft.setTextDatum(MC_DATUM);
        tft.setTextSize(_textSize);
        tft.drawString(_text.c_str(), _x, _y);
    }

    bool Label::isWithin(uint32_t x, uint32_t y)
    {
        return x > _x && x < (_x + _text.length() * LetterWidth) && y > _y && y < (_y + LetterHeight);
    }

    void Label::clear(RenderBuffer& buffer)
    {
        auto tft = buffer.getRender().getTFT();

        tft.fillRect(_x, _y, _x + _text.length() * LetterWidth, _y + LetterHeight, TFT_BLACK);
    }
}
