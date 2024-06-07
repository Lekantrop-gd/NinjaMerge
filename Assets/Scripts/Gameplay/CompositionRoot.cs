using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGames;

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
    [SerializeField] private Market _market;
    [SerializeField] private RectTransform _gameBeatScreen;

    public readonly string LevelKey = nameof(LevelKey);

    public int Level => PlayerPrefs.GetInt(LevelKey, 0);

    private void Awake()
    {
        Application.targetFrameRate = -1;
        
        if (Level >= _levelSystem.Levels.Length)
        {
            CrazySDK.Init(() => CrazySDK.Game.HappyTime());

            _gameBeatScreen.gameObject.SetActive(true);
            return;
        }

        CrazySDK.Init(() => CrazySDK.Game.GameplayStart());

        _levelText.text = "Level " + (Level + 1).ToString();
        _market.UpdatePrices();


        for (int x = 0; x < _levelSystem.Levels[Level].Enemies.Length; x++)
        {
            _spawner.Spawn(_levelSystem.Levels[Level].Enemies[x].Weapon,
                           _levelSystem.Levels[Level].Enemies[x].Armor);
        }
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

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelKey, Level + 1);
        PlayerPrefs.Save();
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteKey(LevelKey);
        PlayerPrefs.Save();
        ReloadScene();
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

        CrazySDK.Game.GameplayStop();
        _endScreen.Show(reward, win);
    }
}