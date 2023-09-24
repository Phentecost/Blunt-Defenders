using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance {get; private set;} = null;
    public enum Game_State
    {
        Preparation, Defending
    }

    public static Game_State _Current_Game_State {get; private set;}= Game_State.Preparation;
    [SerializeField] private float Preparation_fase_Time;
    [SerializeField] private GameObject _Player;
    [SerializeField] private int _nWaves;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Vector2 _Start_Pos;
    [SerializeField] private bool Tutorial;
    [SerializeField] private List<Flowchart> tutorial_Charts;
    private int tutorial_Waves_Index;
    public float timer {get;private set;} = 0;
    public bool OnPouse;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        OnPouse = false;
    }

    void Start()
    {
        Prep_Fase();
        Pause();
    }

    public void Pause()
    {
        OnPouse = !OnPouse;
    }

    public void Ready()
    {
        timer = 0;
    }

    void Update()
    {
        if(OnPouse) return;
        if(_Current_Game_State == Game_State.Preparation)
        {
            if(timer <= 0)
            {
                Defending_Fase();
            }
            else
            {
                timer -= Time.deltaTime;
            }

            txt.text = "Time: " + MathF.Round(timer);
        }
        else
        {
            return;
        }
    }

    private void Prep_Fase()
    {
        Camera_Manager.Instance.Change_Camera(1);
        _Player.SetActive(true);
        _Player.transform.position = _Start_Pos;
        timer = Preparation_fase_Time;
        UI_Manager.Instance.Change_To_Prep();
        Target_Manager.Instance.Activate_Indicators(Wave_Manager.Instance.Check_Wave_Paths());
        _Current_Game_State = Game_State.Preparation;
    }

    private void Defending_Fase()
    {
        Camera_Manager.Instance.Change_Camera(0);
        _Player.SetActive(false);
        UI_Manager.Instance.Change_To_Game();
        UI_WS_Manager.Instance.Cancel_Preview();
        Target_Manager.Instance.Deactivate_Indicators();
        _Current_Game_State = Game_State.Defending; 

        if(Tutorial)
        {
            int i,count;
            Wave_Manager.Instance.Config(Prep_Fase,tutorial_Charts[tutorial_Waves_Index], out i , out count);
            tutorial_Waves_Index++;
            if(i == count)
            {
                Tutorial = false;
            }
        }
        else
        {
            Wave_Manager.Instance.Config(_nWaves, Prep_Fase);
        }
        
    }

    
}
