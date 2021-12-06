import React, { FC } from 'react';
import { BrowserRouter as Router, Switch } from "react-router-dom";

import { Layout, Menu } from 'antd';
// import Home from './Home/Home';
import StepsToDo from "../components/Steps/Steps";
import './styles/base.scss';

const { Header, Content } = Layout;

const App: FC = () => (
  <Router>
    <Switch>
      <Layout className="main-layout">
        <Header className="header-layout">
          <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['2']}>
            <Menu.Item key="1">nav 1</Menu.Item>
            <Menu.Item key="2">nav 2</Menu.Item>
            <Menu.Item key="3">nav 3</Menu.Item>
          </Menu>
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
