using DG.Tweening;
using UnityEngine;


public class UIManager : MonoBehaviour
{
     [SerializeField] private CanvasGroup winPanel;
     [SerializeField] private CanvasGroup losePanel;
     [SerializeField] private CanvasGroup mainPanel;
     [SerializeField] private CanvasGroup inGamePanel;

     private void Start()
     {
          InitializeUIForNewLevel();
          EventsManager.OnGameStart += DisableStartPanel;
          EventsManager.OnGameLose += EnableLosePanel;
          EventsManager.OnGameWin += EnableWinPanel;
     }

     private void OnDisable()
     {
          EventsManager.OnGameStart -= DisableStartPanel;
          EventsManager.OnGameLose -= EnableLosePanel;
          EventsManager.OnGameWin -= EnableWinPanel;
     }

     private void EnableLosePanel()
     {
          losePanel.gameObject.SetActive(true);
          DOTween.To(()=> losePanel.alpha, x=> losePanel.alpha = x, 1, .5f);
          DOTween.To(()=> mainPanel.alpha, x=> mainPanel.alpha = x, 0, .15f);
          DOTween.To(()=> inGamePanel.alpha, x=> inGamePanel.alpha = x, 0, .15f);
     }
     private void EnableWinPanel()
     {
          winPanel.gameObject.SetActive(true);
          DOTween.To(()=> winPanel.alpha, x=> winPanel.alpha = x, 1, .5f);
          DOTween.To(()=> mainPanel.alpha, x=> mainPanel.alpha = x, 0, .15f);
          DOTween.To(()=> inGamePanel.alpha, x=> inGamePanel.alpha = x, 0, .15f);
     }

     private void DisableStartPanel()
     {
          mainPanel.gameObject.SetActive(false);
     }
     private void InitializeUIForNewLevel()
     {
          winPanel.gameObject.SetActive(false);
          winPanel.alpha = 0;
          losePanel.gameObject.SetActive(false);
          losePanel.alpha = 0;
          mainPanel.gameObject.SetActive(true);
          mainPanel.alpha = 1;
     }
     
}
