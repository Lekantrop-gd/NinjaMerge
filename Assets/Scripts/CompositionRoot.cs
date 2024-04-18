using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    public readonly string LevelKey = nameof(LevelKey);

    public int Level => PlayerPrefs.GetInt(LevelKey, 0);

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelKey, Level + 1);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if (Level == 0)
        {
            IncreaseLevel();
        }
    }
}
