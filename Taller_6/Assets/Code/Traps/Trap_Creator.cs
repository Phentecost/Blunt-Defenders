using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Creator : MonoBehaviour
{
    [SerializeField] GameObject MarbleBallCannon;
    [SerializeField] GameObject BowlingBallCannon;
    [SerializeField] GameObject Pops;
    [SerializeField] GameObject CorruptCops;

    public void CreateMCannon(Vector2 location)
    {
        MarbleCannon thistrap = Trap_Manager.mCannon_check ? Trap_Manager.MarbleCannonPool.get() : Instantiate(MarbleBallCannon, location, Quaternion.identity);
    }

    public void CreateBCannon(Vector2 location)
    {
        BowlingCannon thistrap = Trap_Manager.bCannon_check ? Trap_Manager.BowlingCannonPool.get() : Instantiate(BowlingBallCannon, location, Quaternion.identity);
    }

    public void CreateBrivery(Vector2 location)
    {
        Bribery thistrap = Trap_Manager.Bribery_check ? Trap_Manager.BriberyPool.get() : Instantiate(CorruptCops, location, Quaternion.identity);
    }

    public void CreateFirecracker(Vector2 location)
    {
        Firecracker thistrap = Trap_Manager.firecracker_Check ? Trap_Manager.FirecrackerPool.get() : Instantiate(Pops, location, Quaternion.identity);
    }

}