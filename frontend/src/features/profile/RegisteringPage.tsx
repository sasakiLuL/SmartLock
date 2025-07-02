import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/auth/useAuth";
import { useEffect, useState } from "react";
import axios from "axios";
import { Endpoints } from "../../utils/Endpoints";
import { NavLinks } from "../../utils/NavLinks";
import { CircularProgress, Container, Typography } from "@mui/material";
import Grid from "@mui/material/Grid2";
import { NavigationBar } from "../../components/NavigationBar";

export function RegisteringPage() {
  const auth = useAuth();
  const navigate = useNavigate();
  const [registered, setRegistered] = useState<boolean>(false);

  useEffect(() => {
    if (!auth.isAuthenticated) {
      console.log("Not authenticated");
      return;
    }

    const email = auth.tokenParsed?.email;
    const username = auth.tokenParsed?.preferred_username;

    axios
      .post(
        Endpoints.register,
        {
          email,
          username,
        },
        {
          headers: {
            Authorization: `Bearer ${auth.token}`,
          },
        }
      )
      .then(() => {
        console.log("Backend registration successful");
        setRegistered(true);
        navigate(NavLinks.Default);
      })
      .catch((err) => {
        console.error("Backend registration failed", err);
      });
  }, [auth]);

  return (
    <>
      <NavigationBar />
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
          {registered ? (
            <>
              <Typography
                variant="h4"
                sx={{ textAlign: "center", color: "text.disabled" }}
              >
                Registering
              </Typography>
              <CircularProgress />
            </>
          ) : (
            <Typography
              variant="h4"
              sx={{ textAlign: "center", color: "text.disabled" }}
            >
              Registered. Redirecting to main page
            </Typography>
          )}
        </Grid>
      </Container>
    </>
  );
}
