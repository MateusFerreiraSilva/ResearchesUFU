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
      <Route exact path="/" name="default" component={Home} />
      <Route path="/register" name="signup" component={SignUp} />
      <Route path="/login" name="signin" component={SignIn} />
      <Route path="/research" name="research" component={Research} />
      <Route path="/author-bio" name="author-bio" component={AuthorBio} />
    </Switch>
  );
}

export default Routes;
