import { createContext } from 'react';

import IDataProvider from './IDataProvider';

export const DataContext = createContext<IDataProvider | undefined>(undefined);
