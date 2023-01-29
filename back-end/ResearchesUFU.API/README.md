## Recomendações
- Sistema operacional:
    - Windows 10 ou Windows 11 (Recomendado)
    - Linux (Ubuntu 20.04) ou superior
    
- .NET SDK 6.0
- PostgresSQL 15
- Visual Studio IDE (Opcional)
## Executando
Entre na raiz do projeto:
```bash
$ cd ResearchesUFU.API
```

Durante a primeira execução é necessário rodar a _migration_ do banco de dados, para isto basta executar o comando abaixo:
```bash
$ dotnet ef database update
```

Caso ocorra algum problema durante a execução da _migration_ verifique se as informações da _connection string_ presente no arquivo de appsettings.json estão corretas.

Após isso já podemos executar o projeto:
```bash
$ dotnet build
$ dotnet run
```
Pronto! A aplicação está sendo executada!

Os endereços por onde a aplicação está sendo executa serão apresentadas no console. Comumente os endereços são esses:
- https://localhost:7158
- http://localhost:5158
    
Para visualizar o swagger da aplicação, basta adicionar /swagger ao final do endereço, ex:
- https://localhost:7158/swagger