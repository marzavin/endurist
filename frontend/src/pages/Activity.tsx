import ActivityModel from "../interfaces/activities/ActivityModel";
import SegmentTable from "../components/SegmentTable";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { useData } from "../services/DataProvider";
import ActivityGraph from "../components/ActivityGraph";

function Activity() {
  const dataProvider = useData();
  const [activity, setActivity] = useState<ActivityModel>({
    startTime: "",
    distance: 0,
    duration: 0,
    pace: 0,
    averageHeartRate: null,
    averageCadence: null,
    id: "",
    category: 1,
    calories: null,
    segments: [],
    heartRateGraph: [],
    paceGraph: [],
    altitudeGraph: [],
    cadenceGraph: []
  });

  const { id } = useParams();

  useEffect(() => {
    if (id !== undefined) {
      dataProvider.getActivity(id).then((result) => {
        setActivity(result);
      });
    }
  }, []);

  return (
    <div className="app-activity row">
      <div className="col-12 col-lg-6">
        <SegmentTable items={activity.segments ?? []} />
      </div>
      <div className="col-12 col-lg-6">
        <ActivityGraph
          title="Heart Rate Graph"
          graph={activity.heartRateGraph}
          unitOfMeasure="bpm"
        />
      </div>
      <div className="col-12 col-lg-6">
        <ActivityGraph
          title="Altitude Graph"
          graph={activity.altitudeGraph}
          unitOfMeasure="m"
        />
      </div>
      <div className="col-12 col-lg-6">
        <ActivityGraph
          title="Cadence Graph"
          graph={activity.cadenceGraph}
          unitOfMeasure="spm"
        />
      </div>
    </div>
  );
}

export default Activity;
