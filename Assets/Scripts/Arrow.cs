using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	[SerializeField] private float hitDist = 0.1f;
	[SerializeField] private float searchDist = 20f;
	[SerializeField] private float heightOffset = 3f;

	private Transform rendererTransform;
	private TrailRenderer trailRenderer;
	private EnemyAction target;
	private int damage;
	private float speed;
	private Vector3 startPosition;
	private Vector3 lastFrameTemp;
	private LayerMask enemyMask;

	private void Awake()
	{
		enemyMask = LayerMask.GetMask("Enemy");
		rendererTransform = GetComponentInChildren<Renderer>().transform;
		trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void OnDisable()
	{
		target = null;
		damage = 0;
		speed = 0f;
	}

	public void Init(EnemyAction action, int damage, float speed)
	{
		target = action;
		this.damage = damage;
		this.speed = speed;
		trailRenderer.Clear();
		_ = StartCoroutine(CoMove());
	}

	private IEnumerator CoMove()
	{
		float moveStartTime = Time.time;
		float moveEndPredictTime = Time.time + (target.transform.position - transform.position).magnitude / speed;
		float maxHeight = (moveEndPredictTime - moveStartTime) * 0.5f * -Physics.gravity.y + heightOffset;
		while (true)
		{
			if(target.gameObject.activeSelf == false)
			{
				if (SearchNewTarget() == false)
				{
					break;
				}
			}

			Vector3 diff = target.transform.position - transform.position;
			if(diff.sqrMagnitude < hitDist * hitDist)
			{
				target.TakeDamage(damage);
				break;
			}
			Vector3 dir = (diff).normalized;
			transform.Translate(speed * Time.deltaTime * dir, Space.World);

			Vector3 rendererPos = rendererTransform.position;
			float ratio = (Time.time - moveStartTime) / (moveEndPredictTime - moveStartTime);
			float yAmount = Mathf.Sin(Mathf.Lerp(0f, 180f, ratio) * Mathf.Deg2Rad) * maxHeight;
			rendererPos.y = yAmount;
			rendererTransform.position = rendererPos;
			rendererTransform.up = (lastFrameTemp - rendererTransform.position).normalized;
			
			lastFrameTemp = rendererTransform.position;

			yield return null;
		}
		LeanPool.Despawn(this);
	}

	private bool SearchNewTarget()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, searchDist, enemyMask);

		float minSqrDist = 99999f;
		GameObject minObj = null;
		foreach (Collider collider in colliders)
		{
			float sqrMag = (collider.transform.position - transform.position).sqrMagnitude;
			if (sqrMag < minSqrDist)
			{
				minSqrDist = sqrMag;
				minObj = collider.gameObject;
			}
		}

		if (minObj != null)
		{
			target = minObj.GetComponent<EnemyAction>();
			return true;
		}

		target = null;
		return false;
	}
}
