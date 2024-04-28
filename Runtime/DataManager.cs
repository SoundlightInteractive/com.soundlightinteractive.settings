using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SoundlightInteractive.Settings
{
    public static class DataManager
    {
        public static TData GetGameData<TData, TScriptableObject>()
            where TData : struct
            where TScriptableObject : ScriptableObject, IGameData<TData>
        {
            string path = GetResourcePath<TScriptableObject>();
            TScriptableObject scriptableObject = GetData<TScriptableObject>(path);
            return scriptableObject.GetData();
        }

        public static TData GetGameData<TData, TScriptableObject>(string subType)
            where TData : struct
            where TScriptableObject : ScriptableObject, IGameData<TData>
        {
            string path = GetResourcePath<TScriptableObject>(subType);
            TScriptableObject scriptableObject = GetData<TScriptableObject>(path);
            return scriptableObject.GetData();
        }

        private static string GetResourcePath<TScriptableObject>(string subType = null)
            where TScriptableObject : ScriptableObject
        {
            Type type = typeof(TScriptableObject);

            if (type.GetCustomAttributes(typeof(ResourcePathAttribute), false).FirstOrDefault() is not
                ResourcePathAttribute attribute)
            {
                throw new InvalidOperationException($"Resource path attribute is not defined for {type.Name}.");
            }

            return subType == null ? attribute.path : string.Format(attribute.path, subType);
        }

        private static T GetData<T>(string path) where T : ScriptableObject
        {
            T data = Resources.Load<T>(path);

            if (data == null)
            {
                throw new FileNotFoundException($"Data not found at path: {path}", path);
            }

            return data;
        }
    }
}