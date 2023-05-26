using UnityEngine;
using UnityEngine.InputSystem;

namespace Ingame
{
	public sealed class InputService 
	{
		private InputActions _inputActions = new();

		public bool IsLeftMousePressed => _inputActions.Mouse.LeftButton.IsPressed();
		public bool IsRightMousePressed => _inputActions.Mouse.RightButton.IsPressed();

		public Vector2 MousePosition => Mouse.current.position.value;

		public InputService()
		{
			_inputActions.Enable();
		}
	}
}