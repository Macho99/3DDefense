using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
	[SerializeField] Image background;
	[SerializeField] Image greenMask;
	[SerializeField] Image redMask;
	[SerializeField] float lerpSpeed = 5f;

	private EnemyAction owner;
	private Coroutine offCoroutine;
	private float lastOnTime;
	private float turnOnDuration = 3f;

	private void Awake()
	{
		owner = transform.parent.parent.GetComponent<EnemyAction>();
	}

	private void OnEnable()
	{
		owner.OnHpChanged.AddListener(UIUpdate);
		SetVisible(false);
	}

	private void OnDisable()
	{
		owner.OnHpChanged.RemoveListener(UIUpdate);
	}

	public void UIUpdate(float ratio)
	{
		SetVisible(true);

		greenMask.fillAmount = ratio;
		lastOnTime = Time.time;
		StopAllCoroutines();

		if (owner.CurHp <= 0)
		{
			SetVisible(false);
			return;
		}

		StartCoroutine(CoOff());
	}

	private IEnumerator CoOff()
	{
		yield return new WaitForSeconds(1f);

		while (Mathf.Abs(greenMask.fillAmount - redMask.fillAmount) > 0.0001f)
		{
			redMask.fillAmount = Mathf.Lerp(greenMask.fillAmount, redMask.fillAmount, 1 - Time.deltaTime * lerpSpeed);
			yield return null;
		}
	}

	private void SetVisible(bool val)
	{
		background.gameObject.SetActive(val);
		greenMask.gameObject.SetActive(val);
		redMask.gameObject.SetActive(val);
	}
}
