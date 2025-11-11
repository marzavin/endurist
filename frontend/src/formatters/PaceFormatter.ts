function toPaceLabel(pace: number) {
  const seconds = Math.floor((pace / 1000) % 60);
  const minutes = Math.floor((pace / (1000 * 60)) % 60);

  return `${minutes}:${seconds < 10 ? "0" + seconds : seconds} min/km`;
}

export { toPaceLabel };
