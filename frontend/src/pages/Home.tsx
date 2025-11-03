import OpenStreetMap from "../components/OpenStreetMap";

function Home() {
  return (
    <>
      <h1>This is Home page!</h1>
      <OpenStreetMap
        center={{ latitude: 51.1093, longitude: 17.0386 }}
        route={[]}
      />
    </>
  );
}

export default Home;
