/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Author   :   James Peppas                                 *
 * Date     :   16/05/2023                                   *
 *                                                           *
 * Description :    Reqruiting task for Vulcan Forged        *
 *                                                           *
 * Completion Time  :   5:30h                                *
 *                                                           *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task.Classes;

namespace Dialogue
{
    public class Dialogue
    {
        static void PrintChoices()
        {
            Console.WriteLine("Choose your answer:");
            Console.WriteLine("1. Positive");
            Console.WriteLine("2. Negative");
            Console.Write("Enter: ");
        }
        static void CreateTree(DialogueResolver dr)
        {
            //****ROOT INSERT****
            dr.Insert("Mark", "Hello! Welcome to my shop! How can I assist you?", 10);

            //****POSITIVE DIALOGUE****
            dr.Insert("John", "Greetings! I am looking for a magic potion.", 5);
            dr.Insert("Mark", "Yes ofcourse! Here it is!", 7);
            dr.Insert("John", "How much does it cost?", 4);
            dr.Insert("Mark", "60 gold coins", 8);
            dr.Insert("John", "Here you go!", 3);
            dr.Insert("Mark", "Tank you very much! Come again!", 9);

            //****NEGATIVE DIALOGUE****
            dr.Insert("John", "Give me a magic potion and hurry up!", 15);
            dr.Insert("Mark", "Mind your tone! Here it is...", 11);
            dr.Insert("John", "Name your price...", 16);
            dr.Insert("Mark", "80 gold coins", 12);
            dr.Insert("John", "Mmhh", 17);
            dr.Insert("Mark", "Whatever..", 13);
        }

        public static void Main()
        {
            string ans;                             //The string to store the repsonse of the user
            int positive = 0, negative = 0;         //Positive and negative integers to count the responses
            DialogueResolver resolver = new DialogueResolver();
            DialogueNode deserialiser = new DialogueNode(null, null, 0);

            //If the save file exists we load the tree from file. Otherwise we create it from scratch.
            if (File.Exists("./data.save"))
                resolver.root = deserialiser.DeserialiseNode(typeof(DialogueNode), "./data.save") as DialogueNode;
            else
                CreateTree(resolver);
            
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
             * The following section will initiate the dialogue between    *
             * the user and the shopkeeper.                                *
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
            {
                //Start the dialogue
                Console.WriteLine(resolver.FindResponse(10));
                Console.WriteLine();

                //Provide the user with the style of answering
                PrintChoices();
                ans = Console.ReadLine();
                Console.WriteLine();

                //Positive or negative responses
                if (ans == "1")
                {
                    Console.WriteLine(resolver.FindResponse(5));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(7));
                    Console.WriteLine();

                    positive++;
                }
                else
                {
                    Console.WriteLine(resolver.FindResponse(15));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(11));
                    Console.WriteLine();

                    negative++;
                }

                PrintChoices();
                ans = Console.ReadLine();
                Console.WriteLine();

                //Then it continues
                if (ans == "1")
                {
                    Console.WriteLine(resolver.FindResponse(4));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(8));
                    Console.WriteLine();

                    positive++;
                }
                else
                {
                    Console.WriteLine(resolver.FindResponse(16));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(12));
                    Console.WriteLine();

                    negative++;
                }

                PrintChoices();
                ans = Console.ReadLine();
                Console.WriteLine();

            }
            /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
             * The following section will make the shopkeeper to chose how *
             * to reply based on how the user made their character speak to*
             * him. Even if the user changes the way he speaks, the        *
             * shopkeeper will 'remember'                                  *
             * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
            {
                //If the user chose positive or had a positive talk we continue positive
                if (ans == "1" && positive >= negative)
                {
                    Console.WriteLine(resolver.FindResponse(3));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(9));
                    Console.WriteLine();
                }//If the user chose negative but he had a positive talk
                else if (ans != "1" && positive >= negative)
                {
                    Console.WriteLine(resolver.FindResponse(17));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(9));
                    Console.WriteLine();
                }//If the user chose positive but had a negative talk
                else if (ans == "1" && negative >= positive)
                {
                    Console.WriteLine(resolver.FindResponse(3));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(13));
                    Console.WriteLine();
                }//If the user chose negative and had a negative talk
                else if (ans != "1" && negative >= positive)
                {
                    Console.WriteLine(resolver.FindResponse(17));
                    Console.WriteLine();
                    Console.WriteLine(resolver.FindResponse(13));
                    Console.WriteLine();
                }
            }

            //Ready to exit
            Console.WriteLine("Press enter to exit...");

            //Serialise the root node.
            resolver.root.SerialiseNode(resolver.root, "./data.save");

            //Wait for the user to exit
            Console.ReadLine();
        }
    }
}
