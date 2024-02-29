# MouseMaze
a small exercise in random maze generation and pathfinding in the C# [MonoGame](https://monogame.net/) framework

## Demo
![](Content/preview1.gif)

## How It Works
Upon launch, the maze is generated using with an [iterative implementation](https://en.wikipedia.org/wiki/Maze_generation_algorithm#Iterative_implementation_(with_stack)) of the [randomized depth-first search](https://en.wikipedia.org/wiki/Depth-first_search) algorithm.
Mice can then be spawned in the maze with the right mouse button, and cheese can be spawned with the left mouse button.
If there is no cheese in the maze, the mice will wander around randomly with the [random mouse algorithm](https://en.wikipedia.org/wiki/Maze-solving_algorithm#Random_mouse_algorithm).
Otherwise, if there is cheese in the maze, mice will use a [recursive depth-first search](https://en.wikipedia.org/wiki/Maze_solving_algorithm#Recursive_algorithm) to determine a path to the cheese.
### Mouse Behavior
[![](https://mermaid.ink/img/pako:eNpdkt1Kw0AQhV9lmEtJS5o0f4vUCwX1oiIIiroiSzK1wXS2JFu1lr67u4lt0yYQNnNmv3OS2Q3muiAU2Bhl6KpUH7VaDL4CyZJfz95gMJjA5ZyoIfvMPyW3fVDO3rvF-Xk-12VOk8mpFBxpPYZj7poE3DZg5lQTKF7_O4FmV4OpWl5I3lu5KE-KC6oF3OkTods51V8EAp6pcfnd3av3bAMB98rM4cGUVQWPqiqLnlPQEvusY6Vj3qzYdEEks7YxKpoZ0LOeLBl61zUx1RbSgAKm7zbBoYO4AIdxuO4rnZcdgeRd-vblQN-X0MMF1QtVFnaOG4eUaP_fgiQKuyxU_SlR8tb2qZXRD2vOUZh6RR6ulsVh7ChmqmpslYrS6HraHYz2fHi4VIxigz8oRkEyDMNxNo7iJE1Gfjb2cI0ijIZBnKZhnCWRnyVxFG89_NXaYv1hGiWpn_mjOAiSKBmHLe-lFVvP7R_l69F3?type=png)](https://mermaid.live/edit#pako:eNpdkt1Kw0AQhV9lmEtJS5o0f4vUCwX1oiIIiroiSzK1wXS2JFu1lr67u4lt0yYQNnNmv3OS2Q3muiAU2Bhl6KpUH7VaDL4CyZJfz95gMJjA5ZyoIfvMPyW3fVDO3rvF-Xk-12VOk8mpFBxpPYZj7poE3DZg5lQTKF7_O4FmV4OpWl5I3lu5KE-KC6oF3OkTods51V8EAp6pcfnd3av3bAMB98rM4cGUVQWPqiqLnlPQEvusY6Vj3qzYdEEks7YxKpoZ0LOeLBl61zUx1RbSgAKm7zbBoYO4AIdxuO4rnZcdgeRd-vblQN-X0MMF1QtVFnaOG4eUaP_fgiQKuyxU_SlR8tb2qZXRD2vOUZh6RR6ulsVh7ChmqmpslYrS6HraHYz2fHi4VIxigz8oRkEyDMNxNo7iJE1Gfjb2cI0ijIZBnKZhnCWRnyVxFG89_NXaYv1hGiWpn_mjOAiSKBmHLe-lFVvP7R_l69F3)

## Credits
This project utilizes the [Primitive Buddy](https://www.nuget.org/packages/PrimitiveBuddy/) library for rendering primitives.