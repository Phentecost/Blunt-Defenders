using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class UI_WS_Manager : MonoBehaviour
{
    public static UI_WS_Manager Instance {get; private set;} = null;

    [SerializeField] public GameObject Deploy_Panel;
    [SerializeField] private GameObject Confirmation_Panel;
    [SerializeField] private GameObject[] traps_Panels;
    public bool Previewing_Trap{get; private set;}
    TrapsFather trap;
    int trap_ID;
    public bool Deploying = false;

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
        if(!Deploying)
        {
            Deploy_Panel.SetActive(false);
            Confirmation_Panel.SetActive(false);
            if(Previewing_Trap)Cancel_Preview();
        }

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
        traps_Panels_Activation(trap_ID);
        trap = Trap_Manager.Instance.Get_Trap_To_Preveiw(trap_ID);
        trap.show_Outlines();
        Previewing_Trap = true;
    }

    public void Cancel_Preview()
    {
        if(Previewing_Trap)
        {
            Confirmation(false);
        }
    }

    private void traps_Panels_Activation(int i)
    {
        if(traps_Panels[i].activeInHierarchy)
        {
            traps_Panels[i].SetActive(false);
        }
        else
        {
            traps_Panels[i].SetActive(true);
        }
    }

    public void Confirmation(bool B)
    {
        if(B){Trap_Manager.Instance.pos = transform.parent.position; Trap_Manager.Instance.Deploy_Trap(trap_ID); };
        trap.Off_Outlines();
        Previewing_Trap = false;
        Trap_Manager.Instance.Realice_Trap_To_Preveiw(trap);
        traps_Panels_Activation(trap_ID);
        trap = null;
    }
}
