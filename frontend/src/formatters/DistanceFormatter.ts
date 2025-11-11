function toMetersLabel(distance: number) {
  return distance.toFixed(0) + " m";
}

function toKilometersLabel(distance: number) {
  return (distance / 1000).toFixed(2) + " km";
}

export { toMetersLabel, toKilometersLabel };
