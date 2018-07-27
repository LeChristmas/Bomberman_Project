using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("- Array Of All The Menu Sections -")]
    public GameObject[] menu_sections;

    [Header("- String Of The Game Scene Name -")]
    public string levelname;

    [Header("- variables For Displaying Saves -")]
    public GameObject save_ui_prefab;
    public Transform ui_start_point;

    // Called When The Start Button Is Pressed
    public void Start_Button ()
    {
        if (levelname != null)
        {
            Data.game_data.current_scene = Scene.Game;
            SceneManager.LoadScene(levelname);
        }
    }

    // Called When The Scores Button Is Pressed
    public void Scores ()
    {
        for (int i = 0; i < menu_sections.Length; i++)
        {
            if (i == 1)
            {
                menu_sections[i].SetActive(true);
            }
            else
            {
                menu_sections[i].SetActive(false);
            }
        }

        Data.game_data.Load_Data();
        Display_Scores();
    }

    // Called When The Return Button Is Pressed
    public void Return ()
    {
        for (int i = 0; i < menu_sections.Length; i++)
        {
            if (i == 0)
            {
                menu_sections[i].SetActive(true);
            }
            else
            {
                menu_sections[i].SetActive(false);
            }
        }

        GameObject[] saves = GameObject.FindGameObjectsWithTag("Save_Text");

        foreach (GameObject local_save in saves)
        {
            Destroy(local_save);
        }
    }

    // Called When the Quit Button Is Pressed
    public void Quit()
    {
        // Quits The Application
        Application.Quit();
    }

    void Display_Scores ()
    {
        float start_y_position = ui_start_point.transform.position.y;

        for (int i = 0; i < 10; i++)
        {
            if (Data.game_data.score_name[i] != "" || Data.game_data.score_number[i] != 0)
            {
                Vector3 new_position = new Vector3(ui_start_point.transform.position.x, start_y_position, ui_start_point.transform.position.z);

                GameObject save_object = Instantiate(save_ui_prefab, new_position, ui_start_point.transform.rotation, gameObject.transform) as GameObject;

                Text save_text = save_object.GetComponent<Text>();
                save_text.text = (i + 1) + ". Name: " + Data.game_data.score_name[i] + " Score: " + Data.game_data.score_number[i];

                start_y_position -= 50.0f;
            }
        }
    }

    public void Clear_Saves ()
    {
        GameObject[] saves = GameObject.FindGameObjectsWithTag("Save_Text");

        foreach (GameObject local_save in saves)
        {
            Destroy(local_save);
        }

        Data.game_data.Clear_Data();

        Display_Scores();
    }
}
