using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
    [SerializeField] Transform TargetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = TargetPosition.GetChild(0).position;
    }
}
