using UnityEngine;

public class WallsPosition : MonoBehaviour
{
    [SerializeField] private GameObject _restrictionXObject;

    private GameObject _wall;
    private float _restriction;

    private void Start()
    {
        _wall = this.gameObject;

        if (_wall.transform.position.x > 0)
        {
            _restriction = _restrictionXObject.transform.position.x + _wall.transform.localScale.x / 2;
            SetWallPositionX(_restriction, "right");
        }
        else if (_wall.transform.position.x < 0)
        {
            _restriction = _restrictionXObject.transform.position.x + _wall.transform.localScale.x / 2;
            SetWallPositionX(_restriction, "left");
        }
    }

    private void SetWallPositionX(float restriction, string direction)
    {
        if(direction == "right" && _wall.transform.position.x != _restriction)
        {
            _wall.transform.position = new Vector2(_restriction, _wall.transform.position.y);
        }
        else if (direction == "left" && _wall.transform.position.x != -_restriction)
        {
            _wall.transform.position = new Vector2(-_restriction, _wall.transform.position.y);
        }
    }
}
