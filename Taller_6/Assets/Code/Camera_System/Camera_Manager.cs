using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    public static Camera_Manager Instance {get; private set;} = null;

    private Vector3[] _cameras;

    private Transform _main_Camera;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        _cameras = new Vector3[transform.childCount];

        for(int i = 0; i< _cameras.Length; i++)
        {
            _cameras[i] = transform.GetChild(i).transform.position;            
        }

        _main_Camera = Camera.main.transform;
    }

    public void Change_Camera(int i)
    {
        _main_Camera.position = _cameras[i];
    }
}
