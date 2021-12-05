import React, { FC } from 'react';
import { BrowserRouter as Router, Switch, Route, } from "react-router-dom";

import TestModule from './test-module/test-module';

import './styles/base.scss';

const App: FC = () => (
  <Router>
    <Switch>
      <Route exact path="/" component={TestModule} />
    </Switch>
  </Router>
);

export default App;