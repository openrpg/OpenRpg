# OpenRpg.AdviceEngine
A Utility AI flavoured framework for providing considerations to an agent and having them advise you on the best course of action.

## How is it different to Utility AI

The main difference is how it exposes the data to you for action purposes and manages utility variables in relation to other entities.

Utility AI in general scores actions and provides them to you for you to go off and carry out, our difference here is that we provide `Advice` which can be seen as a renamed `Action` or `Behaviour` from Utility AI however we provide context too as part of our `Advice`.

As we key Utility scores off both the `UtilityId` we also allow composite keying for a `RelatedId` which allows us a way to express more complex dependencies between entities.

For example lets say you had considerations for `EnemyDistance` rather than you just having a singular `EnemyDistance` variable or coming up with `EnemyDistance1`, `EnemyDistance2` etc you can instead just make use of the composite key approach to track as many enemy distances as you want by using a key like so:

```c#
var enemy1Key = new UtilityKey(UtilityVariables.EnemyDistance, enemy1.Id);
var enemy2Key = new UtilityKey(UtilityVariables.EnemyDistance, enemy2.Id);
```

This way you could query for all `EnemyDistance` variables and get back both scores and then choose to get the max, or you could choose to get the specific one for your entity.

> You do not **NEED** to use a composite key, it is up to you if you supply one or not, having a single utility variable indicates a 1-1 relationship, and having a composite key as well indicates a 1-N relationship

Now we can track multiple related things under a single variable we can then provide better advice to the `Agent`, so for example if we were to have a `GoAttack` advice object it would need to provide context as to WHAT TO ATTACK, so for example when you get your advice you not only would you get it scored but you also get the context as to **WHAT** to carry out the action on, i.e `GoAttack - Enemy1`.

## How it works?

So there are a few parts that make it all work, so lets run from bottom up:

### `UtilityVariables`
This is a slightly customized version of the normal `Variables` available within OpenRpg, it is keyed off a composite key style object known as a `UtilityKey`.

Ultimately all the `IConsideration` objects will read and write to this providing a suite of scored considerations to factor in.

#### `UtilityKey`
This is a simple key which requires a `UtilityId` (int) variable to indicate what the utility type is, but also allows an optional `RelatedId` which should be tied to another object, be it an enemy, spell, poisition or even an item etc.

This allows the utility to also provide context as to what it is considering, and is more dynamic as you can consider as many related objects as you wish.

### `IConsideration`
This contains the information required to take information in the game world and convert it into a utility value between 0-1 within the `UtilityVariables`.

There are a few different implementations of this that allow you to depend on any data you want, existing utilities etc and you can also write your own to wrap up your own conventions.

### `IAdvice`
This looks at the utilities derived from the `IConsideration` instances and then has a score applied as well as exposing a contextual object which relates to the advice.

For example it may be advice for the `Agent` themselves such as `TakeCover` or `GoHeal`, or it could be advice around what to do to other object such as `GoAttack - Enemy1`, `GoHeal - Ally22`, `HideAt - Position`.

### `IConsiderationHandler`
This manages the collation and updating of considerations within the `Agent`, so will automatically run at whatever interval/trigger it has been told and update the advice objects.

### `IAdviceHandler`
This manages tracking and updating the advice that is available to the `Agent`

### `IAgent`
This acts as the high level entry point for a lot of this, it wraps up the `IConsiderationHandler` and `IAdviceHandler` as well as the `UtilityVariables` and contains a link to the actual object it is processing on behalf of.

## How to use it?

Just pull it in via nuget with your other OpenRpg dependencies and add your agents to whatever objects require advice on what to do.