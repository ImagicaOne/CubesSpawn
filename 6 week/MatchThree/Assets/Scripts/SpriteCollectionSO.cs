using UnityEngine;

[CreateAssetMenu]
public class SpriteCollectionSO : ScriptableObject
{
    public Sprite[] Items => _items;

    [SerializeField]
    private Sprite[] _items;
}
