namespace Mallos.Ai
{
    public interface ITextProcessor
    {
        string Process(string text, Blackboard blackboard);
    }
}
