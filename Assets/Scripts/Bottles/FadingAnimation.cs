using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Bottles
{
    public class FadingAnimation : MonoBehaviour
    {
        private Action _onSpawn;
        private const float TimeToFade = 2f;
        private const float TimeToColor = 0.5f;

        private float _passedTime;

        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;

        // Start is called before the first frame update
        void OnEnable()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
        
            AnimateFadeIn();
        }

        // Update is called once per frame
        void Update()
        {
            if (_passedTime < TimeToFade)
            {
                _passedTime += Time.deltaTime;
                AnimateFadeIn();
            }
            else if (_passedTime < TimeToFade + TimeToColor)
            {
                _passedTime += Time.deltaTime;
                AnimateBlackToColor();
                
                if (_passedTime >= TimeToFade + TimeToColor)
                    _onSpawn?.Invoke();
            }
        }

        private void AnimateFadeIn()
        {
            _spriteRenderer.color = new Color(0f, 0f, 0f, _passedTime / TimeToFade);
        }

        private void AnimateBlackToColor()
        {
            var targetColor = (_passedTime - TimeToFade) / TimeToColor * _originalColor;
            _spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, 1f);
        }

        public bool IsSpawned()
        {
            return _passedTime >= TimeToFade + TimeToColor;
        }

        public void OnSpawn([NotNull] Action action)
        {
            _onSpawn = action;
        }
    }
}
