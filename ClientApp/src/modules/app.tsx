import React, { FC, useEffect, useState } from 'react';
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import { Layout, Dropdown, Avatar, Menu } from 'antd';
import { UserOutlined } from '@ant-design/icons';

import Home from './Home';
import Trip from './Trip';
import Login from './Login';

import './styles/base.scss';
import logo from "../assets/logo.png";
import ProtectedRoute from '../components/ProtectedRoute';
import { GetAuthData } from '../utils/storage-helper';
import { createBrowserHistory } from 'history'
import ReactGA from 'react-ga';

const { Header, Content } = Layout;

ReactGA.initialize((String)(process.env.REACT_APP_GA_TRACKING_NO))
const browserHistory = createBrowserHistory()
browserHistory.listen((location, action) => {
  ReactGA.pageview(location.pathname + location.search)
})

const NavBar = () => (
  <Menu className="dropdown-menu-items">
    <Menu.Item key="1">
      <Link to="/">Logout</Link>
    </Menu.Item>
  </Menu>
);

const App: FC = () => {
  const [isLogged, setIsLogged] = useState<boolean>();

  useEffect(() => {
    const authData = GetAuthData();
    if (authData) {
      setIsLogged(true);
    }
    ReactGA.pageview(window.location.pathname + window.location.search)
  }, []);

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
          {isLogged ?
            (
              <div className="header-elem">
                <Dropdown
                  overlay={NavBar}
                  placement="bottomCenter"
                  trigger={["click"]}
                >
                  <a href="# ">
                    <Avatar
                      icon={<UserOutlined />}
                      size="large"
                      className="user-icon"
                    />
                  </a>
                </Dropdown>
              </div>
            ) :
            (
              <div className="main-page-buttons">
                <Link className="login-button" to="/login">
                  Login
                </Link>
              </div>
            )
          }
        </Header>
        <Content className="content-layout">
          <Switch>
            <ProtectedRoute exact path="/">
              <Home />
            </ProtectedRoute>

            <Route path="/login" component={Login} />
            <ProtectedRoute exact path="/trip/:tripId">
              <Trip />
            </ProtectedRoute>
          </Switch>
        </Content>
      </Layout>
    </Router>
  )
};

export default App;
