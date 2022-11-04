using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Locomotion : MonoBehaviour
{
	private NavMeshAgent agent;
	private float currentMoveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float runSpeed;
	[SerializeField] private float walkSpeed;
	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		currentMoveSpeed = walkSpeed;
		if (agent == null) Debug.Log("Locomotion missing NavMeshAgent");
		
	}

	public void Move(Vector3 hitPoint)
	{
		agent.isStopped = false;
		agent.SetDestination(hitPoint);
	}

	public void StopMovement()
	{
		agent.isStopped = true;
		agent.SetDestination(transform.position);

	}
}