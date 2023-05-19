using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private PlayerController _playerController;

    private Tile _gameObject;

    private bool mooving = false;

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        mooving = _playerController.moving;
        if (Physics.Raycast(ray, out var hitInfo, 1000f, _layerMask) && mooving == false)
        {
            if (_gameObject != hitInfo.collider.gameObject)
            {
                if (_gameObject != null)
                {
                    _gameObject.ChangeColor(Color.white);
                }
                
                _gameObject = hitInfo.collider.gameObject.GetComponentInParent<Tile>();
                _gameObject.ChangeColor(Color.gray);
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(_playerController.MovePlayer(_gameObject));               
            }
        }
        else
        {
            if (_gameObject != null && mooving == false)
            {
                _gameObject.GetComponent<Tile>().ChangeColor(Color.white);
            }
           
        }
    }


}
