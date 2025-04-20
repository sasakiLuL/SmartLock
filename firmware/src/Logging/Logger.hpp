#pragma once

#include <string>
#include "../Ui/Render/Render.hpp"

#define LoggCount 4

namespace SmartLock
{
    class Logger
    {
    private:
        Render &_render;
        std::string _debugMessages[LoggCount];
        int _lastDebugMessageIndex;

        void write(std::string message);

    public:
        Logger(Render &render);

        void logError(std::string message);
        void logInfo(std::string message);
        void logWarning(std::string message);

        size_t maximumLogMessageSize();

        void render();
    };
}
