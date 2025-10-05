using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenCover.Framework.Model;
using UnityEditor.PackageManager;
public static class AppConstants
{
    public static class StatFields
    {
        public const string Power = "power";
        public const string AllPower = "all_power";
        public const string Health = "health";
        public const string AllHealth = "all_health";
        public const string PhysicalAttack = "physical_attack";
        public const string AllPhysicalAttack = "all_physical_attack";
        public const string PhysicalDefense = "physical_defense";
        public const string AllPhysicalDefense = "all_physical_defense";
        public const string MagicalAttack = "magical_attack";
        public const string AllMagicalAttack = "all_magical_attack";
        public const string MagicalDefense = "magical_defense";
        public const string AllMagicalDefense = "all_magical_defense";
        public const string ChemicalAttack = "chemical_attack";
        public const string AllChemicalAttack = "all_chemical_attack";
        public const string ChemicalDefense = "chemical_defense";
        public const string AllChemicalDefense = "all_chemical_defense";
        public const string AtomicAttack = "atomic_attack";
        public const string AllAtomicAttack = "all_atomic_attack";
        public const string AtomicDefense = "atomic_defense";
        public const string AllAtomicDefense = "all_atomic_defense";
        public const string MentalAttack = "mental_attack";
        public const string AllMentalAttack = "all_mental_attack";
        public const string MentalDefense = "mental_defense";
        public const string AllMentalDefense = "all_mental_defense";
        public const string Speed = "speed";
        public const string AllSpeed = "all_speed";
        public const string CriticalDamageRate = "critical_damage_rate";
        public const string AllCriticalDamageRate = "all_critical_damage_rate";
        public const string CriticalRate = "critical_rate";
        public const string AllCriticalRate = "all_critical_rate";
        public const string CriticalResistanceRate = "critical_resistance_rate";
        public const string AllCriticalResistanceRate = "all_critical_resistance_rate";
        public const string IgnoreCriticalRate = "ignore_critical_rate";
        public const string AllIgnoreCriticalRate = "all_ignore_critical_rate";
        public const string PenetrationRate = "penetration_rate";
        public const string AllPenetrationRate = "all_penetration_rate";
        public const string PenetrationResistanceRate = "penetration_resistance_rate";
        public const string AllPenetrationResistanceRate = "all_penetration_resistance_rate";
        public const string EvasionRate = "evasion_rate";
        public const string AllEvasionRate = "all_evasion_rate";
        public const string DamageAbsorptionRate = "damage_absorption_rate";
        public const string AllDamageAbsorptionRate = "all_damage_absorption_rate";
        public const string IgnoreDamageAbsorptionRate = "ignore_damage_absorption_rate";
        public const string AllIgnoreDamageAbsorptionRate = "all_ignore_damage_absorption_rate";
        public const string AbsorbedDamageRate = "absorbed_damage_rate";
        public const string AllAbsorbedDamageRate = "all_absorbed_damage_rate";
        public const string VitalityRegenerationRate = "vitality_regeneration_rate";
        public const string AllVitalityRegenerationRate = "all_vitality_regeneration_rate";
        public const string VitalityRegenerationResistanceRate = "vitality_regeneration_resistance_rate";
        public const string AllVitalityRegenerationResistanceRate = "all_vitality_regeneration_resistance_rate";
        public const string AccuracyRate = "accuracy_rate";
        public const string AllAccuracyRate = "all_accuracy_rate";
        public const string LifestealRate = "lifesteal_rate";
        public const string AllLifestealRate = "all_lifesteal_rate";
        public const string Mana = "mana";
        public const string AllMana = "all_mana";
        public const string ManaRegenerationRate = "mana_regeneration_rate";
        public const string AllManaRegenerationRate = "all_mana_regeneration_rate";
        public const string ShieldStrength = "shield_strength";
        public const string AllShieldStrength = "all_shield_strength";
        public const string Tenacity = "tenacity";
        public const string AllTenacity = "all_tenacity";
        public const string ResistanceRate = "resistance_rate";
        public const string AllResistanceRate = "all_resistance_rate";
        public const string ComboRate = "combo_rate";
        public const string AllComboRate = "all_combo_rate";
        public const string IgnoreComboRate = "ignore_combo_rate";
        public const string AllIgnoreComboRate = "all_ignore_combo_rate";
        public const string ComboDamageRate = "combo_damage_rate";
        public const string AllComboDamageRate = "all_combo_damage_rate";
        public const string ComboResistanceRate = "combo_resistance_rate";
        public const string AllComboResistanceRate = "all_combo_resistance_rate";
        public const string StunRate = "stun_rate";
        public const string AllStunRate = "all_stun_rate";
        public const string IgnoreStunRate = "ignore_stun_rate";
        public const string AllIgnoreStunRate = "all_ignore_stun_rate";
        public const string ReflectionRate = "reflection_rate";
        public const string AllReflectionRate = "all_reflection_rate";
        public const string IgnoreReflectionRate = "ignore_reflection_rate";
        public const string AllIgnoreReflectionRate = "all_ignore_reflection_rate";
        public const string ReflectionDamageRate = "reflection_damage_rate";
        public const string AllReflectionDamageRate = "all_reflection_damage_rate";
        public const string ReflectionResistanceRate = "reflection_resistance_rate";
        public const string AllReflectionResistanceRate = "all_reflection_resistance_rate";
        public const string DamageToDifferentFactionRate = "damage_to_different_faction_rate";
        public const string AllDamageToDifferentFactionRate = "all_damage_to_different_faction_rate";
        public const string ResistanceToDifferentFactionRate = "resistance_to_different_faction_rate";
        public const string AllResistanceToDifferentFactionRate = "all_resistance_to_different_faction_rate";
        public const string DamageToSameFactionRate = "damage_to_same_faction_rate";
        public const string AllDamageToSameFactionRate = "all_damage_to_same_faction_rate";
        public const string ResistanceToSameFactionRate = "resistance_to_same_faction_rate";
        public const string AllResistanceToSameFactionRate = "all_resistance_to_same_faction_rate";
        public const string NormalDamageRate = "normal_damage_rate";
        public const string AllNormalDamageRate = "all_normal_damage_rate";
        public const string NormalResistanceRate = "normal_resistance_rate";
        public const string AllNormalResistanceRate = "all_normal_resistance_rate";
        public const string SkillDamageRate = "skill_damage_rate";
        public const string AllSkillDamageRate = "all_skill_damage_rate";
        public const string SkillResistanceRate = "skill_resistance_rate";
        public const string AllSkillResistanceRate = "all_skill_resistance_rate";
    }
    public static class Rare
    {
        public const string All = "All";
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
        public const string Username = "username";
        public const string Password = "password";


