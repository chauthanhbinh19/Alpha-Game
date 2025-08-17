using System.Collections.Generic;

public class UserSpiritBeastService : IUserSpiritBeastService
{
    private readonly IUserSpiritBeastRepository _userSpiritBeastRepository;

    public UserSpiritBeastService(IUserSpiritBeastRepository userSpiritBeastRepository)
    {
        _userSpiritBeastRepository = userSpiritBeastRepository;
    }

    public static UserSpiritBeastService Create()
    {
        return new UserSpiritBeastService(new UserSpiritBeastRepository());
    }

    public SpiritBeast GetNewLevelPower(SpiritBeast c, double coefficient)
    {
        ISpiritBeastRepository _repository = new SpiritBeastRepository();
        SpiritBeastService _service = new SpiritBeastService(_repository);
        SpiritBeast orginCard = _service.GetSpiritBeastById(c.id);
        SpiritBeast SpiritBeast = new SpiritBeast
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
            ignore_reflection_rate  = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
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
        SpiritBeast.power = EvaluatePower.CalculatePower(
            SpiritBeast.health,
            SpiritBeast.physical_attack, SpiritBeast.physical_defense,
            SpiritBeast.magical_attack, SpiritBeast.magical_defense,
            SpiritBeast.chemical_attack, SpiritBeast.chemical_defense,
            SpiritBeast.atomic_attack, SpiritBeast.atomic_defense,
            SpiritBeast.mental_attack, SpiritBeast.mental_defense,
            SpiritBeast.speed,
            SpiritBeast.critical_damage_rate, SpiritBeast.critical_rate, SpiritBeast.critical_resistance_rate, SpiritBeast.ignore_critical_rate,
            SpiritBeast.penetration_rate, SpiritBeast.penetration_resistance_rate, SpiritBeast.evasion_rate,
            SpiritBeast.damage_absorption_rate, SpiritBeast.ignore_damage_absorption_rate, SpiritBeast.absorbed_damage_rate,
            SpiritBeast.vitality_regeneration_rate, SpiritBeast.vitality_regeneration_resistance_rate,
            SpiritBeast.accuracy_rate, SpiritBeast.lifesteal_rate,
            SpiritBeast.shield_strength, SpiritBeast.tenacity, SpiritBeast.resistance_rate,
            SpiritBeast.combo_rate, SpiritBeast.ignore_combo_rate, SpiritBeast.combo_damage_rate, SpiritBeast.combo_resistance_rate,
            SpiritBeast.stun_rate, SpiritBeast.ignore_stun_rate,
            SpiritBeast.reflection_rate, SpiritBeast.ignore_reflection_rate, SpiritBeast.reflection_damage_rate, SpiritBeast.reflection_resistance_rate,
            SpiritBeast.mana, SpiritBeast.mana_regeneration_rate,
            SpiritBeast.damage_to_different_faction_rate, SpiritBeast.resistance_to_different_faction_rate,
            SpiritBeast.damage_to_same_faction_rate, SpiritBeast.resistance_to_same_faction_rate,
            SpiritBeast.normal_damage_rate, SpiritBeast.normal_resistance_rate,
            SpiritBeast.skill_damage_rate, SpiritBeast.skill_resistance_rate
        );
        return SpiritBeast;
    }
    public SpiritBeast GetNewBreakthroughPower(SpiritBeast c, double coefficient)
    {
        ISpiritBeastRepository _repository = new SpiritBeastRepository();
        SpiritBeastService _service = new SpiritBeastService(_repository);
        SpiritBeast orginCard = _service.GetSpiritBeastById(c.id);
        SpiritBeast SpiritBeast = new SpiritBeast
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
            ignore_reflection_rate  = c.ignore_reflection_rate + orginCard.ignore_reflection_rate * coefficient,
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
        SpiritBeast.power = EvaluatePower.CalculatePower(
            SpiritBeast.health,
            SpiritBeast.physical_attack, SpiritBeast.physical_defense,
            SpiritBeast.magical_attack, SpiritBeast.magical_defense,
            SpiritBeast.chemical_attack, SpiritBeast.chemical_defense,
            SpiritBeast.atomic_attack, SpiritBeast.atomic_defense,
            SpiritBeast.mental_attack, SpiritBeast.mental_defense,
            SpiritBeast.speed,
            SpiritBeast.critical_damage_rate, SpiritBeast.critical_rate, SpiritBeast.critical_resistance_rate, SpiritBeast.ignore_critical_rate,
            SpiritBeast.penetration_rate, SpiritBeast.penetration_resistance_rate, SpiritBeast.evasion_rate,
            SpiritBeast.damage_absorption_rate, SpiritBeast.ignore_damage_absorption_rate, SpiritBeast.absorbed_damage_rate,
            SpiritBeast.vitality_regeneration_rate, SpiritBeast.vitality_regeneration_resistance_rate,
            SpiritBeast.accuracy_rate, SpiritBeast.lifesteal_rate,
            SpiritBeast.shield_strength, SpiritBeast.tenacity, SpiritBeast.resistance_rate,
            SpiritBeast.combo_rate, SpiritBeast.ignore_combo_rate, SpiritBeast.combo_damage_rate, SpiritBeast.combo_resistance_rate,
            SpiritBeast.stun_rate, SpiritBeast.ignore_stun_rate,
            SpiritBeast.reflection_rate, SpiritBeast.ignore_reflection_rate, SpiritBeast.reflection_damage_rate, SpiritBeast.reflection_resistance_rate,
            SpiritBeast.mana, SpiritBeast.mana_regeneration_rate,
            SpiritBeast.damage_to_different_faction_rate, SpiritBeast.resistance_to_different_faction_rate,
            SpiritBeast.damage_to_same_faction_rate, SpiritBeast.resistance_to_same_faction_rate,
            SpiritBeast.normal_damage_rate, SpiritBeast.normal_resistance_rate,
            SpiritBeast.skill_damage_rate, SpiritBeast.skill_resistance_rate
        );
        return SpiritBeast;
    }

