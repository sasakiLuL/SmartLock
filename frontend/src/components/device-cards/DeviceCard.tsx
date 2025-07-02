import {
  Box,
  Card,
  CardActionArea,
  CardContent,
  Chip,
  Stack,
  Typography,
} from "@mui/material";
import dayjs from "dayjs";
import { Device } from "../../features/devices/Device";

export interface DeviceCardProps {
  device: Device;
  onClick: () => void;
  isActive: boolean;
}

const statusToChip = (state: number) => {
  switch (state) {
    case 0:
      return <Chip color="success" label="Activated" />;
    default:
      return <Chip color="disabled" label="Unactivated" />;
  }
};

const stateToChip = (state: boolean) =>
  state ? (
    <Chip color="success" sx={{ alignSelf: "end" }} label="Locked" />
  ) : (
    <Chip color="error" sx={{ alignSelf: "end" }} label="Unlocked" />
  );

export function DeviceCard({ device, onClick, isActive }: DeviceCardProps) {
  return (
    <Box>
      <Card
        onClick={() => {
          onClick();
        }}
      >
        {isActive ? (
          <CardActionArea>
            <CardContent>
              <Stack
                direction="column"
                spacing={1}
                sx={{ alignItems: "flex-start" }}
              >
                <Typography gutterBottom variant="h5" component="div">
                  {device.deviceName}
                </Typography>
                <Chip label={device.hardwareId} />
                <Typography variant="body2">
                  Activation date:{" "}
                  {dayjs(device.registeredOnUtc).format(
                    "dddd, MMMM D, YYYY [at] h:mm A"
                  )}
                </Typography>
                {statusToChip(device.state.status)}
                {stateToChip(device.state.locked)}
                <Typography variant="caption" alignSelf="end">
                  Last updated:{" "}
                  {dayjs(device.state.lastUpdatedOnUtc).format(
                    "dddd, MMMM D, YYYY [at] h:mm A"
                  )}
                </Typography>
              </Stack>
            </CardContent>
          </CardActionArea>
        ) : (
          <CardContent>
            <Stack
              direction="column"
              spacing={1}
              sx={{ alignItems: "flex-start" }}
            >
              <Typography gutterBottom variant="h5" component="div">
                {device.deviceName}
              </Typography>
              <Chip label={device.hardwareId} />
              <Typography variant="body2">
                Last updated:{" "}
                {dayjs(device.registeredOnUtc).format(
                  "dddd, MMMM D, YYYY [at] h:mm A"
                )}
              </Typography>
              {statusToChip(device.state.status)}
              {stateToChip(device.state.locked)}
              <Typography variant="caption" alignSelf="end">
                Last updated:{" "}
                {dayjs(device.state.lastUpdatedOnUtc).format(
                  "dddd, MMMM D, YYYY [at] h:mm A"
                )}
              </Typography>
            </Stack>
          </CardContent>
        )}
      </Card>
    </Box>
  );
}
