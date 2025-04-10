#ifndef BUTTON_H
#define BUTTON_H

#include <WString.h>
#include "UIRenderElement.h"

class UIRenderButton : public UIRenderElement
{
public:
    String text;
    uint32_t color;
    int height;
    int width;
    uint32_t textColor;
    UIRenderButton(UIRender *uiRender) : UIRenderElement(uiRender) {}
    void render() override;
    bool isWithin(uint16_t x, uint16_t yt) override;
};

#endif