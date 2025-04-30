using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string inputType;
    [SerializeField] private GameObject controlling;

    private List<ICamAxisHandler> cameraC;
    private List<IAxisHandler> move;
    private List<IUseable0> use0;
    private List<IUsable1> use1;
    private List<IInteractorHandler> interactor;
    private List<IDropHandler> drop;
    private List<IHiveMindSummoner> hiveMindSummoner;
    private List<IScrollable> scrollable;
    private List<ICamLockable> camLockable;

    public string GetInputType()
    {
        return inputType;
    }
    public void SetInputType(string inputType)
    { this.inputType = inputType; }

    public void SwitchTarget(GameObject target)
    {
        Debug.Log("Attempt switch");
        controlling = target;
        cameraC.Clear();
        move.Clear();
        use0.Clear();
        use1.Clear();
        interactor.Clear();
        drop.Clear();
        hiveMindSummoner.Clear();
        scrollable.Clear();
        camLockable.Clear();

        cameraC.AddRange(target.GetComponents<ICamAxisHandler>());
        move.AddRange(target.GetComponents<IAxisHandler>());
        use0.AddRange(target.GetComponents<IUseable0>());
        use1.AddRange(target.GetComponents<IUsable1>());
        interactor.AddRange(target.GetComponents<IInteractorHandler>());
        drop.AddRange(target.GetComponents<IDropHandler>());
        hiveMindSummoner.AddRange(target.GetComponents<IHiveMindSummoner>());
        scrollable.AddRange(target.GetComponents<IScrollable>());
        camLockable.AddRange(target.GetComponents<ICamLockable>());

        IInputChangeSummoner[] inputChangeSummoner = target.GetComponents<IInputChangeSummoner>();
        if(inputChangeSummoner != null)
        {
            foreach(IInputChangeSummoner i in inputChangeSummoner)
            {
                i.Notify();
            }
        }
    }
    private void Awake()
    {
        cameraC = new List<ICamAxisHandler>();
        move = new List<IAxisHandler>();
        use0 = new List<IUseable0>();
        use1 = new List<IUsable1>();
        interactor = new List<IInteractorHandler>();
        drop = new List<IDropHandler>();
        hiveMindSummoner = new List<IHiveMindSummoner>();
        scrollable = new List<IScrollable>();
        camLockable = new List<ICamLockable>();

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
            
            if (move.Count > 0)
            {
                Vector2 mov = new Vector2(Input.GetAxis("Horizontal" + inputType), Input.GetAxis("Vertical" + inputType));
                for(int i = 0; i < move.Count; i++)
                    move[i].Move(mov.x, mov.y);
                
            }
            if (cameraC.Count > 0)
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
                for (int i = 0; i < cameraC.Count; i++)
                    cameraC[i].MoveCam(axis.x, axis.y);
                
            }
            if (use0.Count > 0)
            {
                if (Input.GetButtonDown("Fire1" + inputType))
                {
                    for (int i = 0; i < use0.Count; i++)
                        use0[i].Use0();
                }
            }
            if (use1.Count > 0)
            {
                if (Input.GetButtonDown("Fire2" + inputType))
                {
                    for (int i = 0; i < use1.Count; i++)
                        use1[i].Use1();
                }
            }
            if (interactor.Count > 0)
            {
                if (Input.GetButtonDown("Interact" + inputType))
                {
                    for (int i = 0; i < interactor.Count; i++)
                        interactor[i].Interact();
                }
            }
            if (drop.Count > 0)
            {
                if (Input.GetButtonDown("Drop" + inputType))
                {
                    for (int i = 0; i < drop.Count; i++)
                        drop[i].Drop();
                }
            }
            if (hiveMindSummoner.Count > 0)
            {
                if (Input.GetButtonDown("OpenHiveMind" + inputType))
                {
                    for (int i = 0; i < hiveMindSummoner.Count; i++)
                        hiveMindSummoner[i].Summon();
                }
            }
            if (camLockable.Count > 0)
            {
                if (Input.GetButtonDown("SwitchCamLock" + inputType))
                {
                    for (int i = 0; i < camLockable.Count; i++)
                        camLockable[i].CamLock();
                }
            }
            if (scrollable != null)
            {
                if (inputType == "KB")
                {
                    for (int i = 0; i < scrollable.Count; i++)
                        scrollable[i].Scroll(-Input.mouseScrollDelta.y);
                }
                else
                {
                    for (int i = 0; i < scrollable.Count; i++)
                    {
                        if (Input.GetButtonDown("ScrollLeftGP")) scrollable[i].Scroll(-1);
                        if (Input.GetButtonDown("ScrollRightGP")) scrollable[i].Scroll(1);
                    }
                }
            }
        }
    }
}
