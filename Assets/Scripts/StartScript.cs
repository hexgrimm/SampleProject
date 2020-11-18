using System;
using Root;
using UnityEngine;

public class StartScript : MonoBehaviour, IUpdater
{
    public event Action Update = () => { };

    private CompositionRoot _compositionRoot;
    
    void Start()
    {
        _compositionRoot = new CompositionRoot(this);
    }
}