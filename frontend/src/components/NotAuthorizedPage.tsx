import { Button, Container, Stack, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { useAuthActions } from "../hooks/auth/useAuthActions";

export interface NotAuthorizedPageProps {
  message: String;
}

export function NotAuthorizedPage({ message }: NotAuthorizedPageProps) {
  const authActions = useAuthActions();
  return (
    <Container maxWidth="xl">
      <Grid
        container
        direction="column"
        sx={{
          minHeight: "50vh",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Stack direction="row" spacing={2}>
          <Typography
            variant="h6"
            sx={{ textAlign: "center", color: "text.primary" }}
          >
            {message}
          </Typography>
          <Button
            variant="contained"
            color="primary"
            size="medium"
            onClick={() => {
              authActions.login();
            }}
          >
            Sign in
          </Button>
        </Stack>
        <Stack
          direction="row"
          spacing={2}
          sx={{
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <Typography
            variant="body2"
            sx={{ textAlign: "center", color: "text.disabled" }}
          >
            New to SmartLock?
          </Typography>
          <Button
            color="primary"
            size="small"
            onClick={() => {
              authActions.register();
            }}
          >
            Sign up
          </Button>
        </Stack>
      </Grid>
    </Container>
  );
}
