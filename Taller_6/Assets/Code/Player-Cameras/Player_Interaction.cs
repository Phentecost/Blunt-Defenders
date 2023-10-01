using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using System.Linq;

public class Player_Interaction : MonoBehaviour
{
    public bool _Can_Deploy = true;
    private TrapsFather _trap;
    bool Show_Outlines;
    public Way_Point _door;
    bool OnShow;
    [SerializeField] private Image img;
    [SerializeField] private GameObject Deply_Icon,Cant_Icon,Up_Icon,Repair_Icon;
    private bool firstTime;
    [SerializeField] Flowchart trapsTutorial;
    public int _current_Money {get; private set;} = 100;
    public int _current_Weed {get; private set;} = 0;
    private interactible_OGJ closest_Interactible; 
    public enum Type_Of_Interaction
    {
        Deploy, Upgrade, Repare, Build
    }

    public Type_Of_Interaction _current_Interaction{get;private set;} = Type_Of_Interaction.Deploy;
    public static Player_Interaction Instance {get;private set;} = null;
    private List<interactible_OGJ> interactibles = new List<interactible_OGJ>();

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
        showe_Closest_Interactible_Outlines();

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
        }else if (_current_Interaction == Type_Of_Interaction.Build)
        {
            Cant_Icon.SetActive(false);
            Deply_Icon.SetActive(false);
            Up_Icon.SetActive(false);
            Repair_Icon.SetActive(false);
            img.color = Color.yellow;
        }
    }

    public void showe_Closest_Interactible_Outlines()
    {
        Debug.Log(interactibles.Count);
        Debug.Log(closest_Interactible);

        if(interactibles.Count > 1)
        {
            for (int i = 0;i < interactibles.Count; i++)
            {
                if(interactibles[i] == closest_Interactible) continue;
                if(Vector2.Distance(interactibles[i].Pos(),transform.parent.position) < Vector2.Distance(closest_Interactible.Pos(),transform.parent.position))
                {
                    if(closest_Interactible.IsActive_Outlines()) closest_Interactible.Off_Outlines();
                    closest_Interactible = interactibles[i];
                    if(!closest_Interactible.IsActive_Outlines()) closest_Interactible.show_Outlines();
                    if(closest_Interactible is TrapsFather){_trap = (TrapsFather) closest_Interactible; _current_Interaction = Type_Of_Interaction.Upgrade;}
                    if(closest_Interactible is Way_Point){_door = (Way_Point) closest_Interactible; _current_Interaction = _door.Door? Type_Of_Interaction.Repare : Type_Of_Interaction.Build;}
                }
            }
        }
        else if(interactibles.Count == 1)
        {
            interactibles[0].show_Outlines();
            closest_Interactible = interactibles[0];
            if(closest_Interactible is TrapsFather){_trap = (TrapsFather) closest_Interactible; _current_Interaction = Type_Of_Interaction.Upgrade;}
            if(closest_Interactible is Way_Point) {_door = (Way_Point) closest_Interactible; _current_Interaction = _door.Door? Type_Of_Interaction.Repare : Type_Of_Interaction.Build;}
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

            case Type_Of_Interaction.Build:
                _door.show_Outlines();
                _door.Make_Door();
                _door.show_Outlines();
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
        /*TrapsFather controller = other.GetComponentInParent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Deploy;
            _trap.Show_Outlines();
            Show_Outlines = false;
            _trap = null;
            
        }

        Way_Point door = other.GetComponentInParent<Way_Point>();
        */
        
        interactible_OGJ inter = other.GetComponentInParent<interactible_OGJ>();
        
        _current_Interaction =  Type_Of_Interaction.Deploy;
        if(inter.IsActive_Outlines())inter.Off_Outlines();
        if(closest_Interactible == inter)
        {
            if(inter is TrapsFather) _trap = null;
            if(inter is Way_Point) _door = null;
        }
        interactibles.Remove(inter);
        if(interactibles.Count == 0)OnShow = false;
    }

    void OnTriggerStay2D (Collider2D other)
    {
        interactible_OGJ inter = other.GetComponentInParent<interactible_OGJ>();
        if(!interactibles.Contains(inter)) interactibles.Add(inter);
        /*TrapsFather controller = other.GetComponentInParent<TrapsFather>();
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
            if(door.Door)
            {
                _current_Interaction = Type_Of_Interaction.Repare;
                _door = door;
                if(!Show_Text)
                {
                    _door.Show_Text();
                    Show_Text = true;
                    
                }
            }
            else
            {
                _current_Interaction = Type_Of_Interaction.Build;
                _door = door;
                if(!Show_Door)
                {
                    _door.show_door();
                    Show_Door = true;
                }

            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {     
        /*TrapsFather controller = other.GetComponentInParent<TrapsFather>();
        if(controller != null)
        {
            
        }
        */

        interactible_OGJ inter = other.GetComponentInParent<interactible_OGJ>();
        if(!interactibles.Contains(inter)) interactibles.Add(inter);

        /*if(OnShow) return;
        if(inter is Way_Point)
        {
            if(inter.IsActive_OBJ())
            {
                _current_Interaction = Type_Of_Interaction.Repare;
                _door = (Way_Point)inter;
                inter.show_Outlines();
                OnShow = true;
                closest_Interactible = inter;
                return;
            }
            else
            {
                _current_Interaction = Type_Of_Interaction.Build;
                _door = (Way_Point)inter;
                inter.show_Outlines();
                OnShow = true;
                closest_Interactible = inter;
                return;
            }
        }

        if(inter is TrapsFather)
        {
            _current_Interaction = Type_Of_Interaction.Upgrade;
            _trap = (TrapsFather)inter;
            inter.show_Outlines();
            OnShow = true;
            closest_Interactible = inter;
        }*/
        
    }

   
}

public interface interactible_OGJ
{
    public void show_Outlines();
    public bool IsActive_OBJ();
    public bool IsActive_Outlines();
    public void Off_Outlines();
    public Vector2 Pos();
}
