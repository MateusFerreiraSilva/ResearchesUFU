import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getResearchesAsync } from '~/services/api';
import {
  List,
  ListItem,
  Content,
  Title,
  Summary,
  Subtitle,
  StyledImage,
  Status,
} from './styles';

const Home = () => {
  const [researches, setResearches] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const data = await getResearchesAsync();
        setResearches(data);
      } catch (error) {
        console.log('Erro ao obter pesquisas:', error);
      }
    }

    fetchData();
  }, []);

  return (
    <div>
      <List>
        {researches.map((research) => (
          <ListItem key={research.id}>
            <Link to={`/researches/${research.id}`}>
              <Content>
                <div>
                  <Title>{research.title}</Title>
                  <Summary>{research.summary}</Summary>
                  <Subtitle>Publication Date: {research.publicationDate}</Subtitle>
                  <Status status={research.status}>{research.status}</Status>
                </div>
                <StyledImage src={research.thumbnail} alt="Thumbnail" />
              </Content>
            </Link>
          </ListItem>
        ))}
      </List>
    </div>
  );
};

export default Home;
