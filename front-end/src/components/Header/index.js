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
        <Link to="/login">
          <LoginBtn>Login</LoginBtn>
        </Link>
      </Wrapper>
    </Container>
  );
};

export default Header;
