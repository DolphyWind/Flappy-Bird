using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public static float pipeSpeed = .5f;
    
    void Start()
    {
        StartCoroutine(DestroyAfterDelay(7f));
    }

    void Update()
    {
        if(!Game.isDead && gameObject != null)
            transform.Translate(Vector2.left * pipeSpeed * Time.deltaTime);
    }

    // I know I can destroy objects with Destroy() function but I want them to stay if we die.
    IEnumerator DestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(!Game.isDead) Destroy(gameObject);
    }
}
