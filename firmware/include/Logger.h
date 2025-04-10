#ifndef LOGGER_H
#define LOGGER_H

#include <WString.h>
#include "UIRender.h"

class Logger
{
private:
    UIRender *_render;

public:
    Logger(UIRender *render);

    void logError(String message);
    void logInfo(String message);
    void logWarning(String message);
};

#endif