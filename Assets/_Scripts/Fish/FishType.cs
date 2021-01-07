using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FishType : MonoBehaviour
{
    #region Variables

    public int price;
    public float fishCount;
    public float minLenght;
    public float maxLenght;
    public float colliderRadius;
    public Sprite sprite;
    protected CircleCollider2D coll;
    protected SpriteRenderer rend;

    #endregion

    #region Properties
    public int Price { get { return price; } set { price = value; } }   
    public float FishCount { get { return fishCount; } set { fishCount = value; } }
    public float MinLenght { get { return minLenght; } set { minLenght = value; } }
    public float MaxLenght { get { return maxLenght; } set { maxLenght = value; } }
    public float ColliderRadius { get { return colliderRadius; } set { colliderRadius = value; } }
    public Sprite Sprite { get { return sprite; } set { sprite = value; } }
    public CircleCollider2D Coll { get { return coll; } set { coll = value; } }
    public SpriteRenderer Rend { get { return rend; } set { rend = value; } }

    #endregion

}
