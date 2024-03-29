using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_HidingAndWaiting : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_HidingAndWaiting passiveSkill_HidingAndWaiting = new PassiveSkill_HidingAndWaiting();

    public override Skill_Base GetSkillInstance() => passiveSkill_HidingAndWaiting.GetSkillInstance();
}
