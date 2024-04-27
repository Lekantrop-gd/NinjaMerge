using System;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _startBalance;
    [SerializeField] private TextMeshProUGUI _balanceText;

    public readonly string WalletKey = nameof(WalletKey);
    public int Balance => PlayerPrefs.GetInt(WalletKey);

    private void Awake()
    {
        if (PlayerPrefs.HasKey(WalletKey) == false)
        {
            PlayerPrefs.SetInt(WalletKey, _startBalance);
            PlayerPrefs.Save();
        }
        
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        _balanceText.text = Balance.ToString();
    }

    public void Take(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else if (amount > Balance)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            PlayerPrefs.SetInt(WalletKey, Balance - amount);
            PlayerPrefs.Save();
        }

        UpdateBalance();
    }

    public void Put(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            PlayerPrefs.SetInt(WalletKey, Balance + amount);
            PlayerPrefs.Save();
        }

        UpdateBalance();
    }
}