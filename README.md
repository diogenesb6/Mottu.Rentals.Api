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





📖 Casos de Uso – MottuRentals
👨‍💼 Usuário Admin
1. Cadastrar Moto

Como admin

Quero cadastrar uma nova moto

Para disponibilizá-la para locação

Regras de negócio:

Dados obrigatórios: Identificador, Ano, Modelo, Placa.

Placa deve ser única (não pode repetir).

Ao cadastrar, gerar evento de MotoCadastrada.

O evento deve ser publicado em um sistema de mensageria.

Criar consumidor para eventos: se Ano == 2024, salvar notificação no banco de dados para consulta futura.

2. Consultar Motos

Como admin

Quero consultar as motos cadastradas

Para gerenciar os veículos disponíveis

Regras:

Permitir listagem completa.

Permitir filtro por placa.

3. Alterar Placa da Moto

Como admin

Quero alterar a placa de uma moto cadastrada incorretamente

Regras:

Apenas o campo placa pode ser alterado.

Validar unicidade da nova placa.

4. Remover Moto

Como admin

Quero remover uma moto cadastrada incorretamente

Regras:

Só pode remover se não houver registro de locações vinculadas.

🏍️ Usuário Entregador
5. Cadastro de Entregador

Como entregador

Quero me cadastrar na plataforma

Para poder alugar motos

Regras:

Dados obrigatórios: Identificador, Nome, CNPJ, Data de Nascimento, Número da CNH, Tipo da CNH, Imagem CNH.

Tipos de CNH válidos: A, B ou A+B.

CNPJ deve ser único.

Número da CNH deve ser único.

Foto da CNH deve ser enviada em formato PNG ou BMP.

Foto não pode ser armazenada no banco → salvar em Storage (local, S3, MinIO etc.).

6. Atualizar Foto da CNH

Como entregador

Quero enviar uma nova foto da CNH

Regras:

Formato permitido: PNG ou BMP.

Deve sobrescrever a imagem anterior no storage.

7. Alugar Moto

Como entregador

Quero alugar uma moto por um período

Regras:

Apenas entregadores com CNH categoria A podem alugar.

Planos disponíveis:

7 dias → R$ 30/dia

15 dias → R$ 28/dia

30 dias → R$ 22/dia

45 dias → R$ 20/dia

50 dias → R$ 18/dia

Datas obrigatórias: Data de Início, Data Prevista de Término.

Data de Início = primeiro dia após a criação da locação.

8. Devolver Moto

Como entregador

Quero informar a data de devolução da moto

Para calcular o valor total da locação

Regras:

Se devolver antes do previsto → cobrar diárias + multa sobre diárias não utilizadas:

Plano 7 dias → multa 20%

Plano 15 dias → multa 40%

Se devolver após o previsto → cobrar R$ 50,00 por diária adicional.

Valor final = diárias + multa/extra (se aplicável).