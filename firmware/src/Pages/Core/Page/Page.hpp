#pragma once

#include "../../../Ui/Render/Render.hpp"
#include "../Controller/Controller.hpp"
#include "../../../Logging/Logger.hpp"
#include "../../../Ui/Buffer/RenderBuffer.hpp"

namespace SmartLock
{
    class Page
    {
    public:
        Page(Controller &controller, Render &render, Logger &logger);
        virtual ~Page() {}

        virtual void bindings();
        virtual void loop();
        virtual void render();

    protected:
        Controller &_controller;
        Render &_render;
        Logger &_logger;
        RenderBuffer _buffer;
    };
}
