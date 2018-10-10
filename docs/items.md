# OpenRpg.Items

This library provides item/inventory related functionality, rather than providing a singular `Item` class it provides the pieces needed for you to build up your own item conventions.

For example you may want your game to constrain inventory based upon weight, slots or even no limit, so rather than have one class which does everything there are various interfaces and default implementations that should provide the building blocks for most scenarios.

---

## Item related

### `IItemTemplate`

An item template is generally the blueprint for an item, it describes the items type, requirements, effects etc and doesnt concern itself with how many of those items you actually have etc. It provides the common template information that all items would potentially need, there are other custom interfaces on top for specific scenarios like `IWeightedItemTemplate` etc.

There should only be 1 instance of a given items template in the game at once, for example you may have a `long sword` item template which describes what a long sword is, and a template for a `health potion` which describes how much health the potion should give you and that its a useable item. Now from here you may have 5 long swords and 50 health potions in your inventory, but all those long swords and potions would share the same underlying template.

This way we separate the notion of a PHYSICAL item in the game from its descriptive TEMPLATE, this makes it far easier to manage items in the long run as you can have a database of item templates, and if you update the template, then all instances of that item in the world would be updated, rather than each item instance in the game containing this same data.

### `IModificationAllowance`

Some games allow you to modify items, such as improving armour/weapons etc or even randomly generating items with varying properties. In these scenarios most items have an allowance on what can be modded, be it a weapon may allow 1 enchantment and 3 gems to be slotted, or a body suit that lets you equip 5 mechanical enhancements.

Whatever your scenario the allowance describes the type of modification allowed and how many are allowed, these then pair with `IModification` interfaces which adhere to a given type.

### `IModification`

The modification describes what the type of modification is and what effects it gives, so like in the allowance scenario if you had a weapon which allowed 1 enchantment and 3 gems, you would have modifications that were of type enchantment and gems (or whatever your types end up being), so you could have 50 different types of gems in your game with varying effects, and your sword would let you put up to 3 of them in, and you could have hundreds of enchantments but you can only use one in the item. 

This separation allows you to store all possible modifications ahead of time, and then apply them to varying many items based on the templates allowance, and again if you decided to update the effects of a modification, these changes would cascade through the game.

### `IItem`

This represents an ACTUAL item/s, so you may have a flaming longsword or a broken nano suit, these would basically wrap the underlying template for a longsword and nano suit and then apply varying modifications to buff/debuff the actual instance of the item. You may also have stackable items, so if you had 50 health potions, this may actually just be 1 item with an amount of 50, so if you were to get another potion it would just bump the amount rather than create a new instance.

This way you can have a few core types of items/equipment templates in the game an optionally provide random enhancements like many dungeon crawling games do, i.e have a core longsword template but when an enemy drops it run it through a processor of some kind which adds a random +/- effect to it, items also allow naming overrides so you could potentially override the items name to be "Flaming longsword" without altering the underlying item template. Giving you lots of flexibility if you want to do this sort of thing without having to move mountains under the hood.

---

More to come later
