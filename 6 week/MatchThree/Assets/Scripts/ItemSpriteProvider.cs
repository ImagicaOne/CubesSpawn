using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemSpriteProvider
{
    [SerializeField]
    private SpriteCollectionSO _spriteCollection;

    public Sprite GetRandomSprite()
    {
        return _spriteCollection.Items[Random.Range(0, _spriteCollection.Items.Length)];
    }
}
