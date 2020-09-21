using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameManager : MonoBehaviour
{
    // This script randomizes Background and Bird color
    // Also makes bird go up and down.

    public Sprite[] backgrounds;
    public RuntimeAnimatorController[] animatorControllers;
    float degree = 0;
    private Vector2 originalPos;
    public GameObject player;
    public GameObject background;

    void Start()
    {
        Application.targetFrameRate = 60;
        originalPos = player.transform.position;
        SelectBackground();
        SelectPlayer();
    }

    void Update()
    {
        if(!Game.isStarted)
            MovePlayerUpDown();
    }

    private void MovePlayerUpDown()
    {
        // Moving player up and down using sine function
        float movement = Mathf.Sin(degree * Mathf.Deg2Rad) * 0.1f;
        player.transform.Translate(Vector2.up * movement * Time.deltaTime);
        degree += 10;
        if(degree >= 360) degree = 0;
    }

    // Select a random color for player
    private void SelectPlayer()
    {
        int i = Random.Range(0, animatorControllers.Length);
        player.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[i];
    }

    // Select a random background
    private void SelectBackground()
    {
        int bg_index = Random.Range(0, backgrounds.Length);
        background.GetComponent<SpriteRenderer>().sprite = backgrounds[bg_index];
    }
    
}
