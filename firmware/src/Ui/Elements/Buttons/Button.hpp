#pragma once

#include "../Element.hpp"

namespace SmartLock
{
    class Button : public Element
    {
    public:
        Button() : Element() {}

        void text(const std::string &text) { _text = text; }
        void color(uint32_t color) { _color = color; }
        void height(uint32_t height) { _height = height; }
        void width(uint32_t width) { _width = width; }
        void textColor(uint32_t textColor) { _textColor = textColor; }

        std::string text() const { return _text; }
        uint32_t color() const { return _color; }
        uint32_t height() const { return _height; }
        uint32_t width() const { return _width; }
        uint32_t textColor() const { return _textColor; }

        void render(RenderBuffer& buffer) override;
        void clear(RenderBuffer& buffer) override;
        bool isWithin(uint32_t x, uint32_t y) override;
    protected:
        std::string _text;
        uint32_t _color;
        uint32_t _height;
        uint32_t _width;
        uint32_t _textColor;
    };
}