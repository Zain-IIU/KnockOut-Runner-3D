using UnityEngine;


public class ColorsManager : MonoBehaviour
{
    [SerializeField] private Color[] borderColors;

    [SerializeField] private Material borderMat;
    private void Start()
    {
        SetColors();
    }


    private void SetColors()
    {
        borderMat.color = borderColors[LevelManager.Instance.GetLevelIndex()];
    }

    private void OnValidate()
    {
        SetColors();
    }
}       
