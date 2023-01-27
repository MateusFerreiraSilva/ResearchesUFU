import React from 'react';
import { Switch } from 'react-router-dom';

import Route from './Route';
import SignIn from '~/pages/SignIn';
import Home from '~/pages/Home';

const Routes = () => {
  return (
    <Switch>
      <Route exact path="/" type="public" component={Home} />
      <Route path="/login" type="auth" component={SignIn} />
    </Switch>
  );
};

export default Routes;
