using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Animations;

public class Target_Manager : MonoBehaviour
{
    public static Target_Manager Instance {get; private set;} = null;
    [SerializeField] private Image [] targets_Indicators;
    [SerializeField] private GameObject [] Paths_Indicators;
    private string[] paths_Indicators_Actions = new string[4];
    Action xd;
    bool path1, path2, path3, path4;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        paths_Indicators_Actions[0] = nameof(Active_Path_1);
        paths_Indicators_Actions[1] = nameof(Active_Path_2);
        paths_Indicators_Actions[2] = nameof(Active_Path_3);
        paths_Indicators_Actions[3] = nameof(Active_Path_4);
    }

    void Active_Path_1()
    {
        if(path1)
        {
            targets_Indicators[0].color = new Color(0,0,0,0);
            path1=false;
        }
        else
        {
            targets_Indicators[0].color = new Color(1,0,0,0.5f);
            path1=true;
        }
    }

    void Active_Path_2()
    {
        if(path2)
        {
            targets_Indicators[1].color = new Color(0,0,0,0);
            path2=false;
        }
        else
        {
            targets_Indicators[1].color = new Color(1,0,0,0.5f);
            path2=true;
        }
    }

    void Active_Path_3()
    {
        if(path3)
        {
            targets_Indicators[2].color = new Color(0,0,0,0);
            path3 = false;
        }
        else
        {
            targets_Indicators[2].color = new Color(1,0,0,0.5f);
            path3 = true;
        }
    }

    void Active_Path_4()
    {
        if(path4)
        {
            targets_Indicators[3].color = new Color(0,0,0,0);
            path4 = false;
        }
        else
        {
            targets_Indicators[3].color = new Color(1,0,0,0.5f);
            path4 = true;
        }
    }



    public void Activate_Indicators(bool[] actives)
    {
        for(int i = 0; i<targets_Indicators.Length; i++)
        {
            if(actives[i])
            {
                Paths_Indicators[i].SetActive(actives[i]);
                InvokeRepeating(paths_Indicators_Actions[i],1,1);
            }
        }
    }

    public void Deactivate_Indicators()
    {
        for(int i = 0; i<targets_Indicators.Length; i++)
        {
            Paths_Indicators[i].SetActive(false);
            targets_Indicators[3].color = new Color(0,0,0,0);
        }

        CancelInvoke();
    }
}
