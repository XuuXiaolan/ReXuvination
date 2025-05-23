# ReXuvination

ReXuvination is a performance mod.
If you're interested in helping with the development of the mod, feel free to reach out to `xuxiaolan` on the Lethal Modding Discord!

## Current Features

- [BENCHMARKED] Optimises EnemyAICollisionDetect colliders.
  - Currently reduces calls to EnemyAICollisionDetect.OnTriggerStay by about half.
  - Tested with spawning 200 enemies and seeing how many calls to EnemyAICollisionDetect.OnTriggerStay are done, without the optimisation the calls averaged between 1200 and 2400, but with the optimisation it averaged around 600 and 1300 instead.
  - The optimisation will be more effective depending on the enemies on the map, but in certain cases now the calls are reduced to almost none.
- [NOT BENCHMARKED (BUT SHOULD STILL BE HELPFUL)] Optimises PlayerPhysicsRegions, BridgeTrigger, DoorLock, QuicksandTrigger, SandSpiderWebTrap, BridgeTriggerType2 (that's the second adamance bridge), CompanyMonsterCollisionDetect, GrabbableObjectPhysicsTrigger, InteractTrigger, KillLocalPlayer, Landmine, OutOfBoundsTrigger, TerrainObstacleTrigger and ToggleFogTrigger colliders.

## Upcoming Features

- Might look into optimising animators a bit.

### Credits

- Preloader help and template: mattymatty
- Icon: Mr. Salted Beef
