import avatar from "../assets/avatar.png";
import logo from "../assets/logo.png";
import "./Header.less";
import { useAuth } from "../services/AuthProvider";

function Header() {
  const authProvider = useAuth();
  const profile = authProvider.getAccount();
  const profileUrl = `/profiles/${profile?.sub}`;

  return (
    <header className="app-header app-border-bottom navbar navbar-expand-md">
      <nav className="app-navbar container-xxl flex-wrap flex-md-wrap">
        <a className="app-brand navbar-brand" href="/">
          <img alt="Logo" src={logo} />
        </a>
        <button
          className="app-header-toggler navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarCollapsableContent"
          aria-controls="navbarCollapsableContent"
          aria-expanded="false"
        >
          <span className="app-icon app-main-color icon-menu" />
        </button>
        <div className="navbar-collapse collapse" id="navbarCollapsableContent">
          <hr className="d-md-none" />
          <ul className="navbar-nav flex-row flex-wrap">
            <li className="nav-item col-12 col-md-auto">
              <a className="app-header-page-link nav-link" href="/profiles">
                Profiles
              </a>
            </li>
            <li className="nav-item col-12 col-md-auto">
              <a className="app-header-page-link nav-link" href="/activities">
                Activities
              </a>
            </li>
            <li className="nav-item col-12 col-md-auto">
              <a className="app-header-page-link nav-link" href="/files">
                Files
              </a>
            </li>
          </ul>
          <hr className="d-md-none" />
          <ul className="navbar-nav flex-row flex-wrap ms-md-auto">
            <li className="nav-item col-12 col-md-auto">
              <a className="nav-link" href="/notifications">
                <span className="app-icon icon-bell" />
              </a>
            </li>
          </ul>
          <a
            className="app-profile d-inline-block my-2 my-md-0 ms-md-3"
            href={profileUrl}
          >
            <img alt="Avatar" src={avatar} />
          </a>
        </div>
      </nav>
    </header>
  );
}

export default Header;
