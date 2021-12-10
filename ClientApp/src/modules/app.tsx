import React, { FC } from 'react';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import { Layout, Menu } from 'antd';
import Home from './Home';
import StepsToDo from "../components/Steps";
import './styles/base.scss';

const { Header, Content } = Layout;

const App: FC = () => (
  <Layout className="main-layout">
    <Header className="header-layout">
      <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['2']}>
        <Menu.Item key="1">nav 1</Menu.Item>
        <Menu.Item key="2">nav 2</Menu.Item>
        <Menu.Item key="3">nav 3</Menu.Item>
      </Menu>
    </Header>
    <Content className="site-layout">
      <Router>
        <Switch>
          <Route exact path="/" component={Home}/>
          <Route exact path="/trip" component={StepsToDo}/>
        </Switch>
      </Router>
    </Content>
  </Layout>
);

export default App;
