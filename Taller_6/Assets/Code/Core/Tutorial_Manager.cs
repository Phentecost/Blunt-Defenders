using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpsIndex = 0;
    private float _timeScale;
    float timer = 0.5f;
    float timer_timer = 2f;
    void Start()
    {
        _timeScale = Time.timeScale;
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
                }
            }
        }else if(popUpsIndex == 7) // Mejorar trampa
        {
            TrapsFather trap = GameObject.FindObjectOfType<TrapsFather>();
            if(trap.Current_Level == 2)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 8) // Acercarse a la puerta
        {
            if(Player_Interaction.Instance._current_Interaction == Player_Interaction.Type_Of_Interaction.Build)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 9) // Countruir una puerta
        {
            if(Player_Interaction.Instance._door.Door)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 10) // Reparar una puerta
        {
            if(Input.touchCount>0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 11) // Indicadores
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 12) // Moverse al indicador
        {
            if(Camera_Manager.Instance.Current_Camera != 1)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 13) // ultima explicacion Indicador
        {
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 14) //Listos para defender
        {
            if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }
        else if(popUpsIndex == 15) //Camaras
        {
            if(Camera_Manager.Instance.Current_Camera !=0)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }
        else if(popUpsIndex == 16) //Enemigos
        {
            Time.timeScale = _timeScale/2;
            if(Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                }
            }
        }else if(popUpsIndex == 17) // Wack-A-Tombo
        {
           if(TouchManager.Instance.touche_this_Frame)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer = timer_timer;
                    Time.timeScale = _timeScale;
                }
                
            } 
        }else if(popUpsIndex == 18) //Matar a todos los enemigos
        {
            if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Preparation)
            {
                if(timer <= 0)
                {
                    popUpsIndex++;
                    timer =timer_timer;
                }
            }
        }else if(popUpsIndex == 19) //Tiempo
        {
            if(Input.touchCount>0 || Input.GetMouseButton(0))
            {
                if(timer <= 0)
                {
                    Game_Manager.Instance.Tutorial = false;
                    popUps[popUpsIndex].SetActive(false);
                }
            }
        }
    }
}
