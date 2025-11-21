import ActivityCategory from "../enums/ActivityCategory";
import ActivityPreviewModel from "../interfaces/activities/ActivityPreviewModel";
import { toKilometersLabel } from "../formatters/DistanceFormatter";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";
import dayjs from "dayjs";

interface Props {
  activity: ActivityPreviewModel;
}

function ActivityCard({ activity }: Props) {
  return (
    <div className="app-card">
      <div className="app-card-row">
        <div>
          <a href={`/activities/${activity.id}`}>
            <strong>{ActivityCategory[activity.category]}</strong>
          </a>
        </div>
        <div className="app-font-s">
          {activity.distance > 42195 ? (
            <span className="app-badge app-error-label">MR</span>
          ) : null}
          {activity.distance > 21097.5 ? (
            <span className="app-badge app-warning-label">HM</span>
          ) : null}
          {activity.distance > 10000 ? (
            <span className="app-badge app-info-label">10K</span>
          ) : null}
          {activity.distance > 5000 ? (
            <span className="app-badge app-success-label">5K</span>
          ) : null}
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Start Time:</strong>
        </div>
        <div>
          <span>
            {dayjs(activity.startTime).format("YYYY-MM-DD") +
              " " +
              dayjs(activity.startTime).format("HH:mm")}
          </span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Distance:</strong>
        </div>
        <div>
          <span>{toKilometersLabel(activity.distance)}</span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Duration:</strong>
        </div>
        <div>
          <span>{toTimeSpanLabel(activity.duration)}</span>
        </div>
      </div>
    </div>
  );
}

export default ActivityCard;
