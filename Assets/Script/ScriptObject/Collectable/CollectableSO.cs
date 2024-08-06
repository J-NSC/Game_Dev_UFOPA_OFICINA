using System.Collections;
using System.Collections.Generic;using System.Runtime.CompilerServices;
using UnityEngine;

public enum CollectabelType
{
    moeda,
    frutas
}

[CreateAssetMenu (fileName = "Collectable", menuName = "collectable/item")]
public class CollectableSO : ScriptableObject
{
    public CollectabelType collectabeltype;
    public Sprite sprite; 
    public int value;
    public int hpValue;

}


