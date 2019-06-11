using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ProductivityApp.Models.Helpers
{
    /// <summary>
    /// Functions for making deep copies of objects to make sure I don't pass around references or alter source objects
    /// </summary>
    public static class CopyHelpers
    {

        /// <summary>
        /// Use Serialize/Deserialize to create a fresh copy of whatever we're trying to clone.
        /// 
        /// The same as saving to e.g. json then reading it back to get a new instance of some particular set of values
        /// https://stackoverflow.com/questions/14007405/how-create-a-new-deep-copy-clone-of-a-listt/43046969
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            //we could also use Json Serialize/Deserialize, but ive had issues with certain value types with no setters. Maybe this will run into the same probs? -MG

            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
        /// <summary>
        /// Set all Guid properties to new values
        /// 
        /// p.s: Thanks stackoverflow! https://stackoverflow.com/questions/42566721/how-to-find-values-of-nested-object-properties?rq=1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T AssignNewGuidIds<T>(this T obj)
        {
            //iterate through each property of the object
            foreach(PropertyInfo property in typeof(T).GetProperties())
            {
                //get the value, if it's another class, we will inspect and change that class as well
                object value = property.GetValue(obj, null);
                if(value.GetType().IsClass)
                {
                    //go through subproperties of the child object
                    AssignNewGuidIds(value);
                } //only care about props where the type is guid -- we reset all guid values
                else if (value.GetType() == typeof(Guid))
                {
                    //set the property to Guid.NewGUid();
                    property.SetValue(obj, Guid.NewGuid());
                }
            }
            return obj;
        }

    }

}
