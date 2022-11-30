using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentMotor))]
public class Player : MonoBehaviour
{
    private Camera mainCamera;
    private AgentMotor motor;
    [SerializeField] private LayerMask movableMask;
    [SerializeField] private Interactable focus;
    // Start is called before the first frame update
    void Start()//собирает данные со скрипта AgentMotor и подставляет полученные переменные в свой код
    {
        mainCamera = Camera.main;
        motor = GetComponent<AgentMotor>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))//при нажатии на левую клавишу
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);//считавает позицию мыши во время нажатия
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movableMask))
            {
                motor.MoveToPoint(hit.point);//и перемещает объект в это место
                RemoveFocus();//убирает предмет следования
            }
        }
        else if (Input.GetMouseButtonDown(1))//а если нажать правой кнопкой на объект со скриптом Interactable то указывает его как предмет, за которым он будет следовать
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,100))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    private void SetFocus(Interactable newFocus)//метод для установка предмета следования
    {
        focus = newFocus;
        motor.FollowTarget(newFocus);
    }

    private void RemoveFocus()//и метод чтобы его убрать
    {
        focus = null;
        motor.StopFollowingTarget();
    }
}
