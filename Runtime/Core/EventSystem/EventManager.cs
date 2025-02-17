using System;
using System.Collections.Generic;

namespace Core.EventSystem
{
    public static class EventManager<T>
    {
        private class EventWrapper
        {
            public event Action<T> Event = delegate { };

            public void Invoke(T data) => Event?.Invoke(data);
            public void Subscribe(Action<T> listener) => Event += listener;
            public void Unsubscribe(Action<T> listener) => Event -= listener;
        }

        private static readonly Dictionary<string, EventWrapper> EventDictionary = new Dictionary<string, EventWrapper>();

        public static void Subscribe(string eventName, Action<T> listener)
        {
            if (!EventDictionary.ContainsKey(eventName))
            {
                EventDictionary[eventName] = new EventWrapper();
            }
            EventDictionary[eventName].Subscribe(listener);
        }

        public static void Unsubscribe(string eventName, Action<T> listener)
        {
            if (EventDictionary.TryGetValue(eventName, out var wrapper))
            {
                wrapper.Unsubscribe(listener);
            }
        }

        public static void Trigger(string eventName, T eventData)
        {
            if (EventDictionary.TryGetValue(eventName, out var wrapper))
            {
                wrapper.Invoke(eventData);
            }
        }
    }
}