using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
	protected Button closeButton;
	protected Text titleText;
	protected Text contentText;

	private void Awake()
	{
		closeButton = transform.Find("Header/CloseButton").GetComponent<Button>();
		titleText = transform.Find("Header/Text").GetComponent<Text>();
		contentText = transform.Find("Content/Text").GetComponent<Text>();

		closeButton.onClick.AddListener(OnClose);
	}

	protected virtual void OnOpen(string title, string content = "")
	{
		titleText.text = title;
		contentText.text = content;	
	}

	protected virtual void OnClose()
	{
		gameObject.SetActive(false);
	}
}
