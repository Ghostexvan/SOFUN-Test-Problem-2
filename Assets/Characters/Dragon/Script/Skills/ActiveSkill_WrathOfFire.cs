using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_WrathOfFire : Skill_Base
{
    [SerializeField]
    private string skillName = "Wrath of Fire";

    [SerializeField]
    private string description = "Deals 150% PD to the enemy target with the lowest health, applying BURN effect to that target.";

    [SerializeField]
    private float physicalDamageMultiplier = 1.5f;

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    public ActiveSkill_WrathOfFire() {

    }

    public ActiveSkill_WrathOfFire(ActiveSkill_WrathOfFire activeSkill_WrathOfFire){
        this.skillName = activeSkill_WrathOfFire.skillName;
        this.description = activeSkill_WrathOfFire.description;
        this.physicalDamageMultiplier = activeSkill_WrathOfFire.physicalDamageMultiplier;
        this.effectList = activeSkill_WrathOfFire.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_WrathOfFire(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = new List<GameObject>(
            GameController.Instance.GetEnemiesTeam(caster.gameObject)
        );

        GameObject target = enemies[0];

        foreach(GameObject enemy in enemies){
            CharacterActionController targetController = target.GetComponent<CharacterActionController>();
            CharacterActionController controller = enemy.GetComponent<CharacterActionController>();

            if (controller.GetCurrentHealth() <= 0){
                continue;
            }

            if (controller.GetCurrentHealth() < targetController.GetCurrentHealth()){
                target = enemy;
            }
        }

        return new List<CharacterActionController>(){
            target.GetComponent<CharacterActionController>()
        };
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            if (target.DealPhysicalDamage(caster.GetCharacterData().physicalDamage.Value * physicalDamageMultiplier, caster.GetCharacterData().moveSpeed.Value)){
                foreach(EffectObject_Base effect in effectList){
                    target.AddActiveEffect(effect.GetEffectInstance());
                }
            }
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
