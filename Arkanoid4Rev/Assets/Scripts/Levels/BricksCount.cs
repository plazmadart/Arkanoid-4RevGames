using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksCount : MonoBehaviour
{
    [SerializeField] private int blocksCount;

    private void Start()
    {
        GameManager.Instance.SetLevelBlockCount(blocksCount);
    }
}
