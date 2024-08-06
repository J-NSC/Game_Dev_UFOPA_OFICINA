using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy
{
    poluidor, 
    desmatador,
    cacador
}

[CreateAssetMenu(fileName = "Enemy", menuName = "enemy/type")]
public class EnemySO : ScriptableObject
{
    public TypeEnemy type;
    public int hp;
    public float speed;
    public float damageValue;
    public float attackArea;
    public float visionRange;
}
