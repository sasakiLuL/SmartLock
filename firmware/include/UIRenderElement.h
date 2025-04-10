#ifndef UI_ELEMENT_H
#define UI_ELEMENT_H

#include <functional>
#include <TFT_eSPI.h>
#include <XPT2046_Touchscreen.h>

using std::function;

class UIRender;

class UIRenderElement
{
protected:
    UIRender *_uiRender;
    TFT_eSPI *_tft;

public:
    UIRenderElement(UIRender *uiRender);
    int x;
    int y;
    function<void()> onClick;
    virtual void render() = 0;
    virtual bool isWithin(uint16_t x, uint16_t y);
};

#endif