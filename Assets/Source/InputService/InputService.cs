using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ingame
{
	public sealed class InputService 
	{
		private InputActions _inputActions = new();

		public bool IsLeftMousePressed => _inputActions.Mouse.LeftButton.IsPressed();
		public bool IsRightMousePressed => _inputActions.Mouse.RightButton.IsPressed();
		public bool IsMiddleMousePressed => _inputActions.Mouse.MiddleMouse.IsPressed();
		
		public bool IsLeftMouseClicked => _inputActions.Mouse.LeftButton.WasPressedThisFrame();
		public bool IsRightMouseClicked => _inputActions.Mouse.RightButton.WasPressedThisFrame();
		public bool IsMiddleMouseClicked => _inputActions.Mouse.MiddleMouse.WasPressedThisFrame();

		public Vector2 MousePosition => Mouse.current.position.value;
		public Vector2 MouseDelta => Mouse.current.delta.ReadValue();
		
		public Vector2 CameraMovement => _inputActions.Camera.Movement.ReadValue<Vector2>();

		public event Action<InputAction.CallbackContext> OnPauseInputReceived
		{
			add => _inputActions.UI.Pause.performed += value;
			remove => _inputActions.UI.Pause.performed -= value;
		} 

		public bool MovementEnabled 
		{
			set
			{
				if(value)
				{
					_inputActions.Mouse.Enable();
					_inputActions.Camera.Enable();
					return;
				}

				_inputActions.Mouse.Disable();
				_inputActions.Camera.Disable();
			}
			get => _inputActions.Mouse.enabled && _inputActions.Camera.enabled;
		}

		public InputService()
		{
			_inputActions.Enable();
		}
	}
}