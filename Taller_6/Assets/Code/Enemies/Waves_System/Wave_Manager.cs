using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;
using Enemies;
using System.Linq;
using System.IO.Compression;
using UnityEngine.UI;

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
    [SerializeField] private List<Wave> tutorial_Waves;
    [SerializeField] private List<Wave> Game_Waves;
    int tutorial_index = 0;
    private bool Auto_Pass;
    [SerializeField] Image IMG;
    public GameObject txt;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        Flowchart_Displayed = false;
        _wave = new Wave
        {
            Path_01 = true,
            Path_02 = true,
            Path_03 = true,
            Path_04 = true
        };
    }

    void Start()
    {
        Enemy_Spawn_System.OnWaveEnd += OnWaveEnds;
    }

    void OnDisable()
    {
        Enemy_Spawn_System.OnWaveEnd -= OnWaveEnds;
    }

    public bool[] Check_Wave_Paths()
    {
        bool[] paths = new bool[4];

        if(!Game_Manager.Instance.Tutorial)
        {
            if(_wave_Index + 3 > Game_Waves.Count)
            {
                for ( int i = 0; i<paths.Length; i++)
                {
                    paths[i] = true;
                }
            }
            else
            {
                for(int i = _wave_Index; i < _wave_Index + 3;i++)
                {
                    if(Game_Waves[i].Path_01)
                    {
                        paths[0] = true;
                    }

                    if(Game_Waves[i].Path_02)
                    {
                        paths[1] = true;
                    }

                    if(Game_Waves[i].Path_03)
                    {
                        paths[2] = true;
                    }

                    if(Game_Waves[i].Path_04)
                    {
                        paths[3] = true;
                    }
                }
            }
        }
        else
        {
            for(int i = 0; i < 4 ;i++)
                {
                    paths[i] = true;
                    paths[0] = false;
                }
        }
        

        return paths;
    }

    public void Config(int i, Action OnEnding)
    {
        waves_left = i;
        this.OnEnding = OnEnding;
        if(_wave_Index < Game_Waves.Count)
        {
            StartCoroutine(NextWave(Game_Waves[_wave_Index],false));
        }
        else
        {
            StartCoroutine(NextWave(_wave,false));
        }
        
    }

    public void Config(Action OnEnding, Flowchart chart)
    {
        waves_left = 1;
        _wave_Index--;
        this.OnEnding = OnEnding;
        tutorial_chart = chart;
        StartCoroutine(NextWave(tutorial_Waves[tutorial_index],true));
        tutorial_index ++;
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

        tutorial_chart.ExecuteBlock("Tombo");
        yield return new WaitForSeconds(5);
        Finish_Fungus();
        
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
        StartCoroutine(onWaveEnds());
    }

    IEnumerator onWaveEnds()
    {
        if(!Game_Manager.Instance.Tutorial)UI_Manager.Instance.Win_Round_Panel_Activation();
        yield return StartCoroutine(Wait());
        if(!Game_Manager.Instance.Tutorial)UI_Manager.Instance.Win_Round_Panel_Activation();
        if(waves_left == 0)
        {
            if(!Flowchart_Displayed){onWin.ExecuteBlock("win"); Flowchart_Displayed = true;}
            OnEnding();
        }
        else
        {
            if(_wave_Index < Game_Waves.Count)
            {
                StartCoroutine(NextWave(Game_Waves[_wave_Index],false));
            }
            else
            {
                StartCoroutine(NextWave(_wave,false));
            }

            
        }
    }

    IEnumerator Wait()
    {
        if(Auto_Pass)
        {
            yield return new WaitForSeconds(2);
        }
        else if(Game_Manager.Instance.Tutorial)
        {
            yield return null;
        }
        else
        {
            while(Input.touchCount==0 && !Input.GetMouseButton(0))
            {
                yield return null;
            }
        }
    }

    public void Change_Mode()
    {
        Auto_Pass = !Auto_Pass;

        if(Auto_Pass)
        {
            IMG.color = Color.green;
            txt.SetActive(false);
        }
        else
        {
            IMG.color = Color.red;
            txt.SetActive(true);
        }
    }

    IEnumerator NextWave(Wave wave, bool tutorial)
    {
        if(!tutorial)UI_Manager.Instance.Next_Round_Panel_Activation();
        yield return new WaitForSeconds(2);
        if(!tutorial)UI_Manager.Instance.Next_Round_Panel_Activation();
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
