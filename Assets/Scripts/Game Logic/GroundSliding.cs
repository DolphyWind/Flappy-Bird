using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSliding : MonoBehaviour
{
    // This scripts slides ground
    public GameObject Ground1, Ground2;
    private Vector2 StartPos1;
    private Vector2 size1, size2;

    bool moveFirst = true;

    void Start()
    {
        StartPos1 = Ground1.transform.position;

        size1 = Ground1.GetComponent<SpriteRenderer>().bounds.size;
        size2 = Ground2.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        if (!Game.isDead)
        {
            //If Ground1 is left move ground1 and position ground2 manually
            if(moveFirst)
            {
                Ground1.transform.Translate(Vector2.left * Pipe.pipeSpeed * Time.deltaTime);
                Ground2.transform.position = Ground1.transform.position + new Vector3(size1.x, 0, 0);
            }
            //Else ground2 and position ground1 manually
            else
            {
                Ground2.transform.Translate(Vector2.left * Pipe.pipeSpeed * Time.deltaTime);
                Ground1.transform.position = Ground2.transform.position + new Vector3(size2.x, 0, 0);
            }

            // If not visible on camera
            if (Ground1.transform.position.x < StartPos1.x - size1.x)
            {
                Ground1.transform.position = new Vector2(Ground2.transform.position.x + size2.x, Ground1.transform.position.y);
                moveFirst = !moveFirst;
            }
            if (Ground2.transform.position.x < StartPos1.x - size2.x)
            {
                Ground2.transform.position = new Vector2(Ground1.transform.position.x + size1.x, Ground2.transform.position.y);
                moveFirst = !moveFirst;
            }
        }
    }

}
