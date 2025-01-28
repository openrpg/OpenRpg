# OpenRpg

A cross platform set of building blocks to build RPG games.

[![Build Status][build-status-image]][build-status-url]
[![Nuget Version][nuget-image]][nuget-url]
[![Join Discord Chat][discord-image]][discord-url]
[![Try Demos][demo-image]][demo-url]

The framework tries to give you all the basic building blocks you need for any genre of RPG game, such as `Race`, `Class`, `Stats`, `Effects`, `Items`, `Inventory` etc.

There is a suite of `OpenRpg.Genre` based extensions which provide conventions on top of the basics to compose them in some "out the box" way for you to just get on and use stuff.

---
## A Quick Rundown

The library has loads of pre-made models that act as data containers for you to layer on your own conventions via extension methods (see the `Genres` for examples).

With this in mind at the highest level there are two main type of data:
- Templates (Static data, created and stored with game data)
- TemplateData (Runtime data, created during gameplay and stored with users data)

These both are different sides of the same object and are often both needed to be used together within the game but are stored and handled differently.

> For more information on this read the [Template Docs](docs/templates.md)

### Important Objects

There are 3 super important objects in this framework which make it nice to use, there is a lot more to it in terms of models etc and you can read about them more in the examples/docs etc, but this is a quick rundown on the key parts of the system.

> There is FAR more to the system than these 3 bits but most of the other bits build on top of these bits so knowing this stuff shows the potential of how you can have really flexible and extensible data models to build off.

#### Effects

Effects can be applied to almost everything and are used to derive variables for things, for example:

```csharp
var raceEffects = new[]
{
    new Effect {Potency = 8, EffectType = EffectTypes.StrengthBonusAmount},
    new Effect {Potency = 100, EffectType = EffectTypes.HealthBonusAmount},
};

var classEffects = new[]
{
    new Effect {Potency = 3, EffectType = EffectTypes.StrengthBonusAmount},
    new Effect {Potency = 1, EffectType = EffectTypes.DexterityBonusAmount},
    new Effect {Potency = 50, EffectType = EffectTypes.HealthBonusAmount}
};

var stats = new DefaultStats();
statsPopulator.Populate(stats, new [] { raceEffects, classEffects });
// stats contains variables str 11, dex 1, health 150
```
> Normally you would use `RaceTemplate` and `ClassTemplate` objects which wrap up the effects and subsequent related data for re-use via `IRepository` objects etc.

As you can see above an effect can describe any effect on an entity, and the effect types can be anything you want, the above examples are `EffectTypes` within the `OpenRpg.Genres.Fantasy` library, but each genre contains its own effect types and population logic to turn effects into variables.

