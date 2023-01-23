import React from 'react';
import { Switch } from 'react-router-dom';

import Route from './Route';
import SignIn from '~/pages/SignIn';
import SignUp from '~/pages/SignUp';
import Research from '~/pages/Research';
import Home from '~/pages/Home';
import AuthorBio from '~/pages/AuthorBio';

const Routes = () => {
  return (
    <Switch>
      <Route exact path="/" type="public" component={Home} />
      <Route path="/register" type="auth" component={SignUp} />
      <Route path="/login" type="auth" component={SignIn} />
      <Route path="/research" type="public" component={Research} />
      <Route path="/author-bio" type="public" component={AuthorBio} />
    </Switch>
  );
};

export default Routes;
