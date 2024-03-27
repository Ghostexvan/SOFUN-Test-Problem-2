using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActionController : MonoBehaviour
{
    #region Private Serialize Fields
    [SerializeField]
    private CharacterData baseData;

    [SerializeField]
    private SkillObject_Base normalSkillObject;

    [SerializeField]
    private SkillObject_Base passiveSkill1Object;

    [SerializeField]
    private SkillObject_Base passiveSkill2Object;

    [SerializeField]
    private SkillObject_Base activeSkillObject;

    #endregion

    #region Private Fields
    private Vector3 initialPosition; 
    private float currentHealthPoint;
    private int currentMana = 50;
    private List<Effect_Base> activeEffect = new List<Effect_Base>();
    private Skill_Base normalSkill;
    private Skill_Base passiveSkill1;
    private Skill_Base passiveSkill2;
    private Skill_Base activeSkill;
    private CharacterData data;
    private Image healthBar;
    private Image mpBar;

    #endregion

    #region MonoBehaviour Callbacks
    private void Awake() {
        initialPosition = this.transform.position;
        data = ScriptableObject.Instantiate(baseData);
        currentHealthPoint = this.data.healthPoint.Value;
        normalSkill = normalSkillObject.GetSkillInstance();
        passiveSkill1 = passiveSkill1Object.GetSkillInstance();
        passiveSkill2 = passiveSkill2Object.GetSkillInstance();
        activeSkill = activeSkillObject.GetSkillInstance();
        healthBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        mpBar = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        healthBar.fillAmount = currentHealthPoint / data.healthPoint.Value;
        mpBar.fillAmount = currentMana / 100f;
    }

    #endregion

    #region Public Methods
    public void ProcessEffect(){
        for (int index = 0; index < activeEffect.Count; index++){
            Effect_Base skill = activeEffect[index];
            bool skillFinished = skill.ProcessEffect(this);

            if (skillFinished){
                index--;
                activeEffect.Remove(skill);
            }
        }

        GameController.Instance.CharacterProcessSkill();
    }

    public void ProcessSkill(){
        if (passiveSkill1.CheckCondition(this)){
            Debug.Log("Passive Skill 1");
            GetComponent<Animator>().Play("Passive1");
            return;
        }

        if (passiveSkill2.CheckCondition(this)){
            Debug.Log("Passive Skill 2");
            GetComponent<Animator>().Play("Passive2");
            return;
        }

        if (activeSkill.CheckCondition(this)){
            Debug.Log("Active Skill");
            GetComponent<Animator>().Play("Active");
        }
        else if (normalSkill.CheckCondition(this)){
            Debug.Log("Normal Skill");
            // normalSkill.ProcessSkill(this, normalSkill.GetTargetList(this));
            GetComponent<Animator>().Play("Normal");
        } else {
            GameController.Instance.CharacterTurnFinish(this.gameObject);
        }
    }


    public void SetSpriteOrder(int index){
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = index + 10;
    }

    public CharacterData GetCharacterData(){
        return this.data;
    }

    public void ChangeCurrentHealthPoint(float amount){
        Debug.Log("Health amount change: " + amount, this);
        this.currentHealthPoint += amount;
        this.currentHealthPoint = Mathf.Min(this.data.healthPoint.Value, this.currentHealthPoint);
    }

    public bool DealPhysicalDamage(float amount, float casterSpeed){
        if (UnityEngine.Random.Range(0f, 100f) <= data.moveSpeed.Value * (1f + 1f / casterSpeed)){
            Debug.Log("Dodged!", this);
            GetComponent<Animator>().Play("Dodged");
            return false;
        } else {
            Debug.Log("Received: " + (amount - data.physicalResistance.Value), this);
            this.currentHealthPoint -= (amount - data.physicalResistance.Value);

            if (currentHealthPoint <= 0){
                GetComponent<Animator>().Play("Death");
            } else {
                GetComponent<Animator>().Play("Damaged");
            }
            return true;
        }
    }

    public bool DealMagicalDamage(float amount, float casterSpeed){
        if (UnityEngine.Random.Range(0f, 100f) <= data.moveSpeed.Value * (1f + 1f / casterSpeed)){
            Debug.Log("Dodged!", this);
            GetComponent<Animator>().Play("Dodged");
            return false;
        } else {
            Debug.Log("Received: " + (amount - data.magicalResistance.Value), this);
            this.currentHealthPoint -= (amount - data.magicalResistance.Value);

            if (currentHealthPoint <= 0){
                GetComponent<Animator>().Play("Death");
            } else {
                GetComponent<Animator>().Play("Damaged");
            }
            return true;
        }
    }

    public void ChangeCurrentManaPoint(int amount){
        Debug.Log("Mana amount change: " + amount, this);
        this.currentMana += amount;
        this.currentMana = Mathf.Min(100, this.currentMana);
    }

    public void AddActiveEffect(Effect_Base skill){
        Effect_Base skillInstance = skill.GetEffectInstance();
        activeEffect.Add(skillInstance);
    }

    public void AddActiveEffect(EffectObject_Base skillObject){
        Effect_Base skillInstance = skillObject.GetEffectInstance();
        activeEffect.Add(skillInstance);
    }

    public int GetCurrentMana(){
        return this.currentMana;
    }

    public float GetCurrentHealth(){
        return this.currentHealthPoint;
    }

    public void ProcessPassiveSkill1(){
        passiveSkill1.ProcessSkill(this, passiveSkill1.GetTargetList(this));
    }

    public void ProcessPassiveSkill2(){
        passiveSkill2.ProcessSkill(this, passiveSkill2.GetTargetList(this));
    }

    public void ProcessNormalSkill(){
        normalSkill.ProcessSkill(this, normalSkill.GetTargetList(this));
    }

    public void ProcessActiveSkill(){
        activeSkill.ProcessSkill(this, activeSkill.GetTargetList(this));
    }

    public void FinishPassiveSkill1(){
        if (passiveSkill2.CheckCondition(this)){
            Debug.Log("Passive Skill 2");
            passiveSkill2.ProcessSkill(this, passiveSkill2.GetTargetList(this));
            return;
        }

        if (activeSkill.CheckCondition(this)){
            Debug.Log("Active Skill");
            GetComponent<Animator>().Play("Active");
        }
        else if (normalSkill.CheckCondition(this)){
            Debug.Log("Normal Skill");
            // normalSkill.ProcessSkill(this, normalSkill.GetTargetList(this));
            GetComponent<Animator>().Play("Normal");
        } else {
            GameController.Instance.CharacterTurnFinish(this.gameObject);
        }
    }

    public void FinishPassiveSkill2(){
        if (activeSkill.CheckCondition(this)){
            Debug.Log("Active Skill");
            GetComponent<Animator>().Play("Active");
        }
        else if (normalSkill.CheckCondition(this)){
            Debug.Log("Normal Skill");
            // normalSkill.ProcessSkill(this, normalSkill.GetTargetList(this));
            GetComponent<Animator>().Play("Normal");
        } else {
            GameController.Instance.CharacterTurnFinish(this.gameObject);
        }
    }

    public void FinishTurn(){
        GameController.Instance.CharacterTurnFinish(this.gameObject);
    }

    #endregion
}