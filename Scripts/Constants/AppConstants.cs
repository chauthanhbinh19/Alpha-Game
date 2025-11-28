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
        public const string ALL = "All";
        public const string SR = "SR";
        public const string SSR = "SSR";
        public const string UR = "UR";
        public const string LG = "LG";
        public const string LGPlus = "LG+";
        public const string MR = "MR";
        public const string SLG = "SLG";
        public const string SLGPlus = "SLG+";
        public const string SP = "SP";
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

        public const string CARD = "Card";
        public const string CARDS = "Cards";

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

        public const string SCIENCE_FICTION = "Science Fiction";
        public const string SUMMON_CARD_HEROES = "Summon Card Heroes";
        public const string SUMMON_BOOKS = "Summon Books";
        public const string SUMMON_CARD_CAPTAINS = "Summon Card Captains";
        public const string SUMMON_CARD_MONSTERS = "Summon Card Monsters";
        public const string SUMMON_CARD_MILITARY = "Summon Card Militaries";
        public const string SUMMON_CARD_SPELLS = "Summon Card Spells";
        public const string SUMMON_CARD_COLONELS = "Summon Card Colonels";
        public const string SUMMON_CARD_GENERALS = "Summon Card Generals";
        public const string SUMMON_CARD_ADMIRALS = "Summon Card Admirals";

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
        public const string CARD_GALLERY = "Card Gallery";
        public const string ARCHITECTURES_GALLERY = "Architectures Gallery";
        public const string TECHNOLOGIES_GALLERY = "Technologies Gallery";
        public const string VEHICLES_GALLERY = "Vehicle Gallery";
        public const string CORES_GALLERY = "Cores Gallery";
        public const string WEAPONS_GALLERY = "Weapons Gallery";
        public const string ROBOTS_GALLERY = "Robots Gallery";
        public const string BADGES_GALLERY = "Bages Gallery";
        public const string MECHA_BEASTS_GALLERY = "Mecha Beasts Gallery";
        public const string RUNES_GALLERY = "Runes Gallery";
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
        public const string CARD_COLLECTION = "Card Collection";
        public const string ARCHITECTURES_COLLECTION = "Architectures Collection";
        public const string TECHNOLOGIES_COLLECTION = "Technologies Collection";
        public const string VEHICLES_COLLECTION = "Vehicle Collection";
        public const string CORES_COLLECTION = "Cores Collection";
        public const string WEAPONS_COLLECTION = "Weapons Collection";
        public const string ROBOTS_COLLECTION = "Robots Collection";
        public const string BADGES_COLLECTION = "Badges Collection";
        public const string MECHA_BEASTS_COLLECTION = "Mecha Beasts Collection";
        public const string RUNES_COLLECTION = "Runes Collection";
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
    public static class Market
    {
        public const string RARE_MARKET = "Rare Market";
        public const string ULTRA_RARE_MARKET = "Ultra Rare Market";
        public const string LEGENDARY_MARKET = "Legendary Market";
        public const string MYSTIC_MARKET = "Mystic Market";
        public const string RARE_MATERIAL_ITEM = "Material – Rare Tier";
        public const string ULTRA_RARE_MATERIAL_ITEM = "Material – Ultra Rare Tier";
        public const string LEGENDARY_MATERIAL_ITEM = "Material – Legendary Tier";
        public const string MYSTIC_MATERIAL_ITEM = "Material – Mystic Tier";
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
        public const string PET = "Pet_Equipment";
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