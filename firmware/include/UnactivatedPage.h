#ifndef UNACTIVATED_PAGE_H
#define UNACTIVATED_PAGE_H

#include "UIRender.h"
#include "Page.h"
#include "Configuration.h"
#include "DeviceStateProvider.h"

class UnactivatedPage : public Page
{
private:
    UIRender *_render;
    Configuration *_config;

public:
    UnactivatedPage(UIRender *render, Configuration *config);
    PageName getPageName() override;
    void render() override;
};

#endif