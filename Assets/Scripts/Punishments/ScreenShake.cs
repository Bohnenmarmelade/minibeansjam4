using UnityEngine;

namespace Utils
{
    public class ScreenShake : MonoBehaviour, IPunishment
    {
        public int shakeMultiplier = 2;
        public int defaultShakeDuration = 5;

        public void startPunishment()
        {
            startPunishment(new Vector3(defaultShakeDuration, 0, 0));
        }

        public void startPunishment(Vector3 position)
        {
            if (Camera.main != null)
            {
                iTween.ShakePosition(Camera.main.gameObject,
                    new Vector3(.1f * shakeMultiplier, .1f * shakeMultiplier, .1f * shakeMultiplier),
                    position.magnitude);
            }
        }
    }
}