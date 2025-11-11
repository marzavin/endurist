import ProfilePreviewModel from "../interfaces/profiles/ProfilePreviewModel";
import ProfilePreviewPanel from "../components/ProfilePreviewPanel";
import { useEffect, useState, CSSProperties } from "react";
import { useData } from "../services/DataProvider";
import SortingModel from "../interfaces/SortingModel";
import { PropagateLoader } from "react-spinners";
import "./Profiles.less";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

function Profiles() {
  const dataProvider = useData();
  const [items, setItems] = useState<ProfilePreviewModel[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const pageSize: number = 48;
  const sorting: SortingModel = { key: "name", descending: true };

  useEffect(() => {
    setLoading(true);
    dataProvider.getProfiles(pageSize, sorting).then((result) => {
      setLoading(false);
      setItems(result);
    });
  }, []);

  return (
    <div className="app-profile-list">
      <div className="row align-items-end justify-content-between">
        <div className="col-auto">
          <h3>Profiles</h3>
          <p>User profiles sorted by name</p>
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
          <ProfilePreviewPanel key={item.id} profile={item} />
        ))}
      </div>
    </div>
  );
}

export default Profiles;
