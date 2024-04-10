using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharsSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] chars;
    public int selectedCharacter = 0;

    public void NextCharacter()
    {
        chars[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % chars.Length;
        chars[selectedCharacter].SetActive(true);
    }
    public void PreviousCharacter() 
    {
        chars[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += chars.Length;
        }
        chars[selectedCharacter].SetActive(true);
    }
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
