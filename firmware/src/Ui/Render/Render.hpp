#pragma once

#include <TFT_eSPI.h>
#include <vector>

#define CalibrationFilePath "/Calibration"
#define ScreenWidth 320
#define ScreenHeight 240
#define LetterHeight 10
#define LetterWidth 10

namespace SmartLock
{
    class Render
    {
    public:
        Render() {}

        void initialize();
        void calibrate();

        TFT_eSPI &getTFT();

    private:
        TFT_eSPI _tft;
    };
}
