import { createContext } from 'react';

import ActivityModel from '../interfaces/activities/ActivityModel';
import ActivityPreviewModel from '../interfaces/activities/ActivityPreviewModel';
import FilePreviewModel from '../interfaces/files/FilePreviewModel';
import FileUploadModel from '../interfaces/files/FileUploadModel';
import ProfileModel from '../interfaces/profiles/ProfileModel';
import ProfilePreviewModel from '../interfaces/profiles/ProfilePreviewModel';
import SortingModel from '../interfaces/SortingModel';
import WidgetModel from '../interfaces/widgets/WidgetModel';

export interface IDataProvider {
  getActivities(skip: number, take: number, sorting: SortingModel): Promise<ActivityPreviewModel[]>;
  getActivity(activityId: string): Promise<ActivityModel>;
  getProfiles(skip: number, take: number, sorting: SortingModel): Promise<ProfilePreviewModel[]>;
  getProfile(profileId: string): Promise<ProfileModel>;
  getFiles(skip: number, take: number, sorting: SortingModel): Promise<FilePreviewModel[]>;
  uploadFile(file: File): Promise<FileUploadModel>;
  downloadFile(fileId: string, fileName: string): Promise<void>;
  getProfileWidget(profileId: string, widgetId: string): Promise<WidgetModel>;
  getActivityWidget(activityId: string, widgetId: string): Promise<WidgetModel>;
}

export const DataContext = createContext<IDataProvider | undefined>(undefined);
