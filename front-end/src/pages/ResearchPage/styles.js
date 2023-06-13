import styled from 'styled-components';

export const Container = styled.div`
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
`;

export const Title = styled.h1`
  font-size: 32px;
  font-weight: bold;
  margin-bottom: 10px;
  color: #333;
`;

export const Summary = styled.p`
  font-size: 18px;
  margin-bottom: 20px;
  line-height: 1.5;
  color: #555;
`;

export const Status = styled.p`
  font-size: 16px;
  margin-bottom: 10px;
  color: ${props => {
    if (props.status === 'Finalizada') return '#00B894';
    if (props.status === 'Cancelada') return '#FF5252';
    if (props.status === 'Em andamento') return '#3498DB';
    return '#888';
  }};
`;

export const PublicationDate = styled.p`
  font-size: 16px;
  margin-bottom: 10px;
  color: #888;
`;

export const Thumbnail = styled.img`
  max-width: 100%;
  height: auto;
  margin-bottom: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
`;

export const List = styled.ul`
  list-style-type: none;
  padding: 0;
  margin-bottom: 20px;
`;

export const ListItem = styled.li`
  font-size: 16px;
  margin-bottom: 10px;
  color: #555;
`;

export const LastUpdated = styled.p`
  font-size: 14px;
  color: #888;
`;

export const TagsList = styled.ul`
  display: flex;
  flex-wrap: wrap;
  margin-bottom: 20px;
  padding: 0;
`;

export const Tag = styled.li`
  background-color: #f2f2f2;
  border-radius: 4px;
  font-size: 14px;
  padding: 6px 12px;
  margin-right: 8px;
  margin-bottom: 8px;
  color: #555;
`;

export const AuthorsList = styled.ul`
  list-style-type: none;
  padding: 0;
  margin-bottom: 20px;
`;

export const Author = styled.li`
  font-size: 16px;
  margin-bottom: 6px;
  color: #555;
`;

export default {
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
};
