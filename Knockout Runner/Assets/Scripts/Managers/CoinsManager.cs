using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class CoinsManager : MonoBehaviour
{
      #region Singleton

      public static CoinsManager Instance;

      private void Awake()
      {
            Instance = this;
      }

      #endregion
      
      
      [SerializeField] private int curCoins;
      [SerializeField] private int multiplier;
      [SerializeField] private RectTransform coinsBar;
      [SerializeField] private RectTransform scoreImage;
      [SerializeField] private RectTransform centerPoint;
      [SerializeField] private TextMeshProUGUI coinsText;
      [SerializeField] private TextMeshProUGUI multipliedCoins;
      private void Start()
      {
            multiplier = 1;
            scoreImage.gameObject.SetActive(false);
            EventsManager.OnCoinPickUp += IncrementCoin;
            EventsManager.OnGameWin += ShowMultipliedText;
            EventsManager.OnGameLose += TinySauceEventForFail;

      }

      private void OnDisable()
      {
            ResetCoinsUI();
            EventsManager.OnCoinPickUp -= IncrementCoin;
            EventsManager.OnGameWin -= ShowMultipliedText;
            EventsManager.OnGameLose -= TinySauceEventForFail;
      }


      public void MultiplyCoins(int multiple)
      {
            multiplier = multiple;
      }

      #region Event Callbacks

      private void IncrementCoin()
      {
            curCoins += 1;

            coinsBar.DOScale(new Vector2(1.2f, 1.2f), .1f).OnComplete(() =>
            {
                  coinsBar.DOScale(Vector2.one, .1f);
            });
                  
            scoreImage.gameObject.SetActive(true);
            RectTransform coin = Instantiate(scoreImage,centerPoint);
            scoreImage.gameObject.SetActive(false);
            coin.DOMove(coinsBar.position, .25f).OnComplete(() =>
            {
                  
                  coin.gameObject.SetActive(false);
            });
            coinsText.text = curCoins.ToString();

      }

      private void ShowMultipliedText()
      {
            var index = PlayerPrefs.GetInt("LevelIndex");
            curCoins = curCoins * multiplier;
            multipliedCoins.text = curCoins.ToString();
          //  TinySauce.OnGameFinished(true,curCoins,index.ToString());
      }

      private void TinySauceEventForFail()
      {
            var index = PlayerPrefs.GetInt("LevelIndex");
           // TinySauce.OnGameFinished(false,curCoins,index.ToString());
      }
      #endregion


      private void ResetCoinsUI()
      {
            curCoins = multiplier = 0;
            coinsText.text = string.Empty;
            multipliedCoins.text=string.Empty;
      }
      
      
      
}