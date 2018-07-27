using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_Pickup : MonoBehaviour
{
    [Header("- How Many Points This Is Worth -")]
    public int points;

    [Header("- Displaying The Sprite -")]
    public Sprite[] bonus_sprites;
    public int sprite_index;
    public SpriteRenderer sprite_renderer;

    private void Start()
    {
        sprite_renderer.sprite = bonus_sprites[sprite_index];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Data.game_data.score += points;
            Destroy(gameObject);
        }
    }
}
