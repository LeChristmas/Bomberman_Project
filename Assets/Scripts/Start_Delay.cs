using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Delay : MonoBehaviour
{
    [Header("- All the Scripts In The Level That Need A Delay To Work -")]
    private Data local_data;
    public Bonuses local_bonuses;
    private Level_Exit local_level_exit;
    public UI local_ui;
    public Wall_Spawner local_wall_spawner;

	// Use this for initialization
	void Start ()
    {
        local_data = Data.game_data;
        StartCoroutine(Initial_Delay());
	}

    IEnumerator Initial_Delay ()
    {
        yield return new WaitForSeconds(0.1f);
        local_wall_spawner.Delay();
        local_data.Delay();
        local_bonuses.Select_Bonus();

        yield return new WaitForSeconds(0.1f);
        local_level_exit = local_wall_spawner.exit;
        local_level_exit.Delay();
        local_ui.Delay();


        yield return new WaitForSeconds(2.0f);
        local_bonuses.started = true;
    }
}
