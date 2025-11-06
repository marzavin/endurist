import { CSSProperties, useEffect, useState } from "react";
import { useData } from "../../services/DataProvider";
import {
  Bar,
  BarChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis
} from "recharts";
import TrainingVolumeWidgetModel from "../../interfaces/widgets/TrainingVolumeWidgetModel";
import { PropagateLoader } from "react-spinners";
import { toKilometersLabel } from "../../formatters/DistanceFormatter";
import dayjs from "dayjs";

const override: CSSProperties = {
  display: "block",
  paddingTop: "3rem",
  paddingBottom: "3rem",
  textAlign: "center"
};

interface Props {
  profileId: string;
}

function TrainingVolumeWidget({ profileId }: Props) {
  const widgetId = "68ade97c10ae0c20fb44152e";
  const dataProvider = useData();
  const [widgetData, setWidgetData] =
    useState<TrainingVolumeWidgetModel | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    if (profileId !== undefined && profileId !== "") {
      setLoading(true);
      dataProvider.getProfileWidget(profileId, widgetId).then((result) => {
        setLoading(false);
        const parsedData = JSON.parse(result.data);
        const widgetModel: TrainingVolumeWidgetModel = {
          id: widgetId,
          title: result.name,
          data: parsedData
        };
        setWidgetData(widgetModel);
      });
    }
  }, [profileId]);

  const tooltipValueFormatter = (value: number) => {
    return [toKilometersLabel(value), null];
  };

  const tooltipLabelFormatter = (value: string) => {
    const startDate = new Date(value);
    const formattedStartDate = dayjs(startDate).format("YYYY-MM-DD");
    const endDate = dayjs(startDate).add(6, "day");
    const formattedEndDate = dayjs(endDate).format("YYYY-MM-DD");
    return `from ${formattedStartDate} to ${formattedEndDate}`;
  };

  const tickFormatterY = (value: any) => {
    return (value / 1000).toString();
  };

  return (
    <div className="app-widget">
      <div className="app-widget-header row">
        <div className="col-12 d-flex justify-content-start">
          <strong>Training Volume</strong>
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
      {widgetData && widgetData.data.monthly.length > 0 ? (
        <ResponsiveContainer width="100%" height={300}>
          <BarChart data={widgetData.data.monthly}>
            <XAxis dataKey="key" />
            <YAxis tickFormatter={tickFormatterY} />
            <Tooltip
              formatter={tooltipValueFormatter}
              labelFormatter={tooltipLabelFormatter}
            />
            <Bar dataKey="value" fill="#8884d8" />
          </BarChart>
        </ResponsiveContainer>
      ) : null}
    </div>
  );
}

export default TrainingVolumeWidget;
