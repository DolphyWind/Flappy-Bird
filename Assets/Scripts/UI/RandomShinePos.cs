using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomShinePos : MonoBehaviour
{
    Vector2 startPos;

    void Start()
    {
        // Top left
        startPos = transform.localPosition;
        startPos.x = Mathf.Abs(startPos.x);
        startPos.y = Mathf.Abs(startPos.y);

        StartCoroutine(RandomPosition());
    }

    IEnumerator RandomPosition()
    {
        while (true)
        {
            Vector2 newPos;
            newPos.x = Random.Range(-startPos.x, startPos.x);
            newPos.y = Random.Range(-startPos.y, startPos.y);
            transform.localPosition = newPos;
            yield return new WaitForSeconds(.5f);
        }
    }
}
