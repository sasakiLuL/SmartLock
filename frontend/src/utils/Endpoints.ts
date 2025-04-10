export const Endpoints = {
  getUser: "https://localhost:5001/users",
  getDevices: "https://localhost:5001/devices",
  deleteDevice: (guid: string) => `https://localhost:5001/devices/${guid}`,
  open: (guid: string) => `https://localhost:5001/devices/${guid}/open`,
  close: (guid: string) => `https://localhost:5001/devices/${guid}/close`,
  activateDevice: (guid: string) =>
    `https://localhost:5001/devices/${guid}/activate`,
};
