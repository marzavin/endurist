import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/js/bootstrap.js";
import "bootstrap-icons/font/bootstrap-icons.css";
import "leaflet/dist/leaflet.css";
import "leaflet/dist/leaflet.js";
import App from "./App.tsx";
import DataProvider from "./services/DataProvider.tsx";
import AuthProvider from "./services/AuthProvider.tsx";
import ThemeProvider from "./services/ThemeProvider.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <AuthProvider>
      <ThemeProvider>
        <DataProvider>
          <App />
        </DataProvider>
      </ThemeProvider>
    </AuthProvider>
  </StrictMode>
);
