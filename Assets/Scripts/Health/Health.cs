using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currenthealth;

    private void Awake()
    {
        currenthealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startingHealth);
    }

    public void AddLife(float _life)
    {
        currenthealth = Mathf.Clamp(currenthealth + _life, 0, startingHealth);
    }
}
