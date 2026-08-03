using System.Collections;
using System.Collections.Generic;
public class Skills : BaseEntity, IPowerSortable, IStats
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string CardId { get; set; }
    public string UserId { get; set; }
    
    public double Quality { get; set; }
    public string Type { get; set; }
    public int CurrentStar { get; set; }
    public int TempStar { get; set; }
    public int Position { get; set; }
    public string SkillType { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string TargetType { get; set; }
    public int TargetCount { get; set; }
    public double PercentAllHealth { get; set; }
    public double PercentAllPhysicalAttack { get; set; }
    public double PercentAllPhysicalDefense { get; set; }
    public double PercentAllMagicalAttack { get; set; }
    public double PercentAllMagicalDefense { get; set; }
    public double PercentAllChemicalAttack { get; set; }
    public double PercentAllChemicalDefense { get; set; }
    public double PercentAllAtomicAttack { get; set; }
    public double PercentAllAtomicDefense { get; set; }
    public double PercentAllMentalAttack { get; set; }
    public double PercentAllMentalDefense { get; set; }
    public Currencies Currency { get; set; }
    public BaseStats BaseStats { get; set; } = new BaseStats();
    public SkillSubTypes SkillSubType { get; set; }
    public List<Effects> Effects{ get; set; } = new List<Effects>();
    public Patterns Pattern = new Patterns();
    double IPowerSortable.Power => Power;
    public List<CardSkillRelation> cardHeroIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardCaptainIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardColonelIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardGeneralIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardAdmiralIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardMonsterIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardMilitaryIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardSpellIds = new List<CardSkillRelation>();
    public List<CardSkillRelation> cardSoldierIds = new List<CardSkillRelation>();
    public Skills()
    {
        PercentAllHealth = -1;
        PercentAllPhysicalAttack = -1;
        PercentAllPhysicalDefense = -1;
        PercentAllMagicalAttack = -1;
        PercentAllMagicalDefense = -1;
        PercentAllChemicalAttack = -1;
        PercentAllChemicalDefense = -1;
        PercentAllAtomicAttack = -1;
        PercentAllAtomicDefense = -1;
        PercentAllMentalAttack = -1;
        PercentAllMentalDefense = -1;
    }
    public Skills Clone()
    {
        return (Skills)this.MemberwiseClone();
    }
}
public class CardSkillRelation
{
    public string id { get; set; }   // Map với key 'id' (card_id) từ JSON
    public int pos { get; set; }     // Map với key 'pos' (position) từ JSON
}
