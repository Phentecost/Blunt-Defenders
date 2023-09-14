using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Fungus;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public static Health Instance {get; private set;} = null;

    [SerializeField] Flowchart loseMessage;

    private void Awake()
    {   
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        health = maxHealth;
    }

    void Start()
    {
        UI_Manager.Instance.UpdateLife(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            loseMessage.ExecuteBlock("lose");
            UI_Manager.Instance.OnLose();
        }

        UI_Manager.Instance.UpdateLife(health);
    }

}
