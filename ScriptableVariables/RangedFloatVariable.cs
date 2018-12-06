using UnityEngine;

namespace Xunity.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Data/RangedFloat")]
    public class RangedFloatVariable : FloatVariable
    {
        [SerializeField] float min, max;

        public override void Set(float v, object source = null)
        {
            base.Set(Mathf.Clamp(v, min, max), source);
        }
    }
}