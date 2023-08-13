using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    public static Wave_Manager Instance {get;private set;} = null;

    public int _wave_Index = 0;

    public Wave _wave;

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
        Enemy_Spawn_System.OnWaveEnd += OnWaveEnds;
        StartCoroutine(NextWave());
    }

    void OnDisable()
    {
        Enemy_Spawn_System.OnWaveEnd -= OnWaveEnds;
    }

    void StartWave()
    {
        _wave_Index++;
        Debug.Log("Ronda: "+_wave_Index);
        Increase_Dificulty();
        StartCoroutine(Enemy_Spawn_System.Instance.SpawnWave(_wave));
    }

    void OnWaveEnds()
    {
        StartWave();
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(2);
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
