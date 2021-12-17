import React, { FC } from 'react';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import Trips from './Trips';
import Trip from './Trip';

import './styles/base.scss';
import ProtectedRoute from '../components/ProtectedRoute';
import CustomLayout from '../components/Layout';
import Home from './Home';

const App: FC = () => {
  return (
    <Router>
      <CustomLayout>
        <Switch>
          <Route exact path="/" component={Home} />
          <ProtectedRoute path="/trips">
            <Trips />
          </ProtectedRoute>
          <ProtectedRoute path="/trip/:tripId">
            <Trip />
          </ProtectedRoute>
        </Switch>
      </CustomLayout>
    </Router>
  )
};

export default App;
