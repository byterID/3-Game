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
    void Start()//�������� ������ �� ������� AgentMotor � ����������� ���������� ���������� � ���� ���
    {
        mainCamera = Camera.main;
        motor = GetComponent<AgentMotor>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))//��� ������� �� ����� �������
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);//��������� ������� ���� �� ����� �������
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movableMask))
            {
                motor.MoveToPoint(hit.point);//� ���������� ������ � ��� �����
                RemoveFocus();//������� ������� ����������
            }
        }
        else if (Input.GetMouseButtonDown(1))//� ���� ������ ������ ������� �� ������ �� �������� Interactable �� ��������� ��� ��� �������, �� ������� �� ����� ���������
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

    private void SetFocus(Interactable newFocus)//����� ��� ��������� �������� ����������
    {
        focus = newFocus;
        motor.FollowTarget(newFocus);
    }

    private void RemoveFocus()//� ����� ����� ��� ������
    {
        focus = null;
        motor.StopFollowingTarget();
    }
}
