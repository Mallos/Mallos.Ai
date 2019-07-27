namespace Mallos.Ai.Dialog
{
    using System;

    public interface IDialogTree
    {
        void AddProperty(Guid nodeKey, string key, object value);

        Guid AddNode(string text);

        Guid AddChoice(string text);

        void AddLink(Guid node, params Guid[] nodes);
    }
}
