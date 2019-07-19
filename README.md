# Mallos.Ai
A lightweight AI library designed for Games.

## Features
- [x] Behavior Tree
- [ ] Dialog Tree
- [ ] Advanced Insight

### Supported Frameworks
* [SadConsole with GoRogue](https://github.com/thesadrogue/SadConsole.GoRogueHelpers)

## Getting Started

#### Blackboard
The blackboard is an esential part of Mallos.Ai, it is a central place to store and look up data related to the BehaviorTree.

As some BehaviorTree nodes require time to function there is a `ElapsedTime` property on the Blackboard which defined how long time it took since the last call. If you are making a turned based game it's a good idea to make it a constant time which is counted every turn.

The Blackboard also have a directory for storing anything that the nodes might want to share data between them. In the example below we store the coordinates where the player was seen and in the `NavigateNode` we say nagivate to those coordinates.

#### Create BehaviorTree
File: [GoRogueSample/Monsters/Monster.cs](https://github.com/erictuvesson/Mallos.Ai/blob/master/samples/GoRogueSample/Monsters/Monster.cs#L40-L55)
```cs
new BehaviorTree(
    new ParallelSequenceNode(
        // Check if we can see the player.
        new EnvironmentQueryNode(
            entity => FOVRadius,            // Entity View Radius.
            entity => entity is Player,     // Check if it is a player.
            spottedKey: "SeePlayer",        // Set a blackboard property with true or false.
            spottedCoordKey: "WalkTo"       // Set a blackboard property with the found coords.
        ),
        new ConditionalNode(
            blackboard => blackboard.GetProperty<bool>("SeePlayer"),
            new NavigateNode("WalkTo"),     // Go to Player
            new WanderNode()                // Wander around
        )
    )
);
```

## Contributing
Contributions are always welcome.

## License
The project is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
