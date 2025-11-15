import ProfilePreviewModel from "../interfaces/profiles/ProfilePreviewModel";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";
import { toKilometersLabel } from "../formatters/DistanceFormatter";

interface Props {
  profile: ProfilePreviewModel;
}

function ProfilePreviewPanel({ profile }: Props) {
  return (
    <div className="app-profile-preview-panel col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2">
      <div>
        <div className="app-panel-line row">
          <div className="col-12 d-flex justify-content-start">
            <a href={`/profiles/${profile.id}`}>
              <strong>{profile.name}</strong>
            </a>
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Distance:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            <span>{toKilometersLabel(profile.distance)}</span>
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Duration:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            <span>{toTimeSpanLabel(profile.duration)}</span>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProfilePreviewPanel;
