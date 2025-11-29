# 🎓 Academic Badges System

Sistema completo de concessão de badges acadêmicas desenvolvido em C# com Arquitetura em Camadas e TDD (Test-Driven Development).

## 🚀 Tecnologias Utilizadas

- **.NET 9.0**
- **C# 12.0**
- **xUnit** - Testes unitários
- **Moq** - Mocking para testes
- **Entity Framework Core** - Persistência em memória
- **Repository Pattern** - Padrão de acesso a dados

## 🏗️ Arquitetura
````
AcademicBadgesSystem/
├── 📁 Domain/ # Camada de Domínio
│ ├── Entities/ # Entidades de negócio
│ ├── Services/ # Lógica de negócio
│ ├── Interfaces/ # Contratos e abstrações
│ └── Exceptions/ # Exceções de domínio
├── 📁 Infrastructure/ # Camada de Infraestrutura
│ ├── Data/ # Contexto do EF Core
│ └── Repositories/ # Implementações dos repositórios
└── 📁 Domain.Tests/ # Camada de Testes
├── Entities/ # Testes das entidades
└── Services/ # Testes dos serviços
````

## 📋 Funcionalidades

### 🎯 Entidades Principais

- **`Estudante`**: Representa um aluno com badges e missões concluídas
- **`Badge`**: Conquista acadêmica com dificuldade (1-5) e pontuação
- **`Missao`**: Desafio acadêmico que concede badges ao ser concluído

### 🔧 Serviços

- **`SistemaConcessaoService`**: Gerencia a concessão automática de badges
- Concessão por conclusão de missões
- Cálculo de pontuação total
- Validações de regras de negócio

### 🧪 Testes (TDD)

- **21 testes unitários** cobrindo todos os cenários
- Testes de validação e invariantes
- Testes de serviços com mocking
- Testes de exceções e casos de erro

## 🎮 Como Usar

### Exemplo Básico

Configuração
var context = new InMemoryDbContext();
var estudanteRepo = new EstudanteRepository(context);
var badgeRepo = new BadgeRepository(context);
var missaoRepo = new MissaoRepository(context);
var service = new SistemaConcessaoService(estudanteRepo, badgeRepo, missaoRepo);

Criar dados
var badge = new Badge("Primeira Conquista", "Completou a primeira missão", 1);
var missao = new Missao("Missão Inicial", "Primeira missão do sistema", 1, badge.Id);
var estudante = new Estudante("João Silva", "joao@email.com");

Concluir missão e conceder badge
estudante.ConcluirMissao(missao);
await service.ConcederBadgePorMissaoAsync(estudante.Id, missao.Id);

## 🧪 Executando os Testes

# Restaurar pacotes
````
dotnet restore
````
# Executar build
````
dotnet build
````
# Executar testes
````
dotnet test
````
## 📊 Resultados dos Testes

Resumo do teste: 
- Total: 21 testes
- Bem-sucedidos: 21
- Falhas: 0
- Ignorados: 0
- Duração: 7.1s

## 🎯 Regras de Negócio Implementadas
Validações
✅ Nome e descrição obrigatórios para badges e missões

✅ Email válido para estudantes

✅ Dificuldade entre 1 e 5

✅ Prevenção de badges duplicadas

✅ Prevenção de missões duplicadas

Concessão de Badges
✅ Badge só é concedida se missão foi concluída

✅ Badge não é concedida duas vezes para o mesmo estudante

✅ Pontuação calculada automaticamente (dificuldade × 100)

## 🔄 Padrões de Projeto Utilizados
Repository Pattern: Abstração do acesso a dados

Dependency Injection: Inversão de dependência

TDD: Desenvolvimento guiado por testes

Domain-Driven Design: Design orientado ao domínio

Clean Architecture: Separação de responsabilidades

## 👨‍💻 Desenvolvimento
Princípios Aplicados

SOLID - Princípios de design orientado a objetos

DRY - Don't Repeat Yourself

KISS - Keep It Simple, Stupid

YAGNI - You Ain't Gonna Need It

Convenções de Código
Nomenclatura em português para domínio

Testes em inglês para padrão xUnit

Commits semânticos

Cobertura completa de testes

## 📝 Licença
Este projeto está sob a licença MIT. Veja o arquivo LICENSE para detalhes.

Desenvolvido como trabalho acadêmico de POO com TDD 🎓
