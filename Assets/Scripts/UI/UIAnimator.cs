using System.Collections;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animation;
    [SerializeField] private float _duration;

    public void Play()
    {
        StartCoroutine(Animate());
    }

    public IEnumerator Animate()
    {
        float expiredSeconds = 0f;
        float progress = 0f;

        while (progress < 1)
        {
            expiredSeconds += Time.deltaTime;
            progress = expiredSeconds / _duration;

            transform.localScale = new Vector3(_animation.Evaluate(progress),
                                           _animation.Evaluate(progress),
                                           _animation.Evaluate(progress));

            yield return null;
        }
    }
}
