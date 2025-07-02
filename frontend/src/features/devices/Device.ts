import { State } from "./State";

export interface Device {
  id: string;
  hardwareId: string;
  ownerId: string;
  deviceName: string;
  state: State;
  registeredOnUtc: Date;
}
