#ifndef LABEL_H
#define LABEL_H

#include <WString.h>
#include "UIRenderElement.h"

#define Letter_Height 10

class UIRenderLabel : public UIRenderElement
{
public:
    String text;
    int textSize;
    uint32_t color;
    UIRenderLabel(UIRender *uiRender) : UIRenderElement(uiRender) {}
    void render() override;
};

#endif