using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour, ITakeDamage
{
    private int currentHP;
    [SerializeField] private int maxHP = 5;
    private void Start()
    {
        currentHP = maxHP;
    }

    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0) DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
