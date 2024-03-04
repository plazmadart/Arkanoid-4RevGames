using UnityEngine;

public class WallsScale : MonoBehaviour
{
    [SerializeField] private GameObject _restictionXObject;

    private GameObject _wall;
    private float _scale;

    private void Start()
    {
        _wall = this.gameObject;

        _scale = _restictionXObject.transform.position.x * 2;
        SetWallScale(_scale);
    }

    private void SetWallScale(float scale)
    {
        _wall.transform.localScale = new Vector2(scale, _wall.transform.localScale.y);
    }
}
