/* eslint-disable jsx-a11y/label-has-associated-control */
import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useHistory } from 'react-router-dom';
import { Form, Input } from '@rocketseat/unform';
import * as Yup from 'yup';

import { MdHome, MdArrowBack } from 'react-icons/md';
import { signInRequest } from '~/store/modules/auth/actions';

import logo from '~/assets/images/logo.svg';

import { Header, Content, Pointer } from './styles';

const schema = Yup.object().shape({
  email: Yup.string()
    .email('Insira um e-mail válido')
    .required('O e-mail é obrigatório'),
  password: Yup.string().required('A senha é obrigatória'),
});

const SignIn = () => {
  const dispatch = useDispatch();
  const loading = useSelector((state) => state.auth.loading);
  const history = useHistory();

  function handleSubmit({ email, password }) {
    dispatch(signInRequest(email, password));
  }

  return (
    <>
      <Header>
        <Pointer>
          <MdArrowBack
            color="#ffff"
            size={25}
            onClick={() => history.goBack()}
          />
        </Pointer>
        <Link to="/">
          <MdHome color="#ffff" size={25} />
        </Link>
      </Header>
      <Content>
        <img src={logo} alt="UFU" />

        <Form schema={schema} onSubmit={() => handleSubmit()}>
          <label>
            E-mail
            <Input
              id="email"
              name="email"
              type="email"
              placeholder="Seu e-mail"
            />
          </label>
          <label>
            Senha
            <Input
              id="password"
              name="password"
              type="password"
              placeholder="Sua senha"
            />
          </label>
          <button type="submit">{loading ? 'Carregando...' : 'Login'}</button>
          <Link to="/register">Problemas com o login?</Link>
        </Form>
      </Content>
    </>
  );
};

export default SignIn;
