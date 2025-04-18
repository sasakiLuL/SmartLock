#include <WiFi.h>

#include "Application/Application.hpp"

SmartLock::Application app = SmartLock::Application();

void setup()
{
    Serial.begin(115200);

    app.initialize();
}

void loop()
{
    app.loop();
}