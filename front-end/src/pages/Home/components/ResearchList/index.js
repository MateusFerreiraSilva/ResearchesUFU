/* eslint-disable react/forbid-prop-types */
import React from 'react';
import PropTypes from 'prop-types';
import { List, ListItem, Content } from './styles';

const ResearchList = ({ items }) => {
  return (
    <List>
      {items.map((i) => (
        <ListItem>
          <Content>
            <section>
              <h1>{i.title}</h1>
              <p>{i.summary}</p>
              <span>
                <b>Temas</b>: {i.fields}
              </span>
              <span>
                <b>Tags</b>: {i.tags}
              </span>
              <span>
                <b>Autores</b>: {i.authors}
              </span>
              <span>
                <b>Status</b>: {i.status}
              </span>
            </section>
            {i.thumbnail && (
              <img src={i.thumbnail} width="180" height="180" alt="" />
            )}
          </Content>
        </ListItem>
      ))}
    </List>
  );
};

export default ResearchList;

ResearchList.propTypes = {
  items: PropTypes.array.isRequired,
};
