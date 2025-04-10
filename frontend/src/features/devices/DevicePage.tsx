import { Button, Container, Stack, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { NavigationBar } from "../../components/NavigationBar";
import { useAuth } from "../../hooks/auth/useAuth";
import { useEffect, useState } from "react";
import { Device } from "./Device";
import axios from "axios";
import { Endpoints } from "../../utils/Endpoints";
import { useNavigate, useParams } from "react-router";
import { DeviceCard } from "./device-cards/DeviceCard";
import { EmptyDeviceCard } from "./device-cards/EmptyDeviceCard";
import { NotAuthorizedPage } from "../../components/NotAuthorizedPage";
import { FailurePage } from "../../components/FailurePage";
import DeleteIcon from "@mui/icons-material/Delete";
import { ErrorSnackbar } from "../../components/ErrorSnackbar";
import { NavLinks } from "../../utils/NavLinks";
import { SuccessSnackbar } from "../../components/SuccessSnackbar";

export function DevicePage() {
  const navigate = useNavigate();
  const auth = useAuth();
  const uriParams = useParams();

  const [device, setDevice] = useState<Device | null>(null);
  const [isDeviceFetchingFailed, setIsDeviceFetchingFailed] =
    useState<boolean>(false);

  const [deletionError, setDeletionError] = useState<string | null>(null);
  const [actionStatus, setActionStatus] = useState<string | null>(null);

  useEffect(() => {
    if (auth.isAuthenticated) {
      axios
        .get(`${Endpoints.getDevices}/${uriParams.id}`, {
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then((response) => {
          setDevice(response.data);
        })
        .catch(() => {
          setIsDeviceFetchingFailed(true);
        });
    }
  }, [auth.token]);

  if (!auth.isAuthenticated) {
    return (
      <>
        <NavigationBar />
        <NotAuthorizedPage
          message={
            "First you need to log in to obtain information about your device!"
          }
        />
      </>
    );
  }

  if (isDeviceFetchingFailed) {
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
            <Typography variant="h2">Device</Typography>
          </Grid>
          <Grid size={6}>
            {device ? (
              <DeviceCard
                isActive={false}
                device={device!}
                onClick={() => {}}
              />
            ) : (
              <EmptyDeviceCard />
            )}
          </Grid>
          <Grid size={6}>
            <Button
              variant="outlined"
              startIcon={<DeleteIcon />}
              onClick={() => {
                axios
                  .delete(Endpoints.deleteDevice(uriParams.id!), {
                    headers: { Authorization: `Bearer ${auth.token}` },
                  })
                  .then(() => {
                    navigate(NavLinks.Devices);
                  })
                  .catch((error) => {
                    if (error.response!.status === 404) {
                      navigate(NavLinks.Devices);
                    } else {
                      setDeletionError(error.response!.Detail);
                    }
                  });
              }}

              //
            >
              Delete
            </Button>
          </Grid>
          <Grid size={12}>
            <Typography variant="h4">Commands</Typography>
            <Stack direction="row" spacing={2}>
              <Button
                onClick={() => {
                  axios
                    .post(Endpoints.open(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setActionStatus("Device has been opened");
                    })
                    .catch((error) => {
                      setDeletionError(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Open
              </Button>
              <Button
                onClick={() => {
                  axios
                    .post(Endpoints.close(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setActionStatus("Device has been closed");
                    })
                    .catch((error) => {
                      setDeletionError(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Close
              </Button>
            </Stack>
          </Grid>
        </Grid>
      </Container>
      <ErrorSnackbar
        message={deletionError ?? ""}
        open={deletionError ? true : false}
      />
      <SuccessSnackbar
        message={actionStatus ?? ""}
        open={actionStatus ? true : false}
      />
    </>
  );
}
