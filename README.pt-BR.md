# Online AutoChess

- [ English ](README.md)
- Português

## Sobre

Este projeto foi desenvolvido para o curso de Engenharia de Software da UniSenai. O objetivo é criar um jogo de auto chess online, onde os jogadores podem competir entre si em tempo real.

## Guia de Configuração

```
dotnet run --project server/Server/Server.csproj
```

Cliente recomendado para testar o servidor:

```
git clone https://github.com/Thinato/SocketClient.git
```

### Pré-requisitos

- dotnet-host >= 8.0
- dotnet-runtime >= 8.0
- dotnet-sdk >= 8.0
- python >= 3.7
- docker >= 27.3 _(opcional)_

## Funcionalidades

Este é o roadmap atual para o projeto:

### Servidor

- [x] **Gerenciamento de Jogadores**
- [x] **Matchmaking**
- [ ] **Adicionar Servidor de Jogo**

### Servidor de Jogo

- [ ] **Heróis**
- [ ] **Sistema de Batalha em Tempo Real**
- [ ] **Itens**

## Leitura Adicional

Aqui estão todos os recursos que utilizamos para desenvolver este projeto:

- [NR-CORE](https://github.com/cp-nilly/NR-CORE) - utilizado para o Networking
- [Fiabiano Swagger of Doom](https://github.com/ossimc82/fabiano-swagger-of-doom) - utilizado para o Networking
- [AutoDwarves](https://github.com/Thinato/AutoDwarves) - utilizado para o cliente em Pygame e a lógica do jogo no Servidor de Jogo
- [Pygame-Typing](https://github.com/Thinato/pygame-typing) - utilizado para o cliente em Pygame e documentação
