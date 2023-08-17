using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsFather : MonoBehaviour
{

    [SerializeField] protected int healt;
    [SerializeField] protected int ammo;
    protected Rigidbody2D Rigidbody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemycontroller = collision.gameObject.GetComponent<Enemy>();
        if (enemycontroller != null)
        {
            DoSomething();
        }
    }

    public void DoSomething()
    {

    }

}