using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Manager : MonoBehaviour
{
    [Header("Marbles")]
    [SerializeField] private Marble marblePrefab;
    [SerializeField] private int marblePool_DefaultCapacity;
    [SerializeField] private int marblePool_MaxCpacity;
    [SerializeField] public static bool marblePool_Check;

    [Header("Bowling Balls")]
    [SerializeField] private BowlingBall bowlPrefab;
    [SerializeField] private int bowlPool_DefaultCapacity;
    [SerializeField] private int bowlPool_MaxCpacity;
    [SerializeField] public static bool bowlPool_Check;

    [Header("Teaser Cables")]
    [SerializeField] private TeaserCables cablePrefab;
    [SerializeField] private int cablePool_DefaultCapacity;
    [SerializeField] private int cablePool_MaxCpacity;
    [SerializeField] public static bool cablePool_Check;

    private ObjectPool<Marble> marblePool;
    private ObjectPool<BowlingBall> bowlPool;
    private ObjectPool<TeaserCables> cablePool;
    public static Bullet_Manager Instance { get; private set; } = null;


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
        marblePool = new ObjectPool<Marble>(() =>
        {
            return Instantiate(marblePrefab);
        }, marble =>
        {
            marble.gameObject.SetActive(true);
        }, marble =>
        {
            marble.gameObject.SetActive(false);
        }, marble =>
        {
            Destroy(marble.gameObject);
        }, marblePool_Check, marblePool_DefaultCapacity, marblePool_MaxCpacity);

        bowlPool = new ObjectPool<BowlingBall>(() =>
        {
            return Instantiate(bowlPrefab);
        }, bowling =>
        {
            bowling.gameObject.SetActive(true);
        }, bowling =>
        {
            bowling.gameObject.SetActive(false);
        }, bowling =>
        {
            Destroy(bowling.gameObject);
        }, bowlPool_Check, bowlPool_DefaultCapacity, bowlPool_MaxCpacity);

        cablePool = new ObjectPool<TeaserCables>(() =>
        {
            return Instantiate(cablePrefab);
        }, cable =>
        {
            cable.gameObject.SetActive(true);
        }, cable =>
        {
            cable.gameObject.SetActive(false);
        }, cable =>
        {
            Destroy(cable.gameObject);
        }, cablePool_Check, cablePool_DefaultCapacity, cablePool_MaxCpacity);

        //StartPool();
    }

    public BowlingBall GetBowlingBall()
    {
        BowlingBall ball = bowlPool.Get();
        ball.config(OnDestroy_ball);
        return ball;
    }

    public Marble GetMarble()
    {
        Marble ball = marblePool.Get();
        ball.config(OnDestroy_ball);
        return ball;
    }

    public TeaserCables GetCable()
    {
        TeaserCables bullet = cablePool.Get();
        bullet.config(OnDestroy_ball);
        return bullet;
    }

    private void OnDestroy_ball(BulletFather bullet)
    {
        if(bullet is BowlingBall) 
        {
             bowlPool.Release((BowlingBall)bullet);
             //Animator animator = ((BowlingBall)bullet).animator; 
             //if (animator != null)
            // {
             //    animator.SetTrigger("LanzarProyectil");
             //}

        }
        if(bullet is Marble) marblePool.Release((Marble)bullet);
        if(bullet is TeaserCables) cablePool.Release((TeaserCables)bullet);
    }

    private void StartPool()
    {
        for(int i = 0; i < marblePool_DefaultCapacity; i++)
        {
            var bullet = marblePool.Get();
            marblePool.Release(bullet);
        }
        for (int i = 0; i < bowlPool_DefaultCapacity; i++)
        {
            var bullet = bowlPool.Get();
            bowlPool.Release(bullet);
        }
        for (int i = 0; i < cablePool_DefaultCapacity; i++)
        {
            var bullet = cablePool.Get();
            cablePool.Release(bullet);
        }
    }
}
