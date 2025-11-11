import KeyValueModel from "../interfaces/KeyValueModel";

interface Props {
  title: string;
  properties: KeyValueModel<string, string>[];
}

function Summary({ title, properties }: Props) {
  return (
    <div className="app-summary">
      <div className="app-summary-header row">
        <div className="col-12 d-flex justify-content-start">
          <strong className="app-font-xl">{title}</strong>
        </div>
      </div>
      <div className="row">
        {properties.length > 0 ? (
          properties.map((item) => (
            <div key={item.key} className="col-6">
              <div className="d-flex justify-content-start app-font-l">
                {item.key}:
              </div>
              <div className="d-flex justify-content-end app-font-l">
                {item.value}
              </div>
            </div>
          ))
        ) : (
          <div className="col-12 d-flex justify-content-center">
            No information to display.
          </div>
        )}
      </div>
    </div>
  );
}

export default Summary;
