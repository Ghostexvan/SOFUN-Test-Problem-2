using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_WishingLuck : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_WishingLuck passiveSkill_WishingLuck = new PassiveSkill_WishingLuck();

    public override Skill_Base GetSkillInstance() => passiveSkill_WishingLuck.GetSkillInstance();
}
