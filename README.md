# OpenRpg

An attempt to provide some out of the box models for RPG games that can be mixed and matched to allow you to get on with just making the game without worrying about the infrastructure. This way rather than providing one solution for everything, it just provides the building blocks for you to compose as you wish, so you can make your own character class which has a race, class and an inventory that is weight based, or you could make a character class which has no race, a list of classes so they can multi class and an inventory that is slot based.

It tries to not have any bias in terms of genre, i.e this system can be used in a fantasy setting, sci fi, space sim etc as the underlying data is almost always the same, you may end up being a space ship rather than a fantasy humanoid, or you may be having effects that enhance shields in space rather than armour in fantasy, but as its just all driven by configurable types that you provide yourself you are free to customize things how you see fit.

--- 

There are docs for each part of the system if you want to find out more

## [OpenRpg.Core](docs/core.md)

This contains the core bits that most other parts of the systems use, such as `Requirements`, `Class`, `Effects` some data and locale related functionality.

## [OpenRpg.Items](docs/items.md)

This contains models related to items, modifications, inventories, loot lists etc

---

This is still very much a work in progress, so feel free to make requests or PRs if you think more scenarios could be supported, going forward more stuff will be added around:

- Combat
- Quests/Missions
- Factions
- Buffs/Skills

Then hopefully some high level pre-made things for scenarios like

- Fantasy
- Scifi

---

This project began after I had multiple prototypes which were all some form of RPG (a JRPG, Space Sim, a web/text based game) where they all had their own flavour of item, classes, quests etc but at the heart it was all really similar, so an attempt was made to begin streamlining the underlying parts of the system so it can be highly composable. This way I can take the bits I want for my scenario and build my own high level models based upon these ones.

So hopefully the same models/data and approaches can be of use to anyone else wanting to just take out the box data models/logic for common RPG data and just build on it.
