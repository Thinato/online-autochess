# Client

feito em pygame

## Instalação

## Arquitetura

### ECS

O jogo eh baseado em ECS (Entity Component System), que eh um padrao de arquitetura de software para jogos. A ideia eh que o jogo eh composto por entidades, que sao basicamente objetos que existem no jogo, como o player, inimigos, etc. Cada entidade eh composta por componentes, que sao basicamente dados que definem o comportamento da entidade. Por exemplo, um componente de fisica, um componente de renderizacao, etc. Cada componente eh atualizado a cada frame do jogo, e pode interagir com outros componentes.

### Eventos

lorem

### Comunicação

lorem

### Engine

Essa pasta eh basicamente a lib do jogo, ela nao deve importar nada de fora, apenas o que esta dentro dela.

### UI

Elementos de interface grafica do usuario

### Scenes

Cenas do jogo, como menu, configuracoes, jogo, etc.

### Entities

Entidades do jogo, como player, inimigos, etc.

### Components

Componentes que podem ser adicionados a entidades, como fisica, renderizacao, etc.

## Sprites

Sprite sheet to json: https://asyed94.github.io/sprite-sheet-to-json/

