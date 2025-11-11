import KeyValueModel from "../KeyValueModel";

interface TrainingVolumeWidgetModel {
  id: string;
  title: string;
  data: {
    weekly: KeyValueModel<string, number>[];
    monthly: KeyValueModel<string, number>[];
  };
}

export default TrainingVolumeWidgetModel;
