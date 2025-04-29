using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationZoneController : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float damageToSwitchTime;
    [SerializeField] private float dmgFrequency;
    private float _dmgFrequency = 0;
    [SerializeField] private ParticleSystem particles;

    private bool activeZone = false;

    public void ActivateZone()
    {
        particles.Play();
        activeZone= true;
        StartCoroutine(ScheduleDeactivation());
    }

    public IEnumerator ScheduleDeactivation()
    {
        yield return new WaitForSeconds(time);
        DeactivateZone();
    }

    public void DeactivateZone()
    {
        particles.Stop();
        activeZone= false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (activeZone)
        {
            _dmgFrequency += Time.deltaTime;
            if (_dmgFrequency >= dmgFrequency)
            {
                GameStateManager.instance.TakeSwitchTimer(damageToSwitchTime);
                _dmgFrequency = 0;
            }
        }
    }
}
