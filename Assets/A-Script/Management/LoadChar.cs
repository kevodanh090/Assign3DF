using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChar : MonoBehaviour
{
    [SerializeField] private GameObject[] chars;
    [SerializeField] private Transform respawnPoint;


    public void Start()
    {
        int selectedChar = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefabChar = chars[selectedChar];
        GameObject cloneChar = Instantiate(prefabChar, respawnPoint.position, Quaternion.identity);

    }
}
