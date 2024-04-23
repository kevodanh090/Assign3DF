using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
    private HealthBar healthBar;
    AnimatorManager animatorManager;


    private void Awake()
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }
    private void Start()
    {
        healthBar = (HealthBar)FindAnyObjectByType(typeof(HealthBar));
        //SetMaxHealthFromHealthLv();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        
    }
    
    //private void SetMaxHealthFromHealthLv()
    //{
    //    maxHealth = heatlhLevel * 10;
    //}
    public void TakeDamge(int dmage)
    {
        currentHealth = currentHealth - dmage;
        healthBar.SetCurrentHealth(currentHealth);
        animatorManager.TargetAnimation("Damged", true);
        if(currentHealth <= 0)
        {
            //Handle Player death;
            currentHealth = 0;
            animatorManager.TargetAnimation("Dead", true);
        }
    }
}
