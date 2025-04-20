#include "Render.hpp"
#include <LittleFS.h>
#include <FS.h>
#include <stdlib.h>
#include <algorithm>

namespace SmartLock
{
    void Render::initialize()
    {
        _tft.init();
        _tft.setRotation(3);
        _tft.setTextFont(1);
        calibrate();
        _tft.fillScreen(TFT_BLACK);
    }

    void Render::calibrate()
    {
        const int calibrationDataSize = 14;
        uint16_t calData[5];
        uint8_t calDataOK = 0;

        if (LittleFS.exists(CalibrationFilePath))
        {
            fs::File f = LittleFS.open(CalibrationFilePath, "r");

            if (f.readBytes((char *)calData, calibrationDataSize) == calibrationDataSize)
            {
                calDataOK = 1;
            }

            f.close();
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

            fs::File f = LittleFS.open(CalibrationFilePath, "w");

            f.write((uint8_t *)calData, calibrationDataSize);

            f.close();
        }
    }

    TFT_eSPI &Render::getTFT()
    {
        return _tft;
    }
}
