# OpenRpg Genres

The `Genres` project is a layering/contractual approach that allows you to express your own conventions within a shareable/re-usable library.

> For example out the box we provide `Fantasy` and `SciFi` genre libraries which provide loads of out the box helper methods/properties to objects.

## Why do I need a Genre?

You maybe don't, but if we take a step back and think about more RPG games, there are some common themes/settings/mechanics at play, for example if is list some common `Fantasy` themes:
- They often have some form of *Human*, *Dwarf*, *Elf* `Races`
- They often have some form of *Fighter*, *Rogue*, *Mage* `Classes`
- They often have the notion of `Health` and `Magic`
- They often have different kinds of `Damage` like *Blunt*, *Fire*, *Wind*, *Piercing*
- You can often equip *weapons*, *chest/leg/shoulder/hand* items
- You can often modify your equipment with *runes/gems/enchantments*

Now imagine I asked you what game/setting im talking about with those above notions? It is impossible to answer right?

These notions could apply to so many RPG games like:
- Dark Souls
- World of Warcraft
- Diablo
- Baldur's Gate
- Final Fantasy <insert number>

This is kinda the point of this framework and the `Genres` project, it provides out the box basics for a given type/genre of game and its up to you to use as much/little as you want.

The other **REALLY IMPORTANT** thing it provides is a contract so everyone using the same `Genre` would have the same stat types at the same indexes, and the same high level extended API available making it easier to re-use code between projects and encapsulate and share behaviour/data extensions.

## The `OpenRpg.Genres.Fantasy` Project

As mentioned above this provides some out the box functionality and type data for common Fantasy themes, so at a high level things like:
- Fantasy Stat Types (Strength, Dexterity etc)
- Fantasy State Types (Health, Magic)
- Fantasy Damage/Defense Types (Fire, Ice, Wind, Slash, Blunt etc)
- Fantasy Effect Types (Strength Bonus Amount/Percentage, Magic Restore Speed etc)

> There is so much more and its recommended you check over the classes in the `Types`, `Extensions` namespaces for exact additions this library provides.

The idea with these `Genres` is you just include it in your project and it automatically adds extensions onto `Entities` and related objects, for example:

```csharp
var someEntity = new Entity();
someEntity.Stats.Strength(10); // Sets strength to 10
var baseIceDamage = someEntity.Stats.IceDamage(); // Gets base ice damage
```

If we looked in the `Genres` source code you would find something like this:

```csharp
// Wraps manually getting/setting the stat with the id of strength (which is 60 if you look at the stat type code)
public static void Strength(this EntityStatsVariables stats, int value) => stats[FantasyEntityStatsVariableTypes.Strength] = value;
public static int Strength(this EntityStatsVariables stats) => (int)stats.Get(FantasyEntityStatsVariableTypes.Strength);
```

In a way think of these `Genres` like additive plugins you can add to your project, maybe you want a `Fantasy` setting with `TowerDefense` mechanics, or a `SciFi` setting with `Tactics` mechanics. It should be as easy as just pulling in the pre-made `Genres` which add the high level contractual 

> There are `TowerDefence` and `Tactics` Genres provided outside of the main repo which you can look at for inspiration etc.