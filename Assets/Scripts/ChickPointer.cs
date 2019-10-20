using UnityEngine;

public class ChickPointer : MonoBehaviour
{
	[SerializeField] private Camera uiCamera;

	[SerializeField] private GameObject chick;
	[SerializeField] private RectTransform pointer;

	private Vector3 chickPos;

	private void Update()
	{
		chickPos = chick.transform.position;

		Vector3 toPos = chickPos;
		Vector3 fromPos = Camera.main.transform.position;
		fromPos.z = 0f;
		Vector3 dir = (toPos - fromPos).normalized;
		float angle = AngleFromVector(dir);
		pointer.localEulerAngles = new Vector3(0, 0, angle);

		Vector3 chickPosOnScreen = Camera.main.WorldToScreenPoint(chickPos);
		bool isOffScreen = chickPosOnScreen.x <= 0 || chickPosOnScreen.x >= Screen.width || chickPosOnScreen.y <= 0 || chickPosOnScreen.y >= Screen.width;

		float borderSize = 40f;

		if (isOffScreen)
		{
			pointer.gameObject.SetActive(isOffScreen);

			Vector3 cappedTargetScreenPosition = chickPosOnScreen;
			cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
			cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

			Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
			pointer.position = pointerWorldPosition;
			pointer.localPosition = new Vector3(pointer.localPosition.x, pointer.localPosition.y, 0f);

		}
		else
		{
			/*Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(chickPosOnScreen);
			pointer.position = pointerWorldPosition;
			pointer.localPosition = new Vector3(pointer.localPosition.x, pointer.localPosition.y, 0f);*/

			pointer.gameObject.SetActive(isOffScreen);

		}
	}

	private float AngleFromVector(Vector3 dir)
	{
		dir = dir.normalized;
		float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		if (n < 0) n += 360;

		return n;
	}
}