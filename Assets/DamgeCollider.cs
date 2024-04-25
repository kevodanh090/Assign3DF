using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeCollider : MonoBehaviour
{
    Collider dmgCollider;
    public int currentWeaponDmg = 25;
    private void Awake()
    {
        dmgCollider = GetComponent<Collider>();
        dmgCollider.gameObject.SetActive(true);
        dmgCollider.isTrigger = true;
        dmgCollider.enabled = false;
    }
    public void EnableDmgCollider()
    {
        dmgCollider.enabled = true;
    }
    public void DisableDmgCollider()
    {
        dmgCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Enemy")
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            
            if (playerStats != null)
            {
                playerStats.TakeDamge(currentWeaponDmg);
            }
        }
        
    }

}
