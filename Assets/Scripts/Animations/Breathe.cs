    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Breathe : MonoBehaviour
    {
        Vector3 startPos;
        public const float amplitude = 0.25f;
        public const float period = 2f;
        protected void Start()
        {
            startPos = transform.position;
        }
        protected void Update()
        {
            float theta = Time.time / period;
            float distance = amplitude * Mathf.Sin(theta);
            transform.position = startPos + Vector3.up * distance;
        }
    }