using UnityEngine;

namespace Xunity.ScriptableEvents
{
    public abstract class GameEventListenerBase : MonoBehaviour
    {
        [Tooltip("Event to register with.")] [SerializeField]
        GameEvent gameEvent;

        public abstract void OnEventRaised();

        protected virtual void OnEnable()
        {
            if (gameEvent)
                gameEvent.RegisterListener(this);
        }

        protected virtual void OnDisable()
        {
            if (gameEvent)
                gameEvent.UnregisterListener(this);
        }
    }
}