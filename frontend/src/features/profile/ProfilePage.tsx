import { useEffect } from "react";
import { useAuth } from "../../hooks/auth/useAuth";
import { Button, Container, Stack, Typography } from "@mui/material";
import { useAuthActions } from "../../hooks/auth/useAuthActions";
import { NavigationBar } from "../../components/NavigationBar";
import CheckBoxIcon from "@mui/icons-material/CheckBox";
import DisabledByDefaultIcon from "@mui/icons-material/DisabledByDefault";
import { useNavigate } from "react-router-dom";
import { NavLinks } from "../../utils/NavLinks";

export function ProfilePage() {
  var auth = useAuth();
  var authActions = useAuthActions();
  var navigate = useNavigate();

  useEffect(() => {}, [auth]);

  return (
    <>
      <NavigationBar />
      <Container maxWidth="xl">
        {auth.isAuthenticated ? (
          <Stack
            spacing={2}
            sx={{
              justifyContent: "center",
              alignItems: "flex-start",
            }}
          >
            <Typography>
              Username: {auth.tokenParsed?.preferred_username || "N/A"}
            </Typography>
            <Stack direction="row" spacing={2}>
              <Typography>Email: {auth.tokenParsed?.email || "N/A"}</Typography>
              {auth.tokenParsed?.email_verified ? (
                <CheckBoxIcon color="success" />
              ) : (
                <DisabledByDefaultIcon color="error" />
              )}
            </Stack>
            <Typography>
              First name: {auth.tokenParsed?.given_name || "N/A"}
            </Typography>
            <Typography>
              Last name: {auth.tokenParsed?.family_name || "N/A"}
            </Typography>
            <Button
              variant="contained"
              color="error"
              onClick={() => {
                authActions.logout();
                navigate(NavLinks.Devices);
              }}
            >
              Log out
            </Button>
          </Stack>
        ) : (
          <Typography>You need to sign in to get profile data!</Typography>
        )}
      </Container>
    </>
  );
}
