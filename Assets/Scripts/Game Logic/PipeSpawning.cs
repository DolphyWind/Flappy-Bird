using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawning : MonoBehaviour
{
    // The script spawns pipes

    public GameObject pipeGroup;
    float pipeDelay = 1.5f;
    float pipeRandomHeight = .5f;
    bool started = false;

    void Update()
    {
        if(!started && Game.isStarted)
            StartCoroutine(StartSpawning(pipeDelay));
    }

    IEnumerator StartSpawning(float startDelay)
    {
        started = true;
        while(Game.isStarted && !Game.isDead)
        {
            yield return new WaitForSeconds(startDelay);
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + Random.Range(-pipeRandomHeight, pipeRandomHeight));
            Instantiate(pipeGroup, pos, Quaternion.identity);
        }
    }
}
