using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        GameManager.Instance.IncreaseDestroyedBlocksCount();
        GameManager.Instance.AddScore(10);
        gameObject.SetActive(false);
    }
}
