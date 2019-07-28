﻿using System;
using Mallos.Ai.Dialog;

namespace DialogTreeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the dialog tree.
            var tree = CreateDialogTree();

            // Create a running instance of the tree.
            var runner = new DialogTreeRunner(
                tree,
                textProcessors: null
            );

            runner.Next();

            while (runner.IsActive)
            {
                Console.WriteLine(runner.CurrentText);
                Console.WriteLine();

                if (runner.CurrentOptions != null)
                {
                    for (int i = 0; i < runner.CurrentOptions.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {runner.CurrentOptions[i]}");
                    }

                    if (runner.CurrentOptions.Length == 1)
                    {
                        Console.ReadLine();
                        runner.Next();
                    }
                    else
                    {
                        var option = ConsoleReadNumber(runner.CurrentOptions.Length);
                        runner.Next(option);
                    }
                }
                else
                {
                    Console.ReadLine();
                    runner.Next();
                }

                Console.Clear();
            }
        }

        static int ConsoleReadNumber(int max)
        {
            while (true)
            {
                Console.WriteLine();
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

            tree.AddLink(r__1_1, r_2);
            tree.AddLink(r__2_1, r_2);
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