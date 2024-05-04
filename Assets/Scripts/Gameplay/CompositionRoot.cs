using System.Collections;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private int _delay;

    public readonly string LevelKey = nameof(LevelKey);

    public int Level => PlayerPrefs.GetInt(LevelKey, 0);

    private void Awake()
    {
        Application.targetFrameRate = 1000;

        _levelText.text = "Level " + (Level + 1).ToString();

        for (int x = 0; x < _levelSystem.Levels[Level].Enemies.Length; x++)
        {
            _spawner.Spawn(_levelSystem.Levels[Level].Enemies[x].Weapon,
                           _levelSystem.Levels[Level].Enemies[x].Armor);
        }
    }

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelKey, Level + 1);
        PlayerPrefs.Save();
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private void OnWon()
    {
        IncreaseLevel();
        StartCoroutine(CallWithDelay(_startWinReward + _rewardAddend * Level, true));
    }

    private void OnDefeat()
    {
        StartCoroutine(CallWithDelay(_startLoseReward + (_rewardAddend / 2) * Level, false));
    }

    private IEnumerator CallWithDelay(int reward, bool win)
    {
        yield return new WaitForSeconds(_delay);

        _endScreen.Show(reward, win);
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}