using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour
{

    public bool _Can_Deploy = true;
    private TrapsFather _trap;
    bool Show_Outlines;
    private Way_Point _door;
    bool Show_Text;

    [SerializeField] private Image img;

    private enum Type_Of_Interaction
    {
        Deploy, Upgrade, Repare
    }

    [SerializeField]private Type_Of_Interaction _current_Interaction = Type_Of_Interaction.Deploy;

    // Start is called before the first frame update
    void Start()
    {
        _Can_Deploy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_current_Interaction == Type_Of_Interaction.Deploy)
        {
            if(!_Can_Deploy)
            {
                img.color = Color.black;
                return;
            }
            img.color = Color.red;
        }
        else if (_current_Interaction == Type_Of_Interaction.Upgrade)
        {

            img.color = Color.blue;
            
        }
        else if(_current_Interaction == Type_Of_Interaction.Repare)
        {
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
                    
                    Trap_Manager.Instance.pos = transform.position;
                    UI_Manager.Instance.Deploy_Panel_Activation();
                }
                else
                {
                    Debug.Log("No se puede");
                }
                
                break;

            case Type_Of_Interaction.Upgrade:

                _trap.Level_Up();

                break;
            
            case Type_Of_Interaction.Repare:
                
                _door.OnRepear();
                
                Debug.Log("Se ha reparado la puerta");

                break;
        }
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
