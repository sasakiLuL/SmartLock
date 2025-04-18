#pragma once

#include <WString.h>

#include "../Logging/Logger.hpp"

#define AmazonRootCA1Path "/AmazonRootCA1.pem"
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

        std::string thingName;
        std::string awsEndpoint;
        int32_t awsPort;
        std::string actionsPolicy;
        std::string activationRequestsPolicy;
        std::string activationResponsesPolicy;
        std::string logsPolicy;
        std::string deactivationsPolicy;
        std::string amazonRootCA1;
        std::string deviceCertificate;
        std::string devicePrivateKey;
    };
}