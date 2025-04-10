#ifndef PAGE_H
#define PAGE_H

#include "PageName.h"

class Page
{
public:
    virtual PageName getPageName() = 0;
    virtual void render() = 0;
    virtual ~Page() {};
};

#endif