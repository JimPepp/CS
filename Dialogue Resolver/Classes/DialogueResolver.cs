using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Classes
{
    public class DialogueResolver
    {
        public DialogueNode root { get; set; }
        
        public void Insert (string name, string text, int response)
        {
            //We insert everything in the root object
            root = InsertItem(root, name, text, response);
        }

        public DialogueNode InsertItem(DialogueNode node, string name, string text, int response)
        {
            DialogueNode newNode = new DialogueNode(name, text, response);
            //If the node is empty we create the root.
            if (node == null)
            {
                node = newNode;
                return newNode;
            }
            //If we have a negative responce key 
            if (response < node.key)
            {
                //We set the new node to the left child  
                node.left = InsertItem(node.left, name, text, response);
            }
            else //If we have a positive responce
            {
                //We set the new node to the right child   
                node.right = InsertItem(node.right, name, text, response);
            }

            return node;
        }
        //Try to find the corresponding response
        public string FindResponse(int response)
        {
            DialogueNode node = FindText(root, response);
            //If the response we are looking for does not exist, we exit.
            if (node == null)
                Environment.Exit(response);
            
            Console.WriteLine(node.GetName());
            
            return node.GetText();
        }

        private DialogueNode FindText(DialogueNode node, int response)
        {
            //If the node is empty we're at the root or if the response 
            //key is the same we found the node we're looking for
            if (node == null || response == node.key)
            {//We just return the root
                return node;
            }//If we have a response smaller than the key 
            else if (response < node.key)
            {//We search for the left child
                return FindText(node.left, response);
            }//Same goes for a response grater that the current key
            else if (response > node.key)
            {//But we look for the right child 
                return FindText(node.right, response);
            }
            else//If all else fails we return null to exit
                return null;
        }
    }
}
