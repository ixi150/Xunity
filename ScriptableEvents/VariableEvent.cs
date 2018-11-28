using System.Collections.Generic;
using Xunity.Base;

namespace Xunity.ScriptableEvents
{
    public class VariableEvent<T> : ScriptableAsset
    {
        readonly List<GameEventListener> eventListeners = new List<GameEventListener>();
        readonly HashSet<GameEventListener> hashedListeners = new HashSet<GameEventListener>();

        public IEnumerable<GameEventListener> Listeners
        {
            get { return eventListeners; }
        }

        public void Raise(T value)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (hashedListeners.Contains(listener))
                return;

            hashedListeners.Add(listener);
            eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (!hashedListeners.Contains(listener))
                return;

            hashedListeners.Remove(listener);
            eventListeners.Remove(listener);
        }
    }
}