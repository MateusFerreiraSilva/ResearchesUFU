import styled from 'styled-components';

const List = styled.ul`
  width: 80%;
`;

const ListItem = styled.li`
  font-size: 14px;
  border: 1px solid black;
  border-radius: 15px;
  margin-bottom: 20px;
  padding: 15px 30px;
  box-shadow: -1px 2px 2px 0px rgba(0, 0, 0, 0.36);
  cursor: pointer;

  & span {
    white-space: nowrap;
    width: 50%;
    overflow: hidden;
    text-overflow: ellipsis;
    display: block;
    margin-bottom: 6px;
  }

  & p {
    width: 100%;
    margin-bottom: 12px;
    margin-top: 15px;
  }
`;

const Content = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

export { List, ListItem, Content };
