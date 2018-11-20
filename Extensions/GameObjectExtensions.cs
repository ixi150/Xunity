using UnityEngine;

namespace IXI.Utils.Extensions
{
    public static class GameObjectExtensions
    {
        public static void Deactivate(this Component component)
        {
            component.gameObject.Deactivate();
        }

        public static void Deactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static void Activate(this Component component)
        {
            component.gameObject.Activate();
        }

        public static void Activate(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
    }
}