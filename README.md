# Wave Spawner

Unity 6000.3.25f1 · URP · Input System

## Wave Configuration
`Assets/Configs/WavesConfig.asset` holds one entry per wave: enemy count, spawn interval, and delay before the next wave.

## Architecture
| Class | Responsibility |
|---|---|
| `WavesConfig` / `WaveSettings` | Wave data (ScriptableObject), read-only at runtime |
| `WaveController` | Wave loop (coroutine), alive enemy counter, state events |
| `EnemySpawner` | Creates enemies via `ObjectPool<Enemy>`, picks the spawn position |
| `Enemy` | Handles taps (`IPointerClickHandler`), raises the `Died` event |
| `WaveUI` | Shows the wave number, alive enemy count and the completion screen |
| `RestartButton` | Reloads the scene |

References point one way only (UI → WaveController → EnemySpawner → Enemy); the reverse direction uses C# events only.

## Key Decisions
- **Coroutine**: the flow is linear and tied to the object's lifetime, so it stops automatically when the object is destroyed.
- **Wave completion**: a wave ends only after all its enemies have been spawned and all have been killed, because waiting for deaths starts after the spawn loop finishes.
- **ObjectPool**: enemies subscribe to `Died` once in `createFunc`, so reusing an enemy never duplicates subscriptions.
- **Restart via scene reload**: guarantees a clean state.
