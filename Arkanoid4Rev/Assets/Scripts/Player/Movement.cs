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

    private void Start()
    {
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
            Vector3 targetPosition = _playerObject.transform.position;

            if (touchPosition.x > _playerObject.transform.position.x + _movementOffsetX)
            {
                if (_playerObject.transform.position.x + 1f < _restriction)
                {
                    targetPosition.x += _movementSpeed * Time.deltaTime;
                }
                else
                {
                    targetPosition.x += _restriction - _playerObject.transform.position.x;
                }
            }
            else if (touchPosition.x < _playerObject.transform.position.x - _movementOffsetX)
            {
                if (_playerObject.transform.position.x - 1f > -_restriction)
                {
                    targetPosition.x -= _movementSpeed * Time.deltaTime;
                }
                else
                {
                    targetPosition.x -= _restriction + _playerObject.transform.position.x;
                }
            }

            MovePlatform(targetPosition);
        }
    }

    private void MovePlatform(Vector3 targetPosition)
    {
        _playerObject.transform.position = Vector3.MoveTowards(_playerObject.transform.position, targetPosition, _movementSpeed * Time.deltaTime);
    }

    private void SetRestriction()
    {
        _restriction = _restrictionObject.transform.position.x - _playerObject.transform.localScale.x / 2;
    }
}
