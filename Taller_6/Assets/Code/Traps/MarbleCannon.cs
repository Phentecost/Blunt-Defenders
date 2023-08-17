using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleCannon : TrapsFather
{
    [SerializeField] Marble Projectile;
    GameObject target;
    float CDTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        this.ammo = 10;
    }

    public void DoSomething()
    {
        Marble bullet = Instantiate(this.Projectile, this.transform.position, Quaternion.identity);
        Vector2 EnemyLocation = this.target.GetComponent<Rigidbody2D>().position;
        EnemyLocation.Normalize();
        bullet.Launch(EnemyLocation);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>()!= null)
        {
            this.target=collision.gameObject;
            DoSomething();
        }
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(CDTime);
    }
}
