import L from "leaflet";
import icon from "leaflet/dist/images/marker-icon.png";
import iconShadow from "leaflet/dist/images/marker-shadow.png";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import MapRouteModel from "../interfaces/MapRouteModel";

function OpenStreetMap({ center }: MapRouteModel) {
  const DefaultIcon = L.icon({
    iconUrl: icon,
    shadowUrl: iconShadow
  });

  return (
    <div className="app-osm-map">
      <MapContainer
        center={[center.latitude, center.longitude]}
        zoom={13}
        scrollWheelZoom={false}
      >
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        <Marker
          position={[center.latitude, center.longitude]}
          icon={DefaultIcon}
        >
          <Popup>
            A pretty CSS3 popup. <br /> Easily customizable.
          </Popup>
        </Marker>
      </MapContainer>
    </div>
  );
}

export default OpenStreetMap;