> For more info view the [Demo Section On Stats/Effects](https://openrpg.github.io/OpenRpg)

#### Variables

Variables are fancy dictionaries that can notify you on changes and let you track any data you want for any scenario, the key convention we have here is that a singular `Variable` object just stores data against keys and we make it nicer to use via extension methods, here is an example:

```csharp
// Setup of models somewhere in project (Genre libs do this for you)
public class GameVariables : DefaultVariables<float>
{}

public interface GameVariableTypes
{
    public static int Money = 1;
    public static int LivesLeft = 2;
}

public static GameVariableExtensions
{
    public static float Money(this GameVariables vars) => vars[GameVariableTypes.Money];
    public static float Money(this GameVariables vars, float value) => vars[GameVariableTypes.Money] = value;
    public static float LivesLeft(this GameVariables vars) => vars[GameVariableTypes.LivesLeft];
    public static float LivesLeft(this GameVariables vars, float value) => vars[GameVariableTypes.LivesLeft] = value;
}

// Use code somewhere
var gameVariables = new GameVariables();
gameVariables.Money(100);
gameVariables.LivesLeft(3);
```

As shown above you can set these values however you want, **BUT** in a lot of cases you would want your values to be derived from `Effects` within contextual objects, i.e your characters `Stats` are just `IVariables<float>`, so you could use the `IStatPopulator` object to pass in all your effects and generate your stats for you, so that way when you equip a new item you can regenerate the stats and it will reflect the changes.

> This approach also makes it easier to separate runtime data from static data, as the effects are often setup within templates which are static, so you only need to persist your live data when saving your data.

#### Requirements

Requirements like `Effects` can be applied to lots of things within the framework, be it `items`, `quests`, `effects` etc and the `Requirement` object provides a mechanism to stop the entity being able to use the related data unless the requirement is met, this is simplest to visualize with quests.

```csharp
var someQuest = new DefaultQuest
{
    Id = 1,
    NameLocaleId = "A Simple Quest",
    DescriptionLocaleId = "An NPC needs you to collect 5 health potions, better get on the case!",
    IsRepeatable = false,
    Objectives = new List<Objective>
    {
        new Objective {ObjectiveType = ObjectiveTypes.ItemObjective, AssociatedId = ItemTemplateLookups.HealingPotion, AssociatedValue = 5}
    },
    Rewards = new List<Reward>
    {
        new Reward {RewardType = RewardTypes.ExperienceReward, AssociatedValue = 100},
        new Reward {RewardType = RewardTypes.GoldReward, AssociatedValue = 50}
    },
    Requirements = new List<Requirement>
    {
        new Requirement { RequirementType = RequirementTypes.StrengthRequirement, AssociatedValue = 5 }
    }
};
```

As you can see above we create a quest that requires you to have a strength of at least 5, however we could change that to a previous quest being complete, an item being in the inventory etc.

You can apply requirements to almost anything in the framework and then use an `IRequirementsChecker` to see if the current context meets the requirements.

> For more information and examples check [The Items & Requirements Demo Page](https://openrpg.github.io/OpenRpg)
---

## HALP! I NEED MORE INFORMATION?

You can join the [discord server](https://discord.gg/nKejjgT) via the `Chat` button at the top or you can go over to the web demos or local docs:
- [Local Docs](docs/core.md)

## Framework Components / Related Libs

There are a few other related libraries/applications which may be of use, such as pre-made genre libraries.

### Core Components

- [OpenRpg.Core](docs/core.md) This contains the core bits that most other parts of the systems use, such as `Requirements`, `Class`, `Effects` some data and locale related functionality.
- [OpenRpg.Items](docs/items.md) This contains models related to items, modifications, inventories, loot lists etc
- [OpenRpg.Combat](docs/combat.md) This wraps up basic models and logic for common combat scenarios, i.e attack data, combat outcome data etc
- [OpenRpg.Quests](docs/quests.md) Contains models around common quest data such as rewards, gifts, triggers etc
- [OpenRpg.Cards](docs/core.md) Contains models for card based games where you would wrap up items, quests, abilities etc as cards for the player to use.

### Genres/Extensions
- [OpenRpg.Genres](docs/genres.md) Contains conventions and extensions for custom genres to build off
- [OpenRpg.Genres.Fantasy](docs/genres.md) Contains common fantasy types and data
- [OpenRpg.Genres.SciFi](docs/genres.md) Contains common sci-fi types and data
- [OpenRpg.Genres.GameDevSim](https://github.com/openrpg/OpenRpg.Genres.GameDevSim) Contains game dev sim related effects and models
- [OpenRpg.Genres.Tactics](https://github.com/openrpg/OpenRpg.Genres.Tactics) Contains models and data related to strat rpg style games
- [OpenRpg.Genres.TowerDefense](https://github.com/openrpg/OpenRpg.Genres.TowerDefense) Contains models and data related to tower defense style games
- [OpenRpg.AdviceEngine](docs/advice-engine.md) A utility based AI framework for use with OpenRpg models/data

### Applications
- [OpenRpg.Editor](docs/editor.md) A simple electron.net based editor for items, classes, races etc
- [OpenRpg.Demos.Web](https://openrpg.github.io/OpenRpg) A Blazor application showing use cases for all the above functionality

---

## A bit about the lib

This project began after I had multiple prototypes which were all some form of RPG (a JRPG, Space Sim, a web/text based game) where they all had their own flavour of item, classes, quests etc but at the heart it was all really similar, so an attempt was made to begin streamlining the underlying parts of the system so it can be highly composable. This way I can take the bits I want for my scenario and build my own high level models based upon these ones.

So hopefully the same models/data and approaches can be of use to anyone else wanting to just take out the box data models/logic for common RPG data and just build on it.

[build-status-image]: https://github.com/openrpg/OpenRpg/actions/workflows/build-and-test.yml/badge.svg
[build-status-url]: https://github.com/openrpg/OpenRpg/actions/workflows/build-and-test.yml
[nuget-image]: https://img.shields.io/nuget/v/openrpg.core.svg
[nuget-url]: https://www.nuget.org/packages/OpenRpg.Core/
[discord-image]: https://img.shields.io/discord/488609938399297536.svg
[discord-url]: https://discord.gg/nKejjgT
[demo-image]: https://img.shields.io/badge/Demo-Site-informational.svg
[demo-url]: https://openrpg.github.io/OpenRpg/