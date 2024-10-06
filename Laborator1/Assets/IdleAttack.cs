using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
	public Animator animator;
	public Transform otherCharacter;
	public float attackDistance = 0.25f;

	// Start is called before the first frame update
	void Start()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		float distance = Vector3.Distance(transform.position, otherCharacter.position);

		if (distance < attackDistance)
		{
			animator.SetBool("Attack", true);
		}
		else
		{
			animator.SetBool("Attack", false);
		}
	}
}