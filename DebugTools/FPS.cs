using UnityEngine;
using Xunity.Behaviours;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
namespace Xunity.DebugTools
{
    public class FPS : GameBehaviour
    {
        string fps;
        int frames;

        void OnEnable()
        {
            InvokeRepeating(FetchFrames, 1, 1);
        }

        void OnDisable()
        {
            CancelInvoke();
        }

        void LateUpdate()
        {
            frames++;
        }

        void FetchFrames()
        {
            fps = "" + frames / 1f;
            frames = 0;
        }
        
        void OnGUI()
        {
            GUI.Label
            (
                new Rect(5, 5, 200, 50),
                fps + " FPS"
            );
        }
    }
}
#endif