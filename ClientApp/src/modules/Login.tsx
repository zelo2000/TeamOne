import React, { FC } from "react";
import GoogleLogin, { GoogleLoginResponse, GoogleLoginResponseOffline } from "react-google-login";
import { useHistory } from "react-router";
import { AuthRequest } from "../models/AuthRequest";
import { externalLogIn } from "../services/AuthService";
import { CleanToken, WriteToken } from "../utils/storage-helper";

const Login: FC = () => {
  const history = useHistory();

  const responseGoogle = (response: GoogleLoginResponse | GoogleLoginResponseOffline) => {
    response = response as GoogleLoginResponse;

    const request = {
      idToken: response.getAuthResponse().id_token
    } as AuthRequest;

    externalLogIn(request)
      .then((response) => {
        WriteToken(response);
        history.push('/')
      })
      .catch(() => CleanToken());
  }

  return (
    <div>
      <GoogleLogin
        clientId={process.env.REACT_APP_GOOGLE_AUTH_CLIENT_ID || ""}
        onSuccess={responseGoogle}
        onFailure={responseGoogle}
        cookiePolicy={'single_host_origin'}
      />
    </div>
  );
}

export default Login;