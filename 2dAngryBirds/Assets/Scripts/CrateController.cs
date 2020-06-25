using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    private HPController crateHP;

    private void Start()
    {
        crateHP = GetComponent<HPController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage damageble = collision.collider.GetComponent<ITakeDamage>();
        if (damageble != null) damageble.TakeDamage(3);
    }
}
