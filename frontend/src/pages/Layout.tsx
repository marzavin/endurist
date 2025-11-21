import { Navigate, Outlet } from "react-router";
import Header from "../components/Header";
import { ToastContainer } from "react-toastify";
import { useAuth } from "../services/AuthProvider";
import { useTheme } from "../services/ThemeProvider";

function Layout() {
  const authProvider = useAuth();

  const themeProvider = useTheme();
  themeProvider.applyTheme();

  if (authProvider.getAccount()) {
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

  return <Navigate to="/sign-in" />;
}

export default Layout;
