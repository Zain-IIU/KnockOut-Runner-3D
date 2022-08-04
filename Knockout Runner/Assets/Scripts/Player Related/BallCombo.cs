using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BallCombo : MonoBehaviour
{
    [SerializeField] private Image comboMeter;

    [SerializeField] private float decreaseSpeed;

    [SerializeField] private float increaseAmount;

    
    
    private float curDecreaseAmount;

    public bool isDecreasingSpeed;
    private void Start()
    {
        isDecreasingSpeed = false;
        curDecreaseAmount = decreaseSpeed;
        EventsManager.OnBallHit += CountCombo;
        EventsManager.OnReachedEnd += HideComboMeter;
        EventsManager.OnGameStart += EnableMeter;
    }

    private void OnDisable()
    {
        EventsManager.OnBallHit -= CountCombo;
        EventsManager.OnReachedEnd -= HideComboMeter;
        EventsManager.OnGameStart -= EnableMeter;
    }

    private void LateUpdate()
    {
        //testing
        if(Input.GetKeyDown(KeyCode.Space))
            CountCombo();
        //testing end

        if (comboMeter.fillAmount <= 0.02f)
        {
            isDecreasingSpeed = false;
            curDecreaseAmount = decreaseSpeed;
        }
        
        comboMeter.fillAmount = Mathf.Lerp(comboMeter.fillAmount, 0, curDecreaseAmount * Time.deltaTime);
    }

    #region Event callbacks

    [ContextMenu("Increase amount")]
    public void CountCombo()
    {
        if(isDecreasingSpeed) return;
        
        comboMeter.fillAmount += (increaseAmount/100);
        
        if (!(comboMeter.fillAmount >= 1)) return;
        
        isDecreasingSpeed = true;
        DropComboAmount();
        EventsManager.FrenzyMode();
    }

    private void HideComboMeter()
    {
        enabled = false;
        gameObject.transform.DOScale(Vector2.zero, .15f);
    }

    private void DropComboAmount()
    {
        curDecreaseAmount = decreaseSpeed * 2;
    }

    private void EnableMeter()
    {
        gameObject.transform.DOScale(Vector2.one, .15f);
    }
    
    
    
    #endregion


    
}


