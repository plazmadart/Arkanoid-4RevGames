using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Ball object")]
    [SerializeField] private GameObject _ballObject;

    [Header("Ball parameters")]
    [SerializeField] private float ballSpeed = 5f;

    private float lastPositionX;
    private GameObject _platformObject;
    private Rigidbody2D _rb2D;
    private Vector2 _startPosition;
    private Vector2 lastVelocity;
    private bool isReady = false;
    private bool isActive = false;

    private void Start()
    {
        _startPosition = transform.position;
        lastPositionX = _startPosition.x;
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

            transform.position = new Vector2(_platformObject.transform.position.x, transform.position.y);
        }
    }

    private void StartMove()
    {
        float randomDirectionX = Random.Range(-1f, 1f);
        float randomDirectionY = Random.Range(0.5f, 1f);
        Vector3 randomDirection = new Vector3(randomDirectionX, randomDirectionY, 0f).normalized;
        _rb2D.velocity = randomDirection * ballSpeed;
        _rb2D.gravityScale = 1f;
    }

    public void MoveToStartPosition()
    {
        _rb2D.gravityScale = 0f;
        _rb2D.velocity = Vector2.zero;
        transform.position = _startPosition;
        isActive = false;
    }

    public void StopMoving()
    {
        lastVelocity = _rb2D.velocity;
        _rb2D.velocity = Vector2.zero;
        _rb2D.gravityScale = 0f;
    }

    public void ContinueMoving()
    {
        _rb2D.velocity = lastVelocity;
        _rb2D.gravityScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Movement platform))
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 platformCenter = collision.collider.bounds.center;
            Vector2 deviation = contactPoint - platformCenter;

            Vector2 newDirection = new Vector2(deviation.x, 1).normalized;
            _rb2D.velocity = newDirection * ballSpeed;
        }
    }
}
