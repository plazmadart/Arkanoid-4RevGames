using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Ball object")]
    [SerializeField] private GameObject _ballObject;

    [Header("Ball parameters")]
    [SerializeField] private float ballSpeed = 5f;

    private GameObject _platformObject;
    private Rigidbody2D _rb2D;
    private Vector2 _startPosition;
    private bool isReady = false;
    private bool isActive = false;

    private void Start()
    {
        _startPosition = _ballObject.transform.position;
        _rb2D = _ballObject.GetComponent<Rigidbody2D>();

        if(_platformObject == null)
        {
            _platformObject = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if(!isActive)
        {
            if (!isReady)
            {
                if (Input.touchCount > 0)
                {
                    isReady = true;
                }
            }
            else if (Input.touchCount == 0)
            {
                StartMove();
                isActive = true;
                isReady = false;
            }

            _ballObject.transform.position = new Vector2(_platformObject.transform.position.x, _ballObject.transform.position.y);
        }
    }

    private void StartMove()
    {
        float randomDirectionX = Random.Range(-1f, 1f);
        float randomDirectionY = Random.Range(0.5f, 1f);
        Vector3 randomDirection = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized;
        _rb2D.velocity = randomDirection * ballSpeed;
    }
}
