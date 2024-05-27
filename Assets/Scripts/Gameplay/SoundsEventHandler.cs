using UnityEngine;
using UnityEngine.Events;

public class SoundsEventHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _win;
    [SerializeField] private UnityEvent _lose;

    private void OnEnable()
    {
        Player.Won += OnWin;
        Player.Defeat += OnDefeat;
    }

    private void OnDefeat()
    {
        _lose?.Invoke();
    }

    private void OnWin()
    {
        _win?.Invoke();
    }
}
