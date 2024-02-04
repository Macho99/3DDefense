using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
	[SerializeField] private float hitDist = 0.1f;
	[SerializeField] private float heightOffset = 3f;
	[SerializeField] private Mesh[] meshes;

	private Transform rendererTransform;
	private MeshFilter meshFilter;
	private TrailRenderer trailRenderer;
	private Vector3 targetPos;
	private int damage;
	private float speed;
	private float explosionRange;
	private LayerMask enemyMask;
	private Vector3 lastFrameTemp;

	private void Awake()
	{
		meshFilter = GetComponentInChildren<MeshFilter>();
		enemyMask = LayerMask.GetMask("Enemy");
		rendererTransform = GetComponentInChildren<Renderer>().transform;
		trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void OnDisable()
	{
		damage = 0;
		speed = 0f;
	}

	public void Init(Vector3 targetPos, int damage, float speed, float explosionRange, int level)
	{
		this.targetPos = targetPos;
		this.damage = damage;
		this.speed = speed;
		this.explosionRange = explosionRange;
		meshFilter.mesh = meshes[level];
		trailRenderer.Clear();
		_ = StartCoroutine(CoMove());
	}

	private IEnumerator CoMove()
	{
		float moveStartTime = Time.time;
		float moveEndPredictTime = Time.time + (targetPos - transform.position).magnitude / speed;
		float maxHeight = (moveEndPredictTime - moveStartTime) * 0.5f * -Physics.gravity.y + heightOffset;
		while (true)
		{
			Vector3 diff = targetPos - transform.position;
			if (diff.sqrMagnitude < hitDist * hitDist)
			{
				Explosion();
				break;
			}
			Vector3 dir = (diff).normalized;
			transform.Translate(speed * Time.deltaTime * dir, Space.World);

			Vector3 rendererPos = rendererTransform.position;
			float ratio = (Time.time - moveStartTime) / (moveEndPredictTime - moveStartTime);
			float yAmount = Mathf.Sin(Mathf.Lerp(0f, 180f, ratio) * Mathf.Deg2Rad) * maxHeight;
			rendererPos.y = yAmount;
			rendererTransform.position = rendererPos;
			rendererTransform.up = -(lastFrameTemp - rendererTransform.position).normalized;

			lastFrameTemp = rendererTransform.position;

			yield return null;
		}
		GameManager.Resource.Destroy(gameObject);
	}

	private void Explosion()
	{
		Collider[] cols = Physics.OverlapSphere(targetPos, explosionRange, enemyMask);

		foreach(Collider col in cols)
		{
			if(col.TryGetComponent<EnemyAction>(out EnemyAction action))
			{
				action.TakeDamage(damage);
			}
		}
	}
}
