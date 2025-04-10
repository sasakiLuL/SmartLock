import { Snackbar } from "@mui/material";

export interface SuccessSnackbarProps {
  message: string;
  open: boolean;
}

export function SuccessSnackbar({ open, message }: SuccessSnackbarProps) {
  return (
    <Snackbar
      sx={{ bgcolor: "success.dark", color: "text.primary" }}
      open={open}
      message={message}
      autoHideDuration={5000}
    />
  );
}
