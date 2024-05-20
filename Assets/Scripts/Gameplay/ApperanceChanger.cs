using Unity.VisualScripting;
using UnityEngine;

public class ApperanceChanger : MonoBehaviour
{
    [SerializeField] private Transform _defaultHair;
    [SerializeField] private Transform _defaultArmorHair;
    [SerializeField] private Transform _defaultHead;
    [SerializeField] private Transform _defaultFace;
    [SerializeField] private Transform _defaultPlayerModel;
    [SerializeField] protected Transform _model;
    [SerializeField] private WeaponSet _weaponSet;
    [SerializeField] private ArmorSet _armorSet;
    [SerializeField] private ModelRouter _router;

    public void SetWeapon(Weapon weapon)
    {
        if (_router.RightHandRoot.childCount > 0)
        {
            Destroy(_router.RightHandRoot.GetChild(0).gameObject);
        }

        if (weapon != null)
        {
            for (int x = 0; x < _weaponSet.WeaponLinks.Length; x++)
            {
                if (weapon.Damage == _weaponSet.WeaponLinks[x].Weapon.Damage)
                {
                    Instantiate(_weaponSet.WeaponLinks[x].Model, _router.RightHandRoot);
                }
            }
        }
    }

    public void SetArmor(Armor armor)
    {
        Transform newPlayerModel = null;
        ModelRouter newRouter = null;
        if (armor == null)
        {
            newPlayerModel = Instantiate(_defaultPlayerModel, transform);
            newRouter = newPlayerModel.GetComponent<ModelRouter>();
            Instantiate(_defaultHair, newRouter.HairRoot);
        }
        else
        {
            for (int x = 0; x < _armorSet.ArmorLinks.Length - 1; x++)
            {
                if (armor.ProtectionPoints == _armorSet.ArmorLinks[x].Armor.ProtectionPoints)
                {
                    newPlayerModel = Instantiate(_armorSet.ArmorLinks[x].ArmorModel, transform);
                    newRouter = newPlayerModel.GetComponent<ModelRouter>();
                    Instantiate(_defaultArmorHair, newRouter.HairRoot);
                    Instantiate(_armorSet.ArmorLinks[x].HatModel, newRouter.HatRoot);
                    break;
                }
            }
        }
        
        if (_router.RightHandRoot.childCount > 0)
        {
            Instantiate(_router.RightHandRoot.GetChild(0), newRouter.RightHandRoot);
        }

        Destroy(_model.gameObject);
        _model = newPlayerModel;
        _router = newRouter;

        Instantiate(_defaultHead, _router.HeadRoot);
        Instantiate(_defaultFace, _router.FaceRoot);

        AddEventHandler();
    }

    public virtual void AddEventHandler()
    {
        _model.AddComponent<EnemyEventHandler>();
    }
}