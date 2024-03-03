using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player components")]
    [SerializeField] private GameObject _playerObject;

    [Header("Player parameters")]
    [SerializeField] private float _movementSpeed = 20f;
    [SerializeField] private float _movementOffsetX = 0f;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
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
                targetPosition.x += _movementSpeed * Time.deltaTime;
            }
            else if (touchPosition.x < _playerObject.transform.position.x - _movementOffsetX)
            {
                targetPosition.x -= _movementSpeed * Time.deltaTime;
            }

            MovePlatform(targetPosition);
        }
    }

    private void MovePlatform(Vector3 targetPosition)
    {
        _playerObject.transform.position = Vector3.MoveTowards(_playerObject.transform.position, targetPosition, _movementSpeed * Time.deltaTime);
    }
}
