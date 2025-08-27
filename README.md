# Mottu Rentals API

API para gerenciamento de **aluguel de motos e entregadores**, conforme desafio técnico Mottu.  
Permite cadastrar motos, entregadores, criar locações e aplicar regras de planos e multas.

---

## ✅ Checklist do Projeto

### 1️⃣ Estrutura e Configuração Inicial
- [x] Criar solution no Visual Studio
- [x] Criar pastas: Controllers, Entities, DTOs, Data
- [x] Remover WeatherForecast do template
- [x] Criar Program.cs limpo
- [x] Criar appsettings.json atualizado
- [x] Criar AppDbContext com DbSets
- [x] Criar entidades principais (Moto, Entregador, Locacao)

### 2️⃣ Módulo Motos
- [ ] Criar DTOs de Moto (request/response)
- [ ] Criar MotoController com CRUD completo
- [ ] Validar unicidade da placa
- [ ] Filtrar motos pela placa
- [ ] Permitir atualizar apenas a placa
- [ ] Permitir remover moto somente se não houver locações
- [ ] Gerar evento de moto cadastrada via mensageria
- [ ] Criar consumidor que notifique motos 2024 e salve no banco

### 3️⃣ Módulo Entregadores
- [ ] Criar DTOs de Entregador
- [ ] Criar EntregadorController com CRUD
- [ ] Validar unicidade de CNPJ e Número da CNH
- [ ] Permitir upload de foto da CNH (png/bmp)
- [ ] Salvar foto em storage (local ou S3/MinIO)

### 4️⃣ Módulo Locações
- [ ] Criar DTOs de Locação
- [ ] Criar LocacaoController com CRUD
- [ ] Validar que apenas entregadores habilitados categoria A podem alugar
- [ ] Aplicar regras de planos e valores por dia
- [ ] Calcular multa por devolução antecipada
- [ ] Calcular valor adicional por devolução atrasada
- [ ] Consultar valor total da locação

### 5️⃣ Diferenciais / Extras
- [ ] Testes unitários
- [ ] Testes de integração
- [ ] Logs bem estruturados
- [ ] Documentação da API (Swagger)
- [ ] Código limpo e organizado
- [ ] Dockerfile + Docker Compose
- [ ] Tratamento de erros global (middleware)

---

## ⚙️ Instruções para rodar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/mottu-rentals.git





MottuRentals/
│
├─ MottuRentals.API/                --> Projeto principal da API
│   ├─ Controllers/                 --> Controllers REST
│   │   ├─ MotoController.cs
│   │   ├─ EntregadorController.cs
│   │   └─ LocacaoController.cs
│   ├─ DTOs/                        --> Data Transfer Objects
│   │   ├─ MotoCreateDto.cs
│   │   ├─ MotoResponseDto.cs
│   │   ├─ EntregadorCreateDto.cs
│   │   ├─ EntregadorResponseDto.cs
│   │   ├─ LocacaoCreateDto.cs
│   │   └─ LocacaoResponseDto.cs
│   ├─ Services/                    --> Regras de negócio
│   │   ├─ MotoService.cs
│   │   ├─ EntregadorService.cs
│   │   └─ LocacaoService.cs
│   ├─ Repositories/                --> Acesso ao banco
│   │   ├─ MotoRepository.cs
│   │   ├─ EntregadorRepository.cs
│   │   └─ LocacaoRepository.cs
│   ├─ Storage/                     --> Gerenciamento de arquivos
│   │   └─ FotoStorageService.cs
│   ├─ Models/                      --> Entidades do EF Core
│   │   ├─ Moto.cs
│   │   ├─ Entregador.cs
│   │   └─ Locacao.cs
│   ├─ Data/                        --> DbContext
│   │   └─ MottuRentalsContext.cs
│   ├─ Events/                      --> Mensageria/Eventos
│   │   ├─ MotoCadastradaEvent.cs
│   │   └─ MotoCadastradaConsumer.cs
│   ├─ Middleware/                  --> Tratamento global de erros
│   │   └─ ExceptionMiddleware.cs
│   ├─ appsettings.json             --> Configurações
│   └─ Program.cs                   --> Configuração da API
│
├─ MottuRentals.Domain/             --> Entidades e regras de negócio (DDD)
│   ├─ Moto.cs
│   ├─ Entregador.cs
│   └─ Locacao.cs
│
├─ MottuRentals.Infrastructure/     --> Implementações de Storage, Repositórios, Mensageria
│   ├─ Storage/
│   │   └─ LocalStorageService.cs
│   ├─ Messaging/
│   │   └─ MotoEventPublisher.cs
│   └─ Repositories/
│       ├─ MotoRepository.cs
│       ├─ EntregadorRepository.cs
│       └─ LocacaoRepository.cs
│
├─ MottuRentals.Tests/              --> Testes unitários e integração
│   ├─ MotoServiceTests.cs
│   ├─ EntregadorServiceTests.cs
│   └─ LocacaoServiceTests.cs
│
├─ docker-compose.yml               --> Orquestração de containers
├─ Dockerfile                       --> Build da API
└─ README.md                        --> Documentação do projeto