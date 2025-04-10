#ifndef MQTT_SERVICE_H
#define MQTT_SERVICE_H

#include "Configuration.h"
#include <PubSubClient.h>
#include <WiFiClientSecure.h>
#include "Logger.h"
#include <ArduinoJson.h>

class MqttService
{
private:
    PubSubClient _mqttClient;
    Configuration *_configuration;
    WiFiClientSecure _networkClient;
    Logger *_logger;

public:
    MqttService(Configuration *configuration, Logger *logger);
    void init();
    bool connect();
    void loop();
    bool publish(String policy, JsonDocument message);
    void setCallback(function<void(char *, uint8_t *, unsigned int)> callback);
    String getCurrentTimeUtc();
    String generateGuid();
};

#endif