using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenCover.Framework.Model;
using UnityEditor.PackageManager;
public static class AppConstants
{
    public static class Setting
    {
        public const string RESOLUTION = "Graphic.Resolution";
        public const string TEXTURE = "Graphic.Texture";
        public const string DAMAGE_FLYTEXT = "Graphic.DamageFlytext";
        public const string IN_GAME_CINEMATIC = "Graphic.InGameCinematic";
        public const string MUSIC = "Sound.Music";
        public const string SFX = "Sound.SFX";
        public const string VOICE = "Sound.Voice";
        public const string LANGUAGE = "Language";
    }
    public static class StatFields
    {
        public const string ID = "Id";
        public const string NAME = "Name";
        public const string RARE = "Rare";
        public const string TYPE = "Type";
        public const string STAR = "Star";
        public const string LEVEL = "Level";
        public const string IMAGE = "Image";
        public const string QUANTITY = "Quantity";
        public const string BLOCK = "Block";
        public const string STATUS = "Status";
        public const string EXPERIMENT = "Experiment";
        public const string POWER = "Power";
        public const string HEALTH = "Health";
        public const string PHYSICAL_ATTACK = "PhysicalAttack";
        public const string PHYSICAL_DEFENSE = "PhysicalDefense";
        public const string MAGICAL_ATTACK = "MagicalAttack";
        public const string MAGICAL_DEFENSE = "MagicalDefense";
        public const string CHEMICAL_ATTACK = "ChemicalAttack";
        public const string CHEMICAL_DEFENSE = "ChemicalDefense";
        public const string ATOMIC_ATTACK = "AtomicAttack";
        public const string ATOMIC_DEFENSE = "AtomicDefense";
        public const string MENTAL_ATTACK = "MentalAttack";
        public const string MENTAL_DEFENSE = "MentalDefense";
        public const string SPEED = "Speed";
        public const string CRITICAL_DAMAGE_RATE = "CriticalDamageRate";
        public const string CRITICAL_RATE = "CriticalRate";
        public const string CRITICAL_RESISTANCE_RATE = "CriticalResistanceRate";
        public const string IGNORE_CRITICAL_RATE = "IgnoreCriticalRate";
        public const string PENETRATION_RATE = "PenetrationRate";
        public const string PENETRATION_RESISTANCE_RATE = "PenetrationResistanceRate";
        public const string EVASION_RATE = "EvasionRate";
        public const string DAMAGE_ABSORPTION_RATE = "DamageAbsorptionRate";
        public const string IGNORE_DAMAGE_ABSORPTION_RATE = "IgnoreDamageAbsorptionRate";
        public const string ABSORBED_DAMAGE_RATE = "AbsorbedDamageRate";
        public const string VITALITY_REGENERATION_RATE = "VitalityRegenerationRate";
        public const string VITALITY_REGENERATION_RESISTANCE_RATE = "VitalityRegenerationResistanceRate";
        public const string ACCURACY_RATE = "AccuracyRate";
        public const string LIFE_STEAL_RATE = "LifestealRate";
        public const string MANA = "Mana";
        public const string MANA_REGENERATION_RATE = "ManaRegenerationRate";
        public const string SHIELD_STRENGTH = "ShieldStrength";
        public const string TENACITY = "Tenacity";
        public const string RESISTANCE_RATE = "ResistanceRate";
        public const string COMBO_RATE = "ComboRate";
        public const string IGNORE_COMBO_RATE = "IgnoreComboRate";
        public const string COMBO_DAMAGE_RATE = "ComboDamageRate";
        public const string COMBO_RESISTANCE_RATE = "ComboResistanceRate";
        public const string STUN_RATE = "StunRate";
        public const string IGNORE_STUN_RATE = "IgnoreStunRate";
        public const string REFLECTION_RATE = "ReflectionRate";
        public const string IGNORE_REFLECTION_RATE = "IgnoreReflectionRate";
        public const string REFLECTION_DAMAGE_RATE = "ReflectionDamageRate";
        public const string REFLECTION_RESISTANCE_RATE = "ReflectionResistanceRate";
        public const string DAMAGE_TO_DIFFERENT_FACTION_RATE = "DamageToDifferentFactionRate";
        public const string RESISTANCE_TO_DIFFERENT_FACTION_RATE = "ResistanceToDifferentFactionRate";
        public const string DAMAGE_TO_SAME_FACTION_RATE = "DamageToSameFactionRate";
        public const string RESISTANCE_TO_SAME_FACTION_RATE = "ResistanceToSameFactionRate";
        public const string NORMAL_DAMAGE_RATE = "NormalDamageRate";
        public const string NORMAL_RESISTANCE_RATE = "NormalResistanceRate";
        public const string SKILL_DAMAGE_RATE = "SkillDamageRate";
        public const string SKILL_RESISTANCE_RATE = "SkillResistanceRate";
        public const string DESCRIPTION = "Description";
    }
    public static class Target
    {
        public const string SELF = "self";
        public const string ENEMY = "enemy";
        public const string ALLY = "ally";
    }
    public static class EffectProperty
    {
        public const string HEALTH = "HEALTH"; // Lượng máu hiện tại hoặc tối đa của nhân vật
        public const string PHYSICAL_ATTACK = "PHYSICAL_ATTACK"; // Sức tấn công vật lý
        public const string PHYSICAL_DEFENSE = "PHYSICAL_DEFENSE"; // Khả năng phòng thủ vật lý
        public const string MAGICAL_ATTACK = "MAGICAL_ATTACK"; // Sức tấn công phép thuật
        public const string MAGICAL_DEFENSE = "MAGICAL_DEFENSE"; // Khả năng phòng thủ phép thuật
        public const string CHEMICAL_ATTACK = "CHEMICAL_ATTACK"; // Sức tấn công hóa học
        public const string CHEMICAL_DEFENSE = "CHEMICAL_DEFENSE"; // Khả năng phòng thủ hóa học
        public const string ATOMIC_ATTACK = "ATOMIC_ATTACK"; // Sức tấn công nguyên tử
        public const string ATOMIC_DEFENSE = "ATOMIC_DEFENSE"; // Khả năng phòng thủ nguyên tử
        public const string MENTAL_ATTACK = "MENTAL_ATTACK"; // Sức tấn công tinh thần
        public const string MENTAL_DEFENSE = "MENTAL_DEFENSE"; // Khả năng phòng thủ tinh thần
        public const string SPEED = "SPEED"; // Tốc độ hành động hoặc di chuyển
        public const string CRITICAL_DAMAGE_RATE = "CRITICAL_DAMAGE_RATE"; // Tỷ lệ sát thương bạo kích
        public const string CRITICAL_RATE = "CRITICAL_RATE"; // Tỷ lệ ra đòn bạo kích
        public const string CRITICAL_RESISTANCE_RATE = "CRITICAL_RESISTANCE_RATE"; // Tỷ lệ kháng bạo kích
        public const string IGNORE_CRITICAL_RATE = "IGNORE_CRITICAL_RATE"; // Tỷ lệ bỏ qua kháng bạo kích
        public const string PENETRATION_RATE = "PENETRATION_RATE"; // Tỷ lệ xuyên giáp hoặc xuyên kháng
        public const string PENETRATION_RESISTANCE_RATE = "PENETRATION_RESISTANCE_RATE"; // Tỷ lệ kháng xuyên giáp hoặc xuyên kháng
        public const string EVASION_RATE = "EVASION_RATE"; // Tỷ lệ né tránh
        public const string DAMAGE_ABSORPTION_RATE = "DAMAGE_ABSORPTION_RATE"; // Tỷ lệ hấp thụ sát thương
        public const string IGNORE_DAMAGE_ABSORPTION_RATE = "IGNORE_DAMAGE_ABSORPTION_RATE"; // Tỷ lệ bỏ qua hấp thụ sát thương
        public const string ABSORBED_DAMAGE_RATE = "ABSORBED_DAMAGE_RATE"; // Tỷ lệ sát thương đã hấp thụ
        public const string VITALITY_REGENERATION_RATE = "VITALITY_REGENERATION_RATE"; // Tỷ lệ hồi phục sinh lực
        public const string VITALITY_REGENERATION_RESISTANCE_RATE = "VITALITY_REGENERATION_RESISTANCE_RATE"; // Tỷ lệ kháng hồi phục sinh lực
        public const string ACCURACY_RATE = "ACCURACY_RATE"; // Tỷ lệ chính xác
        public const string LIFESTEAL_RATE = "LIFESTEAL_RATE"; // Tỷ lệ hút máu
        public const string MANA = "MANA"; // Năng lượng sử dụng kỹ năng
        public const string MANA_REGENERATION_RATE = "MANA_REGENERATION_RATE"; // Tỷ lệ hồi năng lượng
        public const string SHIELD_STRENGTH = "SHIELD_STRENGTH"; // Sức mạnh lá chắn
        public const string TENACITY = "TENACITY"; // Chỉ số kiên cường hoặc giảm thời gian hiệu ứng bất lợi
        public const string RESISTANCE_RATE = "RESISTANCE_RATE"; // Tỷ lệ kháng tất cả loại sát thương hoặc hiệu ứng
        public const string COMBO_RATE = "COMBO_RATE"; // Tỷ lệ kích hoạt đòn liên hoàn
        public const string IGNORE_COMBO_RATE = "IGNORE_COMBO_RATE"; // Tỷ lệ bỏ qua khả năng combo của đối thủ
        public const string COMBO_DAMAGE_RATE = "COMBO_DAMAGE_RATE"; // Tỷ lệ sát thương từ đòn combo
        public const string COMBO_RESISTANCE_RATE = "COMBO_RESISTANCE_RATE"; // Tỷ lệ kháng sát thương combo
        public const string STUN_RATE = "STUN_RATE"; // Tỷ lệ gây choáng
        public const string IGNORE_STUN_RATE = "IGNORE_STUN_RATE"; // Tỷ lệ bỏ qua kháng choáng
        public const string REFLECTION_RATE = "REFLECTION_RATE"; // Tỷ lệ phản đòn hoặc phản sát thương
        public const string IGNORE_REFLECTION_RATE = "IGNORE_REFLECTION_RATE"; // Tỷ lệ bỏ qua khả năng phản đòn của đối thủ
        public const string REFLECTION_DAMAGE_RATE = "REFLECTION_DAMAGE_RATE"; // Tỷ lệ sát thương phản lại
        public const string REFLECTION_RESISTANCE_RATE = "REFLECTION_RESISTANCE_RATE"; // Tỷ lệ kháng sát thương phản lại
        public const string DAMAGE_TO_DIFFERENT_FACTION_RATE = "DAMAGE_TO_DIFFERENT_FACTION_RATE"; // Tỷ lệ sát thương gây lên phe khác
        public const string RESISTANCE_TO_DIFFERENT_FACTION_RATE = "RESISTANCE_TO_DIFFERENT_FACTION_RATE"; // Tỷ lệ kháng sát thương từ phe khác
        public const string DAMAGE_TO_SAME_FACTION_RATE = "DAMAGE_TO_SAME_FACTION_RATE"; // Tỷ lệ sát thương gây lên cùng phe
        public const string RESISTANCE_TO_SAME_FACTION_RATE = "RESISTANCE_TO_SAME_FACTION_RATE"; // Tỷ lệ kháng sát thương từ cùng phe
        public const string NORMAL_DAMAGE_RATE = "NORMAL_DAMAGE_RATE"; // Tỷ lệ sát thương từ đòn đánh thường
        public const string NORMAL_RESISTANCE_RATE = "NORMAL_RESISTANCE_RATE"; // Tỷ lệ kháng sát thương từ đòn đánh thường
        public const string SKILL_DAMAGE_RATE = "SKILL_DAMAGE_RATE"; // Tỷ lệ sát thương từ kỹ năng
        public const string SKILL_RESISTANCE_RATE = "SKILL_RESISTANCE_RATE"; // Tỷ lệ kháng sát thương từ kỹ năng
        public const string CONTROL_STATE = "CONTROL_STATE"; // Trạng thái khống chế hành động của nhân vật
    }
    public static class EffectAction
    {
        public const string INCREASE = "INCREASE";
        public const string DECREASE = "DECREASE";
        public const string RESTORE = "RESTORE";
        public const string LOCK = "LOCK";
        public const string BREAK = "BREAK";
        public const string CONVERT = "CONVERT";
        public const string PREVENT = "PREVENT";
        public const string SET = "SET";
        public const string IMMUNITY = "IMMUNITY";
        public const string STEAL = "STEAL";
        public const string COPY = "COPY";
        public const string TRANSFER = "TRANSFER";
        public const string REVERSE = "REVERSE";
        public const string REFLECT = "REFLECT";
        public const string APPLY = "APPLY";
        public const string REMOVE = "REMOVE";
        public const string EXTEND = "EXTEND";
        public const string SHORTEN = "SHORTEN";
        public const string LIMIT_ACTION = "LIMIT_ACTION";
        public const string DAMAGE = "DAMAGE";
        public const string HEAL = "HEAL";
    }
    public static class Rare
    {
        public const string NONE = "None";
        public const string ALL = "All";
        public const string SR = "SR";
        public const string SSR = "SSR";
        public const string UR = "UR";
        public const string LG = "LG";
        public const string LGPlus = "LGPlus";
        public const string MR = "MR";
        public const string MRPlus = "MRPlus";
        public const string SLG = "SLG";
        public const string SLGPlus = "SLGPlus";
        public const string SP = "SP";
        public const string SPPlus = "SPPlus";
    }
    public static class Type
    {
        public const string ALL = "All";
        public static class CardHeroes
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardHeroTypes = new()
        {
            CardHeroes.ADAMAS,
            CardHeroes.AVIAN,
            CardHeroes.BARBARIAN,
            CardHeroes.CYLLORAN,
            CardHeroes.DREIZEN,
            CardHeroes.ETRIGON,
            CardHeroes.FIRIMIR,
            CardHeroes.GENNESIS,
            CardHeroes.HECARUS,
            CardHeroes.ILLONIMA,
            CardHeroes.JAGUAR,
            CardHeroes.KRYPTONIAN,
            CardHeroes.LAMANIA,
            CardHeroes.MARVERICK,
            CardHeroes.NEMESIS,
            CardHeroes.ONYX,
            CardHeroes.PALLADIAN,
            CardHeroes.QUASAR,
            CardHeroes.RIVERVEN,
            CardHeroes.STARROIAN,
            CardHeroes.TERAC,
            CardHeroes.URIUS,
            CardHeroes.VRIL,
            CardHeroes.WYVERN,
            CardHeroes.XANTHERA,
            CardHeroes.YORNATH,
            CardHeroes.ZERATH
        };
        public static class CardCaptains
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardCaptainTypes = new()
        {
            CardCaptains.ADAMAS,
            CardCaptains.AVIAN,
            CardCaptains.BARBARIAN,
            CardCaptains.CYLLORAN,
            CardCaptains.DREIZEN,
            CardCaptains.ETRIGON,
            CardCaptains.FIRIMIR,
            CardCaptains.GENNESIS,
            CardCaptains.HECARUS,
            CardCaptains.ILLONIMA,
            CardCaptains.JAGUAR,
            CardCaptains.KRYPTONIAN,
            CardCaptains.LAMANIA,
            CardCaptains.MARVERICK,
            CardCaptains.NEMESIS,
            CardCaptains.ONYX,
            CardCaptains.PALLADIAN,
            CardCaptains.QUASAR,
            CardCaptains.RIVERVEN,
            CardCaptains.STARROIAN,
            CardCaptains.TERAC,
            CardCaptains.URIUS,
            CardCaptains.VRIL,
            CardCaptains.WYVERN,
            CardCaptains.XANTHERA,
            CardCaptains.YORNATH,
            CardCaptains.ZERATH
        };
        public static class CardColonels
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardColonelTypes = new()
        {
            CardColonels.ADAMAS,
            CardColonels.AVIAN,
            CardColonels.BARBARIAN,
            CardColonels.CYLLORAN,
            CardColonels.DREIZEN,
            CardColonels.ETRIGON,
            CardColonels.FIRIMIR,
            CardColonels.GENNESIS,
            CardColonels.HECARUS,
            CardColonels.ILLONIMA,
            CardColonels.JAGUAR,
            CardColonels.KRYPTONIAN,
            CardColonels.LAMANIA,
            CardColonels.MARVERICK,
            CardColonels.NEMESIS,
            CardColonels.ONYX,
            CardColonels.PALLADIAN,
            CardColonels.QUASAR,
            CardColonels.RIVERVEN,
            CardColonels.STARROIAN,
            CardColonels.TERAC,
            CardColonels.URIUS,
            CardColonels.VRIL,
            CardColonels.WYVERN,
            CardColonels.XANTHERA,
            CardColonels.YORNATH,
            CardColonels.ZERATH
        };
        public static class CardGenerals
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardGeneralTypes = new()
        {
            CardGenerals.ADAMAS,
            CardGenerals.AVIAN,
            CardGenerals.BARBARIAN,
            CardGenerals.CYLLORAN,
            CardGenerals.DREIZEN,
            CardGenerals.ETRIGON,
            CardGenerals.FIRIMIR,
            CardGenerals.GENNESIS,
            CardGenerals.HECARUS,
            CardGenerals.ILLONIMA,
            CardGenerals.JAGUAR,
            CardGenerals.KRYPTONIAN,
            CardGenerals.LAMANIA,
            CardGenerals.MARVERICK,
            CardGenerals.NEMESIS,
            CardGenerals.ONYX,
            CardGenerals.PALLADIAN,
            CardGenerals.QUASAR,
            CardGenerals.RIVERVEN,
            CardGenerals.STARROIAN,
            CardGenerals.TERAC,
            CardGenerals.URIUS,
            CardGenerals.VRIL,
            CardGenerals.WYVERN,
            CardGenerals.XANTHERA,
            CardGenerals.YORNATH,
            CardGenerals.ZERATH
        };
        public static class CardAdmirals
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardAdmiralTypes = new()
        {
            CardAdmirals.ADAMAS,
            CardAdmirals.AVIAN,
            CardAdmirals.BARBARIAN,
            CardAdmirals.CYLLORAN,
            CardAdmirals.DREIZEN,
            CardAdmirals.ETRIGON,
            CardAdmirals.FIRIMIR,
            CardAdmirals.GENNESIS,
            CardAdmirals.HECARUS,
            CardAdmirals.ILLONIMA,
            CardAdmirals.JAGUAR,
            CardAdmirals.KRYPTONIAN,
            CardAdmirals.LAMANIA,
            CardAdmirals.MARVERICK,
            CardAdmirals.NEMESIS,
            CardAdmirals.ONYX,
            CardAdmirals.PALLADIAN,
            CardAdmirals.QUASAR,
            CardAdmirals.RIVERVEN,
            CardAdmirals.STARROIAN,
            CardAdmirals.TERAC,
            CardAdmirals.URIUS,
            CardAdmirals.VRIL,
            CardAdmirals.WYVERN,
            CardAdmirals.XANTHERA,
            CardAdmirals.YORNATH,
            CardAdmirals.ZERATH
        };
        public static class CardMilitaries
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardMilitaryTypes = new()
        {
            CardMilitaries.ADAMAS,
            CardMilitaries.AVIAN,
            CardMilitaries.BARBARIAN,
            CardMilitaries.CYLLORAN,
            CardMilitaries.DREIZEN,
            CardMilitaries.ETRIGON,
            CardMilitaries.FIRIMIR,
            CardMilitaries.GENNESIS,
            CardMilitaries.HECARUS,
            CardMilitaries.ILLONIMA,
            CardMilitaries.JAGUAR,
            CardMilitaries.KRYPTONIAN,
            CardMilitaries.LAMANIA,
            CardMilitaries.MARVERICK,
            CardMilitaries.NEMESIS,
            CardMilitaries.ONYX,
            CardMilitaries.PALLADIAN,
            CardMilitaries.QUASAR,
            CardMilitaries.RIVERVEN,
            CardMilitaries.STARROIAN,
            CardMilitaries.TERAC,
            CardMilitaries.URIUS,
            CardMilitaries.VRIL,
            CardMilitaries.WYVERN,
            CardMilitaries.XANTHERA,
            CardMilitaries.YORNATH,
            CardMilitaries.ZERATH
        };
        public static class CardMonsters
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardMonsterTypes = new()
        {
            CardMonsters.ADAMAS,
            CardMonsters.AVIAN,
            CardMonsters.BARBARIAN,
            CardMonsters.CYLLORAN,
            CardMonsters.DREIZEN,
            CardMonsters.ETRIGON,
            CardMonsters.FIRIMIR,
            CardMonsters.GENNESIS,
            CardMonsters.HECARUS,
            CardMonsters.ILLONIMA,
            CardMonsters.JAGUAR,
            CardMonsters.KRYPTONIAN,
            CardMonsters.LAMANIA,
            CardMonsters.MARVERICK,
            CardMonsters.NEMESIS,
            CardMonsters.ONYX,
            CardMonsters.PALLADIAN,
            CardMonsters.QUASAR,
            CardMonsters.RIVERVEN,
            CardMonsters.STARROIAN,
            CardMonsters.TERAC,
            CardMonsters.URIUS,
            CardMonsters.VRIL,
            CardMonsters.WYVERN,
            CardMonsters.XANTHERA,
            CardMonsters.YORNATH,
            CardMonsters.ZERATH
        };
        public static class CardSpells
        {
            public const string ATTACK = "Attack";
            public const string COMBAT = "Combat";
            public const string DECREASE = "Decrease";
            public const string DEFENCE = "Defence";
            public const string ENVIRONMENT = "Environment";
            public const string EQUIPMENT = "Equipment";
            public const string FUSION = "Fusion";
            public const string INCREASE = "Increase";
            public const string REBORN = "Reborn";
            public const string SUMMON = "Summon";
            public const string TEAM = "Team";
            public const string ULTIMATE_DECREASE = "Ultimate_Decrease";
            public const string ULTIMATE_ENVIRONMENT = "Ultimate_Environment";
            public const string ULTIMATE_INCREASE = "Ultimate_Increase";
        }
        public static readonly List<string> AllCardSpellTypes = new()
        {
            CardHeroes.ADAMAS,
            CardHeroes.AVIAN,
            CardHeroes.BARBARIAN,
            CardHeroes.CYLLORAN,
            CardHeroes.DREIZEN,
            CardHeroes.ETRIGON,
            CardHeroes.FIRIMIR,
            CardHeroes.GENNESIS,
            CardHeroes.HECARUS,
            CardHeroes.ILLONIMA,
            CardHeroes.JAGUAR,
            CardHeroes.KRYPTONIAN,
            CardHeroes.LAMANIA,
            CardHeroes.MARVERICK,
            CardHeroes.NEMESIS,
            CardHeroes.ONYX,
            CardHeroes.PALLADIAN,
            CardHeroes.QUASAR,
            CardHeroes.RIVERVEN,
            CardHeroes.STARROIAN,
            CardHeroes.TERAC,
            CardHeroes.URIUS,
            CardHeroes.VRIL,
            CardHeroes.WYVERN,
            CardHeroes.XANTHERA,
            CardHeroes.YORNATH,
            CardHeroes.ZERATH
        };
        public static class CardSoldiers
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCardSoldierTypes = new()
        {
            CardSoldiers.ADAMAS,
            CardSoldiers.AVIAN,
            CardSoldiers.BARBARIAN,
            CardSoldiers.CYLLORAN,
            CardSoldiers.DREIZEN,
            CardSoldiers.ETRIGON,
            CardSoldiers.FIRIMIR,
            CardSoldiers.GENNESIS,
            CardSoldiers.HECARUS,
            CardSoldiers.ILLONIMA,
            CardSoldiers.JAGUAR,
            CardSoldiers.KRYPTONIAN,
            CardSoldiers.LAMANIA,
            CardSoldiers.MARVERICK,
            CardSoldiers.NEMESIS,
            CardSoldiers.ONYX,
            CardSoldiers.PALLADIAN,
            CardSoldiers.QUASAR,
            CardSoldiers.RIVERVEN,
            CardSoldiers.STARROIAN,
            CardSoldiers.TERAC,
            CardSoldiers.URIUS,
            CardSoldiers.VRIL,
            CardSoldiers.WYVERN,
            CardSoldiers.XANTHERA,
            CardSoldiers.YORNATH,
            CardSoldiers.ZERATH
        };
        public static class Books
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllBookTypes = new()
        {
            Books.ADAMAS,
            Books.AVIAN,
            Books.BARBARIAN,
            Books.CYLLORAN,
            Books.DREIZEN,
            Books.ETRIGON,
            Books.FIRIMIR,
            Books.GENNESIS,
            Books.HECARUS,
            Books.ILLONIMA,
            Books.JAGUAR,
            Books.KRYPTONIAN,
            Books.LAMANIA,
            Books.MARVERICK,
            Books.NEMESIS,
            Books.ONYX,
            Books.PALLADIAN,
            Books.QUASAR,
            Books.RIVERVEN,
            Books.STARROIAN,
            Books.TERAC,
            Books.URIUS,
            Books.VRIL,
            Books.WYVERN,
            Books.XANTHERA,
            Books.YORNATH,
            Books.ZERATH
        };
        public static class CollaborationEquipments
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllCollaborationEquipmentTypes = new()
        {
            CollaborationEquipments.ADAMAS,
            CollaborationEquipments.AVIAN,
            CollaborationEquipments.BARBARIAN,
            CollaborationEquipments.CYLLORAN,
            CollaborationEquipments.DREIZEN,
            CollaborationEquipments.ETRIGON,
            CollaborationEquipments.FIRIMIR,
            CollaborationEquipments.GENNESIS,
            CollaborationEquipments.HECARUS,
            CollaborationEquipments.ILLONIMA,
            CollaborationEquipments.JAGUAR,
            CollaborationEquipments.KRYPTONIAN,
            CollaborationEquipments.LAMANIA,
            CollaborationEquipments.MARVERICK,
            CollaborationEquipments.NEMESIS,
            CollaborationEquipments.ONYX,
            CollaborationEquipments.PALLADIAN,
            CollaborationEquipments.QUASAR,
            CollaborationEquipments.RIVERVEN,
            CollaborationEquipments.STARROIAN,
            CollaborationEquipments.TERAC,
            CollaborationEquipments.URIUS,
            CollaborationEquipments.VRIL,
            // CollaborationEquipments.WYVERN,
            // CollaborationEquipments.XANTHERA,
            // CollaborationEquipments.YORNATH,
            // CollaborationEquipments.ZERATH
        };
        public static class Equipments
        {
            public const string AMNITUS = "Amnitus_Equipment";
            public const string ANGELIS = "Angelis_Equipment";
            public const string BELLION = "Bellion_Equipment";
            public const string BENZAMIN = "Benzamin_Equipment";
            public const string CELESTIAL = "Celestial_Equipment";
            public const string CEVERUS = "Ceverus_Equipment";
            public const string DELIUS = "Delius_Equipment";
            public const string DOMITIUS = "Domitius_Equipment";
            public const string EVERLYN = "Everlyn_Equipment";
            public const string EXTRA = "Extra_Equipment";
            public const string FAILTUS = "Faltus_Equipment";
            public const string FEALAN = "Fealan_Equipment";
            public const string GAMMA = "Gamma_Equipment";
            public const string GEM = "Gem_Equipment";
            public const string HAGORO = "Hagoro_Equipment";
            public const string HAKALITE = "Hakalite_Equipment";
            public const string IGNIS = "Ignis_Equipment";
            public const string IVITUS = "Ivitus_Equipment";
            public const string JORVAN = "Jorvan_Equipment";
            public const string JULLIAN = "Jullian_Equipment";
            public const string KARIS = "Karis_Equipment";
            public const string KARMUS = "Karmus_Equipment";
            public const string LOTUS = "Lotus_Equipment";
            public const string LUMINIUS = "Luminius_Equipment";
            public const string MACUS = "Macus_Equipment";
            public const string MORGANIS = "Morganis_Equipment";
            public const string NIMIGAZIN = "Nimigazin_Equipment";
            public const string NOVA = "Nova_Equipment";
            public const string OMONITUS = "Omonitus_Equipment";
            public const string OMEGA = "Omega_Equipment";
            public const string PARIUS = "Parius_Equipment";
            public const string PYROS = "Pyros_Equipment";
            public const string QIYANTUS = "Qiyantus_Equipment";
            public const string QUASAR = "Quasar_Equipment";
            public const string RAINBOW = "Rainbow_Equipment";
            public const string REDVENGER = "Redvenger_Equipment";
            public const string SOULS = "Souls_Equipment";
            public const string SYNCROHARON = "Syncroharon_Equipment";
            public const string TARIAN = "Tarian_Equipment";
            public const string TEYRIC = "Teyric_Equipment";
            public const string UNI = "Uni_Equipment";
            public const string ULTRION = "Ultrion_Equipment";
            public const string VARETHION = "Varethion_Equipment";
            public const string VELMIRA = "Velmira_Equipment";
            public const string WENLITHAR = "Wenlithar_Equipment";
            public const string WYRMORA = "Wyrmora_Equipment";
            public const string XALTHEON = "Xaltheon_Equipment";
            public const string XYRALIS = "Xyralis_Equipment";
            public const string YLORAN = "Yloran_Equipment";
            public const string YVARION = "Yvarion_Equipment";
            public const string ZODIAC = "Zodiac_Equipment";
            public const string ZEROX = "Zerox_Equipment";
        }
        public static readonly List<string> AllEquipmentTypes = new()
        {
            Equipments.AMNITUS,
            Equipments.ANGELIS,
            Equipments.BELLION,
            Equipments.BENZAMIN,
            Equipments.CELESTIAL,
            Equipments.CEVERUS,
            Equipments.DELIUS,
            Equipments.DOMITIUS,
            Equipments.EVERLYN,
            Equipments.EXTRA,
            Equipments.FAILTUS,
            Equipments.FEALAN,
            Equipments.GAMMA,
            Equipments.GEM,
            Equipments.HAGORO,
            Equipments.HAKALITE,
            Equipments.IGNIS,
            Equipments.IVITUS,
            Equipments.JORVAN,
            Equipments.JULLIAN,
            Equipments.KARIS,
            Equipments.KARMUS,
            Equipments.LOTUS,
            Equipments.LUMINIUS,
            Equipments.MACUS,
            Equipments.MORGANIS,
            Equipments.NIMIGAZIN,
            Equipments.NOVA,
            Equipments.OMONITUS,
            Equipments.OMEGA,
            Equipments.PARIUS,
            Equipments.PYROS,
            Equipments.QIYANTUS,
            Equipments.QUASAR,
            Equipments.RAINBOW,
            Equipments.REDVENGER,
            Equipments.SOULS,
            Equipments.SYNCROHARON,
            Equipments.TARIAN,
            Equipments.TEYRIC,
            Equipments.UNI,
            Equipments.ULTRION,
            Equipments.VARETHION,
            Equipments.VELMIRA,
            Equipments.WENLITHAR,
            Equipments.WYRMORA,
            Equipments.XALTHEON,
            Equipments.XYRALIS,
            Equipments.YLORAN,
            Equipments.YVARION,
            Equipments.ZODIAC,
            Equipments.ZEROX
        };
        public static class Pets
        {
            public const string EPIC_PET = "Epic_Pet";
            public const string LEGENDARY_DRAGON = "Legendary_Dragon";
            public const string MECHA_ROBOT = "Mecha_Robot";
            public const string NARUTO_BIJUU = "Naruto_Bijuu";
            public const string NARUTO_SUSANOO = "Naruto_Susanoo";
            public const string ONE_PIECE_SHIP = "One_Piece_Ship";
            public const string PRIME_MONSTER = "Prime_Monster";
        }
        public static readonly List<string> AllPetTypes = new()
        {
            Pets.EPIC_PET,
            Pets.LEGENDARY_DRAGON,
            Pets.MECHA_ROBOT,
            Pets.NARUTO_BIJUU,
            Pets.NARUTO_SUSANOO,
            Pets.ONE_PIECE_SHIP,
            Pets.PRIME_MONSTER
        };
        public static class Skills
        {
            public const string ALTERNATIVE = "Alternative";
            public const string CELESTIAL = "Celestial";
            public const string DIVINE = "Divine";
            public const string FORCES = "Forces";
            public const string MAIN = "Main";
            public const string NORMAL = "Normal";
            public const string TEAMWORK = "Teamwork";
            public const string TRANSCENDENCE = "Transcendence";
        }
        public static readonly List<string> AllSkillTypes = new()
        {
            Skills.ALTERNATIVE,
            Skills.CELESTIAL,
            Skills.DIVINE,
            Skills.FORCES,
            Skills.MAIN,
            Skills.NORMAL,
            Skills.TEAMWORK,
            Skills.TRANSCENDENCE
        };
        public static class Symbols
        {
            public const string ANCIENT = "Ancient";
            public const string CYBER = "Cyber";
            public const string ELEMENTAL = "Elemental";
            public const string FUTURISTIC = "Futuristic";
            public const string LEGENDARY = "Legendary";
            public const string MIRACLE = "Miracle";
            public const string MYTHIC = "Mythic";
            public const string PRIME = "Prime";
            public const string ULTIMATE = "Ultimate";
            public const string WORLD = "World";
        }
        public static readonly List<string> AllSymbolTypes = new()
        {
            Symbols.ANCIENT,
            Symbols.CYBER,
            Symbols.ELEMENTAL,
            Symbols.FUTURISTIC,
            Symbols.LEGENDARY,
            Symbols.MIRACLE,
            Symbols.MYTHIC,
            Symbols.PRIME,
            Symbols.ULTIMATE,
            Symbols.WORLD
        };
        public static class MagicFormationCircles
        {
            public const string ATTACK = "Attack";
            public const string DEFENSE = "Defence";
            public const string SUPPORT = "Support";
        }
        public static readonly List<string> AllMagicFormationCircleTypes = new()
        {
            MagicFormationCircles.ATTACK,
            MagicFormationCircles.DEFENSE,
            MagicFormationCircles.SUPPORT
        };
        public static class Relics
        {
            public const string ARM = "Arm";
            public const string BLOOD = "Blood";
            public const string BONE = "Bone";
            public const string BRAIN = "Brain";
            public const string EYES = "Eyes";
            public const string HEART = "Heart";
            public const string LEG = "Leg";
            public const string LUNG = "Lung";
            public const string NAIL = "Nail";
            public const string STOMACH = "Stomach";
            public const string TORSO = "Torso";
        }
        public static readonly List<string> AllRelicTypes = new()
        {
            Relics.ARM,
            Relics.BLOOD,
            Relics.BONE,
            Relics.BRAIN,
            Relics.EYES,
            Relics.HEART,
            Relics.LEG,
            Relics.LUNG,
            Relics.NAIL,
            Relics.STOMACH,
            Relics.TORSO
        };
        public static class Talismans
        {
            public const string ATTACK = "Attack";
            public const string DEFENSE = "Defence";
            public const string SUPPORT = "Support";
        }
        public static readonly List<string> AllTalismanTypes = new()
        {
            Talismans.ATTACK,
            Talismans.DEFENSE,
            Talismans.SUPPORT
        };
        public static class Puppets
        {
            public const string ATTACK = "Attack";
            public const string DEFENSE = "Defence";
            public const string SUPPORT = "Support";
        }
        public static readonly List<string> AllPuppetTypes = new()
        {
            Puppets.ATTACK,
            Puppets.DEFENSE,
            Puppets.SUPPORT
        };
        public static class Alchemies
        {
            public const string ATTACK = "Attack";
            public const string DEFENSE = "Defence";
            public const string SUPPORT = "Support";
        }
        public static readonly List<string> AllAlchemyTypes = new()
        {
            Alchemies.ATTACK,
            Alchemies.DEFENSE,
            Alchemies.SUPPORT
        };
        public static class Forges
        {
            public const string ATTACK = "Attack";
            public const string DEFENSE = "Defence";
            public const string SUPPORT = "Support";
        }
        public static readonly List<string> AllForgeTypes = new()
        {
            Forges.ATTACK,
            Forges.DEFENSE,
            Forges.SUPPORT
        };
        public static class CardLives
        {
            public const string DRINK = "Drink";
            public const string EQUIPMENTS = "Equipments";
            public const string FOOD = "Food";
            public const string SCENERY = "Scenery";
        }
        public static readonly List<string> AllCardLifeTypes = new()
        {
            CardLives.DRINK,
            CardLives.EQUIPMENTS,
            CardLives.FOOD,
            CardLives.SCENERY
        };
        public static class Artworks
        {
            public const string ADAMAS = "Adamas";
            public const string AVIAN = "Avian";
            public const string BARBARIAN = "Barbarian";
            public const string CYLLORAN = "Cylloran";
            public const string DREIZEN = "Dreizen";
            public const string ETRIGON = "Etrigon";
            public const string FIRIMIR = "Firimir";
            public const string GENNESIS = "Gennesis";
            public const string HECARUS = "Hecarus";
            public const string ILLONIMA = "Illonima";
            public const string JAGUAR = "Jaguar";
            public const string KRYPTONIAN = "Kryptonian";
            public const string LAMANIA = "Lamania";
            public const string MARVERICK = "Marverick";
            public const string NEMESIS = "Nemesis";
            public const string ONYX = "Onyx";
            public const string PALLADIAN = "Palladian";
            public const string QUASAR = "Quasar";
            public const string RIVERVEN = "Riverven";
            public const string STARROIAN = "Starroian";
            public const string TERAC = "Terac";
            public const string URIUS = "Urius";
            public const string VRIL = "Vril";
            public const string WYVERN = "Wyvern";
            public const string XANTHERA = "Xanthera";
            public const string YORNATH = "Yornath";
            public const string ZERATH = "Zerath";
        }
        public static readonly List<string> AllArtworkTypes = new()
        {
            Artworks.ADAMAS,
            Artworks.AVIAN,
            Artworks.BARBARIAN,
            Artworks.CYLLORAN,
            Artworks.DREIZEN,
            Artworks.ETRIGON,
            Artworks.FIRIMIR,
            Artworks.GENNESIS,
            Artworks.HECARUS,
            Artworks.ILLONIMA,
            Artworks.JAGUAR,
            Artworks.KRYPTONIAN,
            Artworks.LAMANIA,
            Artworks.MARVERICK,
            Artworks.NEMESIS,
            Artworks.ONYX,
            Artworks.PALLADIAN,
            Artworks.QUASAR,
            Artworks.RIVERVEN,
            Artworks.STARROIAN,
            Artworks.TERAC,
            Artworks.URIUS,
            Artworks.VRIL,
            Artworks.WYVERN,
            Artworks.XANTHERA,
            Artworks.YORNATH,
            Artworks.ZERATH
        };
        public static class SpiritCards
        {
            public const string ANCIENT = "Ancient";
            public const string ARCANUM = "Arcanum";
            public const string DESTINY = "Destiny";
            public const string HERATIC = "Heratic";
            public const string LEGENDARY = "Legendary";
            public const string MYSTIC = "Mystic";
            public const string TRANSCENDENCE = "Transcendence";
        }
        public static readonly List<string> AllSpiritCardTypes = new()
        {
            SpiritCards.ANCIENT,
            SpiritCards.ARCANUM,
            SpiritCards.DESTINY,
            SpiritCards.HERATIC,
            SpiritCards.LEGENDARY,
            SpiritCards.MYSTIC,
            SpiritCards.TRANSCENDENCE
        };
        public static class Vehicles
        {
            public const string GENERATION_1 = "Generation 1";
            public const string GENERATION_2 = "Generation 2";
            public const string GENERATION_3 = "Generation 3";
            public const string GENERATION_4 = "Generation 4";
            public const string GENERATION_5 = "Generation 5";
            public const string GENERATION_6 = "Generation 6";
            public const string GENERATION_7 = "Generation 7";
            public const string GENERATION_8 = "Generation 8";
        }
        public static readonly List<string> AllVehicleTypes = new()
        {
            Vehicles.GENERATION_1,
            Vehicles.GENERATION_2,
            Vehicles.GENERATION_3,
            Vehicles.GENERATION_4,
            Vehicles.GENERATION_5,
            Vehicles.GENERATION_6,
            Vehicles.GENERATION_7,
            Vehicles.GENERATION_8
        };
        // public static class SpiritCards
        // {
        //     public const string ANCIENT = "Ancient";
        //     public const string ARCANUM = "Arcanum";
        //     public const string DESTINY = "Destiny";
        //     public const string HERATIC = "Heratic";
        //     public const string LEGENDARY = "Legendary";
        //     public const string MYSTIC = "Mystic";
        //     public const string TRANSCENDENCE = "Transcendence";
        // }
        // public static readonly List<string> AllSpiritCardTypes = new()
        // {
        //     SpiritCards.ANCIENT,
        //     SpiritCards.ARCANUM,
        //     SpiritCards.DESTINY,
        //     SpiritCards.HERATIC,
        //     SpiritCards.LEGENDARY,
        //     SpiritCards.MYSTIC,
        //     SpiritCards.TRANSCENDENCE
        // };
        // public static class Buildings
        // {
        //     public const string ANCIENT = "Assembly";
        //     public const string ARCANUM = "Combat & Support";
        //     public const string DESTINY = "Cultural_Buildings";
        //     public const string HERATIC = "Goods_Buildings";
        //     public const string LEGENDARY = "Great_Buildings";
        //     public const string MYSTIC = "Logistics";
        //     public const string TRANSCENDENCE = "Military_Buildings";
        //     public const string TRANSCENDENCE = "Miscellaneous";
        //     public const string TRANSCENDENCE = "Planting";
        //     public const string TRANSCENDENCE = "Power";
        //     public const string TRANSCENDENCE = "Transcendence";
        //     public const string TRANSCENDENCE = "Transcendence";
        //     public const string TRANSCENDENCE = "Transcendence";
        //     public const string TRANSCENDENCE = "Transcendence";
        // }
        // public static readonly List<string> AllSpiritCardTypes = new()
        // {
        //     SpiritCards.ANCIENT,
        //     SpiritCards.ARCANUM,
        //     SpiritCards.DESTINY,
        //     SpiritCards.HERATIC,
        //     SpiritCards.LEGENDARY,
        //     SpiritCards.MYSTIC,
        //     SpiritCards.TRANSCENDENCE
        // };
        // public static class SpiritCards
        // {
        //     public const string ANCIENT = "Ancient";
        //     public const string ARCANUM = "Arcanum";
        //     public const string DESTINY = "Destiny";
        //     public const string HERATIC = "Heratic";
        //     public const string LEGENDARY = "Legendary";
        //     public const string MYSTIC = "Mystic";
        //     public const string TRANSCENDENCE = "Transcendence";
        // }
        // public static readonly List<string> AllSpiritCardTypes = new()
        // {
        //     SpiritCards.ANCIENT,
        //     SpiritCards.ARCANUM,
        //     SpiritCards.DESTINY,
        //     SpiritCards.HERATIC,
        //     SpiritCards.LEGENDARY,
        //     SpiritCards.MYSTIC,
        //     SpiritCards.TRANSCENDENCE
        // };
        public static class Items
        {
            public const string ARCHIVE = "Archive";
            public const string CARD = "Card";
            public const string CHEST = "Chest";
            public const string COUPON = "Coupon";
            public const string DISC = "Disc";
            public const string EVENT = "Event";
            public const string EXCHANGE = "Exchange";
            public const string FISH = "Fish";
            public const string HICA = "HICA";
            public const string HICB = "HICB";
            public const string HIDC = "HIDC";
            public const string HIEN = "HIEN";
            public const string HIHN = "HIHN";
            public const string HIIN = "HIIN";
            public const string HIRN = "HIRN";
            public const string HISN = "HISN";
            public const string HITN = "HITN";
            public const string IMBUEMENT = "Imbuement";
            public const string INGREDIENTS = "Ingredients";
            public const string MATERIAL_LG = "Material-LG";
            public const string MATERIAL_MR = "Material-MR";
            public const string MATERIAL_R = "Material-R";
            public const string MATERIAL_SR = "Material-SR";
            public const string MATERIAL_SSR = "Material-SSR";
            public const string MATERIAL_UR = "Material-UR";
            public const string MINERAL = "Mineral";
            public const string PAINTING = "Painting";
            public const string PRODUCT = "Product";
            public const string SEED = "Seed";
            public const string SHARD = "Shard";
            public const string SSWN = "SSWN";
            public const string TICKET = "Ticket";
            public const string TOKEN = "Token";
            public const string UNIVERSE = "Universe";
            public const string VOUCHER = "Voucher";
        }
        public static readonly List<string> GachaItemTypes = new()
        {
            Items.ARCHIVE,
            Items.HICA,
            Items.HICB,
            Items.HIDC,
            Items.HIEN,
            Items.HIHN,
            Items.HIIN,
            Items.HIRN,
            Items.HISN,
            Items.HITN,
            Items.SSWN,
            Items.UNIVERSE,
            Items.CARD,
            Items.MATERIAL_R,
            Items.MATERIAL_SR,
            Items.MATERIAL_SSR,
            Items.MATERIAL_UR,
            Items.MATERIAL_LG,
            Items.MATERIAL_MR,
            Items.DISC,
            Items.SHARD,
            Items.PAINTING,
            Items.IMBUEMENT
        };
    }
    public static class MainType
    {
        public const string USERNAME = "username";
        public const string PASSWORD = "password";


