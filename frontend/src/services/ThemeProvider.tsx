import { ThemeContext } from './ThemeContext';
import { createThemeProvider } from './ThemeProvider';

const ThemeProvider = ({ children }: { children: React.ReactNode }) => {
  const themeProvider = createThemeProvider();
  return <ThemeContext.Provider value={themeProvider}>{children}</ThemeContext.Provider>;
};

export default ThemeProvider;
