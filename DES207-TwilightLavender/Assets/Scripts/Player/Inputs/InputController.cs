using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string inputType;

    private ICamAxisHandler cameraC;
    private IAxisHandler move;
    private IUseable0 use0;
    private IUsable1 use1;
    private IInteractorHandler interactor;
    private IDropHandler drop;

    public string GetInputType()
    {
        return inputType;
    }
    public void SetInputType(string inputType)
    { this.inputType = inputType; }

    public void SwitchTarget(GameObject target)
    {
        cameraC = target.GetComponent<ICamAxisHandler>();
        move = target.GetComponent<IAxisHandler>();
        use0= target.GetComponent<IUseable0>();
        use1= target.GetComponent<IUsable1>();
        interactor = target.GetComponent<IInteractorHandler>();
        drop = target.GetComponent<IDropHandler>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        //Please bring the priest and exorcise this later. Good lord, I defy anyone reading this to do worst. I blame the gremlins. Check the attributes thingy later
        if (inputType != null || inputType != "")
        {
            
            if (move != null)
            {
                Vector2 mov = new Vector2(Input.GetAxis("Horizontal" + inputType), Input.GetAxis("Vertical" + inputType));
                move.Move(mov.x, mov.y);
                
            }
            if (cameraC != null)
            {
                Vector2 axis = Vector2.zero;
                if (inputType[0] == 'K')
                {
                    axis = new Vector2(Input.mousePositionDelta.x, Input.mousePositionDelta.y);
                }
                else
                {
                    axis = new Vector2(Input.GetAxis("HCamGP"), Input.GetAxis("VCamGP"));
                }
                cameraC.MoveCam(axis.x, axis.y);
                
            }
            if (use0 != null)
            {
                if (Input.GetButtonDown("Fire1" + inputType))
                {
                    use0.Use0();
                }
            }
            if (use1 != null)
            {
                if (Input.GetButtonDown("Fire2" + inputType))
                {
                    use1.Use1();
                }
            }
            if (interactor != null)
            {
                if (Input.GetButtonDown("Interact" + inputType))
                {
                    Debug.Log("interact!");
                    interactor.Interact();
                }
            }
            if (drop != null)
            {
                if (Input.GetButtonDown("Drop" + inputType))
                {
                    Debug.Log("Drop!");
                    drop.Drop();
                }
            }
        }
    }
}
