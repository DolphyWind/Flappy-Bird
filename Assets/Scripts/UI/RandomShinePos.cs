using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomShinePos : MonoBehaviour
{
    Vector2 startPos;
    public Sprite[] shineSprites;
    Image image;

    void Start()
    {
        // Top left
        startPos = transform.localPosition;
        startPos.x = Mathf.Abs(startPos.x);
        startPos.y = Mathf.Abs(startPos.y);
        image = GetComponent<Image>();

        StartCoroutine(RandomPosition());
    }

    IEnumerator RandomPosition()
    {
        while (true)
        {
            image.sprite = shineSprites[0];
            Vector2 newPos;
            newPos.x = Random.Range(-startPos.x, startPos.x);
            newPos.y = Random.Range(-startPos.y, startPos.y);
            transform.localPosition = newPos;

            for(int i = 0; i < shineSprites.Length; i++)
            {
                image.sprite = shineSprites[i];
                yield return new WaitForSeconds(.5f/shineSprites.Length);
            }
        }
    }
}
