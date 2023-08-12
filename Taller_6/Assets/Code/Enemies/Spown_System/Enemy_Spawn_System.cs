using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Spawn_System : MonoBehaviour
{
    [SerializeField] private Bachitombo _bachitombo_Prefab;
    private ObjectPool<Bachitombo> _bachitombo_Pool;

    void Start()
    {
        _bachitombo_Pool = new ObjectPool<Bachitombo>(()=> {
            return Instantiate(_bachitombo_Prefab);
        }, bachitombo => {
            bachitombo.gameObject.SetActive(true);
        }, bachitombo =>{
            bachitombo.gameObject.SetActive(false);
        }, bachitombo => {
            Destroy(bachitombo.gameObject);
        },false, 10,20);

        InvokeRepeating(nameof(Spawn),0.2f,0.2f);
    }

    private void Spawn ()
    {
        var enemy = _bachitombo_Pool.Get();
        enemy.Config(OnDeath);
    }

    private void OnDeath(Enemy enemy)
    {
        _bachitombo_Pool.Release((Bachitombo)enemy);
    }
}
