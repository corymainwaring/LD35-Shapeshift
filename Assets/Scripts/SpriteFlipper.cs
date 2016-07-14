using UnityEngine;
using System.Collections;

public class SpriteFlipper : MonoBehaviour {

    public Sprite[] Sprites;
    int currentSprite;
    SpriteRenderer Renderer;

	// Use this for initialization
	void Start () {
        currentSprite = 0;
        Renderer = GetComponent<SpriteRenderer>();
        if (Renderer)
        {
            Renderer.sprite = Sprites[0];
        }
	}

    public Sprite Current()
    {
        return Sprites[currentSprite];
    }

    public Sprite Next()
    {
        return Sprites[(currentSprite + 1) % Sprites.Length];
    }

    public void Flip()
    {
        currentSprite = (currentSprite + 1) % Sprites.Length;

        if (Renderer)
        {
            Renderer.sprite = Sprites[currentSprite];
        }
    }
}
