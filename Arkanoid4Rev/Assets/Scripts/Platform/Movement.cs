using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player object")]
    [SerializeField] private GameObject _playerObject;

    [Header("Player parameters")]
    [SerializeField] private float _movementSpeed = 20f;
    [SerializeField] private float _movementOffsetX = 0.3f;
    [SerializeField] private float _restriction = 5f;

    private Camera _mainCamera;
    private GameObject _restrictionObject;
    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;

        _mainCamera = Camera.main;

        _restrictionObject = GameObject.FindGameObjectWithTag("Restriction");

        if (_playerObject == null)
        {
            _playerObject = GameObject.FindGameObjectWithTag("Player");
        }

        SetRestriction();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
            Vector3 targetPosition = transform.position;

            if (touchPosition.x > transform.position.x + _movementOffsetX)
            {
                if (transform.position.x + _movementOffsetX < _restriction)
                {
                    targetPosition.x += _movementSpeed * Time.deltaTime;
                }
                else
                {
                    targetPosition.x += _restriction - transform.position.x;
                }
            }
            else if (touchPosition.x < transform.position.x - _movementOffsetX)
            {
                if (transform.position.x - _movementOffsetX > -_restriction)
                {
                    targetPosition.x -= _movementSpeed * Time.deltaTime;
                }
                else
                {
                    targetPosition.x -= _restriction + transform.position.x;
                }
            }

            MovePlatform(targetPosition);
        }
    }

    private void MovePlatform(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);
    }

    private void SetRestriction()
    {
        _restriction = _restrictionObject.transform.position.x - transform.localScale.x / 2;
    }

    public void MoveToStartPosition()
    {
        transform.position = _startPosition;
    }
}
