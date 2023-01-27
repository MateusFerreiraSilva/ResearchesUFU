import React from 'react';
import PropTypes from 'prop-types';

import Header from '~/components/Header';
import { Content } from './styles';

export default function DefaultLayout({ children }) {
  return (
    <div>
      <Header />
      <Content>{children}</Content>
    </div>
  );
}

DefaultLayout.propTypes = {
  children: PropTypes.element.isRequired,
};
