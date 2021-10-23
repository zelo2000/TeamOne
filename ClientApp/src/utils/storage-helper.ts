import { IWriteTokenModel } from '../types/auth/IWriteTokenModel';

const WriteToken = (response: IWriteTokenModel) => {
  if (response.rememberMe) {
    localStorage.setItem('auth', JSON.stringify({
      id: response.id,
      token: response.token,
      refreshToken: response.refreshToken,
    }))
  }
  else {
    sessionStorage.setItem('auth', JSON.stringify({
      id: response.id,
      token: response.token,
      refreshToken: response.refreshToken,
    }))
  }
};

const GetAuthData = (): (IWriteTokenModel | undefined) => {
  let authInfo = localStorage.getItem('auth');

  if (!authInfo) {
    authInfo = sessionStorage.getItem('auth');
  }

  let result = authInfo ? JSON.parse(authInfo) as IWriteTokenModel : undefined;

  if (result) {
    result.rememberMe = !!localStorage.getItem('auth');
  }

  return result;
};

const CleanToken = () => {
  sessionStorage.removeItem('auth');
  localStorage.removeItem('auth');
};

export { WriteToken, GetAuthData, CleanToken };