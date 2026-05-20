# Архитектура проекта

```mermaid
classDiagram
    class GameManager {
        <<Singleton>>
        -static GameManager _instance
        -bool isRunning
        +static GameManager Instance
        +Run()
    }
    
    class Map {
        +int Width
        +int Height
        +IsInsideBounds(x, y) bool
    }
    
    class Entity {
        +string Name
        +int Health
    }
    
    class Player {
        +int Score
        +int X
        +int Y
        +Move(deltaX, deltaY)
    }
    
    class Enemy {
        +int Damage
    }

    Player --|> Entity : Is-A
    Enemy --|> Entity : Is-A
    GameManager --> Map : Uses
    GameManager "1" o-- "1" Player : Has-A
    GameManager "1" o-- "1" Enemy : Has-A