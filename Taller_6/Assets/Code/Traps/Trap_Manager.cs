using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Trap_Manager : MonoBehaviour
{
    [Header("Marble Cannon")]
    [SerializeField] private MarbleCannon mCannon_Prefab;
    [SerializeField] private int mCannon_DefaultCapacity;
    [SerializeField] private int mCannon_MaxCapacity;
    [SerializeField] private bool mCannon_Check;
    [SerializeField] private Traps_Config Marbel_Cannon_Configuration;

    [Header("Bowling Ball Cannon")]
    [SerializeField] private BowlingCannon bCannon_Prefab;
    [SerializeField] private int bCannon_DefaultCapacity;
    [SerializeField] private int bCannon_MaxCapacity;
    [SerializeField] private bool bCannon_Check;
    [SerializeField] private Traps_Config Bowling_Ball_Cannon_Configuration;

    [Header("Firecracker")]
    [SerializeField] private PopsNBangs firecracker_Prefab;
    [SerializeField] private int firecracker_DefaultCapacity;
    [SerializeField] private int firecracker_MaxCapacity;
    [SerializeField] private bool firecracker_Check;
    [SerializeField] private Traps_Config Fire_Craker_Configuration;

    [Header("Bribery")]
    [SerializeField] private Bribery bribery_Prefab;
    [SerializeField] private int bribery_DefaultCapacity;
    [SerializeField] private int bribery_MaxCapacity;
    [SerializeField] private bool bribery_Check;
    [SerializeField] private Traps_Config Bribery_Configuration;

    private ObjectPool<MarbleCannon> MarbleCannonPool;
    private ObjectPool<BowlingCannon> BowlingCannonPool;
    private ObjectPool<PopsNBangs> FirecrackerPool;
    private ObjectPool<Bribery> BriberyPool;
    public static Trap_Manager Instance {get; private set;} = null;

    public Vector2 pos;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
         MarbleCannonPool = new ObjectPool<MarbleCannon>(() =>
        {
            return Instantiate(mCannon_Prefab);
        }, MarbleCannon =>
        {
            MarbleCannon.gameObject.SetActive(true);
        }, MarbleCannon =>
        {
            MarbleCannon.gameObject.SetActive(false);
        }, MarbleCannon =>
        {
            Destroy(MarbleCannon.gameObject);
        }, mCannon_Check, mCannon_DefaultCapacity, mCannon_MaxCapacity);

        BowlingCannonPool = new ObjectPool<BowlingCannon>(()=>
        {
            return Instantiate(bCannon_Prefab);
        }, BowlingCannon =>
        {
            BowlingCannon.gameObject.SetActive(true);
        }, BowlingCannon =>
        {
            BowlingCannon.gameObject.SetActive(false);
        }, BowlingCannon =>
        {
            Destroy(BowlingCannon.gameObject);
        }, bCannon_Check, bCannon_DefaultCapacity, bCannon_MaxCapacity);

        BriberyPool = new ObjectPool<Bribery>(() =>
        {
            return Instantiate(bribery_Prefab);
        }, Bribery =>
        {
            Bribery.gameObject.SetActive(true);
        }, Bribery =>
        {
            Bribery.gameObject.SetActive(false);
        }, Bribery =>
        {
            Destroy(Bribery.gameObject);
        }, bribery_Check, bribery_DefaultCapacity, bribery_MaxCapacity);

        FirecrackerPool = new ObjectPool<PopsNBangs>(()=>
        {
            return Instantiate(firecracker_Prefab);
        }, Firecracker =>
        {
            Firecracker.gameObject.SetActive(true);
        }, Firecracker =>
        {
            Firecracker.gameObject.SetActive(false);
        }, Firecracker =>
        {
            Destroy(Firecracker.gameObject);
        }, firecracker_Check, firecracker_DefaultCapacity, firecracker_MaxCapacity);
    }

    public void GetTrap(int i)
    {
        switch(i)
        {
            case 0:

                if(Player_Interaction.Instance.Can_Puchase(Marbel_Cannon_Configuration.levels[0].Coins,Marbel_Cannon_Configuration.levels[0].Weed))
                {
                    MarbleCannon trap_01 = MarbleCannonPool.Get();
                    trap_01.transform.position = pos;
                    trap_01.Config();
                }

            break;

            case 1:
            
            if(Player_Interaction.Instance.Can_Puchase(Bowling_Ball_Cannon_Configuration.levels[0].Coins,Bowling_Ball_Cannon_Configuration.levels[0].Weed))
            {

                BowlingCannon trap_02 = BowlingCannonPool.Get();
                trap_02.transform.position = pos;
                trap_02.Config();

            }

            break;

            case 2:
            
            if(Player_Interaction.Instance.Can_Puchase(Bribery_Configuration.levels[0].Coins,Bribery_Configuration.levels[0].Weed))
            {

                Bribery trap_03 = BriberyPool.Get();
                trap_03.transform.position = pos;
                trap_03.Config();
                
            }

            break;

            case 3:

            if(Player_Interaction.Instance.Can_Puchase(Fire_Craker_Configuration.levels[0].Coins,Fire_Craker_Configuration.levels[0].Weed))
            {

                PopsNBangs trap_04 = FirecrackerPool.Get();
                trap_04.transform.position = pos;
                trap_04.Config();

            }

            
            break;
        }
    }
} 
