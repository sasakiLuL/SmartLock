import { Box, createTheme, ThemeProvider } from "@mui/material";
import { KeycloakProvider } from "./hooks/auth/KeycloakProvider";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ProfilePage } from "./features/profile/ProfilePage";
import { DevicesPage } from "./features/devices/DevicesPage";
import { DevicePage } from "./features/devices/DevicePage";

function App() {
  const theme = createTheme({
    palette: {
      mode: "dark",
      primary: {
        main: "#ff5722",
      },
      secondary: {
        main: "#ffc400",
      },
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <Box
        sx={{
          minHeight: "100vh",
          bgcolor: "background.default",
          color: "text.primary",
        }}
      >
        <KeycloakProvider>
          <BrowserRouter>
            <Routes>
              <Route path="*" element={<DevicesPage />} />
              <Route path="/profile" element={<ProfilePage />} />
              <Route path="/devices" element={<DevicesPage />} />
              <Route path="/devices/:id" element={<DevicePage />} />
            </Routes>
          </BrowserRouter>
        </KeycloakProvider>
      </Box>
    </ThemeProvider>
  );
}

export default App;
