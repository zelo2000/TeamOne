import React, { FC } from 'react';
import { BrowserRouter as Router, Switch, Link } from "react-router-dom";

import { Layout, Dropdown, Avatar, Menu } from 'antd';
// import Home from './Home/Home';
import StepsToDo from "../components/Steps/Steps";
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
    <Switch>
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
        <Content className="site-layout">
          {/* <Route exact path="/" component={Home} /> */}
          <StepsToDo/>
        </Content>
      </Layout>
    </Switch>
  </Router>
);

export default App;
