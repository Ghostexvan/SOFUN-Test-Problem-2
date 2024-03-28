using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassiveSkill_GuidanceOfFire : Skill_Base
{
    [SerializeField]
    private string skillName = "Guidance Of Fire";

    [SerializeField]
    private string description = "Increases MD by 30% once per battle when an alive ally has less than 50% HP.";

    [SerializeField]
    private bool isUsed = false;

    [SerializeField]
    private float magicalDamageMultiplier = 0.3f;

    public PassiveSkill_GuidanceOfFire(){

    }

    public PassiveSkill_GuidanceOfFire(PassiveSkill_GuidanceOfFire passiveSkill_GuidanceOfFire){
        this.skillName = passiveSkill_GuidanceOfFire.skillName;
        this.description = passiveSkill_GuidanceOfFire.description;
        this.isUsed = passiveSkill_GuidanceOfFire.isUsed;
        this.magicalDamageMultiplier = passiveSkill_GuidanceOfFire.magicalDamageMultiplier;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        if (caster.GetCurrentHealth() <= 0 || this.isUsed){
            return false;
        }

        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        foreach(GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();

            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.5f){
                return true;
            }
        }

        return false;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new PassiveSkill_GuidanceOfFire(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> allies = GameController.Instance.GetAllyTeam(caster.gameObject);

        foreach(GameObject ally in allies){
            CharacterActionController controller = ally.GetComponent<CharacterActionController>();

            if (controller.GetCurrentHealth() / controller.GetCharacterData().healthPoint.Value <= 0.5f){
                return new List<CharacterActionController>(){
                    controller
                };
            }
        }

        return null;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        this.isUsed = true;
        caster.GetCharacterData().magicalDamage.AddModifier(
            new StatModifier(
                magicalDamageMultiplier,
                StatModType.PercentAdd,
                this
            )
        );
    }
}
