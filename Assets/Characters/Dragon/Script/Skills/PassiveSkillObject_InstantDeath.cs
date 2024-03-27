using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_InstantDeath : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_InstantDeath passiveSkill_InstantDeath = new PassiveSkill_InstantDeath();

    public override Skill_Base GetSkillInstance() => passiveSkill_InstantDeath.GetSkillInstance();
}
