import ProfilePreviewModel from "../interfaces/profiles/ProfilePreviewModel";
import ProfileCard from "../components/ProfileCard";
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

function Profiles() {
  const dataProvider = useData();
  const [items, setItems] = useState<ProfilePreviewModel[]>([]);
  const [hasMore, setHasMore] = useState<boolean>(true);
  const [sorting] = useState<SortingModel>({
    key: "name",
    descending: true
  });
  const pageSize: number = 48;

  useEffect(() => {
    setHasMore(true);
    dataProvider.getProfiles(0, pageSize, sorting).then((result) => {
      if (result.length < pageSize) {
        setHasMore(false);
      }
      setItems(result);
    });
  }, [sorting]);

  function handleNextPage() {
    dataProvider.getProfiles(items.length, pageSize, sorting).then((result) => {
      if (result.length < pageSize) {
        setHasMore(false);
      }
      setItems((previousItems) => [...previousItems, ...result]);
    });
  }

  return (
    <div className="app-profile-list">
      <div className="row align-items-end justify-content-between">
        <div className="col-auto">
          <h3>Profiles</h3>
          <p>User profiles sorted by name</p>
        </div>
        <div className="col-md-auto col-12"></div>
      </div>
      <InfiniteScroll
        className="app-list row"
        dataLength={items.length}
        next={handleNextPage}
        hasMore={hasMore}
        loader={
          <PropagateLoader
            color="#f48221"
            cssOverride={override}
            loading={true}
          ></PropagateLoader>
        }
      >
        {items.length === 0 ? (
          <span className="app-empty-list-label">
            No information to display.
          </span>
        ) : (
          items.map((item) => (
            <div
              key={item.id}
              className="col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2"
            >
              <ProfileCard profile={item} />
            </div>
          ))
        )}
      </InfiniteScroll>
    </div>
  );
}

export default Profiles;
