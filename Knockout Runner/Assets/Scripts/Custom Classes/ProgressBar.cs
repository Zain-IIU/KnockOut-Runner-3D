using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class ProgressBar : MonoBehaviour
    {
         private Transform player;
         private Transform lastWayPoint;
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI curLevelText;
        private float _fullDistance;
        private int curIndex;

        private void Awake()
        {
            SetLevelText();
        }

        private void Start()
        {
            player = FindObjectOfType<PlayerController>().transform;
            lastWayPoint = GameObject.FindGameObjectWithTag("lastPoint").transform;
            _fullDistance = GetDistance();
            EventsManager.OnGameStart += ShowBar;
            EventsManager.OnReachedEnd += HideBar;
            EventsManager.OnGameLose += HideBar;
            EventsManager.OnNextLevel += NewLevel;

        }

        private void OnDisable()
        {
            EventsManager.OnGameStart -= ShowBar;
            EventsManager.OnReachedEnd -= HideBar;
            EventsManager.OnGameLose -= HideBar;
            EventsManager.OnNextLevel -= NewLevel;
        }

        private void SetLevelText()
        {
            curIndex = 1;
            if (PlayerPrefs.HasKey("LevelIndex"))
            {
                curIndex = PlayerPrefs.GetInt("LevelIndex");
            }
            else
                PlayerPrefs.SetInt("LevelIndex",1);

            curLevelText.text = curIndex.ToString();
        }

        private void NewLevel()
        {
            curIndex++;
            PlayerPrefs.SetInt("LevelIndex",curIndex);
        }
        
        private void HideBar()
        {
            GetComponent<RectTransform>().DOScale(Vector2.zero, .25f);
        }

        private void ShowBar()
        {
            GetComponent<RectTransform>().DOScale(Vector2.one, .15f);
        }
        // Update is called once per frame
        void Update()
        {
            progressBar.fillAmount = Mathf.InverseLerp(_fullDistance, 0, GetDistance());
        }

        float GetDistance()
        {
            return Vector3.Distance(player.position, lastWayPoint.position);
        }

    }
