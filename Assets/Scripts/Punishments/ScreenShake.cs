using UnityEngine;

namespace Utils
{
    public class ScreenShake : MonoBehaviour, IPunishment
    {
        public int shakeMultiplier = 2;
        public int defaultShakeDuration = 2;

        public void startPunishment()
        {
            startPunishment(new Vector3(defaultShakeDuration, 0,0));
        }

        public void startPunishment(Vector3 position)
        {
            iTween.ShakePosition(gameObject, new Vector3(.1f * shakeMultiplier, .1f * shakeMultiplier, .1f * shakeMultiplier), 1 * position.magnitude);
        }
    }
}