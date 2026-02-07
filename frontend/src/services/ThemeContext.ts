import { createContext } from 'react';

import ApplicationTheme from '../enums/ApplicationTheme';

export interface ThemeContextType {
  theme: ApplicationTheme;
  toggleTheme: () => void;
}

export const ThemeContext = createContext<ThemeContextType | undefined>(undefined);
