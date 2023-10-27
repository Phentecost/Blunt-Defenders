using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject Ready_button,tut_01,tut_02;
    private int popUpsIndex = 0;
    float timer = 0.5f;
    float timer_timer = 2f;
    void Start()
    {
        if(!Game_Manager.Instance.Tutorial)return;
        Ready_button.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(!Game_Manager.Instance.Tutorial)return;

        Debug.Log(popUpsIndex);

        for(int i = 0; i< popUps.Length; i++)
        {
            if(i == popUpsIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        timer -= Time.deltaTime;

        if(popUpsIndex == 0) //intro
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0)) 
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
            
        }else if(popUpsIndex == 1) // moverse
        {
            if(PJ.Instance.playerInput.actions["Walk"].ReadValue<Vector2>() != Vector2.zero)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if( popUpsIndex == 2) // Explicarle el camino
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if( popUpsIndex == 3) // Abrir el menu de trampas
        {
            if(UI_WS_Manager.Instance.Deploy_Panel.activeInHierarchy)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if (popUpsIndex == 4) // explicar trampas
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                    tut_01.SetActive(true);
                }
            }
        }else if (popUpsIndex == 5) // Seleccionar una torreta
        {
            if(UI_WS_Manager.Instance.Previewing_Trap)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                    tut_01.SetActive(false);
                    tut_02.SetActive(true);
                }
            }
        }else if(popUpsIndex == 6) // Colocar la torreta
        {
            GameObject trap = GameObject.FindObjectOfType<TrapsFather>().gameObject;
            if(trap != null && !UI_WS_Manager.Instance.Previewing_Trap)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                    tut_02.SetActive(false);
                }
            }
        }else if(popUpsIndex == 7) //Explicacion Radio
        {

            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 8) // Explicacion Boton
        {
            

            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 9) // Mejorar Trampa
        {

            TrapsFather trap = GameObject.FindObjectOfType<TrapsFather>();
            if(trap.Current_Level == 2)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                    Ready_button.SetActive(true);
                }
            }

        }else if(popUpsIndex == 10) // Listo para defender
        {
            if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 11) // Camaras
        {
           if(Camera_Manager.Instance.Current_Camera !=0)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 12) // Camara enemigo
        {
            if(Camera_Manager.Instance.Current_Camera == 0)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 13) // Acaba Con el enemigo
        {
            if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer =timer_timer;
                    Game_Manager.Instance.Pause();
                }
            }

        }else if(popUpsIndex == 14) //Tiempo
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer; 
                }
            }
        }
        else if(popUpsIndex == 15) //Pueta
        {
            if(Player_Interaction.Instance._current_Interaction == Player_Interaction.Type_Of_Interaction.Build)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }
        else if(popUpsIndex == 16) //Mejorar puerta
        {
           if(Player_Interaction.Instance._door.Door)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }

        }else if(popUpsIndex == 17) // Reparacion
        {
           if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 18) //Listo
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    Game_Manager.Instance.Tutorial = false;
                    popUps[popUpsIndex].SetActive(false);
                    Game_Manager.Instance.Pause();
                }
            }
        }
    }
}
