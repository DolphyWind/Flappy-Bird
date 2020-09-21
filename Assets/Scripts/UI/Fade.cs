using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Play fade animations in game.

    static Animator anim;
    static Image image;

    void Awake()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
    
    // Play animation with specified color
    public static void Play(string name, Color color)
    {
        image.color = color;
        anim.Play(name);
    }
}
