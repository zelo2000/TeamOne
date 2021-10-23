import { ILogInResponse } from './ILogInResponse';

export interface IWriteTokenModel extends ILogInResponse {
  rememberMe: boolean;
}