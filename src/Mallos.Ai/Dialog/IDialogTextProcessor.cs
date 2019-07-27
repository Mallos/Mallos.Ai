namespace Mallos.Ai.Dialog
{
    public interface IDialogTextProcessor
    {
        string Process(string text, Blackboard blackboard);
    }
}
