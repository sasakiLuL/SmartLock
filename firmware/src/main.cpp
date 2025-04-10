#include <LittleFS.h>
#include <WiFi.h>
#include "UIRender.h"
#include "Configuration.h"
#include "MqttService.h"
#include "Logger.h"
#include "UnactivatedPage.h"
#include "DeviceStateProvider.h"
#include "ActionsPage.h"
#include "DeactivationPage.h"
#include "ActivationConfirmationPage.h"

const char *ssid = "WestaedtWG";
const char *password = "inWest2_zuhause";

void connectToWiFi()
{
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected");
}

Configuration configuration = Configuration();

UIRender uiRender = UIRender();

Logger logger = Logger(&uiRender);

DeviceStateProvider deviceStateProvider = DeviceStateProvider(&logger);

MqttService mqttService = MqttService(&configuration, &logger);

UnactivatedPage unactivatedPage = UnactivatedPage(&uiRender, &configuration);

DeactivationPage deactivationPage = DeactivationPage(&uiRender, &configuration, &mqttService, &deviceStateProvider, &unactivatedPage);

ActionsPage actionsPage = ActionsPage(&uiRender, &configuration, &mqttService, &deviceStateProvider, &deactivationPage);

ActivationConfirmationPage activationConfirmationPage = ActivationConfirmationPage(&uiRender, &configuration, &deviceStateProvider, &mqttService, &actionsPage, &unactivatedPage);

void mqttCallback(char *topic, byte *payload, unsigned int length)
{
  logger.logInfo(String("Received message"));

  String message;
  for (unsigned int i = 0; i < length; i++)
  {
    message += (char)payload[i];
  }

  JsonDocument messageJson;

  DeserializationError error = deserializeJson(messageJson, message);

  if (error)
  {
    logger.logError("Failed to deserialize message file");
    return;
  }

  if (configuration.ActivationRequestsPolicy.equalsIgnoreCase(topic))
  {
    if (!deviceStateProvider.getDeviceState().isDeviceActivated)
    {
      activationConfirmationPage.setUserName(messageJson["Username"]);
      activationConfirmationPage.render();
      uiRender.draw();
    }
  }
  else if (configuration.ActionsPolicy.equalsIgnoreCase(topic))
  {
    if (deviceStateProvider.getDeviceState().isDeviceActivated)
    {
      if (messageJson["CommandType"].as<int>() == 0)
      {
        deviceStateProvider.setDeviceState({true, true});
        actionsPage.render();
        uiRender.draw();
      }
      else if (messageJson["CommandType"].as<int>() == 1)
      {
        deviceStateProvider.setDeviceState({true, false});
        actionsPage.render();
        uiRender.draw();
      }
      else if (messageJson["CommandType"].as<int>() == 2)
      {
        deviceStateProvider.setDeviceState({false, true});
        unactivatedPage.render();
        uiRender.draw();
      }
    }
  }
}

void setup()
{
  Serial.begin(115200);

  deactivationPage.setActionsPage(&actionsPage);

  if (!LittleFS.begin())
  {
    Serial.println("Failed to mount file system");
  }

  configuration.init();

  deviceStateProvider.init();

  uiRender.init();
  uiRender.enableDebug();

  connectToWiFi();

  mqttService.init();
  mqttService.connect();

  mqttService.setCallback(mqttCallback);

  if (deviceStateProvider.getDeviceState().isDeviceActivated)
  {
    actionsPage.render();
  }
  else
  {
    unactivatedPage.render();
  }

  uiRender.draw();

  LittleFS.end();
}

void loop()
{
  mqttService.loop();

  uiRender.loop();
}