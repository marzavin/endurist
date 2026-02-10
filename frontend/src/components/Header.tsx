import { useAuth } from 'react-oidc-context';

import ThemeSwitcher from './ThemeSwitcher';
import avatar from '../assets/avatar.png';
import logo from '../assets/logo.png';
import './Header.less';

function Header() {
  const authProvider = useAuth();

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
          <i className="bi bi-list" />
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
              <ThemeSwitcher />
            </li>
            <li className="nav-item col-12 col-md-auto">
              <a className="nav-link" href="/notifications">
                <i className="app-font-l bi bi-bell" />
              </a>
            </li>
            {!authProvider.isAuthenticated && (
              <li className="nav-item col-12 col-md-auto">
                <a className="nav-link" onClick={() => authProvider.signinRedirect()}>
                  <i className="app-font-l bi bi-door-open" />
                </a>
              </li>
            )}
          </ul>
          {authProvider.isAuthenticated && (
            <a className="app-profile d-inline-block my-2 my-md-0 ms-md-3" href="/">
              <img alt="Avatar" src={avatar} />
            </a>
          )}
        </div>
      </nav>
    </header>
  );
}

export default Header;
