﻿using UnityEngine;

namespace Xunity.Behaviours
{
    [RequireComponent(typeof(Camera))]
    public class Blit : MonoBehaviour
    {
        [SerializeField] Material material;
    
        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, material);
        }
    }
}
