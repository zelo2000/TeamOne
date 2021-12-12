import React, { FC } from 'react';
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import { Layout, Dropdown, Avatar, Menu } from 'antd';

import Trip from './Trip';

import './styles/base.scss';
import logo from "../assets/logo.png";
import { UserOutlined } from '@ant-design/icons';

const { Header, Content } = Layout;
const isLogged = true;

const NavBar = (
  <Menu className="dropdown-menu-items">
    <Menu.Item key="1">
      <Link to="/">Logout</Link>
    </Menu.Item>
  </Menu>
);

const App: FC = () => (
  <Router>
    <Layout className="main-layout">
      <Header className="header-layout">
        <div className="header-elem">
          <Link to="/">
            <img
              className = "logo-img"
              src = {logo}
              alt = "Logo"
            />
          </Link>
        </div>          
        { isLogged ? 
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
            : 
          <div className="main-page-buttons">
            <Link className="login-button" to="/">
              Login
            </Link>
            <Link className="signup-button" to="/">
              Sign up
            </Link>
          </div>
        }
      </Header>
      <Content className="content-layout">
        <Switch>
          <Route exact path="/trip" component={Trip}/>
        </Switch>
      </Content>
    </Layout>
  </Router>
);

export default App;
