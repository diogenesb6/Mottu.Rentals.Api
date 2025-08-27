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



MotorRetails/
│
├─ MotorRetails.API/          --> Projeto principal da API
│   ├─ Controllers/           --> Controllers REST
│   │   └─ EntregadorController.cs
│   ├─ DTOs/                  --> Data Transfer Objects
│   │   └─ EntregadorCreateDto.cs
│   ├─ Services/              --> Regras de negócio
│   │   └─ EntregadorService.cs
│   ├─ Repositories/          --> Acesso ao banco
│   │   └─ EntregadorRepository.cs
│   ├─ Storage/               --> Gerenciamento de arquivos
│   │   └─ FotoStorageService.cs
│   ├─ Models/                --> Entidades do EF Core
│   │   └─ Entregador.cs
│   ├─ Data/                  --> DbContext
│   │   └─ MotorRetailsContext.cs
│   └─ Program.cs             --> Configuração da API
│
├─ MotorRetails.Domain/       --> Entidades e regras de negócio (opcional, se usar DDD)
│
├─ MotorRetails.Infrastructure/ --> Implementações de Storage, Repositórios
│
└─ MotorRetails.Tests/        --> Testes unitá