function toTimeSpanLabel(duration: number) {
  const seconds = Math.floor((duration / 1000) % 60);
  const minutes = Math.floor((duration / (1000 * 60)) % 60);
  const hours = Math.floor((duration / (1000 * 60 * 60)) % 24);
  const days = Math.floor(duration / (1000 * 60 * 60 * 24));

  return (
    (days > 0 ? days + "d " : "") +
    (hours > 0 ? hours + "h " : "") +
    (minutes > 0 ? minutes + "m " : "") +
    (seconds > 0 ? seconds + "s" : "")
  ).trim();
}

export { toTimeSpanLabel };
