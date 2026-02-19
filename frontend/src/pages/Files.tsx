import FilePreviewModel from "../interfaces/files/FilePreviewModel";
import SortingModel from "../interfaces/SortingModel";
import FilePreviewPanel from "../components/FilePreviewPanel";
import FileUploader from "../components/FileUploader";
import { useEffect, useState, CSSProperties, ChangeEvent } from "react";
import { useData } from "../services/DataProvider";
import { PropagateLoader } from "react-spinners";
import InfiniteScroll from "react-infinite-scroll-component";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

function Files() {
  const dataProvider = useData();
  const [items, setItems] = useState<FilePreviewModel[]>([]);
  const [hasMore, setHasMore] = useState<boolean>(true);
  const [sorting, setSorting] = useState<SortingModel>({
    key: "uploadedAt",
    descending: true
  });
  const pageSize: number = 48;

  useEffect(() => {
    setHasMore(true);
    dataProvider.getFiles(0, pageSize, sorting).then((result) => {
      if (result.length < pageSize) {
        setHasMore(false);
      }
      setItems(result);
    });
  }, [sorting]);

  function handleNextPage() {
    dataProvider.getFiles(items.length, pageSize, sorting).then((result) => {
      if (result.length < pageSize) {
        setHasMore(false);
      }
      setItems((previousItems) => [...previousItems, ...result]);
    });
  }

  function handleRadioChange(e: ChangeEvent<HTMLInputElement>) {
    setSorting({ descending: sorting.descending, key: e.target.value });
  }

  return (
    <div className="app-file-list">
      <div className="row">
        <div className="col-12">
          <h3>Files</h3>
        </div>
        <div className="col-12">
          <FileUploader />
        </div>
        <div className="col-12">
          <div
            className="app-sorting-panel btn-group"
            role="group"
            aria-label="Files sorting radio button group"
          >
            <input
              type="radio"
              className="btn-check"
              name="order-radio"
              id="order-uploaded"
              autoComplete="off"
              checked={sorting.key === "uploadedAt"}
              onChange={handleRadioChange}
              value="uploadedAt"
            />
            <label className="btn btn-outline-primary" htmlFor="order-uploaded">
              Uploaded
            </label>
            <input
              type="radio"
              className="btn-check"
              name="order-radio"
              id="order-processed"
              autoComplete="off"
              checked={sorting.key === "processedAt"}
              onChange={handleRadioChange}
              value="processedAt"
            />
            <label
              className="btn btn-outline-primary"
              htmlFor="order-processed"
            >
              Processed
            </label>
            <input
              type="radio"
              className="btn-check"
              name="order-radio"
              id="order-activity-started"
              autoComplete="off"
              checked={sorting.key === "activityStartedAt"}
              onChange={handleRadioChange}
              value="activityStartedAt"
            />
            <label
              className="btn btn-outline-primary"
              htmlFor="order-activity-started"
            >
              Activity Started
            </label>
          </div>
        </div>
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
              <FilePreviewPanel file={item} />
            </div>
          ))
        )}
      </InfiniteScroll>
    </div>
  );
}

export default Files;
