#include <stdlib.h>
#include <FS.h>
#include "LittleFS.h"
#include "UIRender.h"
#include <algorithm>

UIRender::UIRender()
{
    _lastDebugMessageIndex = 0;
    _isDebug = false;
}

void UIRender::init()
{
    _tft.init();
    _tft.setRotation(3);
    calibrate();
}

void UIRender::calibrate()
{
    uint16_t calData[5];
    uint8_t calDataOK = 0;

    if (LittleFS.exists(Calibration_File_Path))
    {
        File f = LittleFS.open(Calibration_File_Path, "r");
        if (f)
        {
            if (f.readBytes((char *)calData, 14) == 14)
            {
                calDataOK = 1;
            }

            f.close();
        }
    }

    if (calDataOK)
    {
        _tft.setTouch(calData);
    }
    else
    {
        _tft.fillScreen(TFT_BLACK);

        _tft.calibrateTouch(calData, TFT_MAGENTA, TFT_BLACK, 15);

        _tft.setTextColor(TFT_GREEN, TFT_BLACK);

        File f = LittleFS.open(Calibration_File_Path, "w");
        if (f)
        {
            f.write((const unsigned char *)calData, 14);
            f.close();
        }
    }
}

void UIRender::enableDebug()
{
    _isDebug = true;

    _tft.setTextSize(1);
    _tft.fillScreen(TFT_BLACK);
    _tft.setTextColor(TFT_YELLOW, TFT_BLACK);
    _tft.drawString("Debug Info:", 10, 10);
}

void UIRender::writeDebug(String message)
{
    if (!_isDebug)
    {
        return;
    }

    if (_lastDebugMessageIndex == 4)
    {
        for (int i = 1; i < 4; i++)
        {
            _debugMessages[i - 1] = _debugMessages[i].c_str();
        }

        _debugMessages[3] = message.c_str();
    }
    else
    {
        _debugMessages[_lastDebugMessageIndex] = message.c_str();
        _lastDebugMessageIndex++;
    }

    _tft.setTextSize(1);
    _tft.setTextColor(TFT_YELLOW, TFT_BLACK);
    _tft.setTextDatum(TL_DATUM);

    for (int i = 0; i < 4; i++)
    {
        _tft.fillRect(10, 20 + i * Letter_Height, Screen_Width, Letter_Height, TFT_BLACK);
        _tft.drawString(_debugMessages[i].c_str(), Letter_Height, 20 + i * Letter_Height);
    }
}

void UIRender::loop()
{
    uint16_t x, y;

    if (_tft.getTouch(&x, &y))
    {
        auto elementIter = std::find_if(_elements.begin(), _elements.end(), [x, y](UIRenderElement *elem)
                                        { return elem->isWithin(x, y); });

        if (elementIter == _elements.end())
        {
            return;
        }

        (*elementIter)->onClick();

        delay(300);
    }
}

void UIRender::draw()
{
    _tft.fillScreen(TFT_BLACK);

    if (_isDebug)
    {
        _tft.setTextSize(1);
        _tft.setTextColor(TFT_YELLOW, TFT_BLACK);
        _tft.setTextDatum(TL_DATUM);
        _tft.drawString("Debug Info:", 10, 10);

        for (int i = 0; i < 4; i++)
        {
            _tft.fillRect(10, 20 + i * Letter_Height, Screen_Width, Letter_Height, TFT_BLACK);
            _tft.drawString(_debugMessages[i].c_str(), Letter_Height, 20 + i * Letter_Height);
        }
    }

    for (auto element : _elements)
    {
        element->render();
    }
}

UIRenderButton *UIRender::createButton()
{
    UIRenderButton *button = new UIRenderButton(this);

    _elements.push_back(button);

    return button;
}

UIRenderLabel *UIRender::createLabel()
{
    UIRenderLabel *label = new UIRenderLabel(this);

    _elements.push_back(label);

    return label;
}

void UIRender::deleteUIElements()
{
    for (auto element : _elements)
    {
        delete element;
    }

    _elements.clear();
}
