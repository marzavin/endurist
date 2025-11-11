import ActivityPreviewModel from "./ActivityPreviewModel";
import KeyValueModel from "../KeyValueModel";
import SegmentPreviewModel from "../SegmentPreviewModel";

interface ActivityModel extends ActivityPreviewModel {
  segments: SegmentPreviewModel[];
  heartRateGraph: KeyValueModel<number, number | null>[];
  paceGraph: KeyValueModel<number, number>[];
  altitudeGraph: KeyValueModel<number, number | null>[];
  cadenceGraph: KeyValueModel<number, number | null>[];
}

export default ActivityModel;
