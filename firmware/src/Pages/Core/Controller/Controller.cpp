#include "Controller.hpp"
#include "../Page/Page.hpp"

namespace SmartLock
{
    void Controller::loop()
    {
        _pages[_currentPage]->loop();
    }

    void Controller::addPage(Path path, Page *page)
    {
        _pages.insert({path, page});
    }

    void Controller::changePage(Path path)
    {
        currentPath(path);
        render();
    }

    void Controller::render()
    {
        _pages[_currentPage]->render();
    }
}
