import React, { FC, useEffect, useState } from 'react';
import { BrowserRouter as Router, Link, useHistory } from "react-router-dom";
import { Layout, Dropdown, Avatar, Menu } from 'antd';
import { UserOutlined } from '@ant-design/icons';
import GoogleLogin, { GoogleLoginResponse, GoogleLoginResponseOffline, GoogleLogout } from 'react-google-login';

import logo from "../assets/logo.png";
import { CleanToken, GetAuthData, WriteToken } from '../utils/storage-helper';
import { ILogInResponse } from '../models/ILogInResponse';
import { AuthRequest } from '../models/AuthRequest';
import { externalLogIn } from '../services/AuthService';

const { Header, Content } = Layout;

const CustomLayout: FC = ({ children }) => {
  const history = useHistory();

  const [authData, setAuthData] = useState<ILogInResponse>();
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>();

  useEffect(() => {
    const authData = GetAuthData();
    setAuthData(authData);
    if (authData) {
      setIsLoggedIn(true);
    }
  }, []);

  const responseGoogle = (response: GoogleLoginResponse | GoogleLoginResponseOffline) => {
    response = response as GoogleLoginResponse;

    const request = {
      idToken: response.getAuthResponse().id_token
    } as AuthRequest;

    externalLogIn(request)
      .then((response) => {
        setIsLoggedIn(true);
        WriteToken(response);
        history.push('/')
      })
      .catch(() => CleanToken());
  }

  const logOut = () => {
    CleanToken();
    window.location.pathname = "/login";
  };

  const NavBar = () => (
    <Menu className="dropdown-menu-items">
      <Menu.Item key="1">{authData?.email}</Menu.Item>
      <GoogleLogout
        clientId={process.env.REACT_APP_GOOGLE_AUTH_CLIENT_ID || ""}
        buttonText="Logout"
        onLogoutSuccess={logOut}
        render={renderProps => (
          <Menu.Item key="2" onClick={renderProps.onClick} disabled={renderProps.disabled}>Logout</Menu.Item>
        )}
      >
      </GoogleLogout>
    </Menu>
  );

  return (
    <Router>
      <Layout className="main-layout">
        <Header className="header-layout">
          <div className="header-elem">
            <Link to="/">
              <img
                className="logo-img"
                src={logo}
                alt="Logo"
              />
            </Link>
          </div>
          {isLoggedIn ?
            (
              <div className="header-elem">
                <Dropdown
                  overlay={NavBar}
                  placement="bottomCenter"
                  trigger={["click"]}
                >
                  <Avatar
                    icon={<UserOutlined />}
                    size="large"
                    className="user-icon"
                  />
                </Dropdown>
              </div>
            ) : (
              <div className="header-elem">
                <GoogleLogin
                  clientId={process.env.REACT_APP_GOOGLE_AUTH_CLIENT_ID || ""}
                  onSuccess={responseGoogle}
                  onFailure={responseGoogle}
                  cookiePolicy={'single_host_origin'}
                  render={renderProps => (
                    <div className="main-page-buttons" onClick={renderProps.onClick}>
                      Login
                    </div>
                  )}
                />
              </div>)
          }
        </Header>
        <Content className="content-layout">
          {children}
        </Content>
      </Layout>
    </Router>
  )
};

export default CustomLayout;
