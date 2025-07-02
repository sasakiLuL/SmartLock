import {
  AppBar,
  Box,
  Button,
  Container,
  IconButton,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useState } from "react";
import { NavLinks } from "../utils/NavLinks";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/auth/useAuth";
import useAuthActions from "../hooks/auth/useAuthActions";

export function NavigationBar() {
  const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(null);

  const navigate = useNavigate();
  var auth = useAuth();
  var authActions = useAuthActions();

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Typography
            component="a"
            variant="h5"
            noWrap
            href="#app-bar-with-responsive-menu"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              color: "text.primary",
              textDecoration: "none",
            }}
          >
            SmartLock
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              anchorEl={anchorElNav}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{ display: { xs: "block", md: "none" } }}
            >
              <MenuItem
                key={"Devices"}
                onClick={() => {
                  navigate(NavLinks.Devices);
                }}
              >
                <Typography sx={{ textAlign: "center" }}>Devices</Typography>
              </MenuItem>
            </Menu>
          </Box>
          <Typography
            variant="h5"
            noWrap
            color="primary"
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              textDecoration: "none",
            }}
          >
            SmartLock
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            <Button
              key={"Devices"}
              onClick={() => {
                navigate(NavLinks.Devices);
              }}
              sx={{ my: 2, color: "white", display: "block" }}
            >
              Devices
            </Button>
          </Box>
          <Box sx={{ flexGrow: 0 }}>
            {auth.isAuthenticated ? (
              <Button
                variant="text"
                onClick={() => {
                  navigate(NavLinks.Profile);
                }}
              >
                <Typography variant="body2" sx={{ m: 1 }} color="text.primary">
                  {auth.tokenParsed?.preferred_username ?? "N/A"}
                </Typography>
                <AccountCircleIcon />
              </Button>
            ) : (
              <Button
                size="small"
                color="primary"
                variant="outlined"
                onClick={() => {
                  authActions.login();
                }}
              >
                Sign in
              </Button>
            )}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
