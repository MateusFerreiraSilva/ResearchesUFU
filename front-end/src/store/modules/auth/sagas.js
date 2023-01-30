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
    const sha256 = async (message) => {
      // encode as UTF-8
      const msgBuffer = new TextEncoder().encode(message);                    
  
      // hash the message
      const hashBuffer = await crypto.subtle.digest('SHA-256', msgBuffer);
  
      // convert ArrayBuffer to Array
      const hashArray = Array.from(new Uint8Array(hashBuffer));
  
      // convert bytes to hex string                  
      const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
      return hashHex;
    }

    const { email, password } = payload;
    const passwordHash = yield sha256(password);

    console.log(passwordHash);

    const response = yield authenticateAsync({
      email,
      passwordHash,
    });

    const { userId, token } = response.data;

    yield put(signInSuccess(token, userId));

    history.push('/');
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
