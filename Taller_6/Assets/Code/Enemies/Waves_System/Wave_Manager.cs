using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;
using Enemies;

public class Wave_Manager : MonoBehaviour
{
    public static Wave_Manager Instance {get;private set;} = null;
    private int _wave_Index = 0;
    private Wave _wave;
    private int waves_left;
    private event Action OnEnding;
    bool Flowchart_Displayed;
    [SerializeField] Flowchart onWin;
    private Flowchart tutorial_chart;
    private List<Enemy> Tutorial_Enemies;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        Flowchart_Displayed = false;
        _wave = new Wave();
    }

    void Start()
    {
        Enemy_Spawn_System.OnWaveEnd += OnWaveEnds;
    }

    void OnDisable()
    {
        Enemy_Spawn_System.OnWaveEnd -= OnWaveEnds;
    }

    public void Config(int i, Action OnEnding)
    {
        waves_left = i;
        this.OnEnding = OnEnding;
        StartCoroutine(NextWave(_wave,false));
    }

    public void Config(Wave wave, Action OnEnding, Flowchart chart)
    {
        waves_left = 1;
        _wave_Index--;
        this.OnEnding = OnEnding;
        tutorial_chart = chart;
        StartCoroutine(NextWave(wave,true));
    }

    void StartWave(Wave wave,bool tutorial)
    {
        _wave_Index++;
        Increase_Dificulty();
        StartCoroutine(Enemy_Spawn_System.Instance.SpawnWave(wave,tutorial));
    }

    public IEnumerator Tutorial_Fungus(List<Enemy> enemies)
    {
        Tutorial_Enemies = enemies;
        yield return new WaitForSeconds(3);
        foreach(Enemy enemy in Tutorial_Enemies)
        {
            enemy.Activation();
        }
        
        //Play al fungus
    }

    public void Finish_Fungus()
    {
        foreach(Enemy enemy in Tutorial_Enemies)
        {
            enemy.Activation();
        }

        Tutorial_Enemies.Clear();
    }

    void OnWaveEnds()
    {
        if(waves_left == 0)
        {
            if(!Flowchart_Displayed){onWin.ExecuteBlock("win"); Flowchart_Displayed = true;}
            OnEnding();
        }
        else
        {
            StartCoroutine(NextWave(_wave,false));
        }
    }

    IEnumerator NextWave(Wave wave, bool tutorial)
    {
        yield return new WaitForSeconds(2);
        waves_left --;
        StartWave(wave, tutorial);
    }

    void Increase_Dificulty()
    {
        _wave._bachitombo_Count = _wave_Index;
        _wave._tombo_Count = _wave_Index;
        _wave._tombo_Tactico_Count = _wave_Index;
        _wave._tombo_Con_Perro_Count = _wave_Index;
        _wave._esmad_Count = _wave_Index;

        //Aumentar la dificultad
    }
}
