import MetricModel from "../interfaces/MedricModel";

interface Props {
  metric: MetricModel;
}

function MetricCard({ metric }: Props) {
  return (
    <div className="app-card">
      <div className="app-card-row">
        <div>
          <span className="app-font-m">{metric.name}</span>
        </div>
      </div>
      <div className="app-card-row">
        <div className="app-font-l">
          <strong>{metric.value}</strong>
        </div>
        <div className="app-font-s">
          {metric.top > 75 ? (
            <span className="app-badge app-error-label">Top {metric.top}%</span>
          ) : metric.top > 50 ? (
            <span className="app-badge app-warning-label">
              Top {metric.top}%
            </span>
          ) : metric.top > 25 ? (
            <span className="app-badge app-info-label">Top {metric.top}%</span>
          ) : (
            <span className="app-badge app-success-label">
              Top {metric.top}%
            </span>
          )}
        </div>
      </div>
    </div>
  );
}

export default MetricCard;
