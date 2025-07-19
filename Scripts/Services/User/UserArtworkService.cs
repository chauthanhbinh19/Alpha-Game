
using System.Collections.Generic;

public class UserArtworkService : IUserArtworkService
{
    private IUserArtworkRepository _userArtworkRepository;

    public UserArtworkService(IUserArtworkRepository userArtworkRepository)
    {
        _userArtworkRepository = userArtworkRepository;
    }

    public static UserArtworkService Create()
    {
        return new UserArtworkService(new UserArtworkRepository());
    }

    public Artwork GetNewLevelPower(Artwork c, double coefficient)
    {
        IArtworkRepository _repository = new ArtworkRepository();
        ArtworkService _service = new ArtworkService(_repository);
        Artwork orginCard = _service.GetArtworkById(c.id);
        Artwork Artwork = new Artwork
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
        Artwork.power = EvaluatePower.CalculatePower(
            Artwork.health,
            Artwork.physical_attack, Artwork.physical_defense,
            Artwork.magical_attack, Artwork.magical_defense,
            Artwork.chemical_attack, Artwork.chemical_defense,
            Artwork.atomic_attack, Artwork.atomic_defense,
            Artwork.mental_attack, Artwork.mental_defense,
            Artwork.speed,
            Artwork.critical_damage_rate, Artwork.critical_rate, Artwork.critical_resistance_rate, Artwork.ignore_critical_rate,
            Artwork.penetration_rate, Artwork.penetration_resistance_rate, Artwork.evasion_rate,
            Artwork.damage_absorption_rate, Artwork.ignore_damage_absorption_rate, Artwork.absorbed_damage_rate,
            Artwork.vitality_regeneration_rate, Artwork.vitality_regeneration_resistance_rate,
            Artwork.accuracy_rate, Artwork.lifesteal_rate,
            Artwork.shield_strength, Artwork.tenacity, Artwork.resistance_rate,
            Artwork.combo_rate, Artwork.ignore_combo_rate, Artwork.combo_damage_rate, Artwork.combo_resistance_rate,
            Artwork.stun_rate, Artwork.ignore_stun_rate,
            Artwork.reflection_rate, Artwork.ignore_reflection_rate, Artwork.reflection_damage_rate, Artwork.reflection_resistance_rate,
            Artwork.mana, Artwork.mana_regeneration_rate,
            Artwork.damage_to_different_faction_rate, Artwork.resistance_to_different_faction_rate,
            Artwork.damage_to_same_faction_rate, Artwork.resistance_to_same_faction_rate,
            Artwork.normal_damage_rate, Artwork.normal_resistance_rate,
            Artwork.skill_damage_rate, Artwork.skill_resistance_rate
        );
        return Artwork;
    }
    public Artwork GetNewBreakthroughPower(Artwork c, double coefficient)
    {
        IArtworkRepository _repository = new ArtworkRepository();
        ArtworkService _service = new ArtworkService(_repository);
        Artwork orginCard = _service.GetArtworkById(c.id);
        Artwork Artwork = new Artwork
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
        Artwork.power = EvaluatePower.CalculatePower(
            Artwork.health,
            Artwork.physical_attack, Artwork.physical_defense,
            Artwork.magical_attack, Artwork.magical_defense,
            Artwork.chemical_attack, Artwork.chemical_defense,
            Artwork.atomic_attack, Artwork.atomic_defense,
            Artwork.mental_attack, Artwork.mental_defense,
            Artwork.speed,
            Artwork.critical_damage_rate, Artwork.critical_rate, Artwork.critical_resistance_rate, Artwork.ignore_critical_rate,
            Artwork.penetration_rate, Artwork.penetration_resistance_rate, Artwork.evasion_rate,
            Artwork.damage_absorption_rate, Artwork.ignore_damage_absorption_rate, Artwork.absorbed_damage_rate,
            Artwork.vitality_regeneration_rate, Artwork.vitality_regeneration_resistance_rate,
            Artwork.accuracy_rate, Artwork.lifesteal_rate,
            Artwork.shield_strength, Artwork.tenacity, Artwork.resistance_rate,
            Artwork.combo_rate, Artwork.ignore_combo_rate, Artwork.combo_damage_rate, Artwork.combo_resistance_rate,
            Artwork.stun_rate, Artwork.ignore_stun_rate,
            Artwork.reflection_rate, Artwork.ignore_reflection_rate, Artwork.reflection_damage_rate, Artwork.reflection_resistance_rate,
            Artwork.mana, Artwork.mana_regeneration_rate,
            Artwork.damage_to_different_faction_rate, Artwork.resistance_to_different_faction_rate,
            Artwork.damage_to_same_faction_rate, Artwork.resistance_to_same_faction_rate,
            Artwork.normal_damage_rate, Artwork.normal_resistance_rate,
            Artwork.skill_damage_rate, Artwork.skill_resistance_rate
        );
        return Artwork;
    }

    public List<Artwork> GetUserArtwork(string user_id, string type, int pageSize, int offset)
    {
        List<Artwork> list = _userArtworkRepository.GetUserArtwork(user_id, type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetUserArtworkCount(string user_id, string type)
    {
        return _userArtworkRepository.GetUserArtworkCount(user_id, type);
    }

    public bool InsertUserArtwork(Artwork Artwork)
    {
        return _userArtworkRepository.InsertUserArtwork(Artwork);
    }

    public bool UpdateArtworkLevel(Artwork Artwork, int cardLevel)
    {
        return _userArtworkRepository.UpdateArtworkLevel(Artwork, cardLevel);
    }

    public bool UpdateArtworkBreakthrough(Artwork Artwork, int star, int quantity)
    {
        return _userArtworkRepository.UpdateArtworkBreakthrough(Artwork, star, quantity);
    }

    public Artwork GetUserArtworkById(string user_id, string Id)
    {
        return _userArtworkRepository.GetUserArtworkById(user_id, Id);
    }

    public Artwork SumPowerUserArtwork()
    {
        return _userArtworkRepository.SumPowerUserArtwork();
    }
}
