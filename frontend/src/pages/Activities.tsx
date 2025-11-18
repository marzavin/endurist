import ActivityPreviewModel from "../interfaces/activities/ActivityPreviewModel";
import ActivityPreviewPanel from "../components/ActivityPreviewPanel";
import { useEffect, useState, CSSProperties } from "react";
import { useData } from "../services/DataProvider";
import SortingModel from "../interfaces/SortingModel";
import { PropagateLoader } from "react-spinners";
import InfiniteScroll from "react-infinite-scroll-component";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

function Activities() {
  const dataProvider = useData();
  const [items, setItems] = useState<ActivityPreviewModel[]>([]);
  const [hasMore, setHasMore] = useState<boolean>(true);
  const [loading, setLoading] = useState<boolean>(false);
  const [sorting, setSorting] = useState<SortingModel>({
    key: "startTime",
    descending: true
  });
  const pageSize: number = 48;

  useEffect(() => {
    setLoading(true);
    setHasMore(true);
    dataProvider.getActivities(0, pageSize, sorting).then((result) => {
      setLoading(false);
      if (result.length < pageSize) {
        setHasMore(false);
      }
      setItems(result);
    });
  }, [sorting]);

  function handleNextPage() {
    dataProvider
      .getActivities(items.length, pageSize, sorting)
      .then((result) => {
        if (result.length < pageSize) {
          setHasMore(false);
        }
        setItems((previousItems) => [...previousItems, ...result]);
      });
  }

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
      <InfiniteScroll
        className="app-list row"
        dataLength={items.length}
        next={handleNextPage}
        hasMore={hasMore}
        loader={
          <div className="row">
            <div className="col-12">
              <PropagateLoader
                color="#f48221"
                cssOverride={override}
                loading={true}
              ></PropagateLoader>
            </div>
          </div>
        }
      >
        {items.map((item) => (
          <div
            key={item.id}
            className="col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2"
          >
            <ActivityPreviewPanel activity={item} />
          </div>
        ))}
      </InfiniteScroll>
    </div>
  );
}

export default Activities;
