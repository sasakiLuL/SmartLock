import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField,
} from "@mui/material";
import { useFormik } from "formik";
import { object, string } from "yup";

export interface ActivateDeviceEvent {
  id: string;
}

export interface ActivateDeviceDialogProps {
  open: boolean;
  onSubmit: (value: ActivateDeviceEvent) => void;
  onClose: () => void;
}

export function ActivateDeviceDialog({
  open,
  onSubmit,
  onClose,
}: ActivateDeviceDialogProps) {
  const formik = useFormik({
    initialValues: {
      id: "",
    },
    validateOnChange: true,
    validationSchema: object({ id: string().uuid().required() }),
    onSubmit: (values) => {
      onSubmit({ id: values.id });
    },
  });

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Activate</DialogTitle>
      <DialogContent>
        <DialogContentText>
          Enter device hardware id to activate device
        </DialogContentText>
        <TextField
          error={formik.errors.id ? true : false}
          helperText={formik.errors.id}
          required
          id="id"
          name="id"
          label="Hardware Id"
          fullWidth
          variant="standard"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.id}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button type="submit" onClick={formik.handleSubmit}>
          Activate
        </Button>
      </DialogActions>
    </Dialog>
  );
}
