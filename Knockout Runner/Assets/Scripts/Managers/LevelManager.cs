using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class LevelManager : MonoBehaviour
{
     public static LevelManager Instance;
    [SerializeField]
    GameObject[] levels;

    [SerializeField] private GameObject[] environments;

    [SerializeField] private Color[] borderColors;
    
    [SerializeField] private Material borderMat;
    [SerializeField] private bool startDisabled;

    private static int CurrentEnvironment { get; set; }

    private static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }
    private void SetColors()
    {
        borderMat.color = borderColors[GetLevelIndex()];
    }

   
    private void Awake()
    {
        Instance = this;
      
       MMVibrationManager.Haptic(HapticTypes.LightImpact, false,true, this);
        if (!startDisabled) return;
        foreach (var level in levels)  level.SetActive(false);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        LoadGame();   
    }
    

    private void LoadGame()
    {
        CurrentEnvironment = 0;
        var index = PlayerPrefs.GetInt("LevelIndex");
        CurrentLevel = index > levels.Length ? Random.Range(0, levels.Length) : GetLevelIndex();
        if (CurrentLevel % 4 == 0)
        {
            CurrentEnvironment++;
            CurrentEnvironment %= environments.Length;
        }
        // TinySauce.OnGameStarted(index.ToString());

        environments[CurrentEnvironment].SetActive(true);
        levels[CurrentLevel].SetActive(true);
        SetColors();

    }

    public int GetLevelIndex()
    {
        return CurrentLevel % levels.Length;
    }
    
    [ContextMenu("Load Next Level")]
    public void IncrementLevelIndex()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
        Debug.Log("Loading Next level");
        EventsManager.NextLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    [ContextMenu("Restart Level")]
    public void ReplayLevel()
    {
        Debug.Log("Loading same level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  
}