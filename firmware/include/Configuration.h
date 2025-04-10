#ifndef CONFIGURATION_H
#define CONFIGURATION_H

#include <WString.h>

#define Amazon_Root_CA1_Path "/AmazonRootCA1.pem"
#define Device_Certificate_Path "/DeviceCertificate.pem"
#define Device_Private_Key_Path "/DevicePrivateKey.key"
#define Calibration_File_Path "/Calibration"
#define Config_Path "/config.json"

class Configuration
{
private:
    String fileToString(const char *fileName);

public:
    String ThingName;
    String AwsEndpoint;
    int AwsPort;
    String ActionsPolicy;
    String ActivationRequestsPolicy;
    String ActivationResponsesPolicy;
    String LogsPolicy;
    String DeactivationsPolicy;
    String AmazonRootCA1;
    String DeviceCertificate;
    String DevicePrivateKey;

    Configuration();
    Configuration(const Configuration &copy);
    void init();
};

#endif