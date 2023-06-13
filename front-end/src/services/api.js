/* eslint-disable no-return-assign */
/* eslint-disable no-return-await */
/* eslint-disable no-use-before-define */

import axios from 'axios';

import settings from '~/config/appsettings.json';

const api = axios.create({
  baseURL: settings.api.baseUrl,
});

const getResearchesAsync = async () => {
  try {
    const response = await api.get('api/Researches');
    return response.data.map((research) => formatResearch(research));
  } catch (error) {
    console.log('Erro ao obter pesquisas:', error);
    throw error;
  }
};

const getResearchByIdAsync = async (id) => {
  try {
    const response = await api.get(`api/Researches/${id}`);
    const research = response.data;
    return research;
  } catch (error) {
    console.log('Erro ao obter pesquisa por ID:', error);
    throw error;
  }
};


const authenticateAsync = async (payload) => await api.post('/api/Session', payload);

const createUserAsync = async (payload) => await api.post('/User', payload);

const setAuthorizationToken = (token) =>
  (api.defaults.headers.Authorization = `Bearer ${token}`);

const formatResearch = (research) => {
  const formattedResearch = {
    id: research.id,
    title: research.title,
    summary: research.summary,
    status: research.status,
    publicationDate: research.publicationDate,
    thumbnail: research.thumbnail,
    researchStructure: research.researchStructure,
    fields: research.fields.map((field) => field.field.name),
    tags: research.tags.map((tag) => tag.tag.name),
    authors: research.authors.map((author) => author.author.name),
    lastUpdated: research.lastUpdated,
  };

  if (formattedResearch.summary.length > 330) {
    formattedResearch.summary = `${formattedResearch.summary.slice(0, 330).trimEnd()}...`;
  }

  return formattedResearch;
};

export {
  getResearchesAsync,
  authenticateAsync,
  createUserAsync,
  setAuthorizationToken,
  getResearchByIdAsync,
};


