#pragma once

#include <functional>
#include "../Buffer/RenderBuffer.hpp"

namespace SmartLock
{
    class Element
    {
    public:
        Element() {}

        void x(uint32_t x) { _x = x; }
        void y(uint32_t y) { _y = y; }
        void onClick(const std::function<void()> &callback) { _onClickCallback = callback; };

        uint32_t x() const { return _x; }
        uint32_t y() const { return _y; }
        const std::function<void()> &getOnClickCallback() const { return _onClickCallback; }

        virtual void render(RenderBuffer& buffer) = 0;
        virtual void clear(RenderBuffer& buffer) = 0;
        virtual bool isWithin(uint32_t x, uint32_t y);

    protected:
        std::function<void()> _onClickCallback;
        uint32_t _x;
        uint32_t _y;
    };
}