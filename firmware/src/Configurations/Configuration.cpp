#include "Configuration.hpp"
#include <FS.h>
#include <LittleFS.h>
#include <ArduinoJson.h>

namespace SmartLock
{
    std::string Configuration::fileToString(const char *fileName)
    {
        fs::File file = LittleFS.open(fileName, "r");

        if (!file)
        {
            throw std::runtime_error(std::string("Failed to open file: ") + fileName);
        }

        std::string outString = file.readString().c_str();
        file.close();

        return outString;
    }

    void Configuration::initialize()
    {
        amazonRootCA1 = fileToString(AmazonRootCA1Path);
        deviceCertificate = fileToString(DeviceCertificatePath);
        devicePrivateKey = fileToString(DevicePrivateKeyPath);
        configurationPage = fileToString(ConfigurationPagePath);
        std::string configString = fileToString(ConfigPath);

        JsonDocument configDocument;
        DeserializationError error = deserializeJson(configDocument, configString);

        if (error)
        {
            throw std::runtime_error("Failed to deserialize config file");
        }

        thingName = configDocument["ThingName"].as<const char *>();
        serverSSID = "SmartLock_" + thingName.substr(0, 7);
        awsEndpoint = configDocument["AwsEndpoint"].as<const char *>();
        awsPort = configDocument["AwsPort"].as<int32_t>();
        updateTopic = configDocument["UpdateTopic"].as<const char *>();
        deltaTopic = configDocument["DeltaTopic"].as<const char *>();
        getTopic = configDocument["GetTopic"].as<const char *>();
        getAcceptedTopic = configDocument["GetAcceptedTopic"].as<const char *>();
    }
}
