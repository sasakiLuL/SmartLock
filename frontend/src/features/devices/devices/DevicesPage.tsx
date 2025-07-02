import { Button, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { NavigationBar } from "../../../components/NavigationBar";
import { useAuth } from "../../../hooks/auth/useAuth";
import { useEffect, useState } from "react";
import axios from "axios";
import { Endpoints } from "../../../utils/Endpoints";
import { Device } from "../Device";
import { useNavigate } from "react-router-dom";
import { NavLinks } from "../../../utils/NavLinks";
import { NotAuthorizedPage } from "../../../components/NotAuthorizedPage";
import { FailurePage } from "../../../components/FailurePage";
import { ErrorSnackbar } from "../../../components/ErrorSnackbar";
import { SuccessSnackbar } from "../../../components/SuccessSnackbar";
import { AddDeviceDialog } from "../add/AddDeviceDialog";
import { EmptyDeviceCard } from "../../../components/device-cards/EmptyDeviceCard";
import { DeviceCard } from "../../../components/device-cards/DeviceCard";

export function DevicesPage() {
  const auth = useAuth();
  const navigate = useNavigate();

  const [devices, setDevices] = useState<Device[]>([]);
  const [devicesFetchingFailed, setDevicesFetchingFailed] =
    useState<boolean>(false);

  const [devicesLoading, setDevicesLoading] = useState<boolean>(true);

  const [addDeviceDialogOpened, setAddDeviceDialogOpened] =
    useState<boolean>(false);
  const [addingError, setAddingError] = useState<string | null>(null);
  const [addingSuccess, setAddingSuccess] = useState<boolean>(false);

  useEffect(() => {
    if (auth.isAuthenticated) {
      axios
        .get(Endpoints.getDevices, {
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then((response) => {
          setDevices(response.data);
          setDevicesLoading(false);
        })
        .catch(() => {
          setDevicesFetchingFailed(true);
        });
    }
  }, [auth.token, addDeviceDialogOpened]);

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

  if (devicesFetchingFailed) {
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
                setAddDeviceDialogOpened(true);
              }}
            >
              Add device
            </Button>
          </Grid>

          <Grid size={12}>
            <Grid
              container
              spacing={{ xs: 2, md: 3 }}
              columns={{ xs: 4, sm: 8, md: 12 }}
            >
              {devicesLoading ? (
                Array.from({ length: 3 }).map((index) => (
                  <Grid key={index} size={{ xs: 2, sm: 4, md: 4 }}>
                    <EmptyDeviceCard />
                  </Grid>
                ))
              ) : devices.length != 0 ? (
                devices.map((device) => (
                  <Grid key={device.id} size={{ xs: 2, sm: 4, md: 4 }}>
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
      <SuccessSnackbar message="Device has been added" open={addingSuccess} />
      <ErrorSnackbar
        message={addingError ?? ""}
        open={addingError ? true : false}
      />
      <AddDeviceDialog
        open={addDeviceDialogOpened}
        onSubmit={(event) => {
          if (auth.isAuthenticated) {
            axios
              .post(Endpoints.addDevice, event, {
                headers: { Authorization: `Bearer ${auth.token}` },
              })
              .then(() => {
                setAddingSuccess(true);
                setAddDeviceDialogOpened(false);
              })
              .catch((error) => {
                setAddingError(error.message);
                setAddDeviceDialogOpened(false);
              });
          }
        }}
        onClose={() => setAddDeviceDialogOpened(false)}
      />
    </>
  );
}
