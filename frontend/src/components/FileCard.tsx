import dayjs from "dayjs";
import FileStatus from "../enums/FileStatus";
import FilePreviewModel from "../interfaces/files/FilePreviewModel";
import { useData } from "../services/DataProvider";

interface Props {
  file: FilePreviewModel;
}

function FileCard({ file }: Props) {
  const dataProvider = useData();

  function handleFileDownload() {
    dataProvider.downloadFile(file.id, `${file.name}.${file.extension}`);
  }

  return (
    <div className="app-card">
      <div className="app-card-row">
        <div>
          <strong>{file.name}</strong>
        </div>
        <div className="app-font-s">
          <span className="app-badge app-info-label">{file.extension}</span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Status:</strong>
        </div>
        <div>
          <span>{FileStatus[file.status]}</span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Size:</strong>
        </div>
        <div>
          <span>{(file.size / 1024).toFixed(2)} KB</span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Uploaded At:</strong>
        </div>
        <div>
          <span>
            {dayjs(file.uploadedAt).format("YYYY-MM-DD") +
              " " +
              dayjs(file.uploadedAt).format("HH:mm")}
          </span>
        </div>
      </div>
      <div className="app-card-row app-font-s">
        <div>
          <strong>Processed At:</strong>
        </div>
        <div>
          <span>
            {dayjs(file.processedAt).format("YYYY-MM-DD") +
              " " +
              dayjs(file.processedAt).format("HH:mm")}
          </span>
        </div>
      </div>
      <div className="app-card-row">
        <div>
          <a
            className="app-card-link"
            title="Download source file"
            onClick={handleFileDownload}
          >
            <i className="bi bi-download" />
          </a>
        </div>
      </div>
    </div>
  );
}

export default FileCard;
