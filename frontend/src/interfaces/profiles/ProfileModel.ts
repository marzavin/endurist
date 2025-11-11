import KeyValueModel from "../KeyValueModel";
import ProfilePreviewModel from "./ProfilePreviewModel";

interface ProfileModel extends ProfilePreviewModel {
  weeklyVolumes: KeyValueModel<string, number>[];
}

export default ProfileModel;
