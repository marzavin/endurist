import FilePreviewModel from "../interfaces/FilePreviewModel";
import FilePreviewPanel from "../components/FilePreviewPanel";
import FileUploader from "../components/FileUploader";
import { useEffect, useState, CSSProperties, ChangeEvent } from "react";
import { useData } from "../services/DataProvider";
import SortingModel from "../interfaces/SortingModel";
import { PropagateLoader } from "react-spinners";
import "./Files.less";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

function Files() {
  const dataProvider = useData();
  const [items, setItems] = useState<FilePreviewModel[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [sorting, setSorting] = useState<SortingModel>({
    key: "uploadedAt",
    descending: true
  });
  const pageSize: number = 48;

  useEffect(() => {
    setLoading(true);
    dataProvider.getFiles(pageSize, sorting).then((result) => {
      setLoading(false);
      setItems(result);
    });
  }, [sorting]);

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
          <FilePreviewPanel key={item.id} file={item} />
        ))}
      </div>
    </div>
  );
}

export default Files;
