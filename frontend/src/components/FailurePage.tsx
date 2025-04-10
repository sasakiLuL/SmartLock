import { Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";

export function FailurePage() {
  return (
    <Container maxWidth="xl">
      <Grid
        spacing={2}
        container
        direction="column"
        sx={{
          justifyContent: "center",
          alignItems: "center",
          minHeight: "50vh",
        }}
      >
        <Typography
          variant="h4"
          sx={{ textAlign: "center", color: "text.disabled" }}
        >
          Something went wrong...
        </Typography>
        <Typography
          variant="body1"
          sx={{ textAlign: "center", color: "text.disabled" }}
        >
          Please, try to reload page or use service later
        </Typography>
      </Grid>
    </Container>
  );
}
