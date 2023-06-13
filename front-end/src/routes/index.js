import React from 'react';
import { Switch } from 'react-router-dom';

import Route from './Route';
import SignIn from '~/pages/SignIn';
import Home from '~/pages/Home';
import ResearchPage from 'pages/ResearchPage';

const Routes = () => {
  return (
    <Switch>
      <Route exact path="/" type="public" component={Home} />
      <Route path="/login" type="auth" component={SignIn} />
      <Route path="/researches/:id" type="public" component={ResearchPage} />
    </Switch>
  );
};

export default Routes;
