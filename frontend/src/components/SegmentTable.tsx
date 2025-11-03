import { toPaceLabel } from "../formatters/PaceFormatter";
import { toMetersLabel } from "../formatters/DistanceFormatter";
import dayjs from "dayjs";
import SegmentPreviewModel from "../interfaces/SegmentPreviewModel";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";

interface Props {
  items: SegmentPreviewModel[];
}

function SegmentTable({ items }: Props) {
  return (
    <div className="app-table table-responsive small">
      <table className="table">
        <thead>
          <tr>
            <th>Start Time</th>
            <th>Distance</th>
            <th>Duration</th>
            <th>Pace</th>
            <th>Heart Rate</th>
            <th>Cadence</th>
          </tr>
        </thead>
        <tbody>
          {items.length === 0 ? (
            <tr>
              <td colSpan={6}>No information to display.</td>
            </tr>
          ) : null}
          {items.map((item) => (
            <tr key={item.startTime}>
              <td>{dayjs(item.startTime).format("YYYY-MM-DD HH:mm")}</td>
              <td>{toMetersLabel(item.distance)}</td>
              <td>{toTimeSpanLabel(item.duration)}</td>
              <td>{toPaceLabel(item.pace)}</td>
              <td>{item.averageHeartRate}</td>
              <td>{item.averageCadence}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default SegmentTable;
