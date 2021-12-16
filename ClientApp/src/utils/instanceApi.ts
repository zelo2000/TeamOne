import axios, { AxiosRequestConfig, AxiosError } from 'axios';
import { GetAuthData, CleanToken } from './storage-helper';

const instance = axios.create({
  baseURL: `${process.env.REACT_APP_API_URL}/api/`,
});

instance.interceptors.request.use((config: AxiosRequestConfig<any>) => {
  const newConfig = config;
  const authData = GetAuthData();
  if (typeof window !== 'undefined') {
    newConfig.headers = {
      'Content-Type': (config && config.headers && config.headers['content-type']) ? config.headers['content-type'] : 'application/json',
      Authorization: `Bearer ${authData?.token}`,
    };
  }
  return newConfig;
});

instance.interceptors.response.use(
  (response) => response,
  (error: AxiosError<any>) => {
    // In case the 401 response
    if (error.response?.status === 401) {
      window.location.pathname = '/login';
      CleanToken();
      return Promise.reject(error.response || error);
    }

    // Reject promise if usual error
    return Promise.reject(error.response || error);
  },
);

export default instance;