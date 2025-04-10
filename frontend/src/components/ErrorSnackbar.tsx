import { Snackbar } from "@mui/material";

export interface ErrorSnackbarProps {
  message: string;
  open: boolean;
}

export function ErrorSnackbar({ open, message }: ErrorSnackbarProps) {
  return (
    <Snackbar
      sx={{ bgcolor: "error.dark", color: "text.primary" }}
      open={open}
      message={message}
      autoHideDuration={5000}
    />
  );
}
