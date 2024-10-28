using System;
using UnityEngine;

public class DestroyBrokenArrow : MonoBehaviour
{
        private Renderer arrowColorRenderer;

        private void Start()
        {
                // Get the Renderer component on start
                arrowColorRenderer = GetComponent<Renderer>();
        }

        private void Update()
        {
                // Check if the material's color alpha is less than 0.5f
                if (arrowColorRenderer.material.color.a < 0.05f)
                {
                        Destroy(gameObject);
                }
        }
}
