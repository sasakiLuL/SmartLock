export interface Action {
  id: string;
  deviceId: string;
  userId: string;
  type: number;
  status: number;
  requestedOn: string;
  executedOn: string;
}
