#pragma once

#include <string>
#include "../Element.hpp"

namespace SmartLock
{
    class Label : public Element
    {
    public:
        Label() : Element() {}

        void text(const std::string &text) { _text = text; }
        void color(uint32_t color) { _color = color; }
        void textSize(uint32_t textSize) { _textSize = textSize; }

        std::string text() const { return _text; }
        uint32_t color() const { return _color; }
        uint32_t textSize() const { return _textSize; }

        void render(RenderBuffer& buffer) override;
        void clear(RenderBuffer& buffer) override;
        bool isWithin(uint32_t x, uint32_t y) override;
    protected:
        std::string _text;
        uint32_t _textSize;
        uint32_t _color;
    };
}
