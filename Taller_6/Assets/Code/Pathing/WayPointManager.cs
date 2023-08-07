using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField] private GameObject Path1_Container;
    public static Transform[] Path1 {get; private set;}

    void Awake()
    {
        Path1 = new Transform[Path1_Container.transform.childCount];
        for (int i = 0; i < Path1.Length; i++)
        {
            Path1[i] = Path1_Container.transform.GetChild(i);
        }
    }
}
