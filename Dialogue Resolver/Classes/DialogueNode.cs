/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *                                                           *
 *           * * * Serialisation Method * * *                *
 *                                                           *
 *      This program uses the JSON format for seriali-       *
 *      sation.                                              *
 *                                                           *
 *      Advatages:                                           *
 *      • Easy to read the data                              *
 *      • Easier to map data for object oriented programming *
 *      • Faster the binary method, both in small and large  *
 *          file sizes.                                      *
 *                                                           *
 *      Disadvatages:                                        *
 *      • Easy to manipulate the data, making it less secure *
 *      • JSON has no comments                               *
 *      • No distinction between float and decimal point     *
 *                                                           *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Task.Classes
{
    [Serializable]
    public class DialogueNode
    {
        //*****PROPERTIES*****
        public string CharacterName { get; set; }
        public string DialogueText { get; set; }
        public DialogueNode? left { get; set; }
        public DialogueNode? right { get; set; }
        
        public int key { get; set; }
        //*****METHODS*****
        //Constructor
        public DialogueNode(string name,  string text, int _key)
        {
            CharacterName = name;
            DialogueText = text;
            key = _key;
        }
        //Getters for the diallgue text and character name
        public string GetText()
        {
            return DialogueText;
        }
        public string GetName()
        {
            return CharacterName;
        }

        //Serialising the node
        public void SerialiseNode(object data, string filePath)
        {
            JsonSerializer serialiser = new JsonSerializer();
            //We delete the existing file in order to recreate it with new data.
            if (File.Exists(filePath))
                File.Delete(filePath);
            /* We declare a new StreamWriter to store the path and parse it to the Json writer.
             * Then we use the json writer and the data object to serialise the data to the
             * file stored in the file path.*/
            StreamWriter writer = new StreamWriter(filePath);
            JsonTextWriter json = new JsonTextWriter(writer);
            serialiser.Serialize(json, data);
            //Closing the writers
            json.Close();
            writer.Close();
        }
        //Desirilising the node
        public object DeserialiseNode(Type dataType, string filePath)
        {
            //We create the object to be returned and the JSON serialiser
            JObject _object = new JObject();
            JsonSerializer serialiser = new JsonSerializer();

            //If the file exists we will read the file data
            if (File.Exists(filePath))
            {
                /* We use the same logic here as we did on the SerialiseNode function
                 * with the only difference instead of writing we read from the file. */
                StreamReader reader = new StreamReader(filePath);
                JsonReader json = new JsonTextReader(reader);
                _object = serialiser.Deserialize(json) as JObject;
                //Closing the readers
                json.Close();
                reader.Close();
            }

            return _object.ToObject(dataType);
        }
    }

}
