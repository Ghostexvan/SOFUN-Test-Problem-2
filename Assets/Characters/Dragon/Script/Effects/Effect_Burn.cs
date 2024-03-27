using System;
using UnityEngine;

[Serializable]
public class Effect_Burn : Effect_Base
{
    [SerializeField]
    private int durationTurn = 2;

    [SerializeField]
    private float physicalDamageMultiplier = 0.05f;

    [SerializeField]
    private float turnPassed = 0;

    public Effect_Burn(){

    }

    public Effect_Burn(Effect_Burn effect_Burn){
        this.durationTurn = effect_Burn.durationTurn;
        this.physicalDamageMultiplier = effect_Burn.physicalDamageMultiplier;
        this.turnPassed = effect_Burn.turnPassed;
    }

    public override Effect_Base GetEffectInstance()
    {
        return new Effect_Burn(this);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        if (to.GetCurrentHealth() > 0){
            Debug.Log("Burn the target");
            to.DealPhysicalDamage(to.GetCharacterData().healthPoint.Value * physicalDamageMultiplier + to.GetCharacterData().physicalResistance.Value, int.MaxValue);
        }
        
        turnPassed += 1;
        return turnPassed >= durationTurn;
    }
}