        public const string ACHIEVEMENT = "Achievement";
        public const string ACHIEVEMENTS = "Achievements";

        public const string CARD_HERO = "Card Hero";
        public const string CARD_HEROES = "Card Heroes";

        public const string ALCHEMY = "Alchemy";
        public const string ALCHEMIES = "Alchemies";

        public const string AVATAR = "Avatar";
        public const string AVATARS = "Avatars";

        public const string BORDER = "Border";
        public const string BORDERS = "Borders";

        public const string BOOK = "Book";
        public const string BOOKS = "Books";

        public const string CARD_ADMIRAL = "Card Admiral";
        public const string CARD_ADMIRALS = "Card Admirals";

        public const string CARD_CAPTAIN = "Card Captain";
        public const string CARD_CAPTAINS = "Card Captains";

        public const string CARD_COLONEL = "Card Colonel";
        public const string CARD_COLONELS = "Card Colonels";

        public const string CARD_GENERAL = "Card General";
        public const string CARD_GENERALS = "Card Generals";

        public const string CARD_LIFE = "Card Life";
        public const string CARD_LIVES = "Card Lives";

        public const string CARD_MILITARY = "Card Military";
        public const string CARD_MILITARIES = "Card Militaries";

        public const string CARD_MONSTER = "Card Monster";
        public const string CARD_MONSTERS = "Card Monsters";