    public List<SpiritBeast> GetUserSpiritBeast(string user_id, int pageSize, int offset, string rare)
    {
        List<SpiritBeast> list = _userSpiritBeastRepository.GetUserSpiritBeast(user_id, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<SpiritBeast> GetAllUserSpiritBeast(string user_id, int pageSize, int offset)
    {
        List<SpiritBeast> list = _userSpiritBeastRepository.GetAllUserSpiritBeast(user_id, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserSpiritBeastCount(string user_id, string rare)
    {
        return _userSpiritBeastRepository.GetUserSpiritBeastCount(user_id, rare);
    }

    public bool InsertUserSpiritBeast(SpiritBeast SpiritBeast)
    {
        return _userSpiritBeastRepository.InsertUserSpiritBeast(SpiritBeast);
    }

    public bool UpdateSpiritBeastLevel(SpiritBeast SpiritBeast, int cardLevel)
    {
        return _userSpiritBeastRepository.UpdateSpiritBeastLevel(SpiritBeast, cardLevel);
    }

    public bool UpdateSpiritBeastBreakthrough(SpiritBeast SpiritBeast, int star, int quantity)
    {
        return _userSpiritBeastRepository.UpdateSpiritBeastBreakthrough(SpiritBeast, star, quantity);
    }

    public SpiritBeast GetUserSpiritBeastById(string user_id, string Id)
    {
        return _userSpiritBeastRepository.GetUserSpiritBeastById(user_id, Id);
    }

    public SpiritBeast SumPowerUserSpiritBeast()
    {
        return _userSpiritBeastRepository.SumPowerUserSpiritBeast();
    }

    public bool InsertOrUpdateUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardHeroesSpiritBeast(userId, cardHeroes, spiritBeast);
    }

    public bool InsertOrUpdateUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardCaptainsSpiritBeast(userId, cardCaptains, spiritBeast);
    }

    public bool InsertOrUpdateUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardColonelsSpiritBeast(userId, cardColonels, spiritBeast);
    }

    public bool InsertOrUpdateUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardGeneralsSpiritBeast(userId, cardGenerals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardAdmiralsSpiritBeast(userId, cardAdmirals, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardMilitarySpiritBeast(userId, cardMilitary, spiritBeast);
    }

    public bool InsertOrUpdateUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.InsertOrUpdateUserCardMonstersSpiritBeast(userId, cardMonsters, spiritBeast);
    }

    public List<SpiritBeast> GetAllUserCardHeroesSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardHeroesSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardCaptainsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardCaptainsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardColonelsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardColonelsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardGeneralsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardGeneralsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardAdmiralsSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardAdmiralsSpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardMilitarySpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardMilitarySpiritBeast(user_id, pageSize, offset, status);
    }

    public List<SpiritBeast> GetAllUserCardMonstersSpiritBeast(string user_id, int pageSize, int offset, string status)
    {
        return _userSpiritBeastRepository.GetAllUserCardMonstersSpiritBeast(user_id, pageSize, offset, status);
    }

    public SpiritBeast GetUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes)
    {
        return _userSpiritBeastRepository.GetUserCardHeroesSpiritBeast(userId, cardHeroes);
    }

    public SpiritBeast GetUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains)
    {
        return _userSpiritBeastRepository.GetUserCardCaptainsSpiritBeast(userId, cardCaptains);
    }

    public SpiritBeast GetUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels)
    {
        return _userSpiritBeastRepository.GetUserCardColonelsSpiritBeast(userId, cardColonels);
    }

    public SpiritBeast GetUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals)
    {
        return _userSpiritBeastRepository.GetUserCardGeneralsSpiritBeast(userId, cardGenerals);
    }

    public SpiritBeast GetUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals)
    {
        return _userSpiritBeastRepository.GetUserCardAdmiralsSpiritBeast(userId, cardAdmirals);
    }

    public SpiritBeast GetUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary)
    {
        return _userSpiritBeastRepository.GetUserCardMilitarySpiritBeast(userId, cardMilitary);
    }

    public SpiritBeast GetUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters)
    {
        return _userSpiritBeastRepository.GetUserCardMonstersSpiritBeast(userId, cardMonsters);
    }

    public bool DeleteUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardHeroesSpiritBeast(userId, cardHeroes, spiritBeast);
    }

    public bool DeleteUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardCaptainsSpiritBeast(userId, cardCaptains, spiritBeast);
    }

    public bool DeleteUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardColonelsSpiritBeast(userId, cardColonels, spiritBeast);
    }

    public bool DeleteUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardGeneralsSpiritBeast(userId, cardGenerals, spiritBeast);
    }

    public bool DeleteUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardAdmiralsSpiritBeast(userId, cardAdmirals, spiritBeast);
    }

    public bool DeleteUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardMilitarySpiritBeast(userId, cardMilitary, spiritBeast);
    }

    public bool DeleteUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeast spiritBeast)
    {
        return _userSpiritBeastRepository.DeleteUserCardMonstersSpiritBeast(userId, cardMonsters, spiritBeast);
    }
}
