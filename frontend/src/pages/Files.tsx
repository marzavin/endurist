import FilePreviewModel from "../interfaces/FilePreviewModel";
import FilePreviewPanel from "../components/FilePreviewPanel";
import FileUploader from "../components/FileUploader";
import { useEffect, useState, CSSProperties } from "react";
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
  const pageSize: number = 48;
  const sorting: SortingModel = { key: "uploadedAt", descending: true };

  useEffect(() => {
    setLoading(true);
    dataProvider.getFiles(pageSize, sorting).then((result) => {
      setLoading(false);
      setItems(result);
    });
  }, []);

  return (
    <div className="app-file-list">
      <div className="row align-items-end justify-content-between">
        <div className="col-12">
          <h3>Files</h3>
        </div>
        <div className="col-12">
          <FileUploader />
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
