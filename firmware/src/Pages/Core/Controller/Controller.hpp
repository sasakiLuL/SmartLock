#pragma once

#include <map>
#include "../../Path.hpp"

namespace SmartLock
{
    class Page;

    class Controller
    {
    public:
        void loop();
        void render();
        void addPage(Path path, Page *page);

        void currentPath(Path path) { _currentPage = path; }
        Path currentPath() const { return _currentPage; }

        void changePage(Path path);

    private:
        Path _currentPage;
        std::map<Path, Page *> _pages;
    };
}
