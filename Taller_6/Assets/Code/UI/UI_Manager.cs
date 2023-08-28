using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance {get; private set;} = null;
    [SerializeField] private GameObject Lose_Panel;
    [SerializeField] private GameObject Game_Panel;
    [SerializeField] private GameObject Prep_Panel; 
    [SerializeField] private GameObject Deploy_Panel;

    [SerializeField] private GameObject trap;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    public void OnLose()
    {
        Lose_Panel.SetActive(true);
        Game_Panel.SetActive(false);
    }

    public void Change_To_Prep()
    {
        Prep_Panel.SetActive(true);
        Game_Panel.SetActive(false);
    }

    public void Change_To_Game()
    {
        Prep_Panel.SetActive(false);
        Game_Panel.SetActive(true);
    }

    public void Deploy_Panel_Activation()
    {
        if(Deploy_Panel.activeInHierarchy)
        {
            Deploy_Panel.SetActive(false);
        }
        else
        {
            Deploy_Panel.SetActive(true);
        }
    }

    public void dawdasda()
    {
        Instantiate(trap,Player_Interaction.Deploy_Pos,Quaternion.identity);
        Deploy_Panel_Activation();
    }
}
