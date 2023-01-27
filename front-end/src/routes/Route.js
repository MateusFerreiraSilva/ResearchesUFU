import React from 'react';
import PropTypes from 'prop-types';
import { Route, Redirect } from 'react-router-dom';

import AuthLayout from '~/pages/_layouts/auth';
import DefaultLayout from '~/pages/_layouts/default';

import { store } from '~/store';

const RouteWrapper = ({ component: Component, type, ...rest }) => {
  const { signed } = store.getState().auth;

  if (!signed && type === 'private') {
    return <Redirect to="/login" />;
  }

  const Layout =
    type === 'public' || (type === 'private' && signed)
      ? DefaultLayout
      : AuthLayout;

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
};

RouteWrapper.propTypes = {
  type: PropTypes.oneOf(['private', 'public', 'auth']).isRequired,
  component: PropTypes.oneOfType([PropTypes.element, PropTypes.func])
    .isRequired,
};

export default RouteWrapper;
