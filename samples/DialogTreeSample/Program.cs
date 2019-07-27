using System;

namespace DialogTreeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the dialog tree.
            var dialogTree = CreateDialogTree();

            // Create a running instance of the tree.
            var tree = new DialogTreeRunner(
                dialogTree
                textProcessors: null
            );

            do
            {
                if (tree.IsChoice)
                {
                    if (tree.CurrentNodes.Length == 1)
                    {
                        var choice = tree.CurrentChoices[0];
                        Console.WriteLine($".{choice.Key}, {choice.Text}");
                        Console.ReadLine();
                    }
                    else
                    {
                        foreach (var choice in tree.CurrentNodes)
                        {
                            Console.WriteLine($".{choice.Key}, {choice.Text}");
                        }

                        var option = ConsoleReadNumber(tree.CurrentNodes.Length);
                        tree.Next(option);
                    }
                }
                else
                {
                    Console.WriteLine(tree.CurrentNodes[0].Text);
                    Console.ReadLine();
                    tree.Next();
                }
            } while (tree.IsActive);
        }

        static int ConsoleReadNumber(int max)
        {
            while (true)
            {
                Console.Write("Select: ");

                var input = Console.ReadLine();
                int.TryParse(input, out var number);
                if (number >= 0 || number < max)
                {
                    return number - 1;
                }
                else
                {
                    Console.WriteLine("Enter a valid number.");
                }
            }
        }

        static DialogTree CreateDialogTree()
        {
            var tree = new DialogTree();

            var r           = tree.AddNode("Hello there! How are you?");
            var r__1        = tree.AddChoice("I'm well, thank you.");
            var r__1_1      = tree.AddNode("That's great! Glad to hear it!");
            var r__2        = tree.AddChoice("Meh, been better.");
            var r__3        = tree.AddChoice("I'm grumpy.");
            var r__2_1      = tree.AddNode("I'm sorry to hear that.");

            tree.AddLink(r, r__1, r__2, r__3);
            tree.AddLink(r__1, r__1_1);
            tree.AddLink(r__2, r__2_1);
            tree.AddLink(r__3, r__2_1);

            var r_2         = tree.AddChoice("So who are you anyway?");
            var r_2__1      = tree.AddNode("I'm the dialog guy of course!");

            tree.AddLink(r_2, r_2__1);

            var r_3         = tree.AddChoice("Investigate");
            var r_3__1      = tree.AddChoice("Nevermind.");
            var r_3__2      = tree.AddChoice("What is your favorite color?");
            var r_3__2_1    = tree.AddNode("Red.");
            var r_3__3      = tree.AddChoice("How long have you been here?");
            var r_3__3_1    = tree.AddNode("A while.");

            var r_4         = tree.AddNode("Is there anything else I can do for you?");

            tree.AddLink(r_2__1, r_3);
            tree.AddLink(r_3, r_3__1, r_3__2, r_3__3);
            
            tree.AddLink(r_3__1, r_4);

            tree.AddLink(r_3__2, r_3__2_1);
            tree.AddLink(r_3__2_1, r_3);

            tree.AddLink(r_3__3, r_3__3_1);
            tree.AddLink(r_3__3_1, r_3);

            var r_exit      = tree.AddChoice("Goodbye.");

            tree.AddLink(r_3, r_exit);
            tree.AddLink(r_4, r_exit);

            return tree;
        }
    }
}