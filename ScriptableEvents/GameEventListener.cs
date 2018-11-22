using UnityEngine;
using UnityEngine.Events;

namespace Xunity.ScriptableEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        [SerializeField] GameEvent gameEvent;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] UnityEvent response;

        public void OnEventRaised()
        {
            response.Invoke();
        }
        
        void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }
    }
}