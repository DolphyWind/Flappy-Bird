using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    // Splash Screen
    // I know you can make splash screens in Player Settings.
    // But I wanted to make it different.

    void Start()
    {
        StartCoroutine(ChangeScene(2.5f));
    }

    IEnumerator ChangeScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
