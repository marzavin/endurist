import { ChangeEvent, useState } from "react";
import { toast } from "react-toastify";
import { useData } from "../services/DataProvider";

type UploadStatus = "idle" | "uploading" | "success" | "error";

function FileUploader() {
  const dataProvider = useData();
  const [file, setFile] = useState<File | null>(null);
  const [status, setStatus] = useState<UploadStatus>("idle");

  function handleFileChange(e: ChangeEvent<HTMLInputElement>) {
    if (e.target.files) {
      setFile(e.target.files[0]);
    }
  }

  async function handleFileUpload() {
    if (!file) {
      return;
    }

    setStatus("uploading");

    dataProvider.uploadFile(file).then((result) => {
      if (result.name) {
        setStatus("success");
        setFile(null);
        toast.success("File uploaded successfully!");
      } else {
        setStatus("error");
        toast.error("File upload failed! Please try again!");
      }
    });
  }

  return (
    <div className="app-file-uploader">
      <div className="input-group">
        <input
          type="file"
          accept=".tcx"
          className="form-control"
          onChange={handleFileChange}
        />
        <button
          className="btn btn-primary"
          type="button"
          onClick={handleFileUpload}
          disabled={file === null || status === "uploading"}
        >
          Upload
        </button>
      </div>
      {file && (
        <small>
          Type - {file.type}. Size - {(file.size / 1024).toFixed(2)} KB.
        </small>
      )}
    </div>
  );
}

export default FileUploader;