        public const string Alchievement = "Alchievement";
        public const string Achievements = "Alchievements";

        public const string CardHero = "Card Hero";
        public const string CardHeroes = "Card Heroes";

        public const string Alchemy = "Alchemy";
        public const string Alchemies = "Alchemies";

        public const string Avatar = "Avatar";
        public const string Avatars = "Avatars";

        public const string Border = "Border";
        public const string Borders = "Borders";

        public const string Book = "Book";
        public const string Books = "Books";

        public const string CardAdmiral = "Card Admiral";
        public const string CardAdmirals = "Card Admirals";

        public const string CardCaptain = "Card Captain";
        public const string CardCaptains = "Card Captains";

        public const string CardColonel = "Card Colonel";
        public const string CardColonels = "Card Colonels";

        public const string CardGeneral = "Card General";
        public const string CardGenerals = "Card Generals";

        public const string CardLife = "Card Life";
        public const string CardLives = "Card Lives";

        public const string CardMilitary = "Card Military";
        public const string CardMilitaries = "Card Militaries";

        public const string CardMonster = "Card Monster";
        public const string CardMonsters = "Card Monsters";

        public const string CardSpell = "Card Spell";
        public const string CardSpells = "Card Spells";

        public const string CollaborationEquipment = "Collaboration Equipment";
        public const string CollaborationEquipments = "Collaboration Equipments";

        public const string Collaboration = "Collaboration";
        public const string Collaborations = "Collaborations";

        public const string Equipment = "Equipment";
        public const string Equipments = "Equipments";

