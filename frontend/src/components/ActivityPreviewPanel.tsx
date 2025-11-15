import ActivityCategory from "../enums/ActivityCategory";
import ActivityPreviewModel from "../interfaces/activities/ActivityPreviewModel";
import { toKilometersLabel } from "../formatters/DistanceFormatter";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";
import dayjs from "dayjs";

interface Props {
  activity: ActivityPreviewModel;
}

function ActivityPreviewPanel({ activity }: Props) {
  return (
    <div className="app-activity-preview-panel col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2">
      <div>
        <div className="app-panel-line row">
          <div className="col-6 d-flex justify-content-start">
            <a href={`/activities/${activity.id}`}>
              <strong>{ActivityCategory[activity.category]}</strong>
            </a>
          </div>
          <div className="col-6 d-flex justify-content-end">
            {activity.distance > 42195 ? (
              <span className="app-badge badge app-error-label rounded-pill">
                MR
              </span>
            ) : null}
            {activity.distance > 21097.5 ? (
              <span className="app-badge badge app-warning-label">HM</span>
            ) : null}
            {activity.distance > 10000 ? (
              <span className="app-badge badge app-info-label">10K</span>
            ) : null}
            {activity.distance > 5000 ? (
              <span className="app-badge badge app-success-label">5K</span>
            ) : null}
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-4 d-flex justify-content-start">
            <strong>Start Time:</strong>
          </div>
          <div className="col-8 d-flex justify-content-end">
            <span>
              {dayjs(activity.startTime).format("YYYY-MM-DD") +
                " " +
                dayjs(activity.startTime).format("HH:mm")}
            </span>
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Distance:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            {toKilometersLabel(activity.distance)}
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Duration:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            {toTimeSpanLabel(activity.duration)}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ActivityPreviewPanel;
