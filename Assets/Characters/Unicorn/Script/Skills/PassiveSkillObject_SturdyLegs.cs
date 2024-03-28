using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_SturdyLegs : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_SturdyLegs passiveSkill_SturdyLegs = new PassiveSkill_SturdyLegs();

    public override Skill_Base GetSkillInstance() => passiveSkill_SturdyLegs.GetSkillInstance();
}