        public const string Forge = "Forge";
        public const string Forges = "Forges";

        public const string MagicFormationCircle = "Magic Formation Circle";
        public const string MagicFormationCircles = "Magic Formation Circles";

        public const string Medal = "Medal";
        public const string Medals = "Medals";

        public const string Pet = "Pet";
        public const string Pets = "Pets";

        public const string Puppet = "Puppet";
        public const string Puppets = "Puppets";

        public const string Relic = "Relic";
        public const string Relics = "Relics";

        public const string Skill = "Skill";
        public const string Skills = "Skills";

        public const string Symbol = "Symbol";
        public const string Symbols = "Symbols";

        public const string Talisman = "Talisman";
        public const string Talismans = "Talismans";

        public const string Title = "Title";
        public const string Titles = "Titles";

        public const string Item = "Item";
        public const string Items = "Items";

        public const string Artwork = "Artwork";
        public const string Artworks = "Artworks";

        public const string SpiritBeast = "Spirit Beast";
        public const string SpiritBeasts = "Spirit Beasts";

        public const string ScienceFiction = "Science Fiction";
        public const string SummonCardHeroes = "Summon Card Heroes";
        public const string SummonBooks = "Summon Books";
        public const string SummonCardCaptains = "Summon Card Captains";
        public const string SummonCardMonsters = "Summon Card Monsters";
        public const string SummonCardMilitaries = "Summon Card Militaries";
        public const string SummonCardSpells = "Summon Card Spells";
        public const string SummonCardColonels = "Summon Card Colonels";
        public const string SummonCardGenerals = "Summon Card Generals";
        public const string SummonCardAdmirals = "Summon Card Admirals";

        public const string Campaign = "Campaign";
        public const string Campaigns = "Campaigns";
        public const string Bag = "Bag";
        public const string Teams = "Teams";
        public const string More = "More";
        public const string Shop = "Shop";
        public const string Gallery = "Gallery";
        public const string Collection = "Collection";
        public const string Anime = "Anime";
        public const string Arena = "Arena";
        public const string Guild = "Guild";
        public const string Tower = "Tower";
        public const string Event = "Event";
        public const string MasterBoard = "Master Board";
        public const string DailyCheckin = "Daily Checkin";
        public const string Email = "Email";
        public const string Chat = "Chat";

