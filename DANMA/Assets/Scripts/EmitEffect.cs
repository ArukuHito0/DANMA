using UnityEngine;

public class EmitEffect : PooledObject
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        var main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void OnParticleSystemStopped()
    {
        Release();
    }
}
