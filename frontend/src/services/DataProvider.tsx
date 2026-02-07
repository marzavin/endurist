import { DataContext } from './DataContext';
import { createDataProvider } from './DataProvider';

const DataProvider = ({ children }: { children: React.ReactNode }) => {
  const dataProvider = createDataProvider();
  return <DataContext.Provider value={dataProvider}>{children}</DataContext.Provider>;
};

export default DataProvider;