        public const string Buy = "Buy";
        public const string PackageItem = "Package";

    }
    public static class Skill
    {
        public const string Alternative = "Alternative";
        public const string Celestial = "Celestial";
        public const string Divine = "Divine";
        public const string Forces = "Forces";
        public const string Main = "Main";
        public const string Normal = "Normal";
        public const string Teamwork = "Teamwork";
        public const string Transcendence = "Transcendence";
    }
    public static class Status
    {
        public const string Success = "SUCCESS";
        public const string MaxLevel = "MAX_LEVEL";
        public const string NotEnoughResource = "NOT_ENOUGH_RESOURCE";
    }
    public static class Currency
    {
        public const string Silver = "Silver";
        public const string Gold = "Gold";
        public const string Diamond = "Diamond";
    }
    public static class Gallery
    {
        public const string CardHeroesGallery = "Card Heroes Gallery";
        public const string BooksGallery = "Books Gallery";
        public const string PetsGallery = "Pets Gallery";
        public const string CardCaptainsGallery = "Card Captains Gallery";
        public const string CollaborationEquipmentsGallery = "Collaboration Equipments Gallery";
        public const string CardMilitaryGallery = "Card Military Gallery";
        public const string CardSpellGallery = "Card Spell Gallery";
        public const string CollaborationsGallery = "Collaborations Gallery";
        public const string CardMonstersGallery = "Card Monsters Gallery";
        public const string EquipmentsGallery = "Equipments Gallery";
        public const string MedalsGallery = "Medals Gallery";
        public const string SkillsGallery = "Skills Gallery";
        public const string SymbolsGallery = "Symbols Gallery";
        public const string TitlesGallery = "Titles Gallery";
        public const string MagicFormationCircleGallery = "Magic Formation Circle Gallery";
        public const string RelicsGallery = "Relics Gallery";
        public const string CardColonelsGallery = "Card Colonels Gallery";
        public const string CardGeneralsGallery = "Card Generals Gallery";
        public const string CardAdmiralsGallery = "Card Admirals Gallery";
        public const string BordersGallery = "Borders Gallery";
        public const string TalismanGallery = "Talisman Gallery";
        public const string PuppetGallery = "Puppet Gallery";
        public const string AlchemyGallery = "Alchemy Gallery";
        public const string ForgeGallery = "Forge Gallery";
        public const string LifeGallery = "Life Gallery";
        public const string ArtworkGallery = "Artwork Gallery";
        public const string SpiritBeastGallery = "Spirit Beast Gallery";
        public const string AvatarsGallery = "Avatars Gallery";

    }
    public static class Collection
    {
        public const string CardHeroesCollection = "Card Heroes Collection";
        public const string BooksCollection = "Books Collection";
        public const string PetsCollection = "Pets Collection";
        public const string CardCaptainsCollection = "Card Captains Collection";
        public const string CollaborationEquipmentsCollection = "Collaboration Equipments Collection";
        public const string CardMilitaryCollection = "Card Military Collection";
        public const string CardSpellCollection = "Card Spell Collection";
        public const string CollaborationsCollection = "Collaborations Collection";
        public const string CardMonstersCollection = "Card Monsters Collection";
        public const string EquipmentsCollection = "Equipments Collection";
        public const string MedalsCollection = "Medals Collection";
        public const string SkillsCollection = "Skills Collection";
        public const string SymbolsCollection = "Symbols Collection";
        public const string TitlesCollection = "Titles Collection";
        public const string MagicFormationCircleCollection = "Magic Formation Circle Collection";
        public const string RelicsCollection = "Relics Collection";
        public const string CardColonelsCollection = "Card Colonels Collection";
        public const string CardGeneralsCollection = "Card Generals Collection";
        public const string CardAdmiralsCollection = "Card Admirals Collection";
        public const string BordersCollection = "Borders Collection";
        public const string TalismanCollection = "Talisman Collection";
        public const string PuppetCollection = "Puppet Collection";
        public const string AlchemyCollection = "Alchemy Collection";
        public const string ForgeCollection = "Forge Collection";
        public const string LifeCollection = "Life Collection";
        public const string ArtworkCollection = "Artwork Collection";
        public const string SpiritBeastCollection = "Spirit Beast Collection";
        public const string AvatarsCollection = "Avatars Collection";

    }
    public static class Anime
    {
        public const string OnePiece = "One Piece";
        public const string Naruto = "Naruto";
        public const string DragonBall = "Dragon Ball";
        public const string FairyTail = "Fairy Tail";
        public const string SwordArtOnline = "Sword Art Online";
        public const string DemonSlayer = "Demon Slayer";
        public const string Bleach = "Bleach";
        public const string JujutsuKaisen = "Jujutsu Kaisen";
        public const string BlackClover = "Black Clover";
        public const string HunterXHunter = "Hunter X Hunter";
        public const string OnePunchMan = "One Punch Man";
    }
    public static class Market
    {
        public const string RareMarket = "Rare Market";
        public const string UltraRareMarket = "Ultra Rare Market";
        public const string LegendaryMarket = "Legendary Market";
        public const string MysticMarket = "Mystic Market";
        public const string RareMaterialItem = "Material – Rare Tier";
        public const string UltraRareMaterialItem = "Material – Ultra Rare Tier";
        public const string LegendaryMaterialItem = "Material – Legendary Tier";
        public const string MysticMaterialItem = "Material – Mystic Tier";
    }
    public static class MainMenuSet1
    {
        //Set 1
        public const string Equipments = "Equipments";
        public const string Realm = "Realm";
        public const string Upgrade = "Upgrade";
        public const string Aptitude = "Aptitude";
        public const string Affinity = "Affinity";
        public const string Blessing = "Blessing";
        public const string Core = "Core";
        public const string Physique = "Physique";
        public const string Bloodline = "Bloodline";

        public const string Omnivision = "Omnivision";
        public const string Omnipotence = "Omnipotence";
        public const string Omnipresence = "Omnipresence";
        public const string Omniscience = "Omniscience";
        public const string Omnivory = "Omnivory";
        public const string Angel = "Angel";
        public const string Demon = "Demon";