        public const string CARD_SPELL = "Card Spell";
        public const string CARD_SPELLS = "Card Spells";

        public const string CARD_SOLDIER = "Card Soldier";
        public const string CARD_SOLDIERS = "Card Soldiers";

        public const string COLLABORATION_EQUIPMENT = "Collaboration Equipment";
        public const string COLLABORATION_EQUIPMENTS = "Collaboration Equipments";

        public const string COLLABORATION = "Collaboration";
        public const string COLLABORATIONS = "Collaborations";

        public const string EQUIPMENT = "Equipment";
        public const string EQUIPMENTS = "Equipments";

        public const string FORGE = "Forge";
        public const string FORGES = "Forges";

        public const string MAGIC_FORMATION_CIRCLE = "Magic Formation Circle";
        public const string MAGIC_FORMATION_CIRCLES = "Magic Formation Circles";

        public const string MEDAL = "Medal";
        public const string MEDALS = "Medals";

        public const string PET = "Pet";
        public const string PETS = "Pets";

        public const string PUPPET = "Puppet";
        public const string PUPPETS = "Puppets";

        public const string RELIC = "Relic";
        public const string RELICS = "Relics";

        public const string SKILL = "Skill";
        public const string SKILLS = "Skills";

        public const string SYMBOL = "Symbol";
        public const string SYMBOLS = "Symbols";

        public const string TALISMAN = "Talisman";
        public const string TALISMANS = "Talismans";

        public const string TITLE = "Title";
        public const string TITLES = "Titles";

        public const string ITEM = "Item";
        public const string ITEMS = "Items";

        public const string ARTWORK = "Artwork";
        public const string ARTWORKS = "Artworks";

        public const string SPIRIT_BEAST = "Spirit Beast";
        public const string SPIRIT_BEASTS = "Spirit Beasts";

        public const string SPIRIT_CARD = "Spirit Card";
        public const string SPIRIT_CARDS = "Spirit Cards";

        public const string ARCHITECTURE = "Architecture";
        public const string ARCHITECTURES = "Architectures";

        public const string TECHNOLOGY = "Technology";
        public const string TECHONOLOGIES = "Technologies";

        public const string VEHICLE = "Vehicle";
        public const string VEHICLES = "Vehicles";

        public const string ARTIFACT = "Artifact";
        public const string ARTIFACTS = "Artifacts";

        public const string CORE = "Core";
        public const string CORES = "Cores";

        public const string WEAPON = "Weapon";
        public const string WEAPONS = "Weapons";

        public const string ROBOT = "Robot";
        public const string ROBOTS = "Robots";

        public const string BADGE = "Badge";
        public const string BADGES = "Badges";

        public const string MECHA_BEAST = "Mecha Beast";
        public const string MECHA_BEASTS = "Mecha Beasts";

        public const string RUNE = "Rune";
        public const string RUNES = "Runes";

        public const string FURNITURE = "Furniture";
        public const string FURNITURES = "Furnitures";

        public const string FOOD = "Food";
        public const string FOODS = "Foods";

        public const string BEVERAGE = "Beverage";
        public const string BEVERAGES = "Beverages";

        public const string BUILDING = "Building";
        public const string BUILDINGS = "Buildings";

        public const string PLANT = "Plant";
        public const string PLANTS = "Plants";

        public const string FASHION = "Fashion";
        public const string FASHIONS = "Fashions";

        public const string EMOJI = "Emoji";
        public const string EMOJIS = "Emojis";

        public const string SCIENCE_FICTION = "Science Fiction";
        public const string SUMMON_CARD_HERO = "Summon Card Hero";
        public const string SUMMON_BOOK = "Summon Book";
        public const string SUMMON_CARD_CAPTAIN = "Summon Card Captain";
        public const string SUMMON_CARD_MONSTER = "Summon Card Monster";
        public const string SUMMON_CARD_MILITARY = "Summon Card Military";
        public const string SUMMON_CARD_SPELL = "Summon Card Spell";
        public const string SUMMON_CARD_COLONEL = "Summon Card Colonel";
        public const string SUMMON_CARD_GENERAL = "Summon Card General";
        public const string SUMMON_CARD_ADMIRAL = "Summon Card Admiral";

        public const string CAMPAIGN = "Campaign";
        public const string CAMPAIGNS = "Campaigns";
        public const string BAG = "Bag";
        public const string TEAMS = "Teams";
        public const string MORE = "More";
        public const string SHOP = "Shop";
        public const string GALLERY = "Gallery";
        public const string COLLECTION = "Collection";
        public const string ANIME = "Anime";
        public const string ARENA = "Arena";
        public const string GUILD = "Guild";
        public const string TOWER = "Tower";
        public const string EVENT = "Event";
        public const string MASTER_BOARD = "Master Board";
        public const string DAILY_CHECKIN = "Daily Checkin";
        public const string EMAIL = "Email";
        public const string CHAT = "Chat";
        public const string CHIP = "Chip";

        public const string BUY = "Buy";
        public const string PACKAGE = "Package";
        public const string CURRENCY = "Currency";
        public const string FEATURE = "Feature";
    }
    public static class Feature
    {
        public const string BASE = "Base";
        public const string TRAIN = "Train";
        public const string RESEARCH = "Research";
        public const string EMPLOYEE = "Employee";
        public const string WORLD = "World";
        public const string CITY = "City";
    }
    public static class Skill
    {
        public const string ALTERNATIVE = "Alternative";
        public const string CELESTIAL = "Celestial";
        public const string DIVINE = "Divine";
        public const string FORCES = "Forces";
        public const string MAIN = "Main";
        public const string NORMAL = "Normal";
        public const string TEAMWORK = "Teamwork";
        public const string TRANSCENDENCE = "Transcendence";
    }
    public static class Status
    {
        public const string SUCCESS = "SUCCESS";
        public const string MAX_LEVEL = "MAX_LEVEL";
        public const string NOT_ENOUGH_RESOURCE = "NOT_ENOUGH_RESOURCE";
        public const string ACTIVE = "Active";
        public const string PASSIVE = "Passive";
        public const string AVAILABLE = "available";
        public const string PENDING = "pending";
        public const string BLOCK = "block";
    }
    public static class Currency
    {
        public const string SILVER = "Silver";
        public const string GOLD = "Gold";
        public const string DIAMOND = "Diamond";
    }
    public static class Gallery
    {
        public const string CARD_HEROES_GALLERY = "Card Heroes Gallery";
        public const string BOOKS_GALLERY = "Books Gallery";
        public const string PETS_GALLERY = "Pets Gallery";
        public const string CARD_CAPTAINS_GALLERY = "Card Captains Gallery";
        public const string COLLABORATION_EQUIPMENTS_GALLERY = "Collaboration Equipments Gallery";
        public const string CARD_MILITARY_GALLERY = "Card Military Gallery";
        public const string CARD_SPELL_GALLERY = "Card Spell Gallery";
        public const string COLLABORATIONS_GALLERY = "Collaborations Gallery";
        public const string CARD_MONSTERS_GALLERY = "Card Monsters Gallery";
        public const string EQUIPMENTS_GALLERY = "Equipments Gallery";
        public const string MEDALS_GALLERY = "Medals Gallery";
        public const string SKILLS_GALLERY = "Skills Gallery";
        public const string SYMBOLS_GALLERY = "Symbols Gallery";
        public const string TITLES_GALLERY = "Titles Gallery";
        public const string MAGIC_FORMATION_CIRCLE_GALLERY = "Magic Formation Circle Gallery";
        public const string RELICS_GALLERY = "Relics Gallery";
        public const string CARD_COLONELS_GALLERY = "Card Colonels Gallery";
        public const string CARD_GENERALS_GALLERY = "Card Generals Gallery";
        public const string CARD_ADMIRALS_GALLERY = "Card Admirals Gallery";
        public const string CARD_SOLDIERS_GALLERY = "Card Soldiers Gallery";
        public const string BORDERS_GALLERY = "Borders Gallery";
        public const string TALISMAN_GALLERY = "Talisman Gallery";
        public const string PUPPET_GALLERY = "Puppet Gallery";
        public const string ALCHEMY_GALLERY = "Alchemy Gallery";
        public const string FORGE_GALLERY = "Forge Gallery";
        public const string LIFE_GALLERY = "Life Gallery";
        public const string ARTWORK_GALLERY = "Artwork Gallery";
        public const string SPIRIT_BEAST_GALLERY = "Spirit Beast Gallery";
        public const string AVATARS_GALLERY = "Avatars Gallery";
        public const string SPIRIT_CARD_GALLERY = "Spirit Card Gallery";
        public const string ARTIFACT_GALLERY = "Artifact Gallery";
        public const string ARCHITECTURES_GALLERY = "Architectures Gallery";
        public const string TECHNOLOGIES_GALLERY = "Technologies Gallery";
        public const string VEHICLES_GALLERY = "Vehicle Gallery";
        public const string CORES_GALLERY = "Cores Gallery";
        public const string WEAPONS_GALLERY = "Weapons Gallery";
        public const string ROBOTS_GALLERY = "Robots Gallery";
        public const string BADGES_GALLERY = "Bages Gallery";
        public const string MECHA_BEASTS_GALLERY = "Mecha Beasts Gallery";
        public const string RUNES_GALLERY = "Runes Gallery";
        public const string FURNITURES_GALLERY = "Furnitures Gallery";
        public const string FOODS_GALLERY = "Foods Gallery";
        public const string BEVERAGES_GALLERY = "Beverages Gallery";
        public const string BUILDINGS_GALLERY = "Buildings Gallery";
        public const string PLANTS_GALLERY = "Plants Gallery";
        public const string FASHIONS_GALLERY = "Fashions Gallery";
    }
    public static class Collection
    {
        public const string CARD_HEROES_COLLECTION = "Card Heroes Collection";
        public const string BOOKS_COLLECTION = "Books Collection";
        public const string PETS_COLLECTION = "Pets Collection";
        public const string CARD_CAPTAINS_COLLECTION = "Card Captains Collection";
        public const string COLLABORATION_EQUIPMENTS_COLLECTION = "Collaboration Equipments Collection";
        public const string CARD_MILITARY_COLLECTION = "Card Military Collection";
        public const string CARD_SPELL_COLLECTION = "Card Spell Collection";
        public const string COLLABORATIONS_COLLECTION = "Collaborations Collection";
        public const string CARD_MONSTERS_COLLECTION = "Card Monsters Collection";
        public const string EQUIPMENTS_COLLECTION = "Equipments Collection";
        public const string MEDALS_COLLECTION = "Medals Collection";
        public const string SKILLS_COLLECTION = "Skills Collection";
        public const string SYMBOLS_COLLECTION = "Symbols Collection";
        public const string TITLES_COLLECTION = "Titles Collection";
        public const string MAGIC_FORMATION_CIRCLE_COLLECTION = "Magic Formation Circle Collection";
        public const string RELICS_COLLECTION = "Relics Collection";
        public const string CARD_COLONELS_COLLECTION = "Card Colonels Collection";
        public const string CARD_GENERALS_COLLECTION = "Card Generals Collection";
        public const string CARD_ADMIRALS_COLLECTION = "Card Admirals Collection";
        public const string CARD_SOLDIERS_COLLECTION = "Card Soldiers Collection";
        public const string BORDERS_COLLECTION = "Borders Collection";
        public const string TALISMAN_COLLECTION = "Talisman Collection";
        public const string PUPPET_COLLECTION = "Puppet Collection";
        public const string ALCHEMY_COLLECTION = "Alchemy Collection";
        public const string FORGE_COLLECTION = "Forge Collection";
        public const string LIFE_COLLECTION = "Life Collection";
        public const string ARTWORK_COLLECTION = "Artwork Collection";
        public const string SPIRIT_BEAST_COLLECTION = "Spirit Beast Collection";
        public const string AVATARS_COLLECTION = "Avatars Collection";
        public const string SPIRIT_CARD_COLLECTION = "Spirit Card Collection";
        public const string ARTIFACT_COLLECTION = "Artifact Collection";
        public const string ARCHITECTURES_COLLECTION = "Architectures Collection";
        public const string TECHNOLOGIES_COLLECTION = "Technologies Collection";
        public const string VEHICLES_COLLECTION = "Vehicle Collection";
        public const string CORES_COLLECTION = "Cores Collection";
        public const string WEAPONS_COLLECTION = "Weapons Collection";
        public const string ROBOTS_COLLECTION = "Robots Collection";
        public const string BADGES_COLLECTION = "Badges Collection";
        public const string MECHA_BEASTS_COLLECTION = "Mecha Beasts Collection";
        public const string RUNES_COLLECTION = "Runes Collection";
        public const string FURNITURES_COLLECTION = "Furnitures Collection";
        public const string FOODS_COLLECTION = "Foods Collection";
        public const string BEVERAGES_COLLECTION = "Beverages Collection";
        public const string BUILDINGS_COLLECTION = "Buildings Collection";
        public const string PLANTS_COLLECTION = "Plants Collection";
        public const string FASHIONS_COLLECTION = "Fashions Collection";
    }
    public static class Anime
    {
        public const string ONE_PIECE = "One Piece";
        public const string NARUTO = "Naruto";
        public const string DRAGON_BALL = "Dragon Ball";
        public const string FAIRY_TAIL = "Fairy Tail";
        public const string SWORD_ART_ONLINE = "Sword Art Online";
        public const string DEMON_SLAYER = "Demon Slayer";
        public const string BLEACH = "Bleach";
        public const string JUJUTSU_KAISEN = "Jujutsu Kaisen";
        public const string BLACK_CLOVER = "Black Clover";
        public const string HUNTER_X_HUNTER = "Hunter X Hunter";
        public const string ONE_PUNCH_MAN = "One Punch Man";
    }
    public static class Emblem
    {
        public const string FACTION_A = "Faction_A";
        public const string FACTION_B = "Faction_B";
        public const string FACTION_C = "Faction_C";
        public const string FACTION_D = "Faction_D";
        public const string FACTION_E = "Faction_E";
    }
    public static class Class
    {
        public const string CASTER = "Caster";
        public const string DEFENDER = "Defender";
        public const string GUARD = "Guard";
        public const string MEDIC = "Medic";
        public const string SNIPER = "Sniper";
        public const string SPECIALIST = "Specialist";
        public const string SUPPORTER = "Supporter";
        public const string VANGUARD = "Vanguard";
        public static class Caster
        {
            public const string BLAST_CASTER = "Blast_Caster";
            public const string CHAIN_CASTER = "Chain_Caster";
            public const string CORE_CASTER = "Core_Caster";
            public const string MECH_ACCORD_CASTER = "Mech-Accord_Caster";
            public const string MYSTIC_CASTER = "Mystic_Caster";
            public const string PHALANX_CASTER = "Phalanx_Caster";
            public const string PRIMAL_CASTER = "Primal_Caster";
            public const string SHAPER_CASTER = "Shaper_Caster";
            public const string SPLASH_CASTER = "Splash_Caster";
        }
        public static class Defender
        {
            public const string ARTS_PROTECTOR_DEFENDER = "Arts_Protector_Defender";
            public const string DUELIST_DEFENDER = "Duelist_Defender";
            public const string FORTRESS_DEFENDER = "Fortress_Defender";
            public const string GUARDIAN_DEFENDER = "Guardian_Defender";
            public const string JUGGERNAUT_DEFENDER = "Juggernaut_Defender";
            public const string PROTECTOR_DEFENDER = "Protector_Defender";
            public const string SENTINEL_PROTECTOR_DEFENDER = "Sentinel_Protector_Defender";
        }
        public static class Guard
        {
            public const string ARTS_FIGHTER_GUARD = "Arts_Fighter_Guard";
            public const string CENTURION_GUARD = "Centurion_Guard";
            public const string CRUSHER_GUARD = "Crusher_Guard";
            public const string DREADNOUGHT_GUARD = "Dreadnought_Guard";
            public const string FIGHTER_GUARD = "Fighter_Guard";
            public const string INSTRUCOR_GUARD = "Instructor_Guard";
            public const string LIBERATOR_GUARD = "Liberator_Guard";
            public const string LORD_GUARD = "Lord_Guard";
            public const string MESHA_GUARD = "Musha_Guard";
            public const string REAPER_GUARD = "Reaper_Guard";
            public const string SWORDMASTER_GUARD = "Swordmaster_Guard";
        }
        public static class Medic
        {
            public const string CHAIN_MEDIC = "Chain_Medic";
            public const string INCANTATION_MEDIC = "Incantation_Medic";
            public const string MEDIC_MEDIC = "Medic_Medic";
            public const string MULTI_TARGET_MEDIC = "Multi-target_Medic";
            public const string THERAPIST_MEDIC = "Therapist_Medic";
            public const string WANDERING_MEDIC = "Wandering_Medic";
        }
        public static class Sniper
        {
            public const string ARTILLERYMAN_SNIPER = "Artilleryman_Sniper";
            public const string BESIEGER_SNIPER = "Besieger_Sniper";
            public const string DEADEYE_SNIPER = "Deadeye_Sniper";
            public const string FLINGER_SNIPER = "Flinger_Sniper";
            public const string HEAVYSHOOTER_SNIPER = "Heavyshooter_Sniper";
            public const string HUNTER_SNIPER = "Hunter_Sniper";
            public const string MARKSMAN_SNIPER = "Marksman_Sniper";
            public const string SPREADSHOOTER_SNIPER = "Spreadshooter_Sniper";
        }
        public static class Specialist
        {
            public const string AMBUSHER_SPECIALIST = "Ambusher_Specialist";
            public const string DOLLKEEPER_SPECIALIST = "Dollkeeper_Specialist";
            public const string EXECUTOR_SPECIALIST = "Executor_Specialist";
            public const string GEEK_SPECIALIST = "Geek_Specialist";
            public const string HOOKMASTER_SPECIALIST = "Hookmaster_Specialist";
            public const string MERCHANT_SPECIALIST = "Merchant_Specialist";
            public const string PUSH_STROKER_SPECIALIST = "Push_Stroker_Specialist";
            public const string TRAPMASTER_SPECIALIST = "Trapmaster_Specialist";
        }
        public static class Supporter
        {
            public const string ABJURER_SUPPORTER = "Abjurer_Supporter";
            public const string ARTIFICER_SUPPORTER = "Artificer_Supporter";
            public const string BARD_SUPPORTER = "Bard_Supporter";
            public const string DECEL_BINDER_SUPPORTER = "Decel_Binder_Supporter";
            public const string HEXER_SUPPORTER = "Hexer_Supporter";
            public const string RITUALIST_SUPPORTER = "Ritualist_Supporter";
            public const string SUMMONER_SUPPORTER = "Summoner_Supporter";
        }
        public static class Vanguard
        {
            public const string AGENT_VANGUARD = "Agent_Vanguard";
            public const string CHARGER_VANGUARD = "Charger_Vanguard";
            public const string PIONEER_VANGUARD = "Pioneer_Vanguard";
            public const string STANDARD_BEARER_VANGUARD = "Standard_Bearer_Vanguard";
            public const string TACTICIAN_VANGUARD = "Tactician_Vanguard";
        }
    }
    public static class Market
    {
        public const string RARE_MARKET = "Rare Market";
        public const string ULTRA_RARE_MARKET = "Ultra Rare Market";
        public const string LEGENDARY_MARKET = "Legendary Market";
        public const string MYSTIC_MARKET = "Mystic Market";
        public const string RARE_MATERIAL_ITEM = "Material–R";
        public const string ULTRA_RARE_MATERIAL_ITEM = "Material–UR";
        public const string LEGENDARY_MATERIAL_ITEM = "Material–LG";
        public const string MYSTIC_MATERIAL_ITEM = "Material–MR";
    }
    public static class Research
    {
        public const string HOUSING = "Housing";
        public const string INFRASTRUCTURE = "Infrastructure";
        public const string LOGISTICS = "Logistics";
        public const string SANITATION = "Sanitation";
        public const string TRANSPORTATION = "Transportation";
        public const string URBANIZATION = "Urbanization";
        public const string UTILITIES = "Utilities";
        public const string WASTE = "Waste";
        public const string WATER = "Water";
        public const string FACILITIES = "Facilities";

        public const string CONSTRUCTION = "Construction";
        public const string ENERGY = "Energy";
        public const string ENGINEERING = "Engineering";
        public const string INDUSTRY = "Industry";
        public const string MANUFACTURING = "Manufacturing";
        public const string MATERIALS = "Materials";
        public const string POWER = "Power";
        public const string RESOURCE = "Resource";
        public const string SYSTEM = "System";
        public const string MECHANICS = "Mechanics";

        public const string ARMOR = "Armor";
        public const string DEFENSE = "Defense";
        public const string DISASTER = "Disaster";
        public const string EMERGENCY = "Emergency";
        public const string MILITARY = "Military";
        public const string SAFETY = "Safety";
        public const string SHIELDING = "Shielding";
        public const string WEAPONS = "Weapons";
        public const string FORTIFICATION = "Fortification";
        public const string TACTICS = "Tactics";

        public const string COMMERCE = "Commerce";
        public const string ECONOMY = "Economy";
        public const string FINANCE = "Finance";
        public const string INVESTMENT = "Investment";
        public const string PRODUCTIVITY = "Productivity";
        public const string TRADE = "Trade";
        public const string MARKET = "Market";
        public const string ENTERPRISE = "Enterprise";
        public const string SUPPLY = "Supply";
        public const string DISTRIBUTION = "Distribution";

        public const string CLIMATE = "Climate";
        public const string CONSERVATION = "Conservation";
        public const string ECOLOGY = "Ecology";
        public const string ENVIRONMENT = "Environment";
        public const string POLLUTION = "Pollution";
        public const string RECYCLING = "Recycling";
        public const string SUSTAINABILITY = "Sustainability";
        public const string RESTORATION = "Restoration";
        public const string RENEWABLES = "Renewables";
        public const string PRESERVATION = "Preservation";

        public const string ASCENSION = "Ascension";
        public const string COLONIZATION = "Colonization";
        public const string DIMENSIONAL = "Dimensional";
        public const string EXPANSION = "Expansion";
        public const string EXPLORATION = "Exploration";
        public const string MEGASTRUCTURE = "Megastructure";
        public const string SINGULARITY = "Singularity";
        public const string TERRAFORMING = "Terraforming";
        public const string TIME = "Time";
        public const string COSMOLOGY = "Cosmology";

        public const string EPIDEMIOLOGY = "Epidemiology";
        public const string GENETICS = "Genetics";
        public const string HEALTH = "Health";
        public const string LONGEVITY = "Longevity";
        public const string MEDICINE = "Medicine";
        public const string IMMUNOLOGY = "Immunology";
        public const string BIOTECH = "Biotech";
        public const string PHARMACEUTICALS = "Pharmaceuticals";
        public const string NUTRITION = "Nutrition";
        public const string REGENERATION = "Regeneration";

        public const string AI = "AI";
        public const string COMMUNICATION = "Communication";
        public const string CYBERSECURITY = "Cybersecurity";
        public const string DATA = "Data";
        public const string INFORMATION = "Information";
        public const string NETWORKING = "Networking";
        public const string SECURITY = "Security";
        public const string SURVEILLANCE = "Surveillance";
        public const string ANALYTICS = "Analytics";
        public const string CONTROL = "Control";

        public const string AUTOMATION = "Automation";
        public const string BIOLOGY = "Biology";
        public const string CHEMISTRY = "Chemistry";
        public const string COMPUTING = "Computing";
        public const string NANOTECHNOLOGY = "Nanotechnology";
        public const string PHYSICS = "Physics";
        public const string QUANTUM = "Quantum";
        public const string ROBOTICS = "Robotics";
        public const string SCIENCE = "Science";
        public const string INNOVATION = "Innovation";

        public const string CULTURE = "Culture";
        public const string DEMOGRAPHY = "Demography";
        public const string EDUCATION = "Education";
        public const string GOVERNANCE = "Governance";
        public const string HAPPINESS = "Happiness";
        public const string LAW = "Law";
        public const string POLICY = "Policy";
        public const string POPULATION = "Population";
        public const string SOCIETY = "Society";
        public const string CIVICS = "Civics";
    }
    public static class Archive
    {
        public const string ARCHIVE_I = "Archive I";
        public const string ARCHIVE_II = "Archive II";
        public const string ARCHIVE_III = "Archive III";
        public const string ARCHIVE_IV = "Archive IV";
        public const string ARCHIVE_V = "Archive V";
        public const string ARCHIVE_VI = "Archive VI";
        public const string ARCHIVE_VII = "Archive VII";
        public const string ARCHIVE_VIII = "Archive VIII";
        public const string ARCHIVE_IX = "Archive IX";
        public const string ARCHIVE_X = "Archive X";
        public const string ARCHIVE_XI = "Archive XI";
        public const string ARCHIVE_XII = "Archive XII";
        public const string ARCHIVE_XIII = "Archive XIII";
        public const string ARCHIVE_XIV = "Archive XIV";
        public const string ARCHIVE_XV = "Archive XV";
        public const string ARCHIVE_XVI = "Archive XVI";
        public const string ARCHIVE_XVII = "Archive XVII";
        public const string ARCHIVE_XVIII = "Archive XVIII";
        public const string ARCHIVE_XIX = "Archive XIX";
        public const string ARCHIVE_XX = "Archive XX";
        public const string ARCHIVE_XXI = "Archive XXI";
        public const string ARCHIVE_XXII = "Archive XXII";
        public const string ARCHIVE_XXIII = "Archive XXIII";
        public const string ARCHIVE_XXIV = "Archive XXIV";
        public const string ARCHIVE_XXV = "Archive XXV";
        public const string ARCHIVE_XXVI = "Archive XXVI";
        public const string ARCHIVE_XXVII = "Archive XXVII";
        public const string ARCHIVE_XXVIII = "Archive XXVIII";
        public const string ARCHIVE_XXIX = "Archive XXIX";
        public const string ARCHIVE_XXX = "Archive XXX";
        public const string ARCHIVE_XXXI = "Archive XXXI";
        public const string ARCHIVE_XXXII = "Archive XXXII";
    }
    public static class Universe
    {
        public const string UNIVERSE_I = "Universe I";
        public const string UNIVERSE_II = "Universe II";
        public const string UNIVERSE_III = "Universe III";
        public const string UNIVERSE_IV = "Universe IV";
        public const string UNIVERSE_V = "Universe V";
        public const string UNIVERSE_VI = "Universe VI";
        public const string UNIVERSE_VII = "Universe VII";
        public const string UNIVERSE_VIII = "Universe VIII";
        public const string UNIVERSE_IX = "Universe IX";
        public const string UNIVERSE_X = "Universe X";
    }
    public static class HIIN
    {
        public const string HIIN_I = "HIIN I";
        public const string HIIN_II = "HIIN II";
        public const string HIIN_III = "HIIN III";
        public const string HIIN_IV = "HIIN IV";
        public const string HIIN_V = "HIIN V";
        public const string HIIN_VI = "HIIN VI";
        public const string HIIN_VII = "HIIN VII";
        public const string HIIN_VIII = "HIIN VIII";
        public const string HIIN_IX = "HIIN IX";
        public const string HIIN_X = "HIIN X";
    }
    public static class SSWN
    {
        public const string SSWN_I = "SSWN I";
        public const string SSWN_II = "SSWN II";
        public const string SSWN_III = "SSWN III";
        public const string SSWN_IV = "SSWN IV";
        public const string SSWN_V = "SSWN V";
        public const string SSWN_VI = "SSWN VI";
        public const string SSWN_VII = "SSWN VII";
        public const string SSWN_VIII = "SSWN VIII";
        public const string SSWN_IX = "SSWN IX";
        public const string SSWN_X = "SSWN X";
    }
    public static class HITN
    {
        public const string HITN_I = "HITN I";
        public const string HITN_II = "HITN II";
        public const string HITN_III = "HITN III";
        public const string HITN_IV = "HITN IV";
        public const string HITN_V = "HITN V";
        public const string HITN_VI = "HITN VI";
        public const string HITN_VII = "HITN VII";
        public const string HITN_VIII = "HITN VIII";
        public const string HITN_IX = "HITN IX";
        public const string HITN_X = "HITN X";
    }
    public static class HIHN
    {
        public const string HIHN_I = "HIHN I";
        public const string HIHN_II = "HIHN II";
        public const string HIHN_III = "HIHN III";
        public const string HIHN_IV = "HIHN IV";
        public const string HIHN_V = "HIHN V";
        public const string HIHN_VI = "HIHN VI";
        public const string HIHN_VII = "HIHN VII";
        public const string HIHN_VIII = "HIHN VIII";
        public const string HIHN_IX = "HIHN IX";
        public const string HIHN_X = "HIHN X";
    }
    public static class HIEN
    {
        public const string HIEN_I = "HIEN I";
        public const string HIEN_II = "HIEN II";
        public const string HIEN_III = "HIEN III";
        public const string HIEN_IV = "HIEN IV";
        public const string HIEN_V = "HIEN V";
        public const string HIEN_VI = "HIEN VI";
        public const string HIEN_VII = "HIEN VII";
        public const string HIEN_VIII = "HIEN VIII";
        public const string HIEN_IX = "HIEN IX";
        public const string HIEN_X = "HIEN X";
    }
    public static class HICA
    {
        public const string HICA_I = "HICA I";
        public const string HICA_II = "HICA II";
        public const string HICA_III = "HICA III";
        public const string HICA_IV = "HICA IV";
        public const string HICA_V = "HICA V";
        public const string HICA_VI = "HICA VI";
        public const string HICA_VII = "HICA VII";
        public const string HICA_VIII = "HICA VIII";
        public const string HICA_IX = "HICA IX";
        public const string HICA_X = "HICA X";
    }
    public static class HIRN
    {
        public const string HIRN_I = "HIRN I";
        public const string HIRN_II = "HIRN II";
        public const string HIRN_III = "HIRN III";
        public const string HIRN_IV = "HIRN IV";
        public const string HIRN_V = "HIRN V";
        public const string HIRN_VI = "HIRN VI";
        public const string HIRN_VII = "HIRN VII";
        public const string HIRN_VIII = "HIRN VIII";
        public const string HIRN_IX = "HIRN IX";
        public const string HIRN_X = "HIRN X";
    }
    public static class HIDC
    {
        public const string HIDC_I = "HIDC I";
        public const string HIDC_II = "HIDC II";
        public const string HIDC_III = "HIDC III";
        public const string HIDC_IV = "HIDC IV";
        public const string HIDC_V = "HIDC V";
        public const string HIDC_VI = "HIDC VI";
        public const string HIDC_VII = "HIDC VII";
        public const string HIDC_VIII = "HIDC VIII";
        public const string HIDC_IX = "HIDC IX";
        public const string HIDC_X = "HIDC X";
    }
    public static class HICB
    {
        public const string HICB_I = "HICB I";
        public const string HICB_II = "HICB II";
        public const string HICB_III = "HICB III";
        public const string HICB_IV = "HICB IV";
        public const string HICB_V = "HICB V";
        public const string HICB_VI = "HICB VI";
        public const string HICB_VII = "HICB VII";
        public const string HICB_VIII = "HICB VIII";
        public const string HICB_IX = "HICB IX";
        public const string HICB_X = "HICB X";
    }
    public static class HISN
    {
        public const string HISN_I = "HISN I";
        public const string HISN_II = "HISN II";
        public const string HISN_III = "HISN III";
        public const string HISN_IV = "HISN IV";
        public const string HISN_V = "HISN V";
        public const string HISN_VI = "HISN VI";
        public const string HISN_VII = "HISN VII";
        public const string HISN_VIII = "HISN VIII";
        public const string HISN_IX = "HISN IX";
        public const string HISN_X = "HISN X";
    }
    public static class MainMenuSet1
    {
        public const string EQUIPMENTS = "Equipments";
        public const string REALM = "Realm";
        public const string UPGRADE = "Upgrade";
        public const string APTITUDE = "Aptitude";
        public const string AFFINITY = "Affinity";
        public const string BLESSING = "Blessing";
        public const string CORE = "Core";
        public const string PHYSIQUE = "Physique";
        public const string BLOODLINE = "Bloodline";

        public const string OMNIVISION = "Omnivision";
        public const string OMNIPOTENCE = "Omnipotence";
        public const string OMNIPRESENCE = "Omnipresence";
        public const string OMNISCIENCE = "Omniscience";
        public const string OMNIVORY = "Omnivory";
        public const string ANGEL = "Angel";
        public const string DEMON = "Demon";

        public const string SWORD = "Sword";
        public const string SPEAR = "Spear";
        public const string SHIELD = "Shield";
        public const string BOW = "Bow";
        public const string GUN = "Gun";
        public const string CYBER = "Cyber";
        public const string FAIRY = "Fairy";
    }
    public static class MainMenuSet2
    {
        public const string DARK = "Dark";
        public const string LIGHT = "Light";
        public const string FIRE = "Fire";
        public const string ICE = "Ice";
        public const string EARTH = "Earth";
        public const string THUNDER = "Thunder";
        public const string LIFE = "Life";
        public const string SPACE = "Space";
        public const string TIME = "Time";

        public const string NANOTECH = "Nanotech";
        public const string QUANTUM = "Quantum";
        public const string HOLOGRAPHY = "Holography";
        public const string PLASMA = "Plasma";
        public const string BIOMECH = "Biomech";
        public const string CRYOTECH = "Cryotech";
        public const string PSIONICS = "Psionics";

        public const string NEUROTECH = "Neurotech";
        public const string ANTIMATTER = "Antimatter";
        public const string PAHNTOMWARE = "Phantomware";
        public const string GRAVITECH = "Gravitech";
        public const string AETHERNET = "Aethernet";
        public const string STARFORGE = "Starforge";
        public const string ORBITALIS = "Orbitalis";
    }
    public static class MainMenuSet3
    {
        //Set 3
        public const string AZATHOTH = "Azathoth";
        public const string YOG_SOTHOTH = "Yog-Sothoth";
        public const string NYARLATHOTEP = "Nyarlathotep";
        public const string SHUB_NIGGURATH = "Shub-Niggurath";
        public const string NIHORATH = "Nihorath";
        public const string AEONAX = "Aeonax";
        public const string SERAPHIROS = "Seraphiros";
        public const string THORINDAR = "Thorindar";
        public const string ZILTHROS = "Zilthros";

        public const string KHORAZAL = "Khorazal";
        public const string IXITHRA = "Ixithra";
        public const string OMNITHEUS = "Omnitheus";
        public const string PHYRIXA = "Phyrixa";
        public const string ATHERION = "Atherion";
        public const string VORATHOS = "Vorathos";
        public const string TENEBRIS = "Tenebris";

        public const string XYLKOR = "Xylkor";
        public const string VELTHARION = "Veltharion";
        public const string ARCANOS = "Arcanos";
        public const string DOLOMATH = "Dolomath";
        public const string ARATHOR = "Arathor";
        public const string XYPHOS = "Xyphos";
        public const string VAELITH = "Vaelith";
    }
    public static class MainMenuSet4
    {
        //Set 4
        public const string ZARX = "Zarx";
        public const string RAIK = "Raik";
        public const string DRAX = "Drax";
        public const string KRON = "Kron";
        public const string ZOLT = "Zolt";
        public const string GORR = "Gorr";
        public const string RYZE = "Ryze";
        public const string JAXX = "Jaxx";
        public const string THAR = "Thar";

        public const string VORN = "Vorn";
        public const string NYX = "Nyx";
        public const string AROS = "Aros";
        public const string HEX = "Hex";
        public const string LORN = "Lorn";
        public const string BAXX = "Baxx";
        public const string ZEPH = "Zeph";

        public const string KAEL = "Kael";
        public const string DRAV = "Drav";
        public const string TORN = "Torn";
        public const string MYRR = "Myrr";
        public const string VASK = "Vask";
        public const string JORR = "Jorr";
        public const string QUEN = "Quen";
    }
    public static class MainMenuSet5
    {
        public const string ASTRAL_VOICE = "Astral Voice";
        public const string BRANCH_BLADE_SONG = "Branch Blade Song";
        public const string CHAOS_JAZZ = "Chaos Jazz";
        public const string CHAOTIC_METAL = "Chaotic Metal";
        public const string DAWN_S_BLOOM = "Dawn s Bloom";
        public const string FANGED_METAL = "Fanged Metal";
        public const string FREEDOM_BLUES = "Freedom Blues";
        public const string HORMONE_PUNK = "Hormone Punk";
        public const string INFERNO_METAL = "Inferno Metal";
        public const string KING_OF_THE_SUMMIT = "King Of The Summit";
        public const string MOONLIGHT_LULLABY = "Moonlight Lullaby";
        public const string PHAETON_S_MELODY = "Phaeton s Melody";
        public const string POLAR_METAL = "Polar Metal";
        public const string PROTO_PUNK = "Proto Punk";
        public const string PUFFER_ELECTRO = "Puffer Electro";
        public const string SHADOW_HARMONY = "Shadow Harmony";
        public const string SHOCKSTAR_DISCO = "Shockstar Disco";
        public const string SOUL_ROCK = "Soul Rock";
        public const string SWING_JAZZ = "Swing Jazz";
        public const string THUNDER_METAL = "Thunder Metal";
        public const string WOODPECKER_ELECTRO = "Woodpecker Electro";
        public const string YUNKUI_TALES = "Yunkui Tales";
        public const string CHIP = "Chip";
    }
    public static class MainMenuSet6
    {
        public const string FLUX = "Flux";
        public const string NEXUS = "Nexus";
        public const string ECLIPSE = "Eclipse";
        public const string OBLIVION = "Oblivion";
        public const string CATALYST = "Catalyst";
        public const string AXIOM = "Axiom";
        public const string PARALLAX = "Parallax";
        public const string ENTROPY = "Entropy";
        public const string SINGULARITY = "Singularity";
        public const string GENESIS = "Genesis";
        public const string INFERNUM = "Infernum";
        public const string ELYSIUM = "Elysium";
        public const string APOTHEON = "Apotheon";
        public const string PARAGON = "Paragon";
        public const string NULLITY = "Nullity";
        public const string CATACLYSM = "Cataclysm";
        public const string EMPYREAN = "Empyrean";
        public const string HYPERION = "Hyperion";
        public const string DOMINION = "Dominion";
        public const string ZENITH = "Zenith";
        public const string OBLIVIUM = "Oblivium";
        public const string HELIX = "Helix";
        public const string UMBRA = "Umbra";
    }
    public static class MainMenuSet7
    {
        public const string AXIOMATA = "Axiomata";
        public const string CONTINUUM = "Continuum";
        public const string NOVA = "Nova";
        public const string PARADOX = "Paradox";
        public const string ABYSSAL = "Abyssal";
        public const string ARCANE = "Arcane";
        public const string ETERNUM = "Eternum";
        public const string LUMINARY = "Luminary";
        public const string COSMOS = "Cosmos";
        public const string ASTRION = "Astrion";
        public const string NEOTERRA = "Neoterra";
        public const string HORIZON = "Horizon";
        public const string NEXARIUM = "Nexarium";
        public const string CHRONYX = "Chronyx";
        public const string FERRUMAX = "Ferrumax";
        public const string COGNITUM = "Cognitum";
        public const string ASHFRAME = "Ashframe";
        public const string THRENODY = "Threnody";
        public const string MORVANE = "Morvane";
        public const string VELKRYN = "Velkryn";
        public const string XARPHIS = "Xarphis";
        public const string OMNIVEX = "Omnivex";
        public const string KAELTHRA = "Kaelthra";
    }
    public static class Master
    {
        public const string MASTER_OF_BEAST = "Master Of Beast";
        public const string MASTER_OF_DRAGON = "Master Of Dragon";
        public const string MASTER_OF_MAGIC = "Master Of Magic";
        public const string MASTER_OF_MUSIC = "Master Of Music";
        public const string MASTER_OF_SCIENCE = "Master Of Science";
        public const string MASTER_OF_SPIRIT = "Master Of Spirit";
        public const string MASTER_OF_WEAPON = "Master Of Weapon";
        public const string MASTER_OF_CHEMICAL = "Master Of Chemical";
        public const string MASTER_OF_PHYSICAL = "Master Of Physical";
        public const string MASTER_OF_ATOMIC = "Master Of Atomic";
        public const string MASTER_OF_MENTAL = "Master Of Mental";
    }
    public static class Equipment
    {
        public const string AMNITUS = "Amnitus_Equipment";
        public const string ANGELIS = "Angelis_Equipment";
        public const string BELLION = "Bellion_Equipment";
        public const string BENZAMIN = "Benzamin_Equipment";
        public const string CELESTIAL = "Celestial_Equipment";
        public const string CEVERUS = "Ceverus_Equipment";
        public const string DELIUS = "Delius_Equipment";
        public const string DOMITIUS = "Domitius_Equipment";
        public const string EVERLYN = "Everlyn_Equipment";
        public const string EXTRA = "Extra_Equipment";
        public const string FAILTUS = "Faltus_Equipment";
        public const string FEALAN = "Fealan_Equipment";
        public const string GAMMA = "Gamma_Equipment";
        public const string GEM = "Gem_Equipment";
        public const string HAGORO = "Hagoro_Equipment";
        public const string HAKALITE = "Hakalite_Equipment";
        public const string IGNIS = "Ignis_Equipment";
        public const string IVITUS = "Ivitus_Equipment";
        public const string JORVAN = "Jorvan_Equipment";
        public const string JULLIAN = "Jullian_Equipment";
        public const string KARIS = "Karis_Equipment";
        public const string KARMUS = "Karmus_Equipment";
        public const string LOTUS = "Lotus_Equipment";
        public const string LUMINIUS = "Luminius_Equipment";
        public const string MACUS = "Macus_Equipment";
        public const string MORGANIS = "Morganis_Equipment";
        public const string NIMIGAZIN = "Nimigazin_Equipment";
        public const string NOVA = "Nova_Equipment";
        public const string OMONITUS = "Omonitus_Equipment";
        public const string OMEGA = "Omega_Equipment";
        public const string PARIUS = "Parius_Equipment";
        public const string PYROS = "Pyros_Equipment";
        public const string QIYANTUS = "Qiyantus_Equipment";
        public const string QUASAR = "Quasar_Equipment";
        public const string RAINBOW = "Rainbow_Equipment";
        public const string REDVENGER = "Redvenger_Equipment";
        public const string SOULS = "Souls_Equipment";
        public const string SYNCROHARON = "Syncroharon_Equipment";
        public const string TARIAN = "Tarian_Equipment";
        public const string TEYRIC = "Teyric_Equipment";
        public const string UNI = "Uni_Equipment";
        public const string ULTRION = "Ultrion_Equipment";
        public const string VARETHION = "Varethion_Equipment";
        public const string VELMIRA = "Velmira_Equipment";
        public const string WENLITHAR = "Wenlithar_Equipment";
        public const string WYRMORA = "Wyrmora_Equipment";
        public const string XALTHEON = "Xaltheon_Equipment";
        public const string XYRALIS = "Xyralis_Equipment";
        public const string YLORAN = "Yloran_Equipment";
        public const string YVARION = "Yvarion_Equipment";
        public const string ZODIAC = "Zodiac_Equipment";
        public const string ZEROX = "Zerox_Equipment";
    }
    public static class ScienceFiction
    {
        public const string REACTOR_NUMBER_1 = "Reactor Number 1";
        public const string REACTOR_NUMBER_2 = "Reactor Number 2";
        public const string REACTOR_NUMBER_3 = "Reactor Number 3";
        public const string REACTOR_NUMBER_4 = "Reactor Number 4";
        public const string REACTOR_NUMBER_5 = "Reactor Number 5";
        public const string REACTOR_NUMBER_6 = "Reactor Number 6";
        public const string REACTOR_NUMBER_7 = "Reactor Number 7";
        public const string REACTOR_NUMBER_8 = "Reactor Number 8";
        public const string REACTOR_NUMBER_9 = "Reactor Number 9";
        public const string REACTOR_NUMBER_10 = "Reactor Number 10";
        public const string REACTOR_NUMBER_11 = "Reactor Number 11";
        public const string REACTOR_NUMBER_12 = "Reactor Number 12";
        public const string REACTOR_NUMBER_13 = "Reactor Number 13";
        public const string REACTOR_NUMBER_14 = "Reactor Number 14";
        public const string REACTOR_NUMBER_15 = "Reactor Number 15";
        public const string REACTOR_NUMBER_16 = "Reactor Number 16";
        public const string REACTOR_NUMBER_17 = "Reactor Number 17";
        public const string REACTOR_NUMBER_18 = "Reactor Number 18";
        public const string REACTOR_NUMBER_19 = "Reactor Number 19";
        public const string REACTOR_NUMBER_20 = "Reactor Number 20";
    }
}