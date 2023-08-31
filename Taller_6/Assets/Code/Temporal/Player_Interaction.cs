using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Interaction : MonoBehaviour
{

    public bool _Can_Deploy = true;
    private TrapsFather _trap;
    private Way_Point _door;

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
        if(Input.GetKeyDown(KeyCode.F))
        {
            
            Interaction();
        }

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

                Debug.Log("La Torreta" + _trap.name + " Mejoro");

                break;
            
            case Type_Of_Interaction.Repare:
                
                _door.life = 100;
                Debug.Log("Se ha reparado la puerta");

                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        TrapsFather controller = other.GetComponent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Deploy;
            _trap = null;
        }

        Way_Point door = other.GetComponent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Deploy;
            _door = null;
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        TrapsFather controller = other.GetComponent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Upgrade;
            _trap = controller;
        }

        Way_Point door = other.GetComponent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Repare;
            _door = door;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {     

        TrapsFather controller = other.GetComponent<TrapsFather>();
        if(controller != null)
        {
            _current_Interaction = Type_Of_Interaction.Upgrade;
            _trap = controller;
        }

        Way_Point door = other.GetComponent<Way_Point>();
        if(door!= null)
        {
            _current_Interaction = Type_Of_Interaction.Repare;
            _door = door;
        }
    }
}
