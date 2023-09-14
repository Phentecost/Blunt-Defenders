using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WS_Manager : MonoBehaviour
{
    public static UI_WS_Manager Instance {get; private set;} = null;

    [SerializeField] private GameObject Deploy_Panel;
    [SerializeField] private GameObject Confirmation_Panel;
    bool Previewing_Trap;
    TrapsFather trap;
    int trap_ID;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if(!Previewing_Trap) return;

        trap.transform.position = transform.parent.position;
    }

    public void Deploy_Panel_Activation()
    {
        if(Deploy_Panel.activeInHierarchy)
        {
            Deploy_Panel.SetActive(false);
        }
        else
        {
            Deploy_Panel.SetActive(true);
        }
    }

    public void Confirmation_Panel_Activation()
    {
        if(Confirmation_Panel.activeInHierarchy)
        {
            Confirmation_Panel.SetActive(false);
        }
        else
        {
            Confirmation_Panel.SetActive(true);
        }
    }

    public void Preview(int i)
    {
        trap_ID = i;
        trap = Trap_Manager.Instance.Get_Trap_To_Preveiw(trap_ID);
        trap.Show_Outlines();
        Previewing_Trap = true;
    }

    public void Confirmation(bool B)
    {
        if(B){Trap_Manager.Instance.pos = transform.parent.position; Trap_Manager.Instance.Deploy_Trap(trap_ID); };
        trap.Show_Outlines();
        Previewing_Trap = false;
        Trap_Manager.Instance.Realice_Trap_To_Preveiw(trap);
        trap = null;
    }
}
