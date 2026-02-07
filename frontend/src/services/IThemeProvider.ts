import ApplicationTheme from '../enums/ApplicationTheme';

interface IThemeProvider {
  getTheme(): ApplicationTheme;
  switchTheme(): void;
  applyTheme(applicationTheme: ApplicationTheme): void;
}

export default IThemeProvider;
