import { createContext } from 'react';

import ApplicationTheme from '../enums/ApplicationTheme';

export interface IThemeProvider {
  theme: ApplicationTheme;
  toggleTheme: () => void;
}

export const ThemeContext = createContext<IThemeProvider | undefined>(undefined);
