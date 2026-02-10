import ApplicationTheme from '../enums/ApplicationTheme';
import { useTheme } from '../services/useTheme';

function ThemeSwitcher() {
  const themeProvider = useTheme();

  return (
    <a className="nav-link" onClick={themeProvider.toggleTheme}>
      {themeProvider.theme === ApplicationTheme.Light ? (
        <i className="app-font-l bi bi-moon" />
      ) : (
        <i className="app-font-l bi bi-sun" />
      )}
    </a>
  );
}

export default ThemeSwitcher;
