# 3D Pac-Man Clone

![C#](https://img.shields.io/badge/C%23-8.0-blue)
![OpenTK](https://img.shields.io/badge/OpenTK-4.8-blue)
![OpenGL](https://img.shields.io/badge/OpenGL-4.6-red)

A 3D reimagining of the classic Pac-Man game with modern graphics and camera controls. Collect coins, avoid walls, and achieve high scores in a voxel-style environment.

## Key Features
- **3D Gameplay**:
  - Free-floating camera with WASD/mouse controls
  - Dynamic lighting and texture mapping
  - Spherical player/coin meshes with UV unwrapping
- **Core Mechanics**:
  - Coin collection system with score tracking
  - Grid-based movement (UDLR controls)
  - Collision detection with walls
- **Rendering**:
  - Custom shaders (vertex/fragment)
  - VAO/VBO/IBO buffer management
  - Texture loading via `StbImageSharp`
- **Map System**:
  - BMP-based level design (28x31 grid)
  - Auto-detection of walls/coins via pixel colors

## Technologies
- **Core**: C# + .NET 6
- **Graphics**: OpenTK 4.8 + OpenGL 4.6
- **Dependencies**:
  - `StbImageSharp` (texture loading)
  - GL mathematics (`OpenTK.Mathematics`)

## Project Structure
```text
.
├── Game.cs             # Main game loop/window
├── Camera.cs           # 3D camera logic
├── Map.cs              # Level loader (BMP → grid)
├── Player.cs           # Player controls/movement
├── Brick.cs            # Wall object implementation
├── Coin.cs             # Collectible item logic
├── Graphics/           # Rendering components
│   ├── VAO.cs
│   ├── VBO.cs
│   ├── IBO.cs
│   ├── Texture.cs
│   └── ShaderProgram.cs
└── Program.cs          # Entry point
```
## Controls
| Action               | Input                |
|----------------------|----------------------|
| Move Player          | Arrow Keys           |
| Move Camera          | WASD                 |
| Camera Look          | Mouse Movement       |
| Vertical Camera Move | Space/Left Shift     |
| Exit                 | ESC                  |

## Rendering Pipeline
1. **Mesh Generation**  
   - Procedural sphere generation for player/coins
   - Cube meshes for walls
2. **Shader Workflow**  
   - Vertex shader: `shader.vert`
   - Fragment shader: `shader.frag`
3. **Texture Mapping**  
   - UV coordinates calculated via spherical projection

## Performance
- Fixed 900x900 resolution
- VSync-enabled frame rate
- Depth testing for 3D rendering

## Limitations
- No enemy AI
- Basic collision detection (grid-based)
- Limited level customization (requires BMP editing)
- No sound effects

*Educational project - demonstrates 3D graphics fundamentals.*
