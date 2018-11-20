using UnityEngine;

namespace Xunity.Utils
{
    public class RotateBehaviour : MonoBehaviour
    {
        [SerializeField] float _rotateSpeed;

        void FixedUpdate()
        {
            transform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);
        }
    }
}