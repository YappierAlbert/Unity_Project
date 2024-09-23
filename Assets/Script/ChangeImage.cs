using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image image;
    public List<Sprite> spriteChoices;

    private int currentSprite = 0;

    void Update()
    {
        // Check for input in Update
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextSprite();
        }
    }

    public void NextSprite()
    {
        // Change to the next sprite
        currentSprite++;
        if (currentSprite >= spriteChoices.Count)
        {
            currentSprite = 0;
        }
        image.sprite = spriteChoices[currentSprite];
    }
}