        public const string Sword = "Sword";
        public const string Spear = "Spear";
        public const string Shield = "Shield";
        public const string Bow = "Bow";
        public const string Gun = "Gun";
        public const string Cyber = "Cyber";
        public const string Fairy = "Fairy";
    }
    public static class MainMenuSet2
    {
        //Set 2
        public const string Dark = "Dark";
        public const string Light = "Light";
        public const string Fire = "Fire";
        public const string Ice = "Ice";
        public const string Earth = "Earth";
        public const string Thunder = "Thunder";
        public const string Life = "Life";
        public const string Space = "Space";
        public const string Time = "Time";

        public const string Nanotech = "Nanotech";
        public const string Quantum = "Quantum";
        public const string Holography = "Holography";
        public const string Plasma = "Plasma";
        public const string Biomech = "Biomech";
        public const string Cryotech = "Cryotech";
        public const string Psionics = "Psionics";

        public const string Neurotech = "Neurotech";
        public const string Antimatter = "Antimatter";
        public const string Phantomware = "Phantomware";
        public const string Gravitech = "Gravitech";
        public const string Aethernet = "Aethernet";
        public const string Starforge = "Starforge";
        public const string Orbitalis = "Orbitalis";
    }
    public static class MainMenuSet3
    {
        //Set 3
        public const string Azathoth = "Azathoth";
        public const string YogSothoth = "Yog-Sothoth";
        public const string Nyarlathotep = "Nyarlathotep";
        public const string ShubNiggurath = "Shub-Niggurath";
        public const string Nihorath = "Nihorath";
        public const string Aeonax = "Aeonax";
        public const string Seraphiros = "Seraphiros";
        public const string Thorindar = "Thorindar";
        public const string Zilthros = "Zilthros";

        public const string Khorazal = "Khorazal";
        public const string Ixithra = "Ixithra";
        public const string Omnitheus = "Omnitheus";
        public const string Phyrixa = "Phyrixa";
        public const string Atherion = "Atherion";
        public const string Vorathos = "Vorathos";
        public const string Tenebris = "Tenebris";

        public const string Xylkor = "Xylkor";
        public const string Veltharion = "Veltharion";
        public const string Arcanos = "Arcanos";
        public const string Dolomath = "Dolomath";
        public const string Arathor = "Arathor";
        public const string Xyphos = "Xyphos";
        public const string Vaelith = "Vaelith";
    }
    public static class MainMenuSet4
    {
        //Set 4
        public const string Zarx = "Zarx";
        public const string Raik = "Raik";
        public const string Drax = "Drax";
        public const string Kron = "Kron";
        public const string Zolt = "Zolt";
        public const string Gorr = "Gorr";
        public const string Ryze = "Ryze";
        public const string Jaxx = "Jaxx";
        public const string Thar = "Thar";

        public const string Vorn = "Vorn";
        public const string Nyx = "Nyx";
        public const string Aros = "Aros";
        public const string Hex = "Hex";
        public const string Lorn = "Lorn";
        public const string Baxx = "Baxx";
        public const string Zeph = "Zeph";

        public const string Kael = "Kael";
        public const string Drav = "Drav";
        public const string Torn = "Torn";
        public const string Myrr = "Myrr";
        public const string Vask = "Vask";
        public const string Jorr = "Jorr";
        public const string Quen = "Quen";
    }
    public static class Master
    {
        public const string MasterOfBeast = "Master Of Beast";
        public const string MasterOfDragon = "Master Of Dragon";
        public const string MasterOfMagic = "Master Of Magic";
        public const string MasterOfMusic = "Master Of Music";
        public const string MasterOfScience = "Master Of Science";
        public const string MasterOfSpirit = "Master Of Spirit";
        public const string MasterOfWeapon = "Master Of Weapon";
        public const string MasterOfChemical = "Master Of Chemical";
        public const string MasterOfPhysical = "Master Of Physical";
        public const string MasterOfAtomic = "Master Of Atomic";
        public const string MasterOfMental = "Master Of Mental";
    }
    public static class Equipment
    {
        public const string Amnitus = "Amnitus_Equipment";
        public const string Angelis = "Angelis_Equipment";
        public const string Bellion = "Bellion_Equipment";
        public const string Benzamin = "Benzamin_Equipment";
        public const string Celestial = "Celestial_Equipment";
        public const string Ceverus = "Ceverus_Equipment";
        public const string Delius = "Delius_Equipment";
        public const string Domitius = "Domitius_Equipment";
        public const string Everlyn = "Everlyn_Equipment";
        public const string Extra = "Extra_Equipment";
        public const string Faltus = "Faltus_Equipment";
        public const string Fealan = "Fealan_Equipment";
        public const string Gamma = "Gamma_Equipment";
        public const string Gem = "Gem_Equipment";
        public const string Hagoro = "Hagoro_Equipment";
        public const string Hakalite = "Hakalite_Equipment";
        public const string Ignis = "Ignis_Equipment";
        public const string Ivitus = "Ivitus_Equipment";
        public const string Jorvan = "Jorvan_Equipment";
        public const string Jullian = "Jullian_Equipment";
        public const string Karis = "Karis_Equipment";
        public const string Karmus = "Karmus_Equipment";
        public const string Lotus = "Lotus_Equipment";
        public const string Luminius = "Luminius_Equipment";
        public const string Macus = "Macus_Equipment";
        public const string Morganis = "Morganis_Equipment";
        public const string Nimigazin = "Nimigazin_Equipment";
        public const string Nova = "Nova_Equipment";
        public const string Omonitus = "Omonitus_Equipment";
        public const string Omega = "Omega_Equipment";
        public const string Pet = "Pet_Equipment";
        public const string Pyros = "Pyros_Equipment";
        public const string Qiyantus = "Qiyantus_Equipment";
        public const string Quasar = "Quasar_Equipment";
        public const string Rainbow = "Rainbow_Equipment";
        public const string Redvenger = "Redvenger_Equipment";
        public const string Souls = "Souls_Equipment";
        public const string Syncroharon = "Syncroharon_Equipment";
        public const string Tarian = "Tarian_Equipment";
        public const string Teyric = "Teyric_Equipment";
        public const string Uni = "Uni_Equipment";
        public const string Ultrion = "Ultrion_Equipment";
        public const string Varethion = "Varethion_Equipment";
        public const string Velmira = "Velmira_Equipment";
        public const string Wenlithar = "Wenlithar_Equipment";
        public const string Wyrmora = "Wyrmora_Equipment";
        public const string Xaltheon = "Xaltheon_Equipment";
        public const string Xyralis = "Xyralis_Equipment";
        public const string Yloran = "Yloran_Equipment";
        public const string Yvarion = "Yvarion_Equipment";
        public const string Zodiac = "Zodiac_Equipment";
        public const string Zerox = "Zerox_Equipment";
    }
    public static class ScienceFiction
    {
        public const string ReactorNumber1 = "Reactor Number 1";
        public const string ReactorNumber2 = "Reactor Number 2";
        public const string ReactorNumber3 = "Reactor Number 3";
        public const string ReactorNumber4 = "Reactor Number 4";
        public const string ReactorNumber5 = "Reactor Number 5";
        public const string ReactorNumber6 = "Reactor Number 6";
        public const string ReactorNumber7 = "Reactor Number 7";
        public const string ReactorNumber8 = "Reactor Number 8";
        public const string ReactorNumber9 = "Reactor Number 9";
        public const string ReactorNumber10 = "Reactor Number 10";
        public const string ReactorNumber11 = "Reactor Number 11";
        public const string ReactorNumber12 = "Reactor Number 12";
        public const string ReactorNumber13 = "Reactor Number 13";
        public const string ReactorNumber14 = "Reactor Number 14";
        public const string ReactorNumber15 = "Reactor Number 15";
        public const string ReactorNumber16 = "Reactor Number 16";
        public const string ReactorNumber17 = "Reactor Number 17";
        public const string ReactorNumber18 = "Reactor Number 18";
        public const string ReactorNumber19 = "Reactor Number 19";
        public const string ReactorNumber20 = "Reactor Number 20";
    }
}