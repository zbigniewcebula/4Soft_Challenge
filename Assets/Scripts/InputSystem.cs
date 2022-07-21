using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
	//Public/Inspector
	[Header("Bindings")]
	[SerializeField] private PlaneGenerator plane = null;

	//Private
	private InputControls control = null;

	//Method
	private void Start()
	{
		control = new InputControls();
		control.Enable();

		control.Default.WireframeMode.started += WireframeMode_started;
	}

	private void WireframeMode_started(InputAction.CallbackContext obj)
	{
		plane.SwitchMaterial();
	}
}