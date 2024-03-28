using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_PoisonousTouch : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_PoisonousTouch passiveSkill_PoisonousTouch = new PassiveSkill_PoisonousTouch();

    public override Skill_Base GetSkillInstance() => passiveSkill_PoisonousTouch.GetSkillInstance();
}
