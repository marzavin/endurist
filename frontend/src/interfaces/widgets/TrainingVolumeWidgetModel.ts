import KeyValueModel from "../KeyValueModel";

interface TrainingVolumeWidgetModel {
  id: string;
  title: string;
  volumes: KeyValueModel<string, number>[];
}

export default TrainingVolumeWidgetModel;
