using System;
using System.Collections.Generic;

namespace Core.EventSystem
{
    public static class EventManager<T>
    {
        private static Dictionary<string, Action<T>> eventDictionary = new Dictionary<string, Action<T>>();

        public static void Subscribe(string eventName, Action<T> listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = delegate { };
            }
            eventDictionary[eventName] += listener;
        }

        public static void Unsubscribe(string eventName, Action<T> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        public static void Trigger(string eventName, T eventData)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName]?.Invoke(eventData);
            }
        }
    }
}