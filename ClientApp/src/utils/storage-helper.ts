import { ILogInResponse } from "../models/ILogInResponse";


const WriteToken = (response: ILogInResponse) => {
  localStorage.setItem('auth', JSON.stringify({
    id: response.id,
    email: response.email,
    token: response.token,
  }))
};

const GetAuthData = (): (ILogInResponse | undefined) => {
  let authInfo = localStorage.getItem('auth');
  let result = authInfo ? JSON.parse(authInfo) as ILogInResponse : undefined;
  return result;
};

const CleanToken = () => {
  localStorage.removeItem('auth');
};

export { WriteToken, GetAuthData, CleanToken };