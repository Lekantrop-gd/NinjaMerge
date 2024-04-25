using UnityEngine;
using UnityEngine.SceneManagement;

public class CompositionRoot : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelSystem _levelSystem;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private int _startWinReward;
    [SerializeField] private int _startLoseReward;
    [SerializeField] private int _rewardAddend;

    public readonly string LevelKey = nameof(LevelKey);

    public int Level => PlayerPrefs.GetInt(LevelKey, 0);

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelKey, Level + 1);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        Application.targetFrameRate = 1000;

        for (int x = 0; x < _levelSystem.Levels[8].Enemies.Length; x++)
        {
            _spawner.Spawn(_levelSystem.Levels[8].Enemies[x].Weapon,
                           _levelSystem.Levels[8].Enemies[x].Armor);
        }
    }

    private void OnWon()
    {
        IncreaseLevel();
        _wallet.Put(_startWinReward + _rewardAddend * Level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDefeat()
    {
        _wallet.Put(_startWinReward + _rewardAddend / 2 * Level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        Player.Won += OnWon;
        Player.Defeat += OnDefeat;
    }

    private void OnDisable()
    {
        Player.Won -= OnWon;
        Player.Defeat -= OnDefeat;
    }
}
