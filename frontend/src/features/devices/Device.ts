export interface Device {
  id: string;
  hardwareId: string;
  deviceName: string | null;
  deviceStatus: number;
  registeredOnUtc: Date;
}
