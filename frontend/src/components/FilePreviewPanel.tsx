import FileStatus from "../enums/FileStatus";
import FilePreviewModel from "../interfaces/FilePreviewModel";
import { useData } from "../services/DataProvider";

interface Props {
  file: FilePreviewModel;
}

function FilePreviewPanel({ file }: Props) {
  const dataProvider = useData();

  function handleFileDownload() {
    dataProvider.downloadFile(file.id, `${file.name}.${file.extension}`);
  }

  return (
    <div className="app-file-preview-panel col-12 col-sm-6 col-md-4 col-lg-3 col-xl-2">
      <div>
        <div className="app-panel-line row">
          <div className="col-10 d-flex justify-content-start">
            <strong>{file.name}</strong>
          </div>
          <div className="col-2 d-flex justify-content-end">
            <span className="app-badge badge app-info-label rounded-pill">
              {file.extension}
            </span>
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Status:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            {FileStatus[file.status]}
          </div>
        </div>
        <div className="app-panel-line app-font-s row">
          <div className="col-6 d-flex justify-content-start">
            <strong>Size:</strong>
          </div>
          <div className="col-6 d-flex justify-content-end">
            {(file.size / 1024).toFixed(2)} KB
          </div>
        </div>
        <div className="app-panel-line row">
          <div className="col-12 d-flex justify-content-end">
            <a
              className="nav-link app-download-link"
              title="Download source file"
              onClick={handleFileDownload}
            >
              <i className="bi bi-download" />
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}

export default FilePreviewPanel;
