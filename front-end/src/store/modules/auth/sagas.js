import { takeLatest, call, put, all } from 'redux-saga/effects';
import { toast } from 'react-toastify';

import { signInSuccess, signFailure } from './actions';

import history from '~/services/history';
import {
  authenticateAsync,
  createUserAsync,
  setAuthorizationToken,
} from '~/services/api';

export function* signIn({ payload }) {
  try {
    const { email, password } = payload;

    const response = yield call(
      authenticateAsync({
        email,
        password,
      })
    );

    const { token, user } = response.data;

    yield call(put(signInSuccess(token, user)));

    history.push('/home');
  } catch (err) {
    toast.error('Falha na autenticação, verifique os seus dados');
    yield put(signFailure());
  }
}

export function* signUp({ payload }) {
  try {
    const { name, email, password } = payload;

    yield call();
    yield call(
      createUserAsync({
        name,
        email,
        password,
      })
    );

    history.push('/');
  } catch (err) {
    toast.error('Falha no cadastro, verifique seus dados');

    yield put(signFailure());
  }
}

export function setToken({ payload }) {
  if (!payload) return;

  const { token } = payload.auth;

  if (token) {
    setAuthorizationToken(`Bearer ${token}`);
  }
}

export function signOut() {
  history.push('/');
}

export default all([
  takeLatest('persist/REHYDRATE', setToken),
  takeLatest('@auth/SIGN_IN_REQUEST', signIn),
  takeLatest('@auth/SIGN_UP_REQUEST', signUp),
  takeLatest('@auth/SIGN_OUT', signOut),
]);
