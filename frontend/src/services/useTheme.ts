import { useContext } from 'react';

import { ThemeContext } from './ThemeContext';

export const useTheme = () => {
  const context = useContext(ThemeContext);

  if (!context) {
    throw new Error(
      'ThemeContext was not provided. Make sure your component is a child of the ThemeProvider.'
    );
  }

  return context;
};
