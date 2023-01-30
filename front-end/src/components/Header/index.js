import React from 'react';
import { Link } from 'react-router-dom/cjs/react-router-dom.min';

import { MdHome, MdSearch, MdFilterList } from 'react-icons/md';
import {
  Container,
  Wrapper,
  InputWrapper,
  Input,
  SearchContainer,
  LoginBtn,
} from './styles';

import { isUserSiggned } from '~/store/modules/auth/reducer';

const Header = () => {
  return (
    <Container>
      <Wrapper>
        <Link to="/">
          <MdHome size={25} color="#ffff" />
        </Link>
        <SearchContainer>
          <InputWrapper>
            <Input type="text" placeholder="Buscar pesquisa..." />
            <MdSearch size={20} color="#ffff" />
          </InputWrapper>
          <MdFilterList size={25} color="#ffff" />
        </SearchContainer>
        {
          !isUserSiggned ?
          <Link to="/login">
            <LoginBtn>Login</LoginBtn>
          </Link>
        :
          null
        }
      </Wrapper>
    </Container>
  );
};

export default Header;
