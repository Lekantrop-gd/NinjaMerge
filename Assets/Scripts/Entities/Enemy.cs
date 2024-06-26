using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _detectingRadius;
    [SerializeField] private float _reachDistance;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ApperanceChanger _apperanceChanger;
    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] private AudioSource[] _fightSounds;

    private Coroutine _attacking;
    private Player _player;
    private int _health = 0;

    public bool Alive { get; private set; }

    public event Action<Enemy> Died;
    public event Action Run;
    public event Action Fight;
    
    private void OnEnable()
    {
        Player.Won += OnWon;
        Player.Defeat += OnDefeat;
        EnemyEventHandler.Damage += DealDamage;
    }

    private void OnDisable()
    {
        Player.Won -= OnWon;
        Player.Defeat -= OnDefeat;
        EnemyEventHandler.Damage -= DealDamage;
    }

    public void Init(Weapon weapon, Armor armor)
    {
        _weapon = weapon;
        Alive = true;

        if (armor != null)
            _health = armor.ProtectionPoints;

        _apperanceChanger.SetWeapon(weapon);
        _apperanceChanger.SetArmor(armor);
    }

    public void StartFight()
    {
        Collider playerCollider = Physics.OverlapSphere(transform.position, _detectingRadius, _playerLayer)[0];
        Player player = playerCollider.GetComponent<Player>();

        _attacking = StartCoroutine(Attack(player));
    }

    public void DealDamage()
    {
        _player.TakeDamage(_weapon == null ? 0 : _weapon.Damage);
        _fightSounds[UnityEngine.Random.Range(0, _fightSounds.Length)].Play();
    }

    public void TakeDamage(int damage)
    {
        if (damage >= _health)
        {
            _health = 0;
            Died?.Invoke(this);
            StopCoroutine(_attacking);
            Alive = false;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            _health -= damage;
        }
    }

    private void OnDefeat()
    {
        StopCoroutine(_attacking);
    }

    private void OnWon()
    {
        StopCoroutine(_attacking);
    }
    private IEnumerator Attack(Player player)
    {
        while (player.Alive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > _reachDistance)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position,
                                                         player.transform.position,
                                                         _animationSpeed * Time.deltaTime);

                transform.position = new Vector3(transform.position.x, newPosition.y, newPosition.z);

                Quaternion targetRotation = Quaternion.LookRotation(player.transform.position -
                                                                    transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      targetRotation,
                                                      _animationSpeed * Time.deltaTime);
                Run?.Invoke();
            }
            else
            {
                _player = player;
                Fight?.Invoke();
            }

            yield return null;
        }
    }
}