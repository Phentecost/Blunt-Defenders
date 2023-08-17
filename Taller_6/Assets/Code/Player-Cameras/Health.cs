using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    [SerializeField] private TextMeshProUGUI _health_TXT;
    public static Health Instance {get; private set;} = null;



    private void Awake()
    {   
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        health = maxHealth;
        _health_TXT.text = "Life: "+health.ToString();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;

            UI_Manager.Instance.OnLose();
        }

        _health_TXT.text = "Life: "+health.ToString();
    }
}
