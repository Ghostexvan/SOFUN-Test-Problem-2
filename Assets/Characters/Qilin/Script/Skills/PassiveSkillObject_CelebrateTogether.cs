using UnityEngine;

[CreateAssetMenu]
public class PassiveSkillObject_CelebrateTogether : SkillObject_Base
{
    [SerializeField]
    private PassiveSkill_CelebrateTogether passiveSkill_CelebrateTogether = new PassiveSkill_CelebrateTogether();

    public override Skill_Base GetSkillInstance() => passiveSkill_CelebrateTogether.GetSkillInstance();
}
