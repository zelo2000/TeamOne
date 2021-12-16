import React, { FC } from 'react';
import { BrowserRouter as Router, Switch } from "react-router-dom";

import Home from './Home';
import Trip from './Trip';

import './styles/base.scss';
import ProtectedRoute from '../components/ProtectedRoute';
import CustomLayout from '../components/Layout';

const App: FC = () => {
  return (
    <Router>
      <CustomLayout>
        <Switch>
          <ProtectedRoute exact path="/">
            <Home />
          </ProtectedRoute>
          <ProtectedRoute exact path="/trip/:tripId">
            <Trip />
          </ProtectedRoute>
        </Switch>
      </CustomLayout>
    </Router>
  )
};

export default App;
