# ReXuvination

ReXuvination is a performance mod.
If you're interested in helping with the development of the mod, feel free to reach out to `xuxiaolan` on the Lethal Modding Discord!

## Current Features

- Optimises EnemyAICollisionDetect colliders.
  - Currently reduces calls to EnemyAICollisionDetect.OnTriggerStay by about half.
  - Tested with spawning 200 enemies and seeing how many calls to EnemyAICollisionDetect.OnTriggerStay are done, without the optimisation the calls averaged 1200~2400, with the optimisation it averaged 600~1300.

## Upcoming Features

- Might look into optimising animators a bit.
- PlayerPhysicsRegion colliders could also do with being optimised.
