import Activity from "./pages/Activity";
import Activities from "./pages/Activities";
import Home from "./pages/Home";
import Layout from "./pages/Layout";
import Notifications from "./pages/Notifications";
import Profiles from "./pages/Profiles";
import Profile from "./pages/Profile";
import Files from "./pages/Files";
import SignIn from "./pages/SignIn";
import "./App.less";

import { BrowserRouter, Routes, Route } from "react-router";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/sign-in" element={<SignIn />} />
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="activities" element={<Activities />} />
          <Route path="activities/:id" element={<Activity />} />
          <Route path="profiles" element={<Profiles />} />
          <Route path="profiles/:id" element={<Profile />} />
          <Route path="files" element={<Files />} />
          <Route path="notifications" element={<Notifications />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
