import React, { useEffect, useState } from 'react';
import { getResearchesAsync } from '~/services/api';
import ResearchList from './components/ResearchList';

const Home = () => {
  const [researches, setResearches] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const response = await getResearchesAsync();
      setResearches(response);
    }

    fetchData();
  }, []);

  return <ResearchList items={researches} />;
};

export default Home;
