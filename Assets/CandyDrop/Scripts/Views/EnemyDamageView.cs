using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageView : MonoBehaviour
{
    [Header("Дамаг")]
    [SerializeField] private int _damage = 1;

    public int Damage => _damage;
}
