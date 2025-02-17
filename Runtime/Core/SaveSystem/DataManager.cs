using System;
using System.Text;
using UnityEngine;

namespace Core.SaveSystem
{
    public static class DataManager
    {
        public static void SaveString(string key, string value)
        {
            var encryptedValue = Encrypt(value);
            PlayerPrefs.SetString(key, encryptedValue);
            PlayerPrefs.Save();
        }
        
        public static string LoadString(string key, string defaultValue = "")
        {
            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            var encryptedValue = PlayerPrefs.GetString(key);
            return Decrypt(encryptedValue);
        }
        
        public static void SaveInt(string key, int value)
        {
            SaveString(key, value.ToString());
        }
        
        public static int LoadInt(string key, int defaultValue = 0)
        {
            var value = LoadString(key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        
        public static void SaveFloat(string key, float value)
        {
            SaveString(key, value.ToString());
        }


        public static float LoadFloat(string key, float defaultValue = 0f)
        {
            var value = LoadString(key, defaultValue.ToString());
            return float.TryParse(value, out var result) ? result : defaultValue;
        }


        public static void SaveBool(string key, bool value)
        {
            SaveInt(key, value ? 1 : 0);
        }


        public static bool LoadBool(string key, bool defaultValue = false)
        {
            return LoadInt(key, defaultValue ? 1 : 0) == 1;
        }


        public static bool Exists(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        public static void Delete(string key)
        {
            if (Exists(key))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
        
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        
        private static string Encrypt(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }
        
        private static string Decrypt(string data)
        {
            var bytes = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
