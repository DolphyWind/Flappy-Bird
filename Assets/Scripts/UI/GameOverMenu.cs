using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOverMenu : MonoBehaviour
{
    // Activates and animates GameOver menu

    // Elements of GameOver menu
    public Animator gameOver;
    public Animator board;
    public GameObject playButton;
    public GameObject newImage;

    public TextMeshProUGUI scoreText, bestText;
    public Image medalImage;
    public Sprite[] medals;

    // Game Over Menu Animation Controllers
    bool gameOverPlayStarted = false;
    bool gameOverPlayEnded = false;
    bool boardMoveStarted = false;
    bool boardMoveEnded = false;
    bool scoreCoroutineStarted = false;
    bool scoreCoroutineEnded = false;

    // Score values
    int score, bestScore;

    public void setScore(int score)
    {
        this.score = score;
    }

    // Controls Game Over Menu
    void Update()
    {
        if (!gameOver.gameObject.activeSelf) return;

        if (!gameOverPlayStarted)
            StartCoroutine(GameOverAnimation());

        else if (gameOverPlayEnded && !boardMoveStarted)
            StartCoroutine(MoveBoardToCenter());

        else if (boardMoveEnded && !scoreCoroutineStarted)
            StartCoroutine(ScoreBoard());
            
        else if (scoreCoroutineEnded)
            StartCoroutine(EnablePlayButton());
    }

    //////////////
    //////////////

    // Game over image animation
    private IEnumerator GameOverAnimation()
    {
        gameOverPlayStarted = true;
        SoundManager.Play(SoundManager.AudioType.swooshing);
        gameOver.Play("Game Over");
        yield return new WaitForSeconds(1f);
        gameOverPlayEnded = true;
    }

    // Moves board to center
    private IEnumerator MoveBoardToCenter()
    {
        boardMoveStarted = true;
        SoundManager.Play(SoundManager.AudioType.swooshing);
        bestScore = PlayerPrefs.GetInt("Best", 0);
        bestText.text = bestScore.ToString();
        board.Play("Board");
        yield return new WaitForSeconds(.5f);
        boardMoveEnded = true;
    }

    // Controls score board
    IEnumerator ScoreBoard()
    {
        scoreCoroutineStarted = true;
        int s = 0;
        while (s < score)
        {
            yield return new WaitForSeconds(1f / score);
            s++;
            scoreText.text = s.ToString();
        }
        if (score > bestScore)
        {
            yield return new WaitForSeconds(.2f);
            newImage.SetActive(true);
            bestText.text = score.ToString();
            PlayerPrefs.SetInt("Best", score);
        }
        GiveMedal();
        scoreCoroutineEnded = true;
    }

    // Gives Medal
    private void GiveMedal()
    {
        if (score < 10) return;
        int index = (score / 10) - 1;
        if (index >= medals.Length) index = medals.Length - 1;
        medalImage.sprite = medals[index];
        medalImage.gameObject.SetActive(true);
    }

    // Enables Play Button
    private IEnumerator EnablePlayButton()
    {
        yield return new WaitForSeconds(.5f);
        playButton.SetActive(true);
    }
}
