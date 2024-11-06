import { ShipResource } from "./ship.resource";

export interface Ship {
  id: string;
  name: string | null;
  day: number;
  state: string;
  distanceTraveled: number;
  distanceTarget: number;
  resources: ShipResource[]
}
