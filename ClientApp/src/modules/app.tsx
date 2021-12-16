import React, { FC } from 'react';
import { BrowserRouter as Router, Switch } from "react-router-dom";

import Trips from './Trips';
import Trip from './Trip';

import './styles/base.scss';
import ProtectedRoute from '../components/ProtectedRoute';
import CustomLayout from '../components/Layout';
import { createBrowserHistory } from 'history'
import ReactGA from 'react-ga';

const App: FC = () => {
  ReactGA.initialize((String)(process.env.REACT_APP_GA_TRACKING_NO))
  const browserHistory = createBrowserHistory()
  browserHistory.listen((location, action) => {
    ReactGA.pageview(location.pathname + location.search)
  })

  return (
    <Router>
      <CustomLayout>
        <Switch>
          <ProtectedRoute exact path="/">
            <Trips />
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
