using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/SpeedEffect")]
public class SpeedEffect : StatusEffectsBase
{
    [SerializeField] private float speedMod;
    [SerializeField] private int duration;

    public override void StartEffect(StatusEffectsController controller, GameObject source)
    {
        this.source = source;
        if(controller.TryGetComponent(out BodyController body))
        {
            body.playerSpeed += speedMod;
        }
        controller.StartCoroutine(CancelEffect(controller));
    }

    public override void StopEffect(StatusEffectsController controller)
    {
        if (controller.TryGetComponent(out BodyController body))
        {
            body.playerSpeed -= speedMod;
        }
    }

    private IEnumerator CancelEffect(StatusEffectsController controller)
    {
        yield return new WaitForSeconds(10);
        controller.RemoveEffect(this);
    }

    public override void Tick(StatusEffectsController controller)
    {
    }
}
