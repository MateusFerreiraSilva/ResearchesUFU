import styled from 'styled-components';
import { darken, lighten } from 'polished';

export const Wrapper = styled.div`
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
`;

export const Content = styled.div`
  width: 40%;
  background-color: #45188e;
  border-radius: 8px;
  text-align: center;

  form {
    display: flex;
    flex-direction: column;
    margin-top: 60px;
    label {
      color: #ffff;
    }
    input {
      width: 80%;
      border: 1px solid ${darken(0.3, '#ffff')};
      background: transparent;
      border-radius: 20px;
      height: 44px;
      padding: 0 15px;
      color: #fff;
      margin: 0 5px 20px;
      &::placeholder {
        color: rgba(255, 255, 255, 0.7);
      }
    }
    span {
      display: block;
      color: #fb5f91;
      margin-bottom: 20px;
      align-self: flex-start;
      font-size: 12px;
      font-weight: bold;
    }
    button {
      width: 120px;
      margin: 0 auto;
      height: 44px;
      background: none;
      color: #fff;
      border: 1px solid ${darken(0.3, '#ffff')};
      border-radius: 20px;
      font-size: 16px;
      transition: background 0.2s;
      &:hover {
        background: ${lighten(0.06, '#45188e')};
      }
    }
    a {
      color: #ffff;
      margin-top: 8px;
      font-size: 12px;
      opacity: 0.8;
      &:hover {
        opacity: 1;
      }
    }
  }
`;
