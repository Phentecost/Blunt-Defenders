using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance {get; private set;} = null;
    [SerializeField] private GameObject Lose_Panel;
    [SerializeField] private GameObject Game_Panel;
    [SerializeField] private GameObject Prep_Panel; 
    [SerializeField] private GameObject Main_Panel;
    [SerializeField] private GameObject Pause_Panel;
    [SerializeField] private GameObject Cant_Buy_Panel;
    [SerializeField] private GameObject Win_Round_Panel;
    [SerializeField] private GameObject Next_Round_Panel;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject[] Enemies_Panels;
    [SerializeField] private GameObject Sub_Game_Panel;
    [SerializeField] private VideoPlayer[] videos;
    private bool panel_Active;
    private float timer = 1;
    [SerializeField] private TextMeshProUGUI coins_TXT, life_TXT, weed_TXT;
    [SerializeField] private Image life_IMG;
    [SerializeField] private List<Sprite> life_Sprites;

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

    public void Sub_Game_Panel_Activation()
    {
        if(Sub_Game_Panel.activeInHierarchy)
        {
            Sub_Game_Panel.SetActive(false);
        }
        else
        {
            Sub_Game_Panel.SetActive(true);
        }
    }

    public void Win_Round_Panel_Activation()
    {
        if(Win_Round_Panel.activeInHierarchy)
        {
            Win_Round_Panel.SetActive(false);
        }
        else
        {
            Win_Round_Panel.SetActive(true);
        }
    }

    public void Map_Activation()
    {
        if(Map.activeInHierarchy)
        {
            Map.SetActive(false);
        }
        else
        {
            Map.SetActive(true);
        }
    }

    public void Next_Round_Panel_Activation()
    {
        if(Next_Round_Panel.activeInHierarchy)
        {
            Next_Round_Panel.SetActive(false);
        }
        else
        {
            Next_Round_Panel.SetActive(true);   
        }
    }

    public void Pause_Panel_Activation()
    {
        if(Pause_Panel.activeInHierarchy)
        {
            Pause_Panel.SetActive(false);
        }
        else
        {
            Pause_Panel.SetActive(true);
        }
    }

    public void UpdateLife(float i)
    {
        life_TXT.text = i.ToString();
        
        if(i/100 < 0.75)
        {
            life_IMG.sprite = life_Sprites[0];
        }
        else if(i/100 <0.5)
        {
            life_IMG.sprite = life_Sprites[1];
        }
        else if(i/100<0.25)
        {
            life_IMG.sprite = life_Sprites[2];
        }
    }

    public void Enemies_Panels_Activations(int i)
    {
        if(Enemies_Panels[i].activeInHierarchy)
        {
            videos[i].Stop();
            Enemies_Panels[i].SetActive(false);
        }
        else
        {
            videos[i].Play();
            Enemies_Panels[i].SetActive(true);
        }
    }

    public void UpdateCoins(int i)
    {
        coins_TXT.text = i.ToString();
    }

    public void UpdateWeed(int i)
    {
        //weed_TXT.text = "Weed: " + i;
    }

    public void Cant_Buy_Panel_Activation()
    {
        timer = 1;
        panel_Active = true;
        Cant_Buy_Panel.SetActive(true);
    }
}
