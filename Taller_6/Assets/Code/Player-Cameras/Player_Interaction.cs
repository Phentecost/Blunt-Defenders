using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Player_Interaction : MonoBehaviour
{

    public bool _Can_Deploy = true;
    private TrapsFather _trap;
    bool Show_Outlines;
    private Way_Point _door;
    bool Show_Text;
    [SerializeField] private Image img;
    [SerializeField] private GameObject Deply_Icon,Cant_Icon,Up_Icon,Repair_Icon;

    private bool firstTime;
    [SerializeField] Flowchart trapsTutorial;

    public int _current_Money {get; private set;} = 100;
    public int _current_Weed {get; private set;} = 0;

    private enum Type_Of_Interaction
    {
        Deploy, Upgrade, Repare
    }

    private Type_Of_Interaction _current_Interaction = Type_Of_Interaction.Deploy;

    public static Player_Interaction Instance {get;private set;} = null;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UI_Manager.Instance.UpdateCoins(_current_Money);
        UI_Manager.Instance.UpdateWeed(_current_Weed);
        _Can_Deploy = true;
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_current_Interaction == Type_Of_Interaction.Deploy)
        {
            if(!_Can_Deploy)
            {
                img.color = Color.black;
                Cant_Icon.SetActive(true);
                Deply_Icon.SetActive(false);
                Up_Icon.SetActive(false);
                Repair_Icon.SetActive(false);
                return;
            }
            
            
            img.color = Color.red;
            Cant_Icon.SetActive(false);
            Deply_Icon.SetActive(true);
            Up_Icon.SetActive(false);
            Repair_Icon.SetActive(false);
        }
        else if (_current_Interaction == Type_Of_Interaction.Upgrade)
        {
            
            img.color = Color.blue;
            Cant_Icon.SetActive(false);
            Deply_Icon.SetActive(false);
            Up_Icon.SetActive(true);
            Repair_Icon.SetActive(false);
            
        }
        else if(_current_Interaction == Type_Of_Interaction.Repare)
        {
            Cant_Icon.SetActive(false);
            Deply_Icon.SetActive(false);
            Up_Icon.SetActive(false);
            Repair_Icon.SetActive(true);
            img.color = Color.green;
        }
    }

    public void Interaction()
    {
        switch(_current_Interaction)
        {
            case Type_Of_Interaction.Deploy:
                
                if(_Can_Deploy)
                {
                    if(firstTime == true)
                    {
                        trapsTutorial.ExecuteBlock("Explicacion");
                        firstTime = false;
                    }
                    UI_WS_Manager.Instance.Deploy_Panel_Activation();
                }
                else
                {
                    Debug.Log("No se puede");
                }
                
                break;

            case Type_Of_Interaction.Upgrade:

                if(_trap.Current_Level < 5)
                {
                    if(Can_Puchase(_trap._level_Up_Money_Cost,_trap._level_Up_Weed_Cost))
                    {
                        _trap.Level_Up();
                    }
                    else
                    {
                        UI_Manager.Instance.Cant_Buy_Panel_Activation();
                    }
                    
                }

                break;
            
            case Type_Of_Interaction.Repare:
                
                _door.OnRepear();
                
                Debug.Log("Se ha reparado la puerta");

                break;
        }
    }

    public void GetCoins(int i)
    {
        _current_Money += i;
        UI_Manager.Instance.UpdateCoins(_current_Money);
    }

    public void GetWeed(int i)
    {
        _current_Weed += i;
        UI_Manager.Instance.UpdateWeed(_current_Weed);
    }

    public bool Can_Puchase(int m, int w)
    {
        if ( _current_Money >= m && _current_Weed >= w)
        {
            _current_Money -= m;
            _current_Weed -= w;
            UI_Manager.Instance.UpdateWeed(_current_Weed);
            UI_Manager.Instance.UpdateCoins(_current_Money);
            return true;        
        }
        else
        {
            return false;
        }
    }

    public void Deny_Deply()
    {
        _Can_Deploy = !_Can_Deploy;
        Deply_Icon.SetActive(!Deply_Icon.activeInHierarchy);
        Cant_Icon.SetActive(!Cant_Icon.activeInHierarchy);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        TrapsFather controller = other.GetComponentInParent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Deploy;
            _trap.Show_Outlines();
            Show_Outlines = false;
            _trap = null;
            
        }

        Way_Point door = other.GetComponentInParent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Deploy;
            
            _door.Show_Text();
            Show_Text = false;
            _door = null;
            
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        TrapsFather controller = other.GetComponentInParent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Upgrade;
            _trap = controller;
            if(!Show_Outlines)
            {
                _trap.Show_Outlines();
                Show_Outlines = true;
            }
        }

        Way_Point door = other.GetComponentInParent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Repare;
            _door = door;
            if(!Show_Text)
            {
                _door.Show_Text();
                Show_Text = true;
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {     
        TrapsFather controller = other.GetComponentInParent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Upgrade;
            _trap = controller;
            _trap.Show_Outlines();
            Show_Outlines = true;
        }

        Way_Point door = other.GetComponentInParent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Repare;
            _door = door;
            Show_Text = true;
            _door.Show_Text();
        }
    }
}
