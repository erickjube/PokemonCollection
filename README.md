# Pokémon Collection

Aplicação web para criação e gerenciamento de uma coleção de cartas Pokémon em formato de Pokédex.

O projeto está sendo desenvolvido como uma forma de colocar em prática conceitos de desenvolvimento de APIs, arquitetura em camadas, persistência de dados, integração com APIs externas e desenvolvimento de interfaces com React.

> **Projeto em desenvolvimento**
>
> Esta é a primeira versão funcional do projeto. Novas funcionalidades e melhorias estão sendo implementadas gradualmente.

## Objetivo

Criar uma coleção de cartas Pokémon apresentada em formato de Pokédex, permitindo consultar Pokémon e cartas e gerenciar quais cartas fazem parte da coleção.

## Tecnologias

### Backend

* **C#**
* **.NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Swagger**

### Frontend

* **React**
* **Vite**
* **React Router**

## Arquitetura

O backend segue uma arquitetura em camadas, buscando separar as responsabilidades da aplicação:

```text
Controllers
    ↓
Services
    ↓
Repositories
    ↓
Database
```

### Camadas

* **Controllers** → recebem e processam as requisições HTTP.
* **Services** → concentram as regras de negócio da aplicação.
* **Repositories** → responsáveis pelo acesso e persistência dos dados.
* **DTOs** → utilizados para transportar os dados entre as diferentes camadas da aplicação.

O projeto também utiliza o **Entity Framework Core** para comunicação com o banco de dados SQL Server.

## Funcionalidades

Atualmente, a aplicação permite:

* Pesquisar Pokémon com paginação;
* Visualizar os detalhes de um Pokémon;
* Verificar se um Pokémon possui uma carta associada à coleção;
* Pesquisar cartas com paginação;
* Adicionar uma carta à coleção;
* Trocar a carta associada a um Pokémon;
* Remover uma carta da coleção.

## Funcionalidades planejadas

Algumas das próximas funcionalidades planejadas para o projeto são:

* Implementação de autenticação e autorização;
* Separação dos ambientes de desenvolvimento e produção;
* Criação de uma página de estatísticas da coleção;
* Melhorias no sistema de filtros e pesquisa;
* Evolução da interface e experiência do usuário.

## Objetivo de aprendizado

O projeto está sendo desenvolvido de forma incremental, buscando aplicar na prática conhecimentos adquiridos durante os estudos de desenvolvimento.

Entre os principais conceitos trabalhados estão:

* Desenvolvimento de APIs REST;
* Arquitetura em camadas;
* Separação de responsabilidades;
* Entity Framework Core;
* Persistência de dados com SQL Server;
* Integração com APIs externas;
* Desenvolvimento de aplicações com React;
* Consumo de APIs no frontend;
* Paginação e filtros;
* Versionamento de código com Git e GitHub.

## Status do projeto

**Em desenvolvimento.**

A primeira versão do fluxo principal já está funcional, mas o projeto continuará recebendo novas funcionalidades, melhorias de arquitetura e aprimoramentos na interface.
