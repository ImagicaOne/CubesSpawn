using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Map _map;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private Animator _animator;

    private GameObject _player;
    private Tile _start;

    public bool moving = false;

    public void CreatePlayer()
    {
        _player = Instantiate(_playerPrefab);
        _start = _map.GetRandomFreeTile();
        _player.transform.position = _start.transform.position + new Vector3(0,1,0);
        _animator = _player.GetComponent<Animator>();
    }

    public IEnumerator MovePlayer(Tile goal)
    {
        
        List<Tile> path = PathFinder.FindPath(_map.Tiles, _start, goal);
        if (path != null)
        {
            moving = true;
            _animator.SetBool(IsMoving, true);
            foreach (Tile tile in path)
            {
                tile.ChangeColor(Color.blue);
            }
            yield return StartCoroutine(Move(path));

            foreach (Tile tile in path)
            {
                tile.ChangeColor(Color.white);
            }

            _start = goal;
            moving = false;
            _animator.SetBool(IsMoving, false);
        }
       
    }

    private IEnumerator Move(List<Tile> path)
    {
       
        foreach (Tile tile in path)
        {
            _player.transform.LookAt(tile.transform.position + new Vector3(0, 1, 0));
            _player.transform.position = tile.transform.position + new Vector3(0,1,0);         
            yield return new WaitForSeconds(0.5f);
        }

        
    }

}