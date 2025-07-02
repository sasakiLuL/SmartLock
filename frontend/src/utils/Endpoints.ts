export const Endpoints = {
  register: "https://localhost:5001/users/register",
  getUser: "https://localhost:5001/users",

  getDevice: (guid: string) => `https://localhost:5001/devices/${guid}`,
  getDevices: "https://localhost:5001/devices",
  addDevice: "https://localhost:5001/devices",
  removeDevice: (guid: string) => `https://localhost:5001/devices/${guid}`,
  activateDevice: (guid: string) =>
    `https://localhost:5001/devices/${guid}/activate`,
  deactivateDevice: (guid: string) =>
    `https://localhost:5001/devices/${guid}/deactivate`,
  unlockDevice: (guid: string) =>
    `https://localhost:5001/devices/${guid}/unlock`,
  lockDevice: (guid: string) => `https://localhost:5001/devices/${guid}/lock`,

  getActionsByDevice: (
    guid: string,
    pageNumber: number,
    pageSize: number,
    sortColumn?: string,
    sortOrder?: string
  ) => {
    let params = `pageNumber=${pageNumber + 1}&pageSize=${pageSize}`;

    if (sortColumn !== undefined && sortColumn !== null) {
      params = params + `&sortColumn=${sortColumn}`;
    }

    if (sortOrder !== undefined && sortOrder !== null) {
      params = params + `&sortOrder=${sortOrder}`;
    }
    return `https://localhost:5001/actions/devices/${guid}?${params}`;
  },
};
