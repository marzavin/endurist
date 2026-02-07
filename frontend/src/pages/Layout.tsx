import { Outlet } from 'react-router';
import { ToastContainer } from 'react-toastify';

import Header from '../components/Header';

function Layout() {
  return (
    <>
      <Header />
      <div className="px-0 container-fluid">
        <main className="app-content">
          <Outlet />
        </main>
        <ToastContainer position="bottom-right" pauseOnHover />
      </div>
    </>
  );
}

export default Layout;
