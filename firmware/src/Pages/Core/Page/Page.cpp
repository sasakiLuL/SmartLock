#include "Page.hpp"
#include <memory>

namespace SmartLock
{
    Page::Page(Controller &controller, Render &render, Logger &logger)
        : _render(render), _controller(controller), _logger(logger), _buffer(_render) {}

    void Page::bindings()
    {
    }

    void Page::loop()
    {
        _buffer.loop();
    }

    

    void Page::render()
    {
        bindings();
        _render.getTFT().fillScreen(TFT_BLACK);
        _buffer.render();
        _logger.render();
    }
}
