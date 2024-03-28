using UnityEngine;

[CreateAssetMenu]
public class ActiveSkillObject_DirtyPlaying : SkillObject_Base
{
    [SerializeField]
    private ActiveSkill_DirtyPlaying activeSkill_DirtyPlaying = new ActiveSkill_DirtyPlaying();

    public override Skill_Base GetSkillInstance() => activeSkill_DirtyPlaying.GetSkillInstance();
}
