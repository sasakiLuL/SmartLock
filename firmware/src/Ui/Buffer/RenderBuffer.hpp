#pragma once

#include <vector>
#include "../Render/Render.hpp"

namespace SmartLock
{
    class Element;

    class RenderBuffer
    {
    public:
        RenderBuffer(Render &render) : _render(render) {}

        void loop();
        void render();

        void add(Element * element);

        Render& getRender() { return _render; };

    private:
        Render &_render;
        std::vector<Element *> _elements;

        void listenToEvents();
    };

}
