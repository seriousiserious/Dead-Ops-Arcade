using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void Shoot(this ParticleSystem particle)
    {
        particle.Emit(40);
        SoundManager.instance.PlayGunShot(100);
    }
}
