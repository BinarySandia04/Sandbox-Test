using Godot;
using System;

public class CameraController : Camera
{
    // Variables para mirar
    private float currentX = 0f;
    private float currentY = 0f;
    
    [Export]
    public float sensitivity = 0.002f;

    // Variables para moverse
    [Export]
    public float movingSpeed = 10;
    [Export]
    public float runningSpeed = 30;

    private float currentSpeed;
    
    [Export]
    public float smoothness = 0.1f;

    private float speedMultiplier = 2;

    private Node mainNode;

    private Vector3 currentMovement = new Vector3(0, 0, 0);

    public override void _Ready()
    {
        mainNode = GetNode("/root/Root Node");
        currentSpeed = movingSpeed;
    }

    public override void _Process(float delta)
    {
        if (Input.IsKeyPressed((int) KeyList.Shift)) currentSpeed = runningSpeed;
        else currentSpeed = movingSpeed;

        Vector3 normalTranslation = new Vector3(0, 0, 0);

        if (GeneralScript.Instance.pausedMovement)
        {
            if(Input.IsKeyPressed((int) KeyList.W))
            {
                normalTranslation -= GlobalTransform.basis.z * delta * currentSpeed;
            }
            if(Input.IsKeyPressed((int) KeyList.S))
            {
                normalTranslation += GlobalTransform.basis.z * delta * currentSpeed;
            }
            if(Input.IsKeyPressed((int) KeyList.A))
            {
                normalTranslation -= GlobalTransform.basis.x * delta * currentSpeed;
            }
            if(Input.IsKeyPressed((int) KeyList.D))
            {
                normalTranslation += GlobalTransform.basis.x * delta * currentSpeed;
            }
        }

        currentMovement = Utils.Lerp(currentMovement, normalTranslation, smoothness);
        Translation += currentMovement;
    }
    
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    
    public override void _Input(InputEvent ev)
    {
        if (ev.GetType() == typeof(InputEventMouseMotion) && (bool) mainNode.Get("pausedMovement"))
        {
            // Cogemos el evento de mover el raton
            InputEventMouseMotion mousevent = (InputEventMouseMotion) ev;
            
            // modify accumulated mouse rotation
            _rotationX -= mousevent.Relative.x * sensitivity;
            _rotationY -= mousevent.Relative.y * sensitivity;
            
            Transform transform = Transform;
            transform.basis = Basis.Identity;
            Transform = transform;

            
            // Rotamos el objeto
            RotateObjectLocal(Vector3.Up, _rotationX);
            RotateObjectLocal(Vector3.Right, _rotationY);
        }
    }
}
