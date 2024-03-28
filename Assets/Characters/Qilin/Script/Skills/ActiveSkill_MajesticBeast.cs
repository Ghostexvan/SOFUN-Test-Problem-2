using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveSkill_MajesticBeast : Skill_Base
{
    [SerializeField]
    private string skillName = "Majestic Beast";

    [SerializeField]
    private string description = "Inflicts FEAR effect on all enemies.";

    [SerializeField]
    private List<EffectObject_Base> effectList = new List<EffectObject_Base>();

    public ActiveSkill_MajesticBeast(){

    }

    public ActiveSkill_MajesticBeast(ActiveSkill_MajesticBeast activeSkill_MajesticBeast){
        this.skillName = activeSkill_MajesticBeast.skillName;
        this.description = activeSkill_MajesticBeast.description;
        this.effectList = activeSkill_MajesticBeast.effectList;
    }

    public override bool CheckCondition(CharacterActionController caster)
    {
        return caster.GetCurrentHealth() > 0 && caster.GetCurrentMana() == 100 && GameController.Instance.GetEnemiesTeam(caster.gameObject).Count > 0;
    }

    public override Skill_Base GetSkillInstance()
    {
        return new ActiveSkill_MajesticBeast(this);
    }

    public override List<CharacterActionController> GetTargetList(CharacterActionController caster)
    {
        List<GameObject> enemies = GameController.Instance.GetEnemiesTeam(caster.gameObject);

        List<CharacterActionController> enemiesController = new List<CharacterActionController>();

        foreach (GameObject enemy in enemies){
            enemiesController.Add(enemy.GetComponent<CharacterActionController>());
        }

        return enemiesController;
    }

    public override void ProcessSkill(CharacterActionController caster, List<CharacterActionController> targetList)
    {
        foreach(CharacterActionController target in targetList){
            foreach(EffectObject_Base effect in effectList){
                target.AddActiveEffect(effect.GetEffectInstance());
            }
        }

        caster.ChangeCurrentManaPoint(-100);
    }
}
