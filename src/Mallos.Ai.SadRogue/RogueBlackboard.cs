namespace Mallos.Ai.SadRogue
{
    using GoRogue.GameFramework;

    public class RogueBlackboard<T> : Blackboard
    {
        public RogueBlackboard(Map map)
        {
            this.Map = map;
        }

        public Map Map { get; set; }
    }
}
