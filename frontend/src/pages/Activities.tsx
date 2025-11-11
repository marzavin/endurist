import ActivityPreviewModel from "../interfaces/activities/ActivityPreviewModel";
import ActivityPreviewPanel from "../components/ActivityPreviewPanel";
import { useEffect, useState, CSSProperties } from "react";
import { useData } from "../services/DataProvider";
import SortingModel from "../interfaces/SortingModel";
import { PropagateLoader } from "react-spinners";
import "./Activities.less";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

function Activities() {
  const dataProvider = useData();
  const [items, setItems] = useState<ActivityPreviewModel[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const pageSize: number = 48;
  const sorting: SortingModel = { key: "startTime", descending: true };

  useEffect(() => {
    setLoading(true);
    dataProvider.getActivities(pageSize, sorting).then((result) => {
      setLoading(false);
      setItems(result);
    });
  }, []);

  return (
    <div className="app-activity-list">
      <div className="row align-items-end justify-content-between">
        <div className="col-auto">
          <h3>Latest activities</h3>
          <p>User activities sorted by start date and time</p>
        </div>
        <div className="col-md-auto col-12"></div>
      </div>
      {loading ? (
        <div className="row">
          <div className="col-12">
            <PropagateLoader
              color="#f48221"
              cssOverride={override}
              loading={loading}
            ></PropagateLoader>
          </div>
        </div>
      ) : null}
      {items.length === 0 && !loading ? (
        <div className="row">
          <div className="col-12">No information to display.</div>
        </div>
      ) : null}
      <div className="row">
        {items.map((item) => (
          <ActivityPreviewPanel key={item.id} activity={item} />
        ))}
      </div>
    </div>
  );
}

export default Activities;
