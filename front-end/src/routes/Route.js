import React from 'react';
import PropTypes from 'prop-types';
import { Route, Redirect } from 'react-router-dom';

import AuthLayout from '~/pages/_layouts/auth';
import DefaultLayout from '~/pages/_layouts/default';

import { store } from '~/store';

const authPages = ["signin", "signup"];

const RouteWrapper = ({
  component: Component,
  isPrivate,
  name,
  ...rest
}) =>  {
  const { signed } = store.getState().auth;

  if (!signed & isPrivate) {
    return <Redirect to="/" />;
  }

  const Layout = (!isPrivate || signed) && !authPages.some(p => p === name) ? DefaultLayout: AuthLayout;

  return (
    <Route
      {...rest}
      render={(props) => (
        <Layout>
          <Component {...props} />
        </Layout>
      )}
    />
  );
}

RouteWrapper.propTypes = {
  isPrivate: PropTypes.bool,
  name: PropTypes.string,
  component: PropTypes.oneOfType([PropTypes.element, PropTypes.func])
    .isRequired,
};

export default RouteWrapper;
