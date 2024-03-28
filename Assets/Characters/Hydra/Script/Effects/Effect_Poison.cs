using System;
using UnityEngine;

[Serializable]
public class Effect_Poison : Effect_Base
{
    [SerializeField]
    private int durationTurn = 2;

    [SerializeField]
    private float magicalDamage;

    [SerializeField]
    private float magicalDamageMultiplier = 0.01f;

    [SerializeField]
    private float turnPassed = 0;

    public Effect_Poison(){

    }

    public Effect_Poison(Effect_Poison effect_Poison){
        this.durationTurn = effect_Poison.durationTurn;
        this.magicalDamage = effect_Poison.magicalDamage;
        this.turnPassed = effect_Poison.turnPassed;
        this.magicalDamageMultiplier = effect_Poison.magicalDamageMultiplier;
    }

    public Effect_Poison(Effect_Poison effect_Poison, float magicalDamage){
        this.durationTurn = effect_Poison.durationTurn;
        this.magicalDamage = magicalDamage;
        this.turnPassed = effect_Poison.turnPassed;
        this.magicalDamageMultiplier = effect_Poison.magicalDamageMultiplier;
    }

    public override Effect_Base GetEffectInstance(CharacterActionController caster = null)
    {
        if (caster == null){
            return new Effect_Poison(this);
        }
        
        return new Effect_Poison(this, caster.GetCharacterData().magicalDamage.Value);
    }

    public override bool ProcessEffect(CharacterActionController to)
    {
        if (to.GetCurrentHealth() > 0){
            Debug.Log("Poison the target");
            to.DealMagicalDamage(magicalDamage * magicalDamageMultiplier + to.GetCharacterData().physicalResistance.Value, int.MaxValue);
        }
        
        turnPassed += 1;
        return turnPassed >= durationTurn;
    }
}
