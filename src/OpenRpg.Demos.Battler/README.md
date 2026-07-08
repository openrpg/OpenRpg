# OpenRpg Battler Demo

A retro turn-based combat demo built with **MonoGame** + **Gum** on top of the **OpenRpg** framework.

The demo exists to show how OpenRpg's data-driven systems work in practice: game content is defined entirely in JSON files, loaded at startup, and then everything from stats to abilities to combat flows through OpenRpg related functionality.

Controls during battle:
| Key | Action |
|-----|--------|
| **Up / Down** | Navigate menu items |
| **Enter / Space** | Confirm selection |
| **Escape / Back** | Go back, or default attack on first enemy |
| **Space / Enter** | (On game over screen) Restart battle |

## How It Uses OpenRpg

### The Game Data Pipeline

If you have not used the editor before then take a look at that, it's a great way to create and edit game content.

This makes use of a project file that describes the rpg content, and loads it at runtime into the underlying data sources (Locale and Templates).

All game content lives in `Content/Project/`:

```
Content/Project/
├── project.json                # Project manifest (version, plugins)
├── locales/
│   └── en-gb.json              # Strings with locale IDs
└── templates/
    ├── RaceTemplate.json       # Race definitions (e.g. Human)
    ├── ClassTemplate.json      # Character classes (Warrior, Monk, etc.)
    ├── EntityTemplate.json     # Monster templates
    ├── ItemTemplate.json       # Items
    └── AbilityTemplate.json    # Abilities (Slash, FireBolt, etc.)
```

At startup, `BattlerGame` loads `project.json` via the `FileDataLoader` service. This triggers the JSON pipeline:

1. **JsonTemplateLoader** reads each template file and deserializes JSON into typed objects using Newtonsoft.Json with OpenRpg's custom `VariablesConverter`
2. **JsonTemplateDatastorePopulator** populates the `InMemoryDataSource` with RaceTemplate, ClassTemplate, EntityTemplate, ItemTemplate, and AbilityTemplate instances
3. **InMemoryDataSource** becomes the central `IDataSource` service — the rest of the game queries it by ID to look up templates

> Some of this information is only relevant if you are using the JSON related project loader, you can store the project data in any format as long as there is a suitable loader available
> or even bypass the loader entirely and just populate the `IDataSource` directly with your own data at runtime.

Feel free to change the data inside the project, for example if you want to change how much HP a Warrior has? Edit the `HealthBonusAmount` effect in `ClassTemplate.json`. Want to add a new ability? Add an entry to `AbilityTemplate.json`.

### How Templates Become Characters

When a battle starts:

**Party members** go through `GameCharacterBuilder` (a subclass of `FantasyCharacterBuilder`):
1. Creates a `Character` with a Race ID (Human) and Class ID (e.g. Warrior)
2. The **CharacterEffectProcessor** computes effects from the class template (stat bonuses, mana bonuses, etc.)
3. **FantasyStatsPopulator** converts those effects into concrete stats: MaxHealth, Damage, MovementSpeed, MaxMana, attributes, etc.
4. **FantasyStatePopulator** sets initial state: Health = MaxHealth, Mana = MaxMana
5. Abilities from the class template are copied to the character's Variables

**Monsters** use the same pipeline but via `ICharacterPopulator.Populate()` directly with an `EntityTemplate`.

> We currently dont use Race/Class on the monsters EntityTemplate, but the system supports it if you wanted to give entities (monsters or npcs) specific races/classes/equipment etc.

### The DI Wiring

The demo wires everything up in `Program.cs` using four DI related extension methods:

```csharp
.WithOpenRpgFramework()   // Battle services, generators, populators
.WithOpenRpgProject()     // JSON data pipeline, InMemoryDataSource
.WithMonoGameServices()   // MonoGame wrappers (SpriteBatch, Content)
.WithSceneServices()      // Scene management, party/enemy providers, BattleScene
```
## Code Architecture Tour

