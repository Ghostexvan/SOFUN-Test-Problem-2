using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_HidingAndWaiting : Skill_Base
{
    [SerializeField]
    private string skillName = "Hiding And Waiting";

    [SerializeField]
    private string description = "Deals 100% PD to an enemy currently having FEAR effect.";

    [SerializeField]
    private float physicalDamageMultiplier = 1f;

    [SerializeField]
    private List<EffectObject_Base> effectList;

    [SerializeField]
    private int cooldownTurns = 2;

    private int cooldownLeft = 0;

    public PassiveSkill_HidingAndWaiting(){

    }

    public PassiveSkill_HidingAndWaiting(PassiveSkill_HidingAndWaiting passiveSkill_HereComesTheAid){
        this.skillName = passiveSkill_HereComesTheAid.skillName;
        this.description = passiveSkill_HereComesTheAid.description;
        this.physicalDamageMultiplier = passiveSkill_HereComesTheAid.physicalDamageMultiplier;
        this.cooldownLeft = passiveSkill_HereComesTheAid.cooldownLeft;
        this.cooldownTurns = passiveSkill_HereComesTheAid.cooldownTurns;
        this.effectList = passiveSkill_HereComesTheAid.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }

        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        foreach(GameObject enemy in enemies){
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();
            foreach(EffectObject_Base effect in effectList){
                if (controller.IsHavingThisEffect(effect.GetEffectInstance())){
                    return cooldownLeft == 0 && caster.GetCurrentHealth() > 0;
                }
            }
        }

        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_HidingAndWaiting(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        foreach(GameObject enemy in enemies){
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();
            foreach(EffectObject_Base effect in effectList){
                if (controller.IsHavingThisEffect(effect.GetEffectInstance())){
                    return new List<CharacterActionController>(){
                        controller
                    };
                }
            }
        }

        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.DealPhysicalDamage(
                caster.GetCharacterData().physicalDamage.Value * physicalDamageMultiplier,
                caster.GetCharacterData().moveSpeed.Value
            );
        }

        cooldownLeft = cooldownTurns;
    }
}
