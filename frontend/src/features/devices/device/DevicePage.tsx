import { Button, Container, Stack, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { NavigationBar } from "../../../components/NavigationBar";
import { useAuth } from "../../../hooks/auth/useAuth";
import { useEffect, useState } from "react";
import { Device } from "../Device";
import axios from "axios";
import { Endpoints } from "../../../utils/Endpoints";
import { useNavigate, useParams } from "react-router";
import { NotAuthorizedPage } from "../../../components/NotAuthorizedPage";
import { FailurePage } from "../../../components/FailurePage";
import DeleteIcon from "@mui/icons-material/Delete";
import { ErrorSnackbar } from "../../../components/ErrorSnackbar";
import { NavLinks } from "../../../utils/NavLinks";
import { SuccessSnackbar } from "../../../components/SuccessSnackbar";
import UnlockIcon from "@mui/icons-material/LockOpen";
import LockIcon from "@mui/icons-material/HttpsOutlined";
import DeactivateIcon from "@mui/icons-material/ToggleOffOutlined";
import ActivateIcon from "@mui/icons-material/ToggleOnOutlined";
import { DeviceCard } from "../../../components/device-cards/DeviceCard";
import { EmptyDeviceCard } from "../../../components/device-cards/EmptyDeviceCard";
import { Action } from "../../actions/Action";
import { ActionsTable } from "../../../components/actions-table/ActionsTable";

export function DevicePage() {
  const navigate = useNavigate();
  const auth = useAuth();
  const uriParams = useParams();

  const [device, setDevice] = useState<Device | null>(null);
  const [deviceFetchingFailed, setDeviceFetchingFailed] =
    useState<boolean>(false);

  const [actions, setActions] = useState<Action[]>([]);
  const [actionsFetchingFailed, setActionsFetchingFailed] =
    useState<boolean>(false);
  const [actionsPageNumber, setActionsPageNumber] = useState<number>(0);
  const [actionsPageSize, setActionsPageSize] = useState<number>(5);
  const [actionsTotalCount, setActionsTotalCount] = useState<number>(0);
  const [actionsSortColumn, setActionsSortColumn] = useState<
    string | undefined
  >(undefined);
  const [actionsSortOrder, setActionsSortOrder] = useState<"asc" | "desc">(
    "desc"
  );

  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  useEffect(() => {
    if (auth.isAuthenticated) {
      axios
        .get(Endpoints.getDevice(uriParams.id!), {
          headers: { Authorization: `Bearer ${auth.token}` },
        })
        .then((response) => {
          setDevice(response.data);
        })
        .catch(() => {
          setDeviceFetchingFailed(true);
        });
    }
  }, [auth.token]);

  useEffect(() => {
    if (auth.isAuthenticated) {
      axios
        .get(
          Endpoints.getActionsByDevice(
            uriParams.id!,
            actionsPageNumber,
            actionsPageSize,
            actionsSortColumn,
            actionsSortOrder
          ),
          {
            headers: { Authorization: `Bearer ${auth.token}` },
          }
        )
        .then((response) => {
          setActions(response.data.pageItems);
          setActionsTotalCount(response.data.totalItemCount);
        })
        .catch(() => {
          setActionsFetchingFailed(true);
        });
    }
  }, [
    auth.token,
    actionsPageNumber,
    actionsPageSize,
    actionsSortColumn,
    actionsSortOrder,
  ]);

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

  if (deviceFetchingFailed && actionsFetchingFailed) {
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
            <Stack>
              <Button
                variant="outlined"
                sx={{ mb: 1 }}
                startIcon={<DeleteIcon />}
                onClick={() => {
                  axios
                    .delete(Endpoints.removeDevice(uriParams.id!), {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      navigate(NavLinks.Devices);
                    })
                    .catch((error) => {
                      if (error.response!.status === 404) {
                        navigate(NavLinks.Devices);
                      } else {
                        setErrorMessage(error.response!.Detail);
                      }
                    });
                }}
              >
                Remove
              </Button>
              <Button
                startIcon={<ActivateIcon />}
                sx={{ mb: 1 }}
                onClick={() => {
                  axios
                    .post(Endpoints.activateDevice(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setSuccessMessage(
                        "Activation action request has been sent"
                      );
                    })
                    .catch((error) => {
                      setErrorMessage(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Activate
              </Button>
              <Button
                startIcon={<DeactivateIcon />}
                sx={{ mb: 1 }}
                onClick={() => {
                  axios
                    .post(Endpoints.deactivateDevice(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setSuccessMessage(
                        "Deactivation action request has been sent"
                      );
                    })
                    .catch((error) => {
                      setErrorMessage(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Deactivate
              </Button>
              <Button
                startIcon={<UnlockIcon />}
                sx={{ mb: 1 }}
                onClick={() => {
                  axios
                    .post(Endpoints.unlockDevice(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setSuccessMessage("Unlock action request has been sent");
                    })
                    .catch((error) => {
                      setErrorMessage(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Unlock
              </Button>
              <Button
                sx={{ mb: 1 }}
                startIcon={<LockIcon />}
                onClick={() => {
                  axios
                    .post(Endpoints.lockDevice(uriParams.id!), null, {
                      headers: { Authorization: `Bearer ${auth.token}` },
                    })
                    .then(() => {
                      setSuccessMessage("Lock action request has been sent");
                    })
                    .catch((error) => {
                      setErrorMessage(error.response);
                    });
                }}
                color="primary"
                variant="contained"
              >
                Lock
              </Button>
            </Stack>
          </Grid>
          <Grid></Grid>
          <Grid size={12} container>
            <Typography size={12} variant="h4">
              Actions history
            </Typography>
            <ActionsTable
              actions={actions}
              totalCount={actionsTotalCount}
              pageSize={actionsPageSize}
              pageNumber={actionsPageNumber}
              sortOrder={actionsSortOrder}
              sortColumn={actionsSortColumn}
              onSortColumnChange={(column: string) =>
                setActionsSortColumn(column)
              }
              onSortOrderChange={(order: "asc" | "desc") =>
                setActionsSortOrder(order)
              }
              onPageNumberChange={(_, newPage: number) =>
                setActionsPageNumber(newPage)
              }
              onPageSizeChange={(event) =>
                setActionsPageSize(parseInt(event.target.value, 10))
              }
            />
          </Grid>
        </Grid>
      </Container>
      <ErrorSnackbar
        message={errorMessage ?? ""}
        open={errorMessage ? true : false}
      />
      <SuccessSnackbar
        message={successMessage ?? ""}
        open={successMessage ? true : false}
      />
    </>
  );
}
