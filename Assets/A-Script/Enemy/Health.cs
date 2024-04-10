using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onDie;
    public int maxHealthPoint;
    private int healthPoint;
    private bool IsDead => healthPoint <= 0;
    private void Start() => healthPoint = maxHealthPoint;
    public void TakeDame()
    {

    }
    private void Die()
    {
        animator.SetTrigger("Die");
        onDie.Invoke();
    }    
}
