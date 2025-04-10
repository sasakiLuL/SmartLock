import {
  Card,
  CardActionArea,
  CardContent,
  Chip,
  Stack,
  Typography,
} from "@mui/material";
import { Device } from "../Device";
import dayjs from "dayjs";

export interface DeviceCardProps {
  device: Device;
  onClick: () => void;
  isActive: boolean;
}

const numberToChip = (num: number) => {
  switch (num) {
    case 0:
      return (
        <Chip
          color="success"
          sx={{ alignSelf: "flex-end" }}
          label="Activated"
        />
      );
    case 1:
      return (
        <Chip color="primary" sx={{ alignSelf: "flex-end" }} label="Pending" />
      );
    default:
      return (
        <Chip
          color="disabled"
          sx={{ alignSelf: "flex-end" }}
          label="Unactivated"
        />
      );
  }
};

export function DeviceCard({ device, onClick, isActive }: DeviceCardProps) {
  return (
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
                {device.deviceName ?? device.id}
              </Typography>
              <Chip label={device.hardwareId} />
              <Typography noWrap variant="caption">
                Activation date:{" "}
                {dayjs(device.registeredOnUtc).format(
                  "dddd, MMMM D, YYYY [at] h:mm A"
                )}
              </Typography>
              {numberToChip(device.deviceStatus)}
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
              {device.deviceName ?? device.id}
            </Typography>
            <Chip label={device.hardwareId} />
            <Typography noWrap variant="caption">
              Activation date:{" "}
              {dayjs(device.registeredOnUtc).format(
                "dddd, MMMM D, YYYY [at] h:mm A"
              )}
            </Typography>
            {numberToChip(device.deviceStatus)}
          </Stack>
        </CardContent>
      )}
    </Card>
  );
}
