# LogisticsDeliveryManager.API

## 🏛 Sobre o Projeto

Este repositório contém o código-fonte do backend do sistema **Logistics Delivery Manager**. Ele é construído sobre a arquitetura .NET corporativa e fornece uma API REST escalável e modular para as aplicações clientes, evidenciando o modelo de design **DDD (Domain-Driven Design)**.

> **⚠️ Atenção:** Este projeto utiliza **.NET 10** (conforme definido nos arquivos de projeto `.csproj`).

## 🏗 Arquitetura no Modelo DDD

A estrutura do projeto está desenhada seguindo os rígidos princípios de Domain-Driven Design (DDD), dividindo as responsabilidades de forma clara e isolada nas seguintes camadas (presentes no diretório `src/`):

* **LogisticsDeliveryManager.Domain**: O coração da aplicação. Contém as Regras de Negócio centrais, Entidades, Value Objects e interfaces de Repositórios/Serviços. Não possui nenhuma dependência externa (Framework agnostic).
* **LogisticsDeliveryManager.Application**: Regras de aplicação e Casos de Uso (Use Cases). Orquestra as chamadas de domínio, fluxos de negócio e serviços internos, atuando como o elo entre a API e as regras especialistas.
* **LogisticsDeliveryManager.Exception**: Gestão centralizada de erros, englobando as exceções customizadas de domínio e regras de validação de negócio que devem retornar formatadas à API.
* **LogisticsDeliveryManager.Communication**: Contratos de comunicação, Request/Response DTOs e padronização de retornos e entradas rigorosas da API.
* **LogisticsDeliveryManager.Infrastructure**: Camada responsável pela persistência de dados (Acesso a Banco e ORM), injeção de dependências externas (como storage e bus) e integrações com serviços de terceiros.
* **LogisticsDeliveryManager.API**: Camada de Apresentação (REST / Controllers). É o ponto de entrada da aplicação, responsável apenas por receber as requisições HTTP, validar sintaticamente, delegar para a camada de *Application* e retornar as respostas (Status Codes) adequadas ao cliente.

## 🚀 Tecnologias Utilizadas

### Core & Frameworks

* **.NET 10**: Versão principal do SDK base do projeto (`<TargetFramework>net10.0</TargetFramework>`).
* **ASP.NET Core**: Framework principal do projeto Web API para injeção de dependência, roteamento e web server nativo.
* **C# Nullable Reference Types & Implicit Usings**: Configurações de linguagem habilitadas globalmente para um desenvolvimento mais moderno, limpo e à prova de exceções de referência nula (`NullReferenceException`).

### Ferramentas de Desenvolvimento

* **OpenAPI / Swagger**: Documentação interativa e auto-gerada da API viabilizada nativamente pelo pacote mais atual da Microsoft: `Microsoft.AspNetCore.OpenApi` `(v10.0.2)`.

*(Nota: Adicione as bibliotecas referentes à persistência e mensageria na documentação à medida que a camada de Infrastructure for recebendo seus pacotes e orquestrações).*

## ⚙️ Configuração e Variáveis de Ambiente

O projeto utiliza a configuração unificada do ecossistema .NET (`appsettings.json` e `appsettings.Development.json`). As configurações principais, especialmente senhas, strings de conexão a banco e chaves de API, podem e devem ser sobrescritas por variáveis de ambiente padrão do O.S. ou injetadas via contêineres e pipelines.

### 🖥️ Servidor e Contexto

| Variável de Ambiente O.S. | Descrição |
| :--- | :--- |
| `ASPNETCORE_URLS` | Porta e protocolo da aplicação web (ex: `http://+:5000` ou `https://+:5001`). |
| `ASPNETCORE_ENVIRONMENT` | Define e gerencia ativamente o ambiente de deploy para injeção adequada de dependências (ex: `Development`, `Staging`, `Production`). |

## 📖 Documentação da API (Swagger)

A API é estritamente auto-documentada utilizando o suporte OpenAPI/Swagger integrado no framework mais novo do .NET. Abaixo está o mapeamento dos links de acesso padrão:

| Ambiente | URL do Swagger UI | Especificação OpenAPI (JSON) |
| :--- | :--- | :--- |
| **Local / Dev** | `http://localhost:[porta]/swagger/index.html` | `http://localhost:[porta]/openapi/v1.json` (padrão .NET 10.x) |

*(Nota: Substitua a chave `[porta]` e os domínios mapeados em seus respectivos ambientes, como por exemplo os hostnames de Load Balancers ou Ingress Controllers).*

## 💻 Como Executar

### 1. Clonar o repositório

```bash
git clone <url-do-repositorio-LogisticsDeliveryManager>
cd LogisticsDeliveryManager.API
```

### 2. Restaurar e Compilar (CLI)

Na raiz do projeto executando via `dotnet CLI` usando o instalador de solução / pacote:

```bash
dotnet build LogisticsDeliveryManager.API.slnx
```

### 3. Rodar Localmente

O perfil de desenvolvimento local pode ser acionado entrando na hospedagem principal da WebAPI:

```bash
cd src/LogisticsDeliveryManager.API
dotnet run
```

*Alternativamente, rode pelo botão Run/F5 da sua IDE de preferência configurada (Visual Studio / Rider / VS Code).*

## 🧪 Testes

Para iterar e verificar todos os projetos de Testes Automatizados criados para atestar as lógicas unitárias (Domain/Application):

```bash
dotnet test LogisticsDeliveryManager.API.slnx
```
