import styled from 'styled-components';

import { darken } from 'polished';

const Container = styled.header`
  padding: 15px 30px 15px 15px;
  background-color: #45188E;
  font-family: Roboto, sans-serif;
`;

const Wrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const SearchContainer = styled.div`
  width: 100%;
  display: flex;
  justify-content: center;
`;

const InputWrapper = styled.div`
  border: 1px solid ${darken(.3, "#ffff")};
  border-radius: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 50%;
  padding-left: 20px;
  padding-right: 10px;
  margin-right: 5px;
`;

const Input = styled.input`
  width: 100%;
  background-color: none;
  background: transparent;
  border: none;
  color: #ffff;

  :: placeholder {
    color: #ffff;
  }
`;

const LoginBtn = styled.span`
   color: #ffff;
   border: 1px solid ${darken(.3, "#ffff")};
   border-radius: 20px;
   text-align: center;
   padding: 5px 20px;
   font-size: 12px;
`;

export {
  Container,
  Wrapper,
  InputWrapper,
  Input,
  SearchContainer,
  LoginBtn
}
