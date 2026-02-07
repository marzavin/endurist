import useLocalStorage from 'use-local-storage';

import IThemeProvider from './IThemeProvider';
import ApplicationTheme from '../enums/ApplicationTheme';

export const createThemeProvider = (): IThemeProvider => {
  const [theme, setTheme] = useLocalStorage<ApplicationTheme>('theme', ApplicationTheme.Light);

  return {
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
        document.querySelector('body')?.removeAttribute('data-theme');
      } else {
        document.querySelector('body')?.setAttribute('data-theme', 'dark');
      }
    }
  };
};
