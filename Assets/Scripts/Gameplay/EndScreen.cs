using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private RectTransform _endScreen;
    [SerializeField] private RectTransform _winTape;
    [SerializeField] private RectTransform _loseTape;
    [SerializeField] private RectTransform _wheelArrow;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _multipliedRewardText;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CompositionRoot _compositionRoot;
    [SerializeField] private float _gameReloadDelay;
    [SerializeField] private int _spinSpeed;
    [SerializeField] private UnityEvent _shown;
    [SerializeField] private Section[] _section;

    private int _multipliedReward;
    private int _regularReward;
    private Coroutine _wheelSpinning;

    [Serializable]
    public struct Section
    {
        [SerializeField] private int _start;
        [SerializeField] private int _medium;
        [SerializeField] private int _end;
        [SerializeField] private int _multiplier;

        public int Start => _start;
        public int Medium => _medium;
        public int End => _end;
        public int Multiplier => _multiplier;
    }

    public void Show(int reward, bool win)
    {
        _regularReward = reward;

        _endScreen.gameObject.SetActive(true);
        _winTape.gameObject.SetActive(win);
        _loseTape.gameObject.SetActive(!win);
        _rewardText.text =
            reward >= 1000 ? ((reward / 1000f).ToString("0.00") + "k") : reward.ToString();
        _wheelSpinning = StartCoroutine(SpinWheel(reward));
        _shown.Invoke();
    }

    private IEnumerator SpinWheel(int reward)
    {
        while (true)
        {
            _wheelArrow.Rotate(0, 0, _spinSpeed * Time.deltaTime);

            int rotation = (int)_wheelArrow.localEulerAngles.z;

            foreach (var section in _section)
            {
                if ((rotation >= section.Start && rotation <= section.Medium) || 
                    (rotation > section.Medium && rotation < section.End))
                {
                    _multipliedReward = reward * section.Multiplier;

                    _multipliedRewardText.text =
                    reward >= 1000 ? 
                    ((reward * section.Multiplier / 1000f).ToString("0.00") + "k") : 
                    (reward * section.Multiplier).ToString();
                }
            }

            yield return null;
        }
    }

    public void TakeReward()
    {
        _wallet.Put(_multipliedReward);
        StopCoroutine(_wheelSpinning);
        StartCoroutine(EndGame());
    }

    public void LoseReward()
    {
        _wallet.Put(_regularReward);
        StopCoroutine(_wheelSpinning);
        StartCoroutine(EndGame());
    }

    public void StopSpin()
    {
        _spinSpeed = 0;
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(_gameReloadDelay);

        _compositionRoot.ReloadScene();
    }
}