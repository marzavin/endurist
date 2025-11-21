import ProfileModel from "../interfaces/profiles/ProfileModel";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { useData } from "../services/DataProvider";
import TrainingVolumeWidget from "../components/widgets/TrainingVolumeWidget";
import { toKilometersLabel } from "../formatters/DistanceFormatter";
import { toTimeSpanLabel } from "../formatters/DurationFormatter";
import MetricCard from "../components/MetricCard";
import MetricModel from "../interfaces/MedricModel";
//import "./Profile.less";

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
  const [metrics, setMetrics] = useState<MetricModel[]>([]);

  useEffect(() => {
    if (id !== undefined) {
      dataProvider.getProfile(id).then((result) => {
        setProfile(result);
        setMetrics([
          {
            name: "Total activities",
            value: (785).toString(),
            top: 76
          },
          {
            name: "Total time of activities",
            value: toTimeSpanLabel(result.duration),
            top: 38
          },
          {
            name: "Distance covered",
            value: toKilometersLabel(result.distance),
            top: 10
          }
        ]);
      });
    }
  }, []);

  return (
    <div className="app-profile">
      <div className="app-list row">
        {metrics.map((metric) => (
          <div
            key={metric.name}
            className="col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2"
          >
            <MetricCard metric={metric} />
          </div>
        ))}
      </div>
      <div className="row">
        <div className="col-12 col-lg-6">
          <TrainingVolumeWidget profileId={profile.id} />
        </div>
      </div>
    </div>
  );
}

export default Profile;
