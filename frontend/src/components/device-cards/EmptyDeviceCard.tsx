import { Card, CardContent, Skeleton, Stack } from "@mui/material";

export function EmptyDeviceCard() {
  return (
    <Card>
      <CardContent>
        <Stack direction="column" spacing={1} sx={{ alignItems: "flex-start" }}>
          <Skeleton variant="h5" sx={{ fontSize: "1rem" }} width="70%" />
          <Skeleton variant="rounded" sx={{ fontSize: "1rem" }} width="70%" />
          <Skeleton variant="caption" sx={{ fontSize: "1rem" }} width="70%" />
          <Skeleton variant="text" sx={{ fontSize: "1rem" }} width="70%" />
        </Stack>
      </CardContent>
    </Card>
  );
}
