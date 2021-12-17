import { AuthRequest } from '../models/AuthRequest';
import { ILogInResponse } from '../models/ILogInResponse';
import instanceApi from '../utils/instanceApi';

export const externalLogIn = async (authRequest: AuthRequest): Promise<ILogInResponse> => {
  const responce = await instanceApi.post<ILogInResponse>(`/auth/externallogin`, authRequest);
  return responce.data;
}
