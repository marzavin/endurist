import React, { useEffect } from 'react';
import useLocalStorage from 'use-local-storage';

import { ThemeContext } from './ThemeContext';
import ApplicationTheme from '../enums/ApplicationTheme';

const ThemeProvider = ({ children }: { children: React.ReactNode }) => {
  const [theme, setTheme] = useLocalStorage<ApplicationTheme>('app_theme', ApplicationTheme.Light);

  useEffect(() => {
    if (theme === ApplicationTheme.Light) {
      document.querySelector('body')?.removeAttribute('data-theme');
    } else {
      document.querySelector('body')?.setAttribute('data-theme', 'dark');
    }
  }, [theme]);

  const toggleTheme = () => {
    setTheme(theme === ApplicationTheme.Light ? ApplicationTheme.Dark : ApplicationTheme.Light);
  };

  return <ThemeContext.Provider value={{ theme, toggleTheme }}>{children}</ThemeContext.Provider>;
};

export default ThemeProvider;
