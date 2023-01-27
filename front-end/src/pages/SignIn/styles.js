import styled from 'styled-components';

const Header = styled.div`
  padding: 15px;
  display: flex;
  justify-content: space-between;
  align-items: center;
`;

const Pointer = styled.span`
  cursor: pointer;
`;

const Content = styled.div`
  padding: 0 50px 30px 50px;
`;

export { Header, Content, Pointer };
