interface SegmentPreviewModel {
  startTime: string;
  distance: number;
  duration: number;
  pace: number;
  averageHeartRate: number | null;
  averageCadence: number | null;
}

export default SegmentPreviewModel;
