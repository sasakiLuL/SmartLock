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

export interface DeviceAddedEvent {
  hardwareId: string;
  deviceName: string;
}

export interface AddDeviceDialogProps {
  open: boolean;
  onSubmit: (value: DeviceAddedEvent) => void;
  onClose: () => void;
}

export function AddDeviceDialog({
  open,
  onSubmit,
  onClose,
}: AddDeviceDialogProps) {
  const formik = useFormik({
    initialValues: {
      hardwareId: "",
      deviceName: "",
    },
    validateOnChange: true,
    validationSchema: object({
      hardwareId: string().uuid().required(),
      deviceName: string().max(100),
    }),
    onSubmit: (values) => {
      console.log("Im here");
      onSubmit({
        hardwareId: values.hardwareId,
        deviceName: values.deviceName,
      });
    },
  });

  return (
    <Dialog open={open} onClose={onClose}>
      <DialogTitle>Add device</DialogTitle>
      <DialogContent>
        <DialogContentText>Fill fields to add devices</DialogContentText>
        <TextField
          error={formik.errors.deviceName ? true : false}
          helperText={formik.errors.deviceName}
          required
          id="deviceName"
          name="deviceName"
          label="Device name"
          fullWidth
          variant="standard"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.deviceName}
        />
        <TextField
          error={formik.errors.hardwareId ? true : false}
          helperText={formik.errors.hardwareId}
          required
          id="hardwareId"
          name="hardwareId"
          label="Device hardware id"
          fullWidth
          variant="standard"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.hardwareId}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button type="submit" onClick={formik.handleSubmit}>
          Add
        </Button>
      </DialogActions>
    </Dialog>
  );
}
