#include "Logger.hpp"
#include <HardwareSerial.h>

namespace SmartLock
{
    void Logger::write(std::string message)
    {
        auto tft = _render.getTFT();

        if (_lastDebugMessageIndex == LoggCount)
        {
            for (int i = 1; i < LoggCount; i++)
            {
                _debugMessages[i - 1] = _debugMessages[i];
            }

            _debugMessages[LoggCount - 1] = message;
        }
        else
        {
            _debugMessages[_lastDebugMessageIndex] = message;
            _lastDebugMessageIndex++;
        }
    }

    Logger::Logger(Render &render) : _render(render) {}

    void Logger::logError(std::string message)
    {
        std::string logMessage = "[ERROR]: " + message;
        Serial.println(logMessage.c_str());
        write(logMessage);
        render();
    }

    void Logger::logInfo(std::string message)
    {
        std::string logMessage = "[INFO]: " + message;
        Serial.println(logMessage.c_str());
        write(logMessage);
        render();
    }

    void Logger::logWarning(std::string message)
    {
        std::string logMessage = "[WARNING]: " + message;
        Serial.println(logMessage.c_str());
        write(logMessage);
        render();
    }

    void Logger::render()
    {
        auto tft = _render.getTFT();

        tft.setTextSize(1);
        tft.setTextColor(TFT_YELLOW, TFT_BLACK);
        tft.setTextDatum(TL_DATUM);
        tft.drawString("Debug Info:", 10, 10);

        for (int i = 0; i < LoggCount; i++)
        {
            tft.fillRect(10, 20 + i * LetterHeight, ScreenWidth, LetterHeight, TFT_BLACK);
            tft.drawString(_debugMessages[i].c_str(), LetterHeight, 20 + i * LetterHeight);
        }
    }
}
