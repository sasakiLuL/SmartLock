#pragma once

#include <WString.h>

#include "../Logging/Logger.hpp"

#define AmazonRootCA1Path "/AmazonRootCA1.pem"
#define ConfigurationPagePath "/ConfigurationPage.html"
#define DeviceCertificatePath "/DeviceCertificate.pem"
#define DevicePrivateKeyPath "/DevicePrivateKey.key"
#define CalibrationFilePath "/Calibration"
#define ConfigPath "/config.json"

namespace SmartLock
{
    class Configuration
    {
    private:
        Logger &_logger;

        std::string fileToString(const char *fileName);

    public:
        Configuration(Logger &logger) : _logger(logger) {}

        void initialize();

        std::string serverSSID;
        std::string thingName;
        std::string awsEndpoint;
        int32_t awsPort;
        std::string updateTopic;
        std::string deltaTopic;
        std::string amazonRootCA1;
        std::string deviceCertificate;
        std::string devicePrivateKey;
        std::string getTopic;
        std::string getAcceptedTopic;
        std::string configurationPage;
    };
}