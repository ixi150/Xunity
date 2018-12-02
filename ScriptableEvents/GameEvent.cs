using System.Collections.Generic;
using UnityEngine;
using Xunity.Base;

namespace Xunity.ScriptableEvents
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableAsset
    {
        readonly List<GameEventListenerBase> eventListeners = new List<GameEventListenerBase>();
        readonly HashSet<GameEventListenerBase> hashedListeners = new HashSet<GameEventListenerBase>();

        public IEnumerable<GameEventListenerBase> Listeners
        {
            get { return eventListeners; }
        }

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListenerBase listener)
        {
            if (hashedListeners.Contains(listener))
                return;

            hashedListeners.Add(listener);
            eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerBase listener)
        {
            if (!hashedListeners.Contains(listener))
                return;

            hashedListeners.Remove(listener);
            eventListeners.Remove(listener);
        }
    }
}