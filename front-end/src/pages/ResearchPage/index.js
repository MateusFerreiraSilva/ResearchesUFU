import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getResearchByIdAsync } from 'services/api';
import {
  Container,
  Title,
  Summary,
  Status,
  PublicationDate,
  Thumbnail,
  List,
  ListItem,
  LastUpdated,
  TagsList,
  Tag,
  AuthorsList,
  Author,
} from './styles';

const ResearchPage = () => {
  const { id } = useParams();
  const [research, setResearch] = useState(null);

  useEffect(() => {
    async function fetchData() {
      try {
        const researchData = await getResearchByIdAsync(id);
        setResearch(researchData);
      } catch (error) {
        console.log('Erro ao obter pesquisa:', error);
      }
    }

    fetchData();
  }, [id]);

  if (!research) {
    return <div>Carregando...</div>;
  }

  const {
    title,
    summary,
    status,
    publicationDate,
    thumbnail,
    fields,
    tags,
    authors,
    lastUpdated,
  } = research;

  return (
    <Container>
      <Title>{title}</Title>
      <Summary>{summary}</Summary>
      <Status>Status: {status}</Status>
      <PublicationDate>Publication Date: {publicationDate}</PublicationDate>
      {thumbnail && <Thumbnail src={thumbnail} alt="Research Thumbnail" />}
      <h2>Fields:</h2>
      <List>
        {fields.map((field) => (
          <ListItem key={field.field.id}>{field.field.name}</ListItem>
        ))}
      </List>
      <h2>Tags:</h2>
      <TagsList>
        {tags.map((tag) => (
          <Tag key={tag.tag.id}>{tag.tag.name}</Tag>
        ))}
      </TagsList>
      <h2>Authors:</h2>
      <AuthorsList>
        {authors.map((author) => (
          <Author key={author.author.id}>{author.author.name}</Author>
        ))}
      </AuthorsList>
      <LastUpdated>Last Updated: {lastUpdated}</LastUpdated>
    </Container>
  );
};

export default ResearchPage;
