using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_HereComesTheAid : Skill_Base
{
    [SerializeField]
    private string skillName = "Here Comes The Aid";

    [SerializeField]
    private string description = "Heal an ally with lowest HP by 25% own HP when currently having HASTE effect.";

    [SerializeField]
    private float healthPointMultiplier = 0.25f;

    [SerializeField]
    private List<EffectObject_Base> effectList;

    [SerializeField]
    private int cooldownTurns = 2;

    private int cooldownLeft = 0;

    public PassiveSkill_HereComesTheAid(){

    }

    public PassiveSkill_HereComesTheAid(PassiveSkill_HereComesTheAid passiveSkill_HereComesTheAid){
        this.skillName = passiveSkill_HereComesTheAid.skillName;
        this.description = passiveSkill_HereComesTheAid.description;
        this.healthPointMultiplier = passiveSkill_HereComesTheAid.healthPointMultiplier;
        this.cooldownLeft = passiveSkill_HereComesTheAid.cooldownLeft;
        this.cooldownTurns = passiveSkill_HereComesTheAid.cooldownTurns;
        this.effectList = passiveSkill_HereComesTheAid.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (cooldownLeft != 0){
            cooldownLeft = Mathf.Max(0, cooldownLeft - 1);
        }

        foreach(EffectObject_Base effect in effectList){
            if (caster.IsHavingThisEffect(effect.GetEffectInstance())){
                return cooldownLeft == 0 && caster.GetCurrentHealth() > 0;
            }
        }

        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_HereComesTheAid(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        GameObject target = allies[0];

        foreach (GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();
            CharacterActionController targetController = target.GetComponent<CharacterActionController>();

            if (targetController.GetCharacterData().healthPoint.Value > controller.GetCharacterData().healthPoint.Value){
                target = ally;
            }
        }

        return new List<CharacterActionController>(){
            target.GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            target.ChangeCurrentHealthPoint(caster.GetCharacterData().healthPoint.Value * healthPointMultiplier);
        }

        cooldownLeft = cooldownTurns;
    }
}
