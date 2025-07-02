#include <WiFi.h>

#include "Application/Application.hpp"

SmartLock::Application app{};

void setup()
{
    Serial.begin(115200);

    app.initialize();
}

void loop()
{
    app.loop();
}