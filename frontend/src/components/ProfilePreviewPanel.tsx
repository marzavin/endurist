import ProfilePreviewModel from "../interfaces/profiles/ProfilePreviewModel";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";
import { toKilometersLabel } from "../formatters/DistanceFormatter";

interface Props {
  profile: ProfilePreviewModel;
}

function ProfilePreviewPanel({ profile }: Props) {
  return (
    <div className="app-card">
      <div className="app-card-row">
        <div>
          <a href={`/profiles/${profile.id}`}>
            <strong>{profile.name}</strong>
          </a>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Distance:</strong>
        </div>
        <div>
          <span>{toKilometersLabel(profile.distance)}</span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Duration:</strong>
        </div>
        <div>
          <span>{toTimeSpanLabel(profile.duration)}</span>
        </div>
      </div>
    </div>
  );
}

export default ProfilePreviewPanel;
