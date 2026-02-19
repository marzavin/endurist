import { createContext, useContext } from "react";
import useLocalStorage from "use-local-storage";
import ApplicationTheme from "../enums/ApplicationTheme";

export interface IThemeProvider {
  getTheme(): ApplicationTheme;
  switchTheme(): void;
  applyTheme(applicationTheme: ApplicationTheme): void;
}

const ThemeContext = createContext<IThemeProvider | undefined>(undefined);

export const useTheme = () => {
  var themeContext = useContext<IThemeProvider | undefined>(ThemeContext);

  if (themeContext === undefined) {
    throw new Error(
      "ThemeProviderContext was not provided. Make sure your component is a child of the ThemeProvider."
    );
  }

  return themeContext;
};

const ThemeProvider = ({ children }: { children: React.ReactNode }) => {
  const [theme, setTheme] = useLocalStorage<ApplicationTheme>(
    "theme",
    ApplicationTheme.Light
  );

  const themeProvider = {
    getTheme(): ApplicationTheme {
      return theme;
    },
    switchTheme(): void {
      if (theme === ApplicationTheme.Light) {
        this.applyTheme(ApplicationTheme.Dark);
        setTheme(ApplicationTheme.Dark);
      } else {
        this.applyTheme(ApplicationTheme.Light);
        setTheme(ApplicationTheme.Light);
      }
    },
    applyTheme(applicationTheme: ApplicationTheme): void {
      if (applicationTheme === ApplicationTheme.Light) {
        document.querySelector("body")?.removeAttribute("data-theme");
      } else {
        document.querySelector("body")?.setAttribute("data-theme", "dark");
      }
    }
  };

  return (
    <ThemeContext.Provider value={themeProvider}>
      {children}
    </ThemeContext.Provider>
  );
};

export default ThemeProvider;
