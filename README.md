# Chain Reaction
- A clone of the original mobile Chain-Reaction game.
- If Orb is in Unstable state, clicking on it will explode the Orb, trigger a chain reaction.
- Upto 4 players can play this game. Goal is to be the last one standing. Good Luck !
- **Playable WEBGL Link** :


### Features :
- **Top Down Code Architecture**. Grid -> Tile -> Orb
- **Services** are the highest in hierarchy and talk with each Other. (PlayerManager, GridService, UIService, LobbyService, AudioService)
- Customized **State Machines** for Orb Type (SINGLE, DOUBLE, TRIPLE) and Orb Status (STABLE, UNSTABLE).
- Customized **Object Pool** to simulate Orb Travelling after triggering Chain Reaction.
- To simulate chain reaction, **BFS Neighbour - based exploding** is implemented (Nested + Chaining Coroutines).


### Demo :