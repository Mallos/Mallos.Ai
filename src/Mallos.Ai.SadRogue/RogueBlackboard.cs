namespace Mallos.Ai
{
    using GoRogue.GameFramework;
    using SadConsole;

    public class RogueBlackboard : Blackboard
    {
        public RogueBlackboard(Map map, BasicEntity entity)
        {
            this.Map = map;
            this.Entity = entity;
        }

        public BasicEntity Entity { get; set; }

        public Map Map { get; set; }
    }
}
