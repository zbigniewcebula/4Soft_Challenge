using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
	//Public/Inspector
	[Header("Bindings")]
	[SerializeField] private TMP_Text count = null;

	[Header("Settings")]
	[SerializeField] private float updateInterval = 1f;

	//Private
	private float timer = 0f;
	private float accumulator = 0f;
	private int frames = 0;

	//Method
	private void Update()
	{
		accumulator += (1f / Time.deltaTime);
		++frames;

		if((Time.timeSinceLevelLoad - timer) > updateInterval)
		{
			count.text = (accumulator / frames).ToString("F0");
			accumulator = 0f;
			frames = 0;
			timer = Time.timeSinceLevelLoad;
		}
	}
}