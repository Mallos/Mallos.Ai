namespace Mallos.Ai.Dialog
{
    public interface IDialogTextProcessor
    {
        string Process(string text, Blackboard blackboard);
    }

    public interface IDialogTreeRunner
    {
        bool IsActive { get; }

        bool IsChoice { get; }

        string[] CurrentOptions { get; }

        bool Next(int index = 0);
    }

    public class DialogTreeRunner : IDialogTreeRunner
    {
        public bool IsActive { get; private set; }

        public bool IsChoice { get; private set; }

        public string[] CurrentOptions { get; private set; }

        public DialogTreeRunner(
            DialogTree dialogTree,
            IDialogTextProcessor[] textProcessors = null)
        {
            
        }

        public bool Act(int index = 0)
        {
            return true;
        }
    }
}
