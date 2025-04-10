#ifndef UI_RENDER_H
#define UI_RENDER_H

#include <TFT_eSPI.h>
#include <vector>
#include "UIRenderElement.h"
#include "UIRenderButton.h"
#include "UIRenderLabel.h"

using std::vector;

#define Calibration_File_Path "/Calibration"
#define Screen_Width 320
#define Screen_Height 240

class UIRender
{
private:
    String _debugMessages[4];
    int _lastDebugMessageIndex;

protected:
    TFT_eSPI _tft;
    bool _isDebug;
    vector<UIRenderElement *> _elements;

public:
    UIRender();

    void init();
    void calibrate();
    void enableDebug();
    void writeDebug(String message);
    void loop();
    void draw();

    UIRenderButton *createButton();
    UIRenderLabel *createLabel();
    void deleteUIElements();

    friend class UIRenderElement;
};

#endif