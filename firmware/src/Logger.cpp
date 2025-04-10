#include <Logger.h>
#include <HardwareSerial.h>

Logger::Logger(UIRender *render)
{
    _render = render;
}

void Logger::logError(String message)
{
    String logMessage = "[ERROR]: " + message;
    Serial.println(logMessage.c_str());
    _render->writeDebug(logMessage);
}

void Logger::logInfo(String message)
{
    String logMessage = "[INFO]: " + message;
    Serial.println(logMessage.c_str());
    _render->writeDebug(logMessage);
}

void Logger::logWarning(String message)
{
    String logMessage = "[WARNING]: " + message;
    Serial.println(logMessage.c_str());
    _render->writeDebug(logMessage);
}
