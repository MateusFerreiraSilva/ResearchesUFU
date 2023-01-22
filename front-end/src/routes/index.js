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
      <Route exact path="/" component={SignIn} />
      <Route path="/register" component={SignUp} />
      <Route path="/home" component={Home} />
      <Route path="/research" component={Research} />
      <Route path="/author-bio" component={AuthorBio} />
    </Switch>
  );
}

export default Routes;
