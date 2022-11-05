using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Locomotion : MonoBehaviour
{
	private NavMeshAgent agent;
	private float currentMoveSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float runSpeed = 8f;
	[SerializeField] private float walkSpeed = 5f;
	private float currentTargetSpeed;
	bool isManualRotation = false;
	private Transform currentTarget;
	private float currentMovementSpeed;
	private Vector3 lastPosition;
	private float lastMovementSpeed;
	public event Action<float> SpeedChanged;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		currentMoveSpeed = walkSpeed;
		if (agent == null) Debug.Log("Locomotion missing NavMeshAgent");
		currentTargetSpeed = walkSpeed;
		lastPosition = transform.position;
		agent.speed = walkSpeed;
	}

	private void Update()
	{
		UpdateCurrentSpeed();

		if (isManualRotation) RotateTowardsTarget();
	}

	private void UpdateCurrentSpeed()
	{
		currentMovementSpeed = Mathf.Lerp(currentMovementSpeed,
			(transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
		if (Mathf.Abs(lastMovementSpeed - currentMovementSpeed) > 0.05)
		{
			SpeedChanged?.Invoke(currentMovementSpeed);
		}

		lastMovementSpeed = currentMovementSpeed;
		lastPosition = transform.position;
	}

	private void RotateTowardsTarget()
	{
		if (currentTarget == null)
		{
			isManualRotation= false;
			return;
		}
		
		Vector3 direction = currentTarget.position - transform.position;
		direction.Normalize();
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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

	public void SetAutoRotation()
	{
		isManualRotation = false;
		agent.stoppingDistance = 0f;
		agent.updateRotation = true;
	}

	public void SetManualRotation(Interactable target)
	{
		currentTarget = target.transform;
		isManualRotation = true;
		agent.updateRotation = false;
		agent.stoppingDistance = target.GetInteractionRadius() * 0.8f;
	}

	public void SetRun(bool isRunning)
	{
		currentTargetSpeed = isRunning ? runSpeed : walkSpeed;
	}
}