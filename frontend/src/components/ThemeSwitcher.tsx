import ApplicationTheme from '../enums/ApplicationTheme';
import { useTheme } from '../services/useTheme';

function ThemeSwitcher() {
  const { theme, toggleTheme } = useTheme();

  return (
    <a className="nav-link" onClick={toggleTheme}>
      {theme === ApplicationTheme.Light ? (
        <i className=".app-font-l bi bi-moon" />
      ) : (
        <i className=".app-font-l bi bi-sun" />
      )}
    </a>
  );
}

export default ThemeSwitcher;
