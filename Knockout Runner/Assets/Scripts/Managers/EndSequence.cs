using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSequence : MonoBehaviour
{
    [SerializeField] private List<Transform> multiplierBlocks = new List<Transform>();
    
    private void Awake()
    {
        SetupBlocks();
    }

  
    private void SetupBlocks()
    {
        var blockIndex = 1;
        foreach (var block in multiplierBlocks)
        {
            block.GetComponentInChildren<Multiplier>().SetMultiplier(blockIndex);
            block.GetComponentInChildren<TextMeshPro>().text = "x" + blockIndex;
            blockIndex++;
        }
    }
    
 
}
