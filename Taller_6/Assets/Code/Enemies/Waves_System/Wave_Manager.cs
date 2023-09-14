using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class Wave_Manager : MonoBehaviour
{
    public static Wave_Manager Instance {get;private set;} = null;

    private int _wave_Index = 0;

    private Wave _wave;

    private int waves_left;

    private event Action OnEnding;

    [SerializeField] Flowchart onWin;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

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
        StartCoroutine(NextWave());
    }

    void StartWave()
    {
        _wave_Index++;
        //txt.text = "Round: " + _wave_Index;
        Increase_Dificulty();
        StartCoroutine(Enemy_Spawn_System.Instance.SpawnWave(_wave));
    }

    void OnWaveEnds()
    {
        if(waves_left == 0)
        {
            onWin.ExecuteBlock("win");
            OnEnding();
        }
        else
        {
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(2);
        waves_left --;
        StartWave();
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
