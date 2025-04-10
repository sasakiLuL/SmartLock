#include "UIRenderElement.h"
#include "UIRender.h"

UIRenderElement::UIRenderElement(UIRender *uiRender)
{
    _uiRender = uiRender;
    _tft = &(uiRender->_tft);
}

bool UIRenderElement::isWithin(uint16_t x, uint16_t y)
{
    return false;
}
