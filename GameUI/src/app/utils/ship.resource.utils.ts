import { ShipResource } from "../models/ship.resource";

export function getShipResourceDisplayName(shipResource: ShipResource): string {
  return shipResource.name
    ? `${shipResource.name} (${shipResource.resourceType})`
    : shipResource.resourceType;
}
