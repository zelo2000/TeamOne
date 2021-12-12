import axios, { AxiosRequestConfig, AxiosError, AxiosResponse } from 'axios';
import { WriteToken, GetAuthData, CleanToken } from './storage-helper';

const instance = axios.create({
  baseURL: `${process.env.REACT_APP_API_URL}/api/`,
});

instance.interceptors.request.use((config: AxiosRequestConfig<any>) => {
  const newConfig = config;
  const authData = GetAuthData();
  if (typeof window !== 'undefined') {
    newConfig.headers = {
      'Content-Type': (config && config.headers && config.headers['content-type'])
        ? config.headers['content-type']
        : 'application/json',
      Authorization: `Bearer ${authData?.token}`,
    };
  }
  return newConfig;
});

const createAxiosResponseInterceptor = () => {
  const interceptor = instance.interceptors.response.use(
    (response) => response,
    (error: AxiosError<any>) => {

      // Eject the interceptor so it doesn't loop in case token refresh causes the 401 response
      if (error.response?.status === 401 && error.config?.url && error.config.url.includes('/api/user/refresh-token')) {
        window.location.pathname = '/login';
        CleanToken();
        return Promise.reject(error.response || error);
      }

      // When response status is 401, try to refresh the token.
      if (error.response?.status === 401) {
        instance.interceptors.response.eject(interceptor);
        const authData = GetAuthData();
        const refreshToken = authData?.refreshToken;
        const options: AxiosRequestConfig = {
          method: 'POST',
          headers: { 'content-type': 'application/json' },
          data: {
            refreshToken: refreshToken
          },
          url: '/api/user/refresh-token',
        };

        return axios(options).then((response: AxiosResponse<any>) => {
          response.data.rememberMe = !!authData?.rememberMe;
          WriteToken(response.data);
          instance.defaults.headers.common.Authorization = `Bearer ${response.data.token}`;
          if (error.response?.config && error.response.config.headers) {
            error.response.config.headers.Authorization = `Bearer ${response.data.token}`;
            const inst = axios(error.response?.config);
            return inst.then((response) => {
              return Promise.resolve(response);
            })
              .catch((error) => {
                return Promise.reject(error.response || error);
              });
          }
        }).finally(createAxiosResponseInterceptor);
      }

      // Reject promise if usual error
      return Promise.reject(error.response || error);
    },
  );
};

createAxiosResponseInterceptor();

export default instance;