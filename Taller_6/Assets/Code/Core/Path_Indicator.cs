using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Path_Indicator : MonoBehaviour
{
    public GameObject Target;
    public float offScreenThreshold = 10f;
    private Camera mainCamera;
    private bool Active = true;

    private SpriteRenderer _renderer;

    void Start()
    {
        mainCamera = Camera.main;
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Active)
        {
            Vector3 target_Direction = Target.transform.position - transform.position;
            float distance_To_Target = target_Direction.magnitude;

            if(distance_To_Target < offScreenThreshold)
            {
                _renderer.sortingOrder = -11;
                //gameObject.SetActive(false);
                //Active = false;
            }
            else
            {
                Vector3 target_Viewport_Position = mainCamera.WorldToViewportPoint(Target.transform.position);
                if(target_Viewport_Position.z > 0 && target_Viewport_Position.x > 0 && target_Viewport_Position.x < 1 && target_Viewport_Position.y > 0 && target_Viewport_Position.y<1)
                {
                    _renderer.sortingOrder = -11;
                    //gameObject.SetActive(false);
                }
                else
                {
                    _renderer.sortingOrder = 11;
                    //gameObject.SetActive(true);
                    Vector3 screen_Edge = mainCamera.ViewportToWorldPoint(new Vector3(Mathf.Clamp(target_Viewport_Position.x,0.1f,0.9f),Mathf.Clamp(target_Viewport_Position.y,0.1f,0.7f),mainCamera.nearClipPlane));
                    transform.position = new Vector3(screen_Edge.x,screen_Edge.y,0);
                    Vector3 direction = Target.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                    transform.rotation = quaternion.Euler(0,0,angle-90);
                }
            }
        }
    }
}
