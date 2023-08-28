using System;
using System.Collections;
using System.Collections.Generic;
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
    public float timer {get;private set;} = 0;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Prep_Fase();
    }

    void Update()
    {
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
        _Player.SetActive(true);
        _Player.transform.position = Vector2.zero;
        timer = Preparation_fase_Time;
        UI_Manager.Instance.Change_To_Prep();
        _Current_Game_State = Game_State.Preparation;

    }

    private void Defending_Fase()
    {
        _Player.SetActive(false);
        UI_Manager.Instance.Change_To_Game();
        _Current_Game_State = Game_State.Defending; 
        Wave_Manager.Instance.Config(_nWaves, Prep_Fase);
    }

    
}
