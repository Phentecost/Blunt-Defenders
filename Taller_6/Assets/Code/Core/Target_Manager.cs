using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Target_Manager : MonoBehaviour
{
    public static Target_Manager Instance {get; private set;} = null;
    private GameObject [] targets_Indicators;
    [SerializeField] private GameObject [] Paths_Indicators;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        targets_Indicators = new GameObject[transform.childCount];

        for(int i = 0 ; i< targets_Indicators.Length; i++)
        {
            targets_Indicators[i] = transform.GetChild(i).gameObject;
        }
    }

    public void Activate_Indicators(bool[] actives)
    {
        for(int i = 0; i<targets_Indicators.Length; i++)
        {
            targets_Indicators[i].SetActive(actives[i]);
            Paths_Indicators[i].SetActive(actives[i]);
        }
    }

    public void Deactivate_Indicators()
    {
        for(int i = 0; i<targets_Indicators.Length; i++)
        {
            targets_Indicators[i].SetActive(false);
            Paths_Indicators[i].SetActive(false);
        }
    }
}
