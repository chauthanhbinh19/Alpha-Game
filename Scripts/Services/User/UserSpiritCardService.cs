using System.Collections.Generic;

public class UserSpiritCardService : IUserSpiritCardService
{
    private readonly IUserSpiritCardRepository _userSpiritCardRepository;

    public UserSpiritCardService(IUserSpiritCardRepository userSpiritCardRepository)
    {
        _userSpiritCardRepository = userSpiritCardRepository;
    }

    public static UserSpiritCardService Create()
    {
        return new UserSpiritCardService(new UserSpiritCardRepository());
    }

    public SpiritCard GetNewLevelPower(SpiritCard c, double coefficient)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        SpiritCard orginCard = _service.GetSpiritCardById(c.id);
        SpiritCard SpiritCard = new SpiritCard
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            critical_resistance_rate = c.critical_resistance_rate + orginCard.critical_resistance_rate * coefficient,
            ignore_critical_rate = c.ignore_critical_rate + orginCard.ignore_critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            penetration_resistance_rate = c.penetration_resistance_rate + orginCard.penetration_resistance_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + orginCard.ignore_damage_absorption_rate * coefficient,
            absorbed_damage_rate = c.absorbed_damage_rate + orginCard.absorbed_damage_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + orginCard.vitality_regeneration_resistance_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            ignore_combo_rate = c.ignore_combo_rate + orginCard.ignore_combo_rate * coefficient,
            combo_damage_rate = c.combo_damage_rate + orginCard.combo_damage_rate * coefficient,
            combo_resistance_rate = c.combo_resistance_rate + orginCard.combo_resistance_rate * coefficient,
            stun_rate = c.stun_rate + orginCard.stun_rate * coefficient,
            ignore_stun_rate = c.ignore_stun_rate + orginCard.ignore_stun_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            ignore_reflection_rate = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
            reflection_damage_rate = c.reflection_damage_rate + orginCard.reflection_damage_rate * coefficient,
            reflection_resistance_rate = c.reflection_resistance_rate + orginCard.reflection_resistance_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            normal_damage_rate = c.normal_damage_rate + orginCard.normal_damage_rate * coefficient,
            normal_resistance_rate = c.normal_resistance_rate + orginCard.normal_resistance_rate * coefficient,
            skill_damage_rate = c.skill_damage_rate + orginCard.skill_damage_rate * coefficient,
            skill_resistance_rate = c.skill_resistance_rate + orginCard.skill_resistance_rate * coefficient
        };
        SpiritCard.power = EvaluatePower.CalculatePower(
            SpiritCard.health,
            SpiritCard.physical_attack, SpiritCard.physical_defense,
            SpiritCard.magical_attack, SpiritCard.magical_defense,
            SpiritCard.chemical_attack, SpiritCard.chemical_defense,
            SpiritCard.atomic_attack, SpiritCard.atomic_defense,
            SpiritCard.mental_attack, SpiritCard.mental_defense,
            SpiritCard.speed,
            SpiritCard.critical_damage_rate, SpiritCard.critical_rate, SpiritCard.critical_resistance_rate, SpiritCard.ignore_critical_rate,
            SpiritCard.penetration_rate, SpiritCard.penetration_resistance_rate, SpiritCard.evasion_rate,
            SpiritCard.damage_absorption_rate, SpiritCard.ignore_damage_absorption_rate, SpiritCard.absorbed_damage_rate,
            SpiritCard.vitality_regeneration_rate, SpiritCard.vitality_regeneration_resistance_rate,
            SpiritCard.accuracy_rate, SpiritCard.lifesteal_rate,
            SpiritCard.shield_strength, SpiritCard.tenacity, SpiritCard.resistance_rate,
            SpiritCard.combo_rate, SpiritCard.ignore_combo_rate, SpiritCard.combo_damage_rate, SpiritCard.combo_resistance_rate,
            SpiritCard.stun_rate, SpiritCard.ignore_stun_rate,
            SpiritCard.reflection_rate, SpiritCard.ignore_reflection_rate, SpiritCard.reflection_damage_rate, SpiritCard.reflection_resistance_rate,
            SpiritCard.mana, SpiritCard.mana_regeneration_rate,
            SpiritCard.damage_to_different_faction_rate, SpiritCard.resistance_to_different_faction_rate,
            SpiritCard.damage_to_same_faction_rate, SpiritCard.resistance_to_same_faction_rate,
            SpiritCard.normal_damage_rate, SpiritCard.normal_resistance_rate,
            SpiritCard.skill_damage_rate, SpiritCard.skill_resistance_rate
        );
        return SpiritCard;
    }
    public SpiritCard GetNewBreakthroughPower(SpiritCard c, double coefficient)
    {
        ISpiritCardRepository _repository = new SpiritCardRepository();
        SpiritCardService _service = new SpiritCardService(_repository);
        SpiritCard orginCard = _service.GetSpiritCardById(c.id);
        SpiritCard SpiritCard = new SpiritCard
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage_rate = c.critical_damage_rate + orginCard.critical_damage_rate * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            critical_resistance_rate = c.critical_resistance_rate + orginCard.critical_resistance_rate * coefficient,
            ignore_critical_rate = c.ignore_critical_rate + orginCard.ignore_critical_rate * coefficient,
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            penetration_resistance_rate = c.penetration_resistance_rate + orginCard.penetration_resistance_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            ignore_damage_absorption_rate = c.ignore_damage_absorption_rate + orginCard.ignore_damage_absorption_rate * coefficient,
            absorbed_damage_rate = c.absorbed_damage_rate + orginCard.absorbed_damage_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            vitality_regeneration_resistance_rate = c.vitality_regeneration_resistance_rate + orginCard.vitality_regeneration_resistance_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            ignore_combo_rate = c.ignore_combo_rate + orginCard.ignore_combo_rate * coefficient,
            combo_damage_rate = c.combo_damage_rate + orginCard.combo_damage_rate * coefficient,
            combo_resistance_rate = c.combo_resistance_rate + orginCard.combo_resistance_rate * coefficient,
            stun_rate = c.stun_rate + orginCard.stun_rate * coefficient,
            ignore_stun_rate = c.ignore_stun_rate + orginCard.ignore_stun_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            ignore_reflection_rate = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
            reflection_damage_rate = c.reflection_damage_rate + orginCard.reflection_damage_rate * coefficient,
            reflection_resistance_rate = c.reflection_resistance_rate + orginCard.reflection_resistance_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            normal_damage_rate = c.normal_damage_rate + orginCard.normal_damage_rate * coefficient,
            normal_resistance_rate = c.normal_resistance_rate + orginCard.normal_resistance_rate * coefficient,
            skill_damage_rate = c.skill_damage_rate + orginCard.skill_damage_rate * coefficient,
            skill_resistance_rate = c.skill_resistance_rate + orginCard.skill_resistance_rate * coefficient
        };
        SpiritCard.power = EvaluatePower.CalculatePower(
            SpiritCard.health,
            SpiritCard.physical_attack, SpiritCard.physical_defense,
            SpiritCard.magical_attack, SpiritCard.magical_defense,
            SpiritCard.chemical_attack, SpiritCard.chemical_defense,
            SpiritCard.atomic_attack, SpiritCard.atomic_defense,
            SpiritCard.mental_attack, SpiritCard.mental_defense,
            SpiritCard.speed,
            SpiritCard.critical_damage_rate, SpiritCard.critical_rate, SpiritCard.critical_resistance_rate, SpiritCard.ignore_critical_rate,
            SpiritCard.penetration_rate, SpiritCard.penetration_resistance_rate, SpiritCard.evasion_rate,
            SpiritCard.damage_absorption_rate, SpiritCard.ignore_damage_absorption_rate, SpiritCard.absorbed_damage_rate,
            SpiritCard.vitality_regeneration_rate, SpiritCard.vitality_regeneration_resistance_rate,
            SpiritCard.accuracy_rate, SpiritCard.lifesteal_rate,
            SpiritCard.shield_strength, SpiritCard.tenacity, SpiritCard.resistance_rate,
            SpiritCard.combo_rate, SpiritCard.ignore_combo_rate, SpiritCard.combo_damage_rate, SpiritCard.combo_resistance_rate,
            SpiritCard.stun_rate, SpiritCard.ignore_stun_rate,
            SpiritCard.reflection_rate, SpiritCard.ignore_reflection_rate, SpiritCard.reflection_damage_rate, SpiritCard.reflection_resistance_rate,
            SpiritCard.mana, SpiritCard.mana_regeneration_rate,
            SpiritCard.damage_to_different_faction_rate, SpiritCard.resistance_to_different_faction_rate,
            SpiritCard.damage_to_same_faction_rate, SpiritCard.resistance_to_same_faction_rate,
            SpiritCard.normal_damage_rate, SpiritCard.normal_resistance_rate,
            SpiritCard.skill_damage_rate, SpiritCard.skill_resistance_rate
        );
        return SpiritCard;
    }

    public List<SpiritCard> GetUserSpiritCard(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCard> list = _userSpiritCardRepository.GetUserSpiritCard(user_id, type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<SpiritCard> GetAllUserSpiritCard(string user_id, int pageSize, int offset)
    {
        List<SpiritCard> list = _userSpiritCardRepository.GetAllUserSpiritCard(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSpiritCardCount(string user_id, string type, string rare)
    {
        return _userSpiritCardRepository.GetUserSpiritCardCount(user_id, type, rare);
    }

    public bool InsertUserSpiritCard(SpiritCard SpiritCard)
    {
        return _userSpiritCardRepository.InsertUserSpiritCard(SpiritCard);
    }

    public bool UpdateSpiritCardLevel(SpiritCard SpiritCard, int cardLevel)
    {
        return _userSpiritCardRepository.UpdateSpiritCardLevel(SpiritCard, cardLevel);
    }

    public bool UpdateSpiritCardBreakthrough(SpiritCard SpiritCard, int star, int quantity)
    {
        return _userSpiritCardRepository.UpdateSpiritCardBreakthrough(SpiritCard, star, quantity);
    }

    public SpiritCard GetUserSpiritCardById(string user_id, string Id)
    {
        return _userSpiritCardRepository.GetUserSpiritCardById(user_id, Id);
    }

    public SpiritCard SumPowerUserSpiritCard()
    {
        return _userSpiritCardRepository.SumPowerUserSpiritCard();
    }

    public bool InsertOrUpdateUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardHeroesSpiritCard(userId, cardHeroes, spiritBeast);
    }

    public bool InsertOrUpdateUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardCaptainsSpiritCard(userId, cardCaptains, spiritBeast);
    }

    public bool InsertOrUpdateUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardColonelsSpiritCard(userId, cardColonels, spiritBeast);
    }

    public bool InsertOrUpdateUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardGeneralsSpiritCard(userId, cardGenerals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardAdmiralsSpiritCard(userId, cardAdmirals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardMilitarySpiritCard(userId, cardMilitary, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardMonstersSpiritCard(userId, cardMonsters, spiritBeast);
    }

    public bool InsertOrUpdateUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.InsertOrUpdateUserCardSpellSpiritCard(userId, cardSpell, spiritBeast);
    }

    public List<SpiritCard> GetAllUserCardHeroesSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardHeroesSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardCaptainsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardCaptainsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardColonelsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardColonelsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardGeneralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardGeneralsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardAdmiralsSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardAdmiralsSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardMilitarySpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardMilitarySpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardMonstersSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardMonstersSpiritCard(user_id, pageSize, offset, status);
    }

    public List<SpiritCard> GetAllUserCardSpellSpiritCard(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritCardRepository.GetAllUserCardSpellSpiritCard(user_id, pageSize, offset, status);
    }

    public SpiritCard GetUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes)
    {
        return _userSpiritCardRepository.GetUserCardHeroesSpiritCard(userId, cardHeroes);
    }

    public SpiritCard GetUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains)
    {
        return _userSpiritCardRepository.GetUserCardCaptainsSpiritCard(userId, cardCaptains);
    }

    public SpiritCard GetUserCardColonelsSpiritCard(string userId, CardColonels cardColonels)
    {
        return _userSpiritCardRepository.GetUserCardColonelsSpiritCard(userId, cardColonels);
    }

    public SpiritCard GetUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals)
    {
        return _userSpiritCardRepository.GetUserCardGeneralsSpiritCard(userId, cardGenerals);
    }

    public SpiritCard GetUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals)
    {
        return _userSpiritCardRepository.GetUserCardAdmiralsSpiritCard(userId, cardAdmirals);
    }

    public SpiritCard GetUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary)
    {
        return _userSpiritCardRepository.GetUserCardMilitarySpiritCard(userId, cardMilitary);
    }

    public SpiritCard GetUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters)
    {
        return _userSpiritCardRepository.GetUserCardMonstersSpiritCard(userId, cardMonsters);
    }

    public SpiritCard GetUserCardSpellSpiritCard(string userId, CardSpell cardSpell)
    {
        return _userSpiritCardRepository.GetUserCardSpellSpiritCard(userId, cardSpell);
    }

    public bool DeleteUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardHeroesSpiritCard(userId, cardHeroes, spiritBeast);
    }

    public bool DeleteUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardCaptainsSpiritCard(userId, cardCaptains, spiritBeast);
    }

    public bool DeleteUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardColonelsSpiritCard(userId, cardColonels, spiritBeast);
    }

    public bool DeleteUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardGeneralsSpiritCard(userId, cardGenerals, spiritBeast);
    }

    public bool DeleteUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardAdmiralsSpiritCard(userId, cardAdmirals, spiritBeast);
    }

    public bool DeleteUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardMilitarySpiritCard(userId, cardMilitary, spiritBeast);
    }

    public bool DeleteUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardMonstersSpiritCard(userId, cardMonsters, spiritBeast);
    }
    
    public bool DeleteUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast)
    {
        return _userSpiritCardRepository.DeleteUserCardSpellSpiritCard(userId, cardSpell, spiritBeast);
    }
}
