using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Plays Game
    public void PlayGame()
    {
        Fade.Play("Fade Out", new Color(0, 0, 0, 0));
        StartCoroutine(WaitAndLoadScene(.5f));
    }

    IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
