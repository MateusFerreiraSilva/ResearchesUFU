import React from 'react';
import PropTypes from 'prop-types';
import { List, ListItem, Content } from './styles';

const ResearchList = ({ items }) => {
  return (
    <List>
      {items.map((research) => (
        <ListItem key={research.id}>
          <Content>
            <section>
              <h1>{research.title}</h1>
              <p>{research.summary}</p>
              <span>
                <b>Temas</b>: {research.fields.join(', ')}
              </span>
              <span>
                <b>Tags</b>: {research.tags.join(', ')}
              </span>
              <span>
                <b>Autores</b>: {research.authors.join(', ')}
              </span>
              <span>
                <b>Status</b>: {research.status}
              </span>
            </section>
            {research.thumbnail && (
              <img src={research.thumbnail} width="180" height="180" alt="" />
            )}
          </Content>
        </ListItem>
      ))}
    </List>
  );
};

export default ResearchList;

ResearchList.propTypes = {
  items: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      title: PropTypes.string.isRequired,
      summary: PropTypes.string.isRequired,
      fields: PropTypes.arrayOf(PropTypes.string).isRequired,
      tags: PropTypes.arrayOf(PropTypes.string).isRequired,
      authors: PropTypes.arrayOf(PropTypes.string).isRequired,
      status: PropTypes.string.isRequired,
      thumbnail: PropTypes.string,
    })
  ).isRequired,
};
