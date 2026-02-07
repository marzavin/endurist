import { Line, LineChart, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';

import { toTimeSpanLabel } from '../formatters/DurationFormatter';
import KeyValueModel from '../interfaces/KeyValueModel';

interface Props {
  title: string;
  graph: KeyValueModel<number, number | null>[];
  unitOfMeasure: string;
}

function ActivityGraph({ title, graph, unitOfMeasure }: Props) {
  return (
    <div className="app-widget">
      <div className="app-widget-header row">
        <div className="col-12 d-flex justify-content-start">
          <strong>{title}</strong>
        </div>
      </div>
      {graph && graph.length > 0 ? (
        <ResponsiveContainer width="100%" height={300}>
          <LineChart data={graph}>
            <XAxis
              dataKey="key"
              tickFormatter={(value) => {
                return toTimeSpanLabel(Number(value));
              }}
            />
            <YAxis />
            <Tooltip
              formatter={(value) => {
                return [unitOfMeasure ? value + unitOfMeasure : value, null];
              }}
              labelFormatter={(value) => {
                return toTimeSpanLabel(Number(value));
              }}
            />
            <Line dataKey="value" fill="#8884d8" dot={false} />
          </LineChart>
        </ResponsiveContainer>
      ) : (
        <div>
          <div className="col-12 d-flex justify-content-center">No information to display.</div>
        </div>
      )}
    </div>
  );
}

export default ActivityGraph;
