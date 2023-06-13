import styled from 'styled-components';

const List = styled.ul`
  width: 98%;
  margin: 0 auto;
  display: flex;
  flex-wrap: wrap;
  justify-content: center; /* Centraliza horizontalmente */
  align-items: center; /* Centraliza verticalmente */
`;

const ListItem = styled.li`
  font-size: 14px;
  border: 1px solid #ddd;
  border-radius: 15px;
  margin-bottom: 20px;
  padding: 15px 30px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  cursor: pointer;
  background-color: #fff;
`;

const Content = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const Title = styled.h1`
  font-size: 24px;
  margin-bottom: 10px;
  color: #333;
`;

const Summary = styled.p`
  margin-bottom: 10px;
  color: #777;
`;

const Subtitle = styled.h3`
  margin-bottom: 5px;
  color: #555;
`;

const StyledImage = styled.img`
  width: 180px;
  height: 180px;
  border-radius: 10px;
  object-fit: cover;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
`;

const Status = styled.p`
  font-weight: bold;
  color: ${(props) => {
    if (props.status === 'finalizada') return '#28a745';
    if (props.status === 'em andamento') return '#ff9800';
    if (props.status === 'cancelada') return '#dc3545';
    return '#333';
  }};
`;

export { List, ListItem, Content, Title, Summary, Subtitle, StyledImage, Status };