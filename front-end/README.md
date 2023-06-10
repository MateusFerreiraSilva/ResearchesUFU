#Tecnlogias e Dependências 

- Tecnologia: React;
- Dependências 
     1. Yarn => 1.22;
     2. Aplicação back-end executando;

#Organizacão do projeto

1. Assets: possui os arquivos públicos do projeto

2. Components: possui os componentes compartilhados;

3. Config: possui as configurações do projeto;

4. Pages: possui as páginas e layouts da aplicação;

5. Routes: possui a configuração da página a ser  renderizada pelo path da mesma;

6. Services: possui os arquivos de serviços, como integração com a api e history para navegação dentro do browser;

7. Store: com a aplicação do conceito Redux, armazena dados compartilhados entre as telas e componentes, como o token do usuário logado;

8. Styles: possui os estilos globais da aplicação

## Executando

Na pasta do frontend, basta rodar os comandos abaixo:
```bash
$ yarn
```

```bash
$ yarn start
```


Rodando com o docker:
```bash
$ docker compose up
```