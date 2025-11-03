import ActivityCategory from "../../enums/ActivityCategory";
import SegmentPreviewModel from "../SegmentPreviewModel";

interface ActivityPreviewModel extends SegmentPreviewModel {
  id: string;
  category: ActivityCategory;
  calories: number | null;
}

export default ActivityPreviewModel;
