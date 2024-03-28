using UnityEngine;

abstract public class EffectObject_Base : ScriptableObject
{
    public abstract Effect_Base GetEffectInstance(CharacterActionController caster = null);
}
