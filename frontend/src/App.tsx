import { BrowserRouter, Routes, Route } from 'react-router';

import Activities from './pages/Activities';
import Activity from './pages/Activity';
import Callback from './pages/Callback';
import Files from './pages/Files';
import Home from './pages/Home';
import Layout from './pages/Layout';
import Notifications from './pages/Notifications';
import Profile from './pages/Profile';
import Profiles from './pages/Profiles';

import './App.less';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          <Route path="callback" element={<Callback />} />
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
