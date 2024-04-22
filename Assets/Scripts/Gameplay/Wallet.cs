using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _balance;

    public int Balance => _balance;

    public void Take(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else if (amount > _balance)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            _balance -= amount;
        }
    }

    public void Put(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            _balance += amount;
        }
    }
}