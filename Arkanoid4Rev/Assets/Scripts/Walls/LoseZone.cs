using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BallMovement ball))
        {
            ball.MoveToStartPosition();
            GameManager.Instance.DecreaseLives();
        }
    }
}
