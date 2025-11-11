import ProfileModel from "../interfaces/profiles/ProfileModel";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { useData } from "../services/DataProvider";
import TrainingVolumeWidget from "../components/widgets/TrainingVolumeWidget";
import Summary from "../components/Summary";
import KeyValueModel from "../interfaces/KeyValueModel";
import { toKilometersLabel } from "../formatters/DistanceFormatter";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";

function Profile() {
  const dataProvider = useData();
  const { id } = useParams();
  const [profile, setProfile] = useState<ProfileModel>({
    id: "",
    name: "",
    distance: 0,
    duration: 0,
    weeklyVolumes: []
  });
  const [details, setDetails] = useState<KeyValueModel<string, string>[]>([]);

  useEffect(() => {
    if (id !== undefined) {
      dataProvider.getProfile(id).then((result) => {
        setProfile(result);
        const profileDetails: KeyValueModel<string, string>[] = [
          { key: "Distance", value: toKilometersLabel(result.distance) },
          { key: "Duration", value: toTimeSpanLabel(result.duration) }
        ];
        setDetails(profileDetails);
      });
    }
  }, []);

  return (
    <div className="app-profile row">
      <div className="col-12 col-lg-6">
        <Summary title={profile.name} properties={details} />
      </div>
      <div className="col-12 col-lg-6">
        <TrainingVolumeWidget profileId={profile.id} />
      </div>
    </div>
  );
}

export default Profile;
