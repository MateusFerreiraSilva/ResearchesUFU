/* eslint-disable no-return-assign */
/* eslint-disable no-return-await */
/* eslint-disable no-use-before-define */

import axios from 'axios';

import settings from '~/config/appsettings.json';

const results = [
  {
    id: 1,
    name: 'Test',
    description:
      'Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.',
    authors: [{ name: 'João' }],
    tags: ['teste, test1, test2'],
    fields: ['IA', 'Rede Neural'],
    thumbnail:
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrilCi6dpVoww5XqibnYG1QaO7QOxL34nsINC9qO8HfDors-Aib6bdA40XJCUWZKK-DAA&usqp=CAU',
  },
  {
    id: 2,
    name: 'Test 2',
    description:
      'Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.',
    authors: [{ name: 'João Victor' }],
    tags: ['teste 2'],
    fields: ['IA', 'Rede Neural'],
    thumbnail:
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrilCi6dpVoww5XqibnYG1QaO7QOxL34nsINC9qO8HfDors-Aib6bdA40XJCUWZKK-DAA&usqp=CAU',
  },
  {
    id: 3,
    name: 'Test 3',
    description:
      'Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.',
    authors: [{ name: 'João 3' }],
    tags: ['teste 3'],
    fields: ['IA', 'Rede Neural'],
    thumbnail:
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrilCi6dpVoww5XqibnYG1QaO7QOxL34nsINC9qO8HfDors-Aib6bdA40XJCUWZKK-DAA&usqp=CAU',
  },
];

const api = axios.create({
  baseURL: settings.api.baseURL,
});

const getResearchesAsync = async () => {
  /* const results = await api.get('/Researches')/ */
  return results.map((r) => formatResearch(r));
};

const authenticateAsync = async (payload) => await api.post('/Auth', payload);

const createUserAsync = async (payload) => await api.post('/User', payload);

const setAuthorizationToken = (token) =>
  (api.defaults.headers.Authorization = `Bearer ${token}`);

const formatResearch = (r) => {
  r.authors = r.authors.map((a) => a.name).toString();
  r.tags = r.tags.toString();
  r.fields = r.fields.toString();

  if (r.description.length > 330)
    r.description = `${r.description.slice(0, 330).trimEnd()}...`;

  return r;
};

export {
  getResearchesAsync,
  authenticateAsync,
  createUserAsync,
  setAuthorizationToken,
};
