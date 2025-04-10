import { Button, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { NavigationBar } from "../../components/NavigationBar";
import { useAuth } from "../../hooks/auth/useAuth";
import { useEffect, useState } from "react";
import axios from "axios";
import { Endpoints } from "../../utils/Endpoints";
import { Device } from "./Device";
import { useNavigate } from "react-router-dom";
import { NavLinks } from "../../utils/NavLinks";
import { NotAuthorizedPage } from "../../components/NotAuthorizedPage";
import { DeviceCard } from "./device-cards/DeviceCard";
import { EmptyDeviceCard } from "./device-cards/EmptyDeviceCard";
import { FailurePage } from "../../components/FailurePage";
import { ActivateDeviceDialog } from "./ActivateDeviceDialog";
import { ErrorSnackbar } from "../../components/ErrorSnackbar";
import { SuccessSnackbar } from "../../components/SuccessSnackbar";

export function DevicesPage() {
  const auth = useAuth();
  const navigate = useNavigate();

  const [devices, setDevices] = useState<Device[]>([]);
  const [isDevicesFetchingFailed, setIsDevicesFetchingFailed] =
    useState<boolean>(false);

  const [isDevicesLoading, setIsDevicesLoading] = useState<boolean>(true);

  const [isActivateDeviceDialogOpened, setIsActivateDeviceDialogOpened] =
    useState<boolean>(false);
  const [activationError, setActivationError] = useState<string | null>(null);
  const [isActivationSuccess, setIsActivationSuccess] =
    useState<boolean>(false);

  useEffect(() => {
    if (auth.isAuthenticated) {
      axios
        .get(Endpoints.getDevices, {
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then((response) => {
          setDevices(response.data);
          setIsDevicesLoading(false);
        })
        .catch(() => {
          setIsDevicesFetchingFailed(true);
        });
    }
  }, [auth.token, isActivateDeviceDialogOpened]);

  if (!auth.isAuthenticated) {
    return (
      <>
        <NavigationBar />
        <NotAuthorizedPage
          message={
            "First you need to log in to obtain all information about your devices!"
          }
        />
      </>
    );
  }

  if (isDevicesFetchingFailed) {
    return (
      <>
        <NavigationBar />
        <FailurePage />
      </>
    );
  }

  return (
    <>
      <NavigationBar />
      <Container maxWidth="xl">
        <Grid container spacing={2}>
          <Grid size={12}>
            <Typography variant="h2">Devices</Typography>
          </Grid>
          <Grid size={12}>
            <Button
              variant="contained"
              onClick={() => {
                setIsActivateDeviceDialogOpened(true);
              }}
            >
              Activate device
            </Button>
          </Grid>

          <Grid size={12}>
            <Grid
              container
              spacing={{ xs: 2, md: 3 }}
              columns={{ xs: 4, sm: 8, md: 12 }}
            >
              {isDevicesLoading ? (
                Array.from({ length: 3 }).map((index) => (
                  <Grid key={index} size={{ xs: 2, sm: 4, md: 4 }}>
                    <EmptyDeviceCard />
                  </Grid>
                ))
              ) : devices.length != 0 ? (
                devices.map((device, index) => (
                  <Grid key={index} size={{ xs: 2, sm: 4, md: 4 }}>
                    <DeviceCard
                      isActive={true}
                      device={device}
                      onClick={() => {
                        navigate(`${NavLinks.Device}/${device.id}`);
                      }}
                    />
                  </Grid>
                ))
              ) : (
                <Typography
                  variant="body2"
                  color="text.disabled"
                  xs={{ textAlign: "center" }}
                >
                  No devices
                </Typography>
              )}
            </Grid>
          </Grid>
        </Grid>
      </Container>
      <SuccessSnackbar
        message="Activation request has been successfuly sent!"
        open={isActivationSuccess}
      />
      <ErrorSnackbar
        message={activationError ?? ""}
        open={activationError ? true : false}
      />
      <ActivateDeviceDialog
        open={isActivateDeviceDialogOpened}
        onSubmit={(event) => {
          if (auth.isAuthenticated) {
            axios
              .post(Endpoints.activateDevice(event.id), null, {
                headers: { Authorization: `Bearer ${auth.token}` },
              })
              .then(() => {
                setIsActivationSuccess(true);
                setIsActivateDeviceDialogOpened(false);
              })
              .catch((error) => {
                setActivationError(error.message);
                setIsActivateDeviceDialogOpened(false);
              });
          }
        }}
        onClose={() => setIsActivateDeviceDialogOpened(false)}
      />
    </>
  );
}
