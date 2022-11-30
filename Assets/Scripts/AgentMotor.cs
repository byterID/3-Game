using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentMotor : MonoBehaviour//отвечает за передвижение игрока при нажатии
{
    private NavMeshAgent agent;
    private Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            LookAtTarget();
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.interactRadius;
        agent.updateRotation = false;
        target = newTarget.transform;

    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
    private void LookAtTarget()//≈сли перемещать объект следовани€, игрок будет поворачиватьс€ в его сторону
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
}
