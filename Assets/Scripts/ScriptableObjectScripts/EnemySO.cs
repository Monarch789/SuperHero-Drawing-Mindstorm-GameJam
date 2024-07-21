using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemySO",fileName = "EnemySO")]
public class EnemySO : ScriptableObject{
    public GameObject EnemyModelPrefab;
    public float Health;
    public float Damage;

}