import { Ship } from "../models/ship";
import { ShipResource } from "../models/ship.resource";
import { getShipResourceDisplayName } from "./ship.resource.utils";

export function getSortedResources(ship: Ship | undefined): ShipResource[] {
  if (!ship) {
    return []
  }

  return [...ship.resources].sort((a, b) => {
    const nameA = getShipResourceDisplayName(a).toUpperCase();
    const nameB = getShipResourceDisplayName(b).toUpperCase();
    if (nameA < nameB) return -1;
    if (nameA > nameB) return 1;
    return 0;
  });
}

export function getShipDisplayName(ship: Ship | undefined): string {
  if (!ship) {
    return ""
  }

  return ship.name
    ? `${ship.name} (${ship.id})`
    : ship.id.toString();
}
