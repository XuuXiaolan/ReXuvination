# v1.1.0

- Change the system to be instead of excluding layers I only include essential layers, this means that other mods just need to the include layers if they want to add some collisions that arent normally in vanilla.
- Added null checks for mods that access some stuff too early before StartOfRound is created.

## v1.0.1

- Fixed an issue with Colliders and enemies not being able doo due to priority.

## v1.0.0

- Optimised all I could with every base game collision type, added optimisation patches for the following:
  - BridgeTriggerType2
  - GrabbableObjectPhysicsTrigger
  - InteractTrigger
  - KillLocalPlayer
  - Landmine
  - OutOfBoundsTrigger
  - TerrainObstacleTrigger
  - ToggleFogTrigger
  - CompanyMonsterCollisionDetect.

## v0.4.0

- Optimised `EnemyAICollisionDetect` even further by making it so that enemies that don't typically collide with other enemies don't send eachother callbacks unless there actually is an interaction.
- i.e. a bunch of hoarding bugs next to eachother won't be spamming eachother unnecessary collision callbacks because they can't collide with eachother anymore.

## v0.3.1

- Fixed mod not being properly clientside thanks to ButteryStancakes.

## v0.3.0

- Added optimisation patches for `SandSpiderWebTrap`, `QuicksandTrigger`, `DoorLock` and `BridgeTrigger`.

## v0.2.0

- Added the optimisation patch for `PlayerPhysicsRegions`.

## v0.1.1

- I forgor to do the thing.

## v0.1.0

- Fixed another oopsie with potentially hazards and vehicle interactions with enemies.

## v0.0.4

- New Icon.
- Fixed an oopsie that caused enemies to be unable to open doors.

## v0.0.3

- Added null check to enemy prefab.

## v0.0.2

- Fixed README being stupid with markdown smh.
- Added a blacklist config incase an enemy was being special with it's colliders that are next to EnemyAICollisionDetect components.

## v0.0.1

- Initial Release
