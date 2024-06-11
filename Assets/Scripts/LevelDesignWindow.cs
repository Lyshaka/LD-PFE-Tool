using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelDesignWindow : MonoBehaviour, IDragHandler
{
	[SerializeField] RectTransform toDrag;

	public void OnDrag(PointerEventData eventData)
	{
		toDrag.anchoredPosition += eventData.delta;

		if (toDrag.anchoredPosition.x < -(Screen.width / 2))
		{
			toDrag.anchoredPosition = new Vector2(-(Screen.width / 2), toDrag.anchoredPosition.y);
		}
		if (toDrag.anchoredPosition.x > (Screen.width / 2))
		{
			toDrag.anchoredPosition = new Vector2(Screen.width / 2, toDrag.anchoredPosition.y);
		}
		if (toDrag.anchoredPosition.y < -(Screen.height / 2))
		{
			toDrag.anchoredPosition = new Vector2(toDrag.anchoredPosition.x, -(Screen.height / 2));
		}
		if (toDrag.anchoredPosition.y > (Screen.height / 2))
		{
			toDrag.anchoredPosition = new Vector2(toDrag.anchoredPosition.x, Screen.height / 2);
		}
	}
}