```
Code/
├── Program.cs                    # Entry point — DI setup, kicks off the game
├── BattlerGame.cs                # Game loop: loads project, delegates to SceneManager
├── Builders/
│   └── GameCharacterBuilder.cs   # FantasyCharacterBuilder subclass (copies AssetCode)
├── Extensions/
│   └── IServiceCollectionExtensions.cs   # All DI registration methods
├── Scenes/
│   ├── IScene.cs / ISceneManager.cs / SceneManager.cs   # Scene lifecycle abstraction
│   └── Battle/                   # Everything battle-related
│       ├── BattleScene.cs        # Orchestrator — wires providers, UI, combat together
│       ├── Combat/
│       │   └── TurnManager.cs    # Turn state machine + ability/attack execution
│       ├── Models/
│       │   ├── BattleEntity.cs   # Wraps a Character with position, sprite, HP delegation
│       │   ├── PlayerAction.cs   # Data class for player commands
│       │   ├── FloatingDamageNumber.cs  # Float-up damage text animation
│       │   ├── Team.cs           # Player / Enemy enum
│       │   └── ActionType.cs     # BasicAttack / Ability / UseItem / Flee enum
│       ├── Providers/
│       │   ├── IPartyProvider.cs / PartyProvider.cs          # Builds 4 random party members
│       │   └── IEnemyFormationProvider.cs / EnemyFormationProvider.cs  # Builds 6 monsters
│       ├── Rendering/
│       │   ├── EntityRenderer.cs # Draws sprites, HP bars, arrows, backgrounds
│       │   ├── SpriteCache.cs    # Loads and caches textures from ContentManager
│       │   └── Palette.cs        # Shared color constants
│       └── UI/
│           ├── CombatLogUi.cs    # Action message strip
│           ├── TurnOrderUi.cs    # Turn order chips
│           ├── BattleBottomPanelUi.cs  # HP/MP bar panel
│           ├── CommandMenuUi.cs  # Fight / Ability / Target selection menus
│           ├── TextHelper.cs     # Kenney Pixel space-width workaround
│           ├── GumExtensions.cs  # SetRectColor() helper
│           ├── NameHelper.cs     # PascalCase → spaced words
│           └── InputHelper.cs    # Keyboard edge detection
├── Services/Game/
│   └── IGameServices.cs / GameServices.cs   # MonoGame service bag
└── Types/
    ├── ClassLookups.cs           # Class ID constants + GetRandomPartyIds()
    └── RaceLookups.cs            # Race ID constants
```

### How a Turn Works

This isnt really specific to OpenRpg, but it's a good example of how the combat system works.

```
Idle
  → AdvanceTurn (find next alive entity by Initiative order)
       ├─ Party turn → PlayerInput (keyboard menu via CommandMenuUi)
       │    └─ BattleScene calls SubmitPlayerAction() → ExecuteAction()
       └─ Enemy turn → TurnDwell (0.8s auto)
       
  TurnDwell (0.8s): gold arrow bobs above attacker
       └─ ExecuteAction():
            ├─ Player command: BasicAttack → ExecuteBasicAttack()
            │                     or Ability  → ExecuteAbility()
            └─ Enemy: auto basic attack with random target

  ExecuteAbility:
       ├─ Clone template Damage object
       ├─ FantasyAttackGenerator.GenerateAttack() → adds stat bonuses, ±5%, crit check
       ├─ DefaultAttackProcessor.ProcessAttack()  → subtracts target defenses
       ├─ Deduct HP (min 1), deduct mana
       └─ Set LastActionMessage

  → TargetFlash (0.3s): red flash on hit targets
  → CheckGameOver → if both alive, back to Idle
```

### The Attack Pipeline

All damage flows through OpenRpg's combat pipeline:

```
Damage { Type: Fire(80), Value: 22 }
  → FantasyAttackGenerator
       ├─ Adds attacker's FireDamage stat bonus
       ├─ ±5% random variance
       ├─ Critical hit check (multiply by CriticalDamageMultiplier)
       └─ Returns Attack (array of typed damages + IsCritical flag)
  → DefaultAttackProcessor
       ├─ Looks up target's defense for each damage type
       ├─ damage = max(0, damage - defense)
       └─ Returns ProcessedAttack with final DamageDone
```

This means an ability like **FireBolt** (Fire damage 22) will also benefit from any +FireDamage stats the caster has, and the target's FireDefense will reduce it.

---

## How to Customize the Demo

### Add a New Class
1. Add an entry to `Content/Project/templates/ClassTemplate.json` with a unique ID, name locale ID, asset code, and effects (stat bonuses, mana)
2. Add its sprite to `Content/Sprites/Players/` and add a Content Pipeline entry in `Content.mgcb`
3. Add the class ID constant to `Code/Types/ClassLookups.cs`
4. (Optionally) Create abilities that require this class in `AbilityTemplate.json`

### Add a New Monster
1. Add an entry to `Content/Project/templates/EntityTemplate.json` with a unique ID, name, asset code, and effects
2. Add its sprite to `Content/Sprites/Enemies/` and add a Content Pipeline entry

### Add a New Ability
1. Add an entry to `Content/Project/templates/AbilityTemplate.json` with damage, target type/count, mana cost.
2. Ensure the class template lists this ability ID in its `Variables.Abilities` array

> We currently have requirements in place on abilities so that they can only be used by a specific class, but this isnt mandatory.

### Tweak Stats
All stats come from **effects** on templates. For example, the Warrior class has an effect with type `HealthBonusAmount` (60) which gives +60 MaxHP. Change the value there, or add more effects (Strength, Damage, etc.).