using System.Collections.Generic;

public class UserEquipmentsService : IUserEquipmentsService
{
    private IUserEquipmentsRepository _userEquipmentsRepository;

    public UserEquipmentsService(IUserEquipmentsRepository userEquipmentsRepository)
    {
        _userEquipmentsRepository = userEquipmentsRepository;
    }

    public static UserEquipmentsService Create()
    {
        return new UserEquipmentsService(new UserEquipmentsRepository());
    }

    public List<Equipments> GetAllRankPower(string user_id, List<Equipments> EquipmentsList)
    {
        IUserEquipmentsRankRepository userEquipmentsRankRepository = new UserEquipmentsRankRepository();
        UserEquipmentsRankService userEquipmentsService = new UserEquipmentsRankService(userEquipmentsRankRepository);
        foreach (var c in EquipmentsList)
        {
            Equipments card = _userEquipmentsRepository.GetUserEquipmentsById(user_id, c.id);
            Rank rank = userEquipmentsService.GetSumEquipmentsRank(user_id, c.id);
            c.health = c.health + rank.health + card.health * rank.percent_all_health / 100;
            c.physical_attack = c.physical_attack + rank.physical_attack + card.physical_attack * rank.percent_all_physical_attack / 100;
            c.physical_defense = c.physical_defense + rank.physical_defense + card.physical_defense * rank.percent_all_physical_defense / 100;
            c.magical_attack = c.magical_attack + rank.magical_attack + card.magical_attack * rank.percent_all_magical_attack / 100;
            c.magical_defense = c.magical_defense + rank.magical_defense + card.magical_defense * rank.percent_all_magical_defense / 100;
            c.chemical_attack = c.chemical_attack + rank.chemical_attack + card.chemical_attack * rank.percent_all_chemical_attack / 100;
            c.chemical_defense = c.chemical_defense + rank.chemical_defense + card.chemical_defense * rank.percent_all_chemical_defense / 100;
            c.atomic_attack = c.atomic_attack + rank.atomic_attack + card.atomic_attack * rank.percent_all_atomic_attack / 100;
            c.atomic_defense = c.atomic_defense + rank.atomic_defense + card.atomic_defense * rank.percent_all_atomic_defense / 100;
            c.mental_attack = c.mental_attack + rank.mental_attack + card.mental_attack * rank.percent_all_mental_attack / 100;
            c.mental_defense = c.mental_defense + rank.mental_defense + card.mental_defense * rank.percent_all_mental_defense / 100;
            c.speed = c.speed + rank.speed;
            c.critical_damage_rate = c.critical_damage_rate + rank.critical_damage_rate;
            c.critical_rate = c.critical_rate + rank.critical_rate;
            c.penetration_rate = c.penetration_rate + rank.penetration_rate;
            c.evasion_rate = c.evasion_rate + rank.evasion_rate;
            c.damage_absorption_rate = c.damage_absorption_rate + rank.damage_absorption_rate;
            c.vitality_regeneration_rate = c.vitality_regeneration_rate + rank.vitality_regeneration_rate;
            c.accuracy_rate = c.accuracy_rate + rank.accuracy_rate;
            c.lifesteal_rate = c.lifesteal_rate + rank.lifesteal_rate;
            c.shield_strength = c.shield_strength + rank.shield_strength;
            c.tenacity = c.tenacity + rank.tenacity;
            c.resistance_rate = c.resistance_rate + rank.resistance_rate;
            c.combo_rate = c.combo_rate + rank.combo_rate;
            c.reflection_rate = c.reflection_rate + rank.reflection_rate;
            c.mana = c.mana + rank.mana;
            c.mana_regeneration_rate = c.mana_regeneration_rate + rank.mana_regeneration_rate;
            c.damage_to_different_faction_rate = c.damage_to_different_faction_rate + rank.damage_to_different_faction_rate;
            c.resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + rank.resistance_to_different_faction_rate;
            c.damage_to_same_faction_rate = c.damage_to_same_faction_rate + rank.damage_to_same_faction_rate;
            c.resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + rank.resistance_to_same_faction_rate;

            c.power = EvaluatePower.CalculatePower(
            c.health,
            c.physical_attack, c.physical_defense,
            c.magical_attack, c.magical_defense,
            c.chemical_attack, c.chemical_defense,
            c.atomic_attack, c.atomic_defense,
            c.mental_attack, c.mental_defense,
            c.speed,
            c.critical_damage_rate, c.critical_rate,
            c.penetration_rate, c.evasion_rate,
            c.damage_absorption_rate, c.vitality_regeneration_rate,
            c.accuracy_rate, c.lifesteal_rate,
            c.shield_strength, c.tenacity, c.resistance_rate,
            c.combo_rate, c.reflection_rate,
            c.mana, c.mana_regeneration_rate,
            c.damage_to_different_faction_rate, c.resistance_to_different_faction_rate,
            c.damage_to_same_faction_rate, c.resistance_to_same_faction_rate
        );
        }
        return EquipmentsList;
    }
    public Equipments GetNewLevelPower(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = _service.GetEquipmentById(c.id);
        Equipments equipments = new Equipments
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
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            special_health = c.special_health + orginCard.special_health * coefficient,
            special_physical_attack = c.special_physical_attack + orginCard.special_physical_attack * coefficient,
            special_physical_defense = c.special_physical_defense + orginCard.special_physical_defense * coefficient,
            special_magical_attack = c.special_magical_attack + orginCard.special_magical_attack * coefficient,
            special_magical_defense = c.special_magical_defense + orginCard.special_magical_defense * coefficient,
            special_chemical_attack = c.special_chemical_attack + orginCard.special_chemical_attack * coefficient,
            special_chemical_defense = c.special_chemical_defense + orginCard.special_chemical_defense * coefficient,
            special_atomic_attack = c.special_atomic_attack + orginCard.special_atomic_attack * coefficient,
            special_atomic_defense = c.special_atomic_defense + orginCard.special_atomic_defense * coefficient,
            special_mental_attack = c.special_mental_attack + orginCard.special_mental_attack * coefficient,
            special_mental_defense = c.special_mental_defense + orginCard.special_mental_defense * coefficient,
            special_speed = c.special_speed + orginCard.special_speed * coefficient,
        };
        equipments.power = EvaluatePower.CalculatePower(
            equipments.health + equipments.special_health,
            equipments.physical_attack + equipments.special_physical_attack, equipments.physical_defense + equipments.special_physical_defense,
            equipments.magical_attack + equipments.special_magical_attack, equipments.magical_defense + equipments.special_magical_defense,
            equipments.chemical_attack + equipments.special_chemical_attack, equipments.chemical_defense + equipments.special_chemical_defense,
            equipments.atomic_attack + equipments.special_atomic_attack, equipments.atomic_defense + equipments.special_atomic_defense,
            equipments.mental_attack + equipments.mental_attack, equipments.mental_defense + equipments.mental_defense,
            equipments.speed,
            equipments.critical_damage_rate, equipments.critical_rate,
            equipments.penetration_rate, equipments.evasion_rate,
            equipments.damage_absorption_rate, equipments.vitality_regeneration_rate,
            equipments.accuracy_rate, equipments.lifesteal_rate,
            equipments.shield_strength, equipments.tenacity, equipments.resistance_rate,
            equipments.combo_rate, equipments.reflection_rate,
            equipments.mana, equipments.mana_regeneration_rate,
            equipments.damage_to_different_faction_rate, equipments.resistance_to_different_faction_rate,
            equipments.damage_to_same_faction_rate, equipments.resistance_to_same_faction_rate
        );
        return equipments;
    }
    public Equipments GetNewBreakthroughPower(Equipments c, double coefficient)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        Equipments orginCard = _service.GetEquipmentById(c.id);
        Equipments equipments = new Equipments
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
            penetration_rate = c.penetration_rate + orginCard.penetration_rate * coefficient,
            evasion_rate = c.evasion_rate + orginCard.evasion_rate * coefficient,
            damage_absorption_rate = c.damage_absorption_rate + orginCard.damage_absorption_rate * coefficient,
            vitality_regeneration_rate = c.vitality_regeneration_rate + orginCard.vitality_regeneration_rate * coefficient,
            accuracy_rate = c.accuracy_rate + orginCard.accuracy_rate * coefficient,
            lifesteal_rate = c.lifesteal_rate + orginCard.lifesteal_rate * coefficient,
            shield_strength = c.shield_strength + orginCard.shield_strength * coefficient,
            tenacity = c.tenacity + orginCard.tenacity * coefficient,
            resistance_rate = c.resistance_rate + orginCard.resistance_rate * coefficient,
            combo_rate = c.combo_rate + orginCard.combo_rate * coefficient,
            reflection_rate = c.reflection_rate + orginCard.reflection_rate * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient,
            mana_regeneration_rate = c.mana_regeneration_rate + orginCard.mana_regeneration_rate * coefficient,
            damage_to_different_faction_rate = c.damage_to_different_faction_rate + orginCard.damage_to_different_faction_rate * coefficient,
            resistance_to_different_faction_rate = c.resistance_to_different_faction_rate + orginCard.resistance_to_different_faction_rate * coefficient,
            damage_to_same_faction_rate = c.damage_to_same_faction_rate + orginCard.damage_to_same_faction_rate * coefficient,
            resistance_to_same_faction_rate = c.resistance_to_same_faction_rate + orginCard.resistance_to_same_faction_rate * coefficient,
            special_health = c.special_health + orginCard.special_health * coefficient,
            special_physical_attack = c.special_physical_attack + orginCard.special_physical_attack * coefficient,
            special_physical_defense = c.special_physical_defense + orginCard.special_physical_defense * coefficient,
            special_magical_attack = c.special_magical_attack + orginCard.special_magical_attack * coefficient,
            special_magical_defense = c.special_magical_defense + orginCard.special_magical_defense * coefficient,
            special_chemical_attack = c.special_chemical_attack + orginCard.special_chemical_attack * coefficient,
            special_chemical_defense = c.special_chemical_defense + orginCard.special_chemical_defense * coefficient,
            special_atomic_attack = c.special_atomic_attack + orginCard.special_atomic_attack * coefficient,
            special_atomic_defense = c.special_atomic_defense + orginCard.special_atomic_defense * coefficient,
            special_mental_attack = c.special_mental_attack + orginCard.special_mental_attack * coefficient,
            special_mental_defense = c.special_mental_defense + orginCard.special_mental_defense * coefficient,
            special_speed = c.special_speed + orginCard.special_speed * coefficient,
        };
        equipments.power = EvaluatePower.CalculatePower(
            equipments.health + equipments.special_health,
            equipments.physical_attack + equipments.special_physical_attack, equipments.physical_defense + equipments.special_physical_defense,
            equipments.magical_attack + equipments.special_magical_attack, equipments.magical_defense + equipments.special_magical_defense,
            equipments.chemical_attack + equipments.special_chemical_attack, equipments.chemical_defense + equipments.special_chemical_defense,
            equipments.atomic_attack + equipments.special_atomic_attack, equipments.atomic_defense + equipments.special_atomic_defense,
            equipments.mental_attack + equipments.mental_attack, equipments.mental_defense + equipments.mental_defense,
            equipments.speed,
            equipments.critical_damage_rate, equipments.critical_rate,
            equipments.penetration_rate, equipments.evasion_rate,
            equipments.damage_absorption_rate, equipments.vitality_regeneration_rate,
            equipments.accuracy_rate, equipments.lifesteal_rate,
            equipments.shield_strength, equipments.tenacity, equipments.resistance_rate,
            equipments.combo_rate, equipments.reflection_rate,
            equipments.mana, equipments.mana_regeneration_rate,
            equipments.damage_to_different_faction_rate, equipments.resistance_to_different_faction_rate,
            equipments.damage_to_same_faction_rate, equipments.resistance_to_same_faction_rate
        );
        return equipments;
    }

    public List<Equipments> GetUserEquipments(string user_id, string type, int pageSize, int offset)
    {
        List<Equipments> list = _userEquipmentsRepository.GetUserEquipments(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserEquipmentsCount(string user_id, string type)
    {
        return _userEquipmentsRepository.GetUserEquipmentsCount(user_id, type);
    }

    public Equipments GetUserEquipmentsById(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetUserEquipmentsById(user_id, Id);
    }

    public bool BuyEquipment(string Id)
    {
        IEquipmentsRepository _repository = new EquipmentsRepository();
        EquipmentsService _service = new EquipmentsService(_repository);
        return _userEquipmentsRepository.BuyEquipment(Id, _service.GetEquipmentById(Id));
    }

    public bool UpdateEquipmentsLevel(Equipments equipments, int cardLevel)
    {
        return _userEquipmentsRepository.UpdateEquipmentsLevel(equipments, cardLevel);
    }

    public bool UpdateEquipmentsBreakthrough(Equipments equipments, int star, int quantity)
    {
        return _userEquipmentsRepository.UpdateEquipmentsBreakthrough(equipments, star, quantity);
    }

    public void UpdateUserCurrency(string Id)
    {
        _userEquipmentsRepository.UpdateUserCurrency(Id);
    }

    public void InsertCardHeroesEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardHeroesEquipments(Id, equipments, position);
    }

    public void InsertCardCaptainsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardCaptainsEquipments(Id, equipments, position);
    }

    public void InsertCardColonelsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardColonelsEquipments(Id, equipments, position);
    }

    public void InsertCardGeneralsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardGeneralsEquipments(Id, equipments, position);
    }

    public void InsertCardAdmiralsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardAdmiralsEquipments(Id, equipments, position);
    }

    public void InsertCardMonstersEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardMonstersEquipments(Id, equipments, position);
    }

    public void InsertCardMilitaryEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardMilitaryEquipments(Id, equipments, position);
    }

    public void InsertCardSpellEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertCardSpellEquipments(Id, equipments, position);
    }

    public void InsertBooksEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertBooksEquipments(Id, equipments, position);
    }

    public void InsertPetsEquipments(string Id, Equipments equipments, int position)
    {
        _userEquipmentsRepository.InsertPetsEquipments(Id, equipments, position);
    }

    public List<Equipments> GetCardHeroesEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardHeroesEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardCaptainsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardCaptainsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardColonelsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardColonelsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardGeneralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardGeneralsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardAdmiralsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardAdmiralsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardMonstersEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardMonstersEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardMilitaryEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardMilitaryEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetCardSpellEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetCardSpellEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetBooksEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetBooksEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetPetsEquipments(string user_id, string card_id, string type)
    {
        List<Equipments> list = _userEquipmentsRepository.GetPetsEquipments(user_id, card_id, type);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardHeroesEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardHeroesEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardCaptainsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardCaptainsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardColonelsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardColonelsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardGeneralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardGeneralsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardAdmiralsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardAdmiralsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardMonstersEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardMonstersEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardMilitaryEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardMilitaryEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllCardSpellEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllCardSpellEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllBooksEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllBooksEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public List<Equipments> GetAllPetsEquipments(string user_id, string type, int limit, int offset, string status)
    {
        List<Equipments> list = _userEquipmentsRepository.GetAllPetsEquipments(user_id, type, limit, offset, status);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public Equipments GetAllEquipmentsByCardHeoresId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardHeoresId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardCaptainsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardCaptainsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardColonelsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardColonelsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardGeneralsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardGeneralsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardAdmiralsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardAdmiralsId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardMonstersId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardMonstersId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardMilitaryId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardMilitaryId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByCardSpellId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByCardSpellId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByBooksId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByBooksId(user_id, Id);
    }

    public Equipments GetAllEquipmentsByPetsId(string user_id, string Id)
    {
        return _userEquipmentsRepository.GetAllEquipmentsByPetsId(user_id, Id);
    }
}
