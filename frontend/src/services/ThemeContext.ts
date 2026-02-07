import { createContext } from 'react';

import IThemeProvider from './IThemeProvider';

export const ThemeContext = createContext<IThemeProvider | undefined>(undefined);
