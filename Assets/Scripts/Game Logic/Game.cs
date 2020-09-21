using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject getReadyMenu;
    private Animator[] getReadyAnimators;
    public GameOverMenu gameOverMenu;

    // Game controllers
    public static bool isDead = false;
    public static bool isStarted = false;
    public static bool fellDown = false;
    bool whiteFadePlayed = false;

    // Jump
    public float jumpSpeed;
    public float fallingConstant;
    private float vertSpeed = 0;

    // Score
    private int score = 0;
    public TextMeshProUGUI scoreText;

    // Player components
    private Animator anim;

    void Start()
    {
        // Play Fade In animation at start
        Fade.Play("Fade In", new Color(0, 0, 0, 0));

        anim = GetComponent<Animator>();
        getReadyMenu.SetActive(true);
        getReadyAnimators = getReadyMenu.GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        if (!isDead)
            Fly();
        
        // Fell down when died
        else if (!fellDown)
            transform.Translate(Vector2.up * vertSpeed * Time.deltaTime, Space.World);

        if (isStarted)
        {
            RotatePlayerHead();
            // Decrease Vertical speed
            vertSpeed -= fallingConstant * Time.deltaTime;
        }
    }

    private void Fly()
    {
        // Fly when pressed
        if (Input.GetMouseButtonDown(0) && transform.position.y < 1.23f)
        {
            // Starts game plays Get Ready Out Animation
            if (!isStarted)
            {
                isStarted = true;
                for (int i = 0; i < getReadyAnimators.Length; i++)
                    getReadyAnimators[i].Play("Get Ready Out");
            }

            vertSpeed = jumpSpeed;
            SoundManager.Play(SoundManager.AudioType.wing);
        }
        if (isStarted)
            transform.Translate(Vector2.up * vertSpeed * Time.deltaTime, Space.World);
    }

    private void RotatePlayerHead()
    {
        // Rotate player's head by vertical speed
        float angle;
        float minSpeed = -1.5f;
        if (vertSpeed > minSpeed)
            angle = (vertSpeed - minSpeed) * 45;
        else angle = (vertSpeed - minSpeed) * 90;

        angle = Mathf.Clamp(angle, -90, 20);
        transform.eulerAngles = new Vector3(0, 0, angle);
        anim.SetFloat("angle", angle);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            // Add score
            if (other.tag == "Score Area")
            {
                score++;
                scoreText.text = score.ToString();
                SoundManager.Play(SoundManager.AudioType.point);
            }
            // Die
            else
            {
                isDead = true;
                SoundManager.Play(SoundManager.AudioType.hit);
                StartCoroutine(PlaySoundAfterDelay(SoundManager.AudioType.die, .2f));

                // Play Game Over Animation
                StartCoroutine(EnableGameOver(1f));

                // If player collided with ground don't make it fall.
                if (other.tag == "Ground")
                    BirdDie();

                // Play White Fade
                if (!whiteFadePlayed)
                    Fade.Play("White Fade", new Color(1, 1, 1, 0));
                whiteFadePlayed = true;
            }
        }
        else
        {
            // If player collided with ground stop falling
            if (other.tag == "Ground")
                BirdDie();
        }
    }

    private void BirdDie()
    {
        transform.position = new Vector3(transform.position.x, -0.66f, transform.position.z);
        fellDown = true;
    }

    private IEnumerator PlaySoundAfterDelay(SoundManager.AudioType audioType, float delay)
    {
        yield return new WaitForSeconds(delay);
        SoundManager.Play(audioType);
    }

    // Restarts game
    public void RestartGame()
    {
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        Fade.Play("Fade Out", new Color(0, 0, 0, 0));
        yield return new WaitForSeconds(.5f);

        isStarted = false;
        isDead = false;
        fellDown = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    // Enables Game Over Menu
    private IEnumerator EnableGameOver(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scoreText.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(true);
        gameOverMenu.setScore(score);
    }
}
