using TMPro;
using UnityEngine;

public class Market : MonoBehaviour
{
    [SerializeField] private int _startWeaponPrice;
    [SerializeField] private int _weaponPriceAddend;
    [SerializeField] private int _startArmorPrice;
    [SerializeField] private int _armorPriceAddend;
    [SerializeField] private TextMeshProUGUI _weaponPrice;
    [SerializeField] private GameObject _weaponAdvert;
    [SerializeField] private TextMeshProUGUI _armorPrice;
    [SerializeField] private GameObject _armorAdvert;
    [SerializeField] private CellsGrid _cellsGrid;
    [SerializeField] private Wallet _wallet;

    public readonly string WeaponPriceKey = nameof(WeaponPriceKey);
    public readonly string ArmorPriceKey = nameof(ArmorPriceKey);

    private void Awake()
    {
        UpdatePrices();
    }

    private void UpdatePrices()
    {
        if (PlayerPrefs.HasKey(WeaponPriceKey))
        {
            _weaponPrice.text = PlayerPrefs.GetInt(WeaponPriceKey).ToString();

            _weaponAdvert.SetActive(_wallet.Balance < PlayerPrefs.GetInt(WeaponPriceKey));
            _weaponPrice.enabled = _wallet.Balance >= PlayerPrefs.GetInt(WeaponPriceKey);
        }
        else
        {
            PlayerPrefs.SetInt(WeaponPriceKey, _startWeaponPrice);
            _weaponPrice.text = _startWeaponPrice.ToString();
        }

        if (PlayerPrefs.HasKey(ArmorPriceKey))
        {
            _armorPrice.text = PlayerPrefs.GetInt(ArmorPriceKey).ToString();

            _armorAdvert.SetActive(_wallet.Balance < PlayerPrefs.GetInt(WeaponPriceKey));
            _armorPrice.enabled = _wallet.Balance >= PlayerPrefs.GetInt(WeaponPriceKey);
        }
        else
        {
            PlayerPrefs.SetInt(ArmorPriceKey, _startArmorPrice);
            _armorPrice.text = _startArmorPrice.ToString();
        }

        PlayerPrefs.Save();
    }

    public void BuyWeapon()
    {
        if (_wallet.Balance >= PlayerPrefs.GetInt(WeaponPriceKey))
        {
            if (_cellsGrid.SpawnWeapon()) {
                _wallet.Take(PlayerPrefs.GetInt(WeaponPriceKey));
                PlayerPrefs.SetInt(WeaponPriceKey, PlayerPrefs.GetInt(WeaponPriceKey) + _weaponPriceAddend);
                PlayerPrefs.Save();
                UpdatePrices();
            }
        }
    }

    public void BuyArmor()
    {
        if (_wallet.Balance >= PlayerPrefs.GetInt(ArmorPriceKey))
        {
            if ( _cellsGrid.SpawnArmor())
            {
                _wallet.Take(PlayerPrefs.GetInt(ArmorPriceKey));
                PlayerPrefs.SetInt(ArmorPriceKey, PlayerPrefs.GetInt(ArmorPriceKey) + _armorPriceAddend);
                PlayerPrefs.Save();
                UpdatePrices();
            }
            
        }
    }
}