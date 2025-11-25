using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameControls.IPlayerActions
{
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction openMenuEvent;
    private GameControls controls;
    void OnEnable()
    {
        if(controls == null)
        {
            controls = new GameControls();
            controls.Player.SetCallbacks(this);
        }
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
    public void OnMenuOpen(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
