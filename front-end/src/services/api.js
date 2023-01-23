import axios from 'axios';

import settings from '~/config/appsettings.json'

const api = axios.create({
  baseURL: settings.api.baseURL,
});

const getResearchesAsync = async () =>
  await api.get("/Researches");

const authenticateAsync = async (payload) =>
  await api.post("/Auth", payload);

const createUserAsync = async (payload) =>
 await api.post("/User", payload);

const setAuthorizationToken = (token) =>
  api.defaults.headers.Authorization = `Bearer ${token}`;

export {
  getResearchesAsync,
  authenticateAsync,
  createUserAsync,
  setAuthorizationToken
}
