import axios from 'axios';
import { toast } from 'react-toastify';
import useLocalStorage from 'use-local-storage';

import IDataProvider from './IDataProvider';
import config from '../application.json';
import ActivityModel from '../interfaces/activities/ActivityModel';
import ActivityPreviewModel from '../interfaces/activities/ActivityPreviewModel';
import FilePreviewModel from '../interfaces/files/FilePreviewModel';
import FileUploadModel from '../interfaces/files/FileUploadModel';
import ProfileModel from '../interfaces/profiles/ProfileModel';
import ProfilePreviewModel from '../interfaces/profiles/ProfilePreviewModel';
import ServerResponseModel from '../interfaces/ServerResponseModel';
import SortingModel from '../interfaces/SortingModel';
import WidgetModel from '../interfaces/widgets/WidgetModel';

export const createDataProvider = (): IDataProvider => {
  const serverBaseUrl = config.apiUrl;
  const serverErrorText = 'An error occurred during execution your request.';
  const [accessToken] = useLocalStorage<string | null>('access_token', null);

  const axiosInstance = axios.create();
  axiosInstance.interceptors.request.use((config) => {
    if (accessToken) {
      config.headers['Authorization'] = 'Bearer ' + accessToken;
    }
    return config;
  });

  return {
    async getActivities(
      skip: number,
      take: number,
      sorting: SortingModel
    ): Promise<ActivityPreviewModel[]> {
      return axiosInstance
        .get<
          ServerResponseModel<ActivityPreviewModel[]>
        >(sorting ? `${serverBaseUrl}/api/activities?paging.skip=${skip}&paging.take=${take}&sorting.key=${sorting.key}&sorting.descending=${sorting.descending}` : `${serverBaseUrl}/api/activities?paging.skip=${skip}&paging.take=${take}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return [];
          }
        );
    },
    async getActivity(activityId: string): Promise<ActivityModel> {
      return axiosInstance
        .get<ServerResponseModel<ActivityModel>>(`${serverBaseUrl}/api/activities/${activityId}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return {
              startTime: '',
              distance: 0,
              duration: 0,
              pace: 0,
              averageHeartRate: null,
              averageCadence: null,
              id: activityId,
              category: 1,
              calories: null,
              segments: [],
              heartRateGraph: [],
              paceGraph: [],
              altitudeGraph: [],
              cadenceGraph: []
            };
          }
        );
    },
    async getProfiles(
      skip: number,
      take: number,
      sorting: SortingModel
    ): Promise<ProfilePreviewModel[]> {
      return axiosInstance
        .get<
          ServerResponseModel<ProfilePreviewModel[]>
        >(sorting ? `${serverBaseUrl}/api/profiles?paging.skip=${skip}&paging.take=${take}&sorting.key=${sorting.key}&sorting.descending=${sorting.descending}` : `${serverBaseUrl}/api/profiles?paging.skip=${skip}&paging.take=${take}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return [];
          }
        );
    },
    async getProfile(profileId: string): Promise<ProfileModel> {
      return axiosInstance
        .get<ServerResponseModel<ProfileModel>>(`${serverBaseUrl}/api/profiles/${profileId}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return {
              id: profileId,
              name: '',
              distance: 0,
              duration: 0,
              weeklyVolumes: []
            };
          }
        );
    },
    async getFiles(skip: number, take: number, sorting: SortingModel): Promise<FilePreviewModel[]> {
      return axiosInstance
        .get<
          ServerResponseModel<FilePreviewModel[]>
        >(sorting ? `${serverBaseUrl}/api/files?paging.skip=${skip}&paging.take=${take}&sorting.key=${sorting.key}&sorting.descending=${sorting.descending}` : `${serverBaseUrl}/api/files?paging.skip=${skip}&paging.take=${take}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return [];
          }
        );
    },
    async uploadFile(file: File): Promise<FileUploadModel> {
      const formData = new FormData();
      formData.append('file', file);

      return axiosInstance
        .post<ServerResponseModel<FileUploadModel>>(`${serverBaseUrl}/api/files/upload`, formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return { name: '', size: 0, fileStatus: 2 };
          }
        );
    },
    async downloadFile(fileId: string, fileName: string): Promise<void> {
      return axiosInstance
        .get(`${serverBaseUrl}/api/files/${fileId}/download`, {
          responseType: 'blob'
        })
        .then(
          function (response) {
            const url = URL.createObjectURL(response.data);
            const a = document.createElement('a');
            a.href = url;
            a.download = fileName;
            a.style.display = 'none';
            document.body.appendChild(a);
            a.click();
            a.remove();
            URL.revokeObjectURL(url);
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
          }
        );
    },
    async getProfileWidget(profileId: string, widgetId: string): Promise<WidgetModel> {
      return axiosInstance
        .get<
          ServerResponseModel<WidgetModel>
        >(`${serverBaseUrl}/api/profiles/${profileId}/widgets/${widgetId}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return {
              id: '',
              name: '',
              data: '{}'
            };
          }
        );
    },
    async getActivityWidget(activityId: string, widgetId: string): Promise<WidgetModel> {
      return axiosInstance
        .get<
          ServerResponseModel<WidgetModel>
        >(`${serverBaseUrl}/api/activities/${activityId}/widgets/${widgetId}`)
        .then(
          function (response) {
            return response.data.data;
          },
          function (error) {
            console.log(error);
            toast.error(serverErrorText);
            return {
              id: '',
              name: '',
              data: '{}'
            };
          }
        );
    }
  };
};
