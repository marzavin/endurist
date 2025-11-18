interface FilePreviewModel {
  id: string;
  name: string;
  extension: string;
  size: number;
  status: number;
  uploadedAt: string;
  processedAt: string;
}

export default FilePreviewModel;
