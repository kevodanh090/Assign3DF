using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgePlayer : MonoBehaviour
{
    public int dmg = 25;
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamge(dmg);
        }
    }
}
