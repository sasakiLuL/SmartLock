#include "RenderBuffer.hpp"
#include "../Elements/Element.hpp"
#include <functional>
#include <algorithm>

namespace SmartLock
{
    void RenderBuffer::listenToEvents()
    {
        uint16_t x, y;

        if (_render.getTFT().getTouch(&x, &y))
        {
            auto elementIter = std::find_if(_elements.begin(), _elements.end(), [x, y](Element *element)
                                            { return element->isWithin(x, y); });

            if (elementIter == _elements.end())
            {
                return;
            }

            (*elementIter)->getOnClickCallback()();

            delay(300);
        }
    }

    void RenderBuffer::loop()
    {
        listenToEvents();
    }

    void RenderBuffer::render()
    {
        for (int i = 0; i < _elements.size(); i++)
        {
            _elements[i]->render(*this);
        }
    }

    void RenderBuffer::add(Element *element)
    {
        _elements.push_back(element);
    }
}
