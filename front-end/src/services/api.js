/* eslint-disable no-return-assign */
/* eslint-disable no-return-await */
/* eslint-disable no-use-before-define */

import axios from 'axios';

import settings from '~/config/appsettings.json';

const api = axios.create({
  baseURL: settings.api.baseUrl,
});

const getResearchesAsync = async () => {
  const results = await api.get('api/Researches');
  return results.data.map(r => formatResearch(r));
};

const authenticateAsync = async (payload) => await api.post('/Auth', payload);

const createUserAsync = async (payload) => await api.post('/User', payload);

const setAuthorizationToken = (token) =>
  (api.defaults.headers.Authorization = `Bearer ${token}`);

const formatResearch = (r) => {
  r.authors = r.authors.map((a) => a.name).join(", ");
  r.tags = r.tags.map((t) => t.name).join(", ");
  r.fields = r.fields.map((f) => f.name).join(", ");

  if (r.summary.length > 330)
    r.summary = `${r.summary.slice(0, 330).trimEnd()}...`;

  return r;
};

export {
  getResearchesAsync,
  authenticateAsync,
  createUserAsync,
  setAuthorizationToken,
};
