using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [Header("- The Explosions Going Out From The Bomb -")]
    public GameObject up_explosion;
    public GameObject right_explosion;
    public GameObject down_explosion;
    public GameObject left_explosion;

    // Script attached to the player
    private Player_Movement player_script;

    // Stats of bomb
    private int bomb_strength;
    private float bomb_timer;

    // Called on first frame
    void Start()
    {
        // initalises player_script
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }

    // Called when the bomb is spawned
    public void Bomb_Spawned (int strength, float timer, bool detonator)
    {
        // Sets local variables with variables form function
        bomb_strength = strength;
        bomb_timer = timer;

        // Changes size of blast area
        if (bomb_strength > 1)
        {
            // Up Explosion Size Changer
            up_explosion.transform.position = new Vector3(up_explosion.transform.position.x, up_explosion.transform.position.y, up_explosion.transform.position.z + (0.5f * (bomb_strength - 1)));
            up_explosion.transform.localScale = new Vector3(up_explosion.transform.localScale.x, up_explosion.transform.localScale.y, up_explosion.transform.localScale.z + bomb_strength - 1);

            // Right Explosion Size Changer
            right_explosion.transform.position = new Vector3(right_explosion.transform.position.x + (0.5f * (bomb_strength - 1)), right_explosion.transform.position.y, right_explosion.transform.position.z);
            right_explosion.transform.localScale = new Vector3(right_explosion.transform.localScale.x + bomb_strength - 1, right_explosion.transform.localScale.y, right_explosion.transform.localScale.z);

            // Down Explosion Size Changer
            down_explosion.transform.position = new Vector3(down_explosion.transform.position.x, down_explosion.transform.position.y, down_explosion.transform.position.z - (0.5f * (bomb_strength - 1)));
            down_explosion.transform.localScale = new Vector3(down_explosion.transform.localScale.x, down_explosion.transform.localScale.y, down_explosion.transform.localScale.z + bomb_strength - 1);

            // left Explosion Size Changer
            left_explosion.transform.position = new Vector3(left_explosion.transform.position.x - (0.5f * (bomb_strength - 1)), left_explosion.transform.position.y, left_explosion.transform.position.z);
            left_explosion.transform.localScale = new Vector3(left_explosion.transform.localScale.x + bomb_strength - 1, left_explosion.transform.localScale.y, left_explosion.transform.localScale.z);
        }

        // Stops the bomb from exploding on timer
        if(!detonator)
        {
            StartCoroutine(Wait_timer());
        }
    }

    // Called when bomb is caught in blast of another bomb
    public void Chain_Bang ()
    {
        GameObject.Find("Bonus").GetComponent<Bonuses>().Chain_Bomb();
        bomb_timer = 0.1f;
        StartCoroutine(Wait_timer());
    }

    // Timer on bomb till detonation
    IEnumerator Wait_timer ()
    {
        yield return new WaitForSeconds(bomb_timer);
        Bang();
    }

    // Used to destroy surrounding area
    public void Bang ()
    {
        Explosion ue = up_explosion.GetComponent<Explosion>();
        ue.Blow_Up_Walls();

        Explosion re = right_explosion.GetComponent<Explosion>();
        re.Blow_Up_Walls();

        Explosion de = down_explosion.GetComponent<Explosion>();
        de.Blow_Up_Walls();

        Explosion le = left_explosion.GetComponent<Explosion>();
        le.Blow_Up_Walls();

        StartCoroutine(Secondary_Timer());
    }

    // For chaining Bombs
    IEnumerator Secondary_Timer ()
    {
        yield return new WaitForSeconds(0.01f);
        player_script.bombs.Remove(this);

        Explosion[] explosions = FindObjectsOfType<Explosion>();

        foreach (Explosion exp in explosions)
        {
            exp.wall_colliders.Remove(this.GetComponent<Collider>());
        }

        Destroy(gameObject);
    }
}
