#include "Configuration.h"
#include <FS.h>
#include <LittleFS.h>
#include <ArduinoJson.h>

String Configuration::fileToString(const char *fileName)
{
    File file = LittleFS.open(fileName, "r");

    if (!file)
    {
        Serial.print("Failed to open file: ");
        Serial.println(fileName);
        return "";
    }

    String outString = file.readString();
    file.close();

    return outString;
}

Configuration::Configuration()
{
}

Configuration::Configuration(const Configuration &copy)
{
    ThingName = copy.ThingName;
    AwsEndpoint = copy.AwsEndpoint;
    AwsPort = copy.AwsPort;
    ActionsPolicy = copy.ActionsPolicy;
    ActivationRequestsPolicy = copy.ActivationRequestsPolicy;
    ActivationResponsesPolicy = copy.ActivationResponsesPolicy;
    LogsPolicy = copy.LogsPolicy;
    AmazonRootCA1 = copy.AmazonRootCA1;
    DeviceCertificate = copy.DeviceCertificate;
    DevicePrivateKey = copy.DevicePrivateKey;
}

void Configuration::init()
{
    AmazonRootCA1 = fileToString(Amazon_Root_CA1_Path);
    DeviceCertificate = fileToString(Device_Certificate_Path);
    DevicePrivateKey = fileToString(Device_Private_Key_Path);
    String configString = fileToString(Config_Path);

    JsonDocument configDocument;
    DeserializationError error = deserializeJson(configDocument, configString);

    if (error)
    {
        Serial.println("Failed to deserialize config file");
    }

    ThingName = configDocument["ThingName"].as<String>();
    AwsEndpoint = configDocument["AwsEndpoint"].as<String>();
    AwsPort = configDocument["AwsPort"];
    ActionsPolicy = configDocument["ActionsPolicy"].as<String>();
    ActivationRequestsPolicy = configDocument["ActivationRequestsPolicy"].as<String>();
    ActivationResponsesPolicy = configDocument["ActivationResponsesPolicy"].as<String>();
    LogsPolicy = configDocument["LogsPolicy"].as<String>();
    DeactivationsPolicy = configDocument["DeactivationsPolicy"].as<String>();
}
