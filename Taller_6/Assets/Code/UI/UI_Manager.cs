using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance {get; private set;} = null;
    [SerializeField] private GameObject Lose_Panel;
    [SerializeField] private GameObject Game_Panel;
    [SerializeField] private GameObject Prep_Panel; 
    
    [SerializeField] private GameObject How_To_Panel;
    [SerializeField] private GameObject Main_Panel;
    [SerializeField] private GameObject Cant_Buy_Panel;
    private bool panel_Active;
    private float timer = 1;
    [SerializeField] private TextMeshProUGUI coins_TXT, life_TXT, weed_TXT;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    void Update()
    {
        if(!panel_Active)return;

        if(timer <= 0)
        {
            panel_Active = false;
            Cant_Buy_Panel.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
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

    

    public void How_To_Panel_Activation()
    {
        if(How_To_Panel.activeInHierarchy)
        {
            How_To_Panel.SetActive(false);
        }
        else
        {
            How_To_Panel.SetActive(true);
        }
    }

    public void Main_Panel_Activation()
    {
        if(Main_Panel.activeInHierarchy)
        {
            Main_Panel.SetActive(false);
        }
        else
        {
            Main_Panel.SetActive(true);
        }
    }

    public void UpdateLife(int i)
    {
        life_TXT.text = "Life: " + i;
    }

    public void UpdateCoins(int i)
    {
        coins_TXT.text = "Coins: " + i;
    }

    public void UpdateWeed(int i)
    {
        weed_TXT.text = "Weed: " + i;
    }

    public void Cant_Buy_Panel_Activation()
    {
        timer = 1;
        panel_Active = true;
        Cant_Buy_Panel.SetActive(true);
    }
}
