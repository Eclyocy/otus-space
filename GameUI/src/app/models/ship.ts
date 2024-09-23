import { ShipResource } from "./ship.resource";

export interface Ship {
  id: string;
  day: number;
  resources: ShipResource[]
}
