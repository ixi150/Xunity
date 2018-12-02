using UnityEngine;
using UnityEngine.Events;

namespace Xunity.ScriptableEvents
{
    public class GameEventListener : GameEventListenerBase
    {
        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] UnityEvent response;

        public override void OnEventRaised()
        {
            response.Invoke();
        }
    }
}