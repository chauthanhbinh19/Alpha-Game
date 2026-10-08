using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

public class RankController : MonoBehaviour
{
    public static RankController Instance { get; private set; }
    private Transform MainPanel;
    private GameObject RankPanelPrefab;
    private GameObject RankButtonPrefab;
    private GameObject PopupRankPanelPrefab;
    private GameObject PopupRankQuantityPanelPrefab;
    private GameObject PopupRankButtonPrefab;
    private GameObject MainRankPanelPrefab;
    private GameObject RankItemPrefab;
    private Transform Content;
    private const int ITEMS_PER_PAGE = 50;
    private int CurrentPage = 0;
    private List<KeyValuePair<string, FeatureRankDTO>> FeatureList;
    private IStats Stat;
    private PaginationManager PaginationManager;
    private string Type = "";
    private string Image = "";
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        MainPanel = UIManager.Instance.GetTransform(AppConstants.Transform.MAIN_PANEL);
        RankPanelPrefab = UIManager.Instance.Get(PrefabConstants.Rank.RANK_PANEL_PREFAB);
        RankButtonPrefab = UIManager.Instance.Get(PrefabConstants.Rank.RANK_BUTTON_PREFAB);
        PopupRankPanelPrefab = UIManager.Instance.Get(PrefabConstants.Rank.POPUP_RANK_PANEL_PREFAB);
        PopupRankQuantityPanelPrefab = UIManager.Instance.Get(PrefabConstants.Rank.POPUP_RANK_QUANTITY_PANEL_PREFAB);
        PopupRankButtonPrefab = UIManager.Instance.Get(PrefabConstants.Rank.POPUP_RANK_BUTTON_PREFAB);
        MainRankPanelPrefab = UIManager.Instance.Get(PrefabConstants.Rank.MAIN_RANK_PANEL_PREFAB);
        RankItemPrefab = UIManager.Instance.Get(PrefabConstants.Rank.RANK_ITEM_PREFAB);
    }

    public async Task GetRankAsync(string type, IStats stat)
    {
        switch (type)
        {
            // Set 1 (1 - 23)
            case AppConstants.MainMenuSet1.EQUIPMENTS:
                Type = type;
                Image = ImageConstants.MainMenuSet1.EQUIPMENTS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.REALM:
                Type = type;
                Image = ImageConstants.MainMenuSet1.REALM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.UPGRADE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.UPGRADE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.APTITUDE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.APTITUDE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.AFFINITY:
                Type = type;
                Image = ImageConstants.MainMenuSet1.AFFINITY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.BLESSING:
                Type = type;
                Image = ImageConstants.MainMenuSet1.BLESSING;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.CORE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.CORE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.PHYSIQUE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.PHYSIQUE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.BLOODLINE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.BLOODLINE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.OMNIVISION:
                Type = type;
                Image = ImageConstants.MainMenuSet1.OMNIVISION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.OMNIPOTENCE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.OMNIPOTENCE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.OMNIPRESENCE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.OMNIPRESENCE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.OMNISCIENCE:
                Type = type;
                Image = ImageConstants.MainMenuSet1.OMNISCIENCE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.OMNIVORY:
                Type = type;
                Image = ImageConstants.MainMenuSet1.OMNIVORY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.ANGEL:
                Type = type;
                Image = ImageConstants.MainMenuSet1.ANGEL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.DEMON:
                Type = type;
                Image = ImageConstants.MainMenuSet1.DEMON;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.SWORD:
                Type = type;
                Image = ImageConstants.MainMenuSet1.SWORD;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.SPEAR:
                Type = type;
                Image = ImageConstants.MainMenuSet1.SPEAR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.SHIELD:
                Type = type;
                Image = ImageConstants.MainMenuSet1.SHIELD;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.BOW:
                Type = type;
                Image = ImageConstants.MainMenuSet1.BOW;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.GUN:
                Type = type;
                Image = ImageConstants.MainMenuSet1.GUN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.CYBER:
                Type = type;
                Image = ImageConstants.MainMenuSet1.CYBER;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet1.FAIRY:
                Type = type;
                Image = ImageConstants.MainMenuSet1.FAIRY;
                await CreateRankControllerAsync(stat);
                break;

            // Set 2 (24 - 46)
            case AppConstants.MainMenuSet2.DARK:
                Type = type;
                Image = ImageConstants.MainMenuSet2.DARK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.LIGHT:
                Type = type;
                Image = ImageConstants.MainMenuSet2.LIGHT;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.FIRE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.FIRE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.ICE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.ICE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.EARTH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.EARTH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.THUNDER:
                Type = type;
                Image = ImageConstants.MainMenuSet2.THUNDER;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.LIFE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.LIFE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.SPACE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.SPACE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.TIME:
                Type = type;
                Image = ImageConstants.MainMenuSet2.TIME;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.NANOTECH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.NANOTECH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.QUANTUM:
                Type = type;
                Image = ImageConstants.MainMenuSet2.QUANTUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.HOLOGRAPHY:
                Type = type;
                Image = ImageConstants.MainMenuSet2.HOLOGRAPHY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.PLASMA:
                Type = type;
                Image = ImageConstants.MainMenuSet2.PLASMA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.BIOMECH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.BIOMECH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.CRYOTECH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.CRYOTECH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.PSIONICS:
                Type = type;
                Image = ImageConstants.MainMenuSet2.PSIONICS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.NEUROTECH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.NEUROTECH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.ANTIMATTER:
                Type = type;
                Image = ImageConstants.MainMenuSet2.ANTIMATTER;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.PHANTOMWARE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.PHANTOMWARE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.GRAVITECH:
                Type = type;
                Image = ImageConstants.MainMenuSet2.GRAVITECH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.AETHERNET:
                Type = type;
                Image = ImageConstants.MainMenuSet2.AETHERNET;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.STARFORGE:
                Type = type;
                Image = ImageConstants.MainMenuSet2.STARFORGE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet2.ORBITALIS:
                Type = type;
                Image = ImageConstants.MainMenuSet2.ORBITALIS;
                await CreateRankControllerAsync(stat);
                break;

            // Set 3 (47 - 69)
            case AppConstants.MainMenuSet3.AZATHOTH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.AZATHOTH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.YOG_SOTHOTH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.YOG_SOTHOTH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.NYARLATHOTEP:
                Type = type;
                Image = ImageConstants.MainMenuSet3.NYARLATHOTEP;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.SHUB_NIGGURATH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.SHUB_NIGGURATH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.NIHORATH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.NIHORATH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.AEONAX:
                Type = type;
                Image = ImageConstants.MainMenuSet3.AEONAX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.SERAPHIROS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.SERAPHIROS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.THORINDAR:
                Type = type;
                Image = ImageConstants.MainMenuSet3.THORINDAR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.ZILTHROS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.ZILTHROS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.KHORAZAL:
                Type = type;
                Image = ImageConstants.MainMenuSet3.KHORAZAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.IXITHRA:
                Type = type;
                Image = ImageConstants.MainMenuSet3.IXITHRA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.OMNITHEUS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.OMNITHEUS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.PHYRIXA:
                Type = type;
                Image = ImageConstants.MainMenuSet3.PHYRIXA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.ATHERION:
                Type = type;
                Image = ImageConstants.MainMenuSet3.ATHERION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.VORATHOS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.VORATHOS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.TENEBRIS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.TENEBRIS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.XYLKOR:
                Type = type;
                Image = ImageConstants.MainMenuSet3.XYLKOR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.VELTHARION:
                Type = type;
                Image = ImageConstants.MainMenuSet3.VELTHARION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.ARCANOS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.ARCANOS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.DOLOMATH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.DOLOMATH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.ARATHOR:
                Type = type;
                Image = ImageConstants.MainMenuSet3.ARATHOR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.XYPHOS:
                Type = type;
                Image = ImageConstants.MainMenuSet3.XYPHOS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet3.VAELITH:
                Type = type;
                Image = ImageConstants.MainMenuSet3.VAELITH;
                await CreateRankControllerAsync(stat);
                break;

            // Set 4 (70 - 92)
            case AppConstants.MainMenuSet4.ZARX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.ZARX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.RAIK:
                Type = type;
                Image = ImageConstants.MainMenuSet4.RAIK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.DRAX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.DRAX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.KRON:
                Type = type;
                Image = ImageConstants.MainMenuSet4.KRON;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.ZOLT:
                Type = type;
                Image = ImageConstants.MainMenuSet4.ZOLT;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.GORR:
                Type = type;
                Image = ImageConstants.MainMenuSet4.GORR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.RYZE:
                Type = type;
                Image = ImageConstants.MainMenuSet4.RYZE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.JAXX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.JAXX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.THAR:
                Type = type;
                Image = ImageConstants.MainMenuSet4.THAR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.VORN:
                Type = type;
                Image = ImageConstants.MainMenuSet4.VORN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.NYX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.NYX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.AROS:
                Type = type;
                Image = ImageConstants.MainMenuSet4.AROS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.HEX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.HEX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.LORN:
                Type = type;
                Image = ImageConstants.MainMenuSet4.LORN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.BAXX:
                Type = type;
                Image = ImageConstants.MainMenuSet4.BAXX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.ZEPH:
                Type = type;
                Image = ImageConstants.MainMenuSet4.ZEPH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.KAEL:
                Type = type;
                Image = ImageConstants.MainMenuSet4.KAEL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.DRAV:
                Type = type;
                Image = ImageConstants.MainMenuSet4.DRAV;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.TORN:
                Type = type;
                Image = ImageConstants.MainMenuSet4.TORN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.MYRR:
                Type = type;
                Image = ImageConstants.MainMenuSet4.MYRR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.VASK:
                Type = type;
                Image = ImageConstants.MainMenuSet4.VASK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.JORR:
                Type = type;
                Image = ImageConstants.MainMenuSet4.JORR;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet4.QUEN:
                Type = type;
                Image = ImageConstants.MainMenuSet4.QUEN;
                await CreateRankControllerAsync(stat);
                break;

            // Set 5 (93 - 115)
            case AppConstants.MainMenuSet5.ASTRAL_VOICE:
                Type = type;
                Image = ImageConstants.MainMenuSet5.ASTRAL_VOICE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.BRANCH_BLADE_SONG:
                Type = type;
                Image = ImageConstants.MainMenuSet5.BRANCH_BLADE_SONG;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.CHAOS_JAZZ:
                Type = type;
                Image = ImageConstants.MainMenuSet5.CHAOS_JAZZ;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.CHAOTIC_METAL:
                Type = type;
                Image = ImageConstants.MainMenuSet5.CHAOTIC_METAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.DAWN_S_BLOOM:
                Type = type;
                Image = ImageConstants.MainMenuSet5.DAWN_S_BLOOM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.FANGED_METAL:
                Type = type;
                Image = ImageConstants.MainMenuSet5.FANGED_METAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.FREEDOM_BLUES:
                Type = type;
                Image = ImageConstants.MainMenuSet5.FREEDOM_BLUES;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.HORMONE_PUNK:
                Type = type;
                Image = ImageConstants.MainMenuSet5.HORMONE_PUNK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.INFERNO_METAL:
                Type = type;
                Image = ImageConstants.MainMenuSet5.INFERNO_METAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.KING_OF_THE_SUMMIT:
                Type = type;
                Image = ImageConstants.MainMenuSet5.KING_OF_THE_SUMMIT;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.MOONLIGHT_LULLABY:
                Type = type;
                Image = ImageConstants.MainMenuSet5.MOONLIGHT_LULLABY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.PHAETON_S_MELODY:
                Type = type;
                Image = ImageConstants.MainMenuSet5.PHAETON_S_MELODY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.POLAR_METAL:
                Type = type;
                Image = ImageConstants.MainMenuSet5.POLAR_METAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.PROTO_PUNK:
                Type = type;
                Image = ImageConstants.MainMenuSet5.PROTO_PUNK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.PUFFER_ELECTRO:
                Type = type;
                Image = ImageConstants.MainMenuSet5.PUFFER_ELECTRO;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.SHADOW_HARMONY:
                Type = type;
                Image = ImageConstants.MainMenuSet5.SHADOW_HARMONY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.SHOCKSTAR_DISCO:
                Type = type;
                Image = ImageConstants.MainMenuSet5.SHOCKSTAR_DISCO;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.SOUL_ROCK:
                Type = type;
                Image = ImageConstants.MainMenuSet5.SOUL_ROCK;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.SWING_JAZZ:
                Type = type;
                Image = ImageConstants.MainMenuSet5.SWING_JAZZ;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.THUNDER_METAL:
                Type = type;
                Image = ImageConstants.MainMenuSet5.THUNDER_METAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.WOODPECKER_ELECTRO:
                Type = type;
                Image = ImageConstants.MainMenuSet5.WOODPECKER_ELECTRO;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.YUNKUI_TALES:
                Type = type;
                Image = ImageConstants.MainMenuSet5.YUNKUI_TALES;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet5.CHIP:
                Type = type;
                Image = ImageConstants.MainMenuSet5.CHIP;
                await CreateRankControllerAsync(stat);
                break;

            // Set 6 (116 - 138)
            case AppConstants.MainMenuSet6.FLUX:
                Type = type;
                Image = ImageConstants.MainMenuSet6.FLUX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.NEXUS:
                Type = type;
                Image = ImageConstants.MainMenuSet6.NEXUS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.ECLIPSE:
                Type = type;
                Image = ImageConstants.MainMenuSet6.ECLIPSE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.OBLIVION:
                Type = type;
                Image = ImageConstants.MainMenuSet6.OBLIVION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.CATALYST:
                Type = type;
                Image = ImageConstants.MainMenuSet6.CATALYST;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.AXIOM:
                Type = type;
                Image = ImageConstants.MainMenuSet6.AXIOM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.PARALLAX:
                Type = type;
                Image = ImageConstants.MainMenuSet6.PARALLAX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.ENTROPY:
                Type = type;
                Image = ImageConstants.MainMenuSet6.ENTROPY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.SINGULARITY:
                Type = type;
                Image = ImageConstants.MainMenuSet6.SINGULARITY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.GENESIS:
                Type = type;
                Image = ImageConstants.MainMenuSet6.GENESIS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.INFERNUM:
                Type = type;
                Image = ImageConstants.MainMenuSet6.INFERNUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.ELYSIUM:
                Type = type;
                Image = ImageConstants.MainMenuSet6.ELYSIUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.APOTHEON:
                Type = type;
                Image = ImageConstants.MainMenuSet6.APOTHEON;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.PARAGON:
                Type = type;
                Image = ImageConstants.MainMenuSet6.PARAGON;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.NULLITY:
                Type = type;
                Image = ImageConstants.MainMenuSet6.NULLITY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.CATACLYSM:
                Type = type;
                Image = ImageConstants.MainMenuSet6.CATACLYSM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.EMPYREAN:
                Type = type;
                Image = ImageConstants.MainMenuSet6.EMPYREAN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.HYPERION:
                Type = type;
                Image = ImageConstants.MainMenuSet6.HYPERION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.DOMINION:
                Type = type;
                Image = ImageConstants.MainMenuSet6.DOMINION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.ZENITH:
                Type = type;
                Image = ImageConstants.MainMenuSet6.ZENITH;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.OBLIVIUM:
                Type = type;
                Image = ImageConstants.MainMenuSet6.OBLIVIUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.HELIX:
                Type = type;
                Image = ImageConstants.MainMenuSet6.HELIX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet6.UMBRA:
                Type = type;
                Image = ImageConstants.MainMenuSet6.UMBRA;
                await CreateRankControllerAsync(stat);
                break;

            // Set 7 (139 - 161)
            case AppConstants.MainMenuSet7.AXIOMATA:
                Type = type;
                Image = ImageConstants.MainMenuSet7.AXIOMATA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.CONTINUUM:
                Type = type;
                Image = ImageConstants.MainMenuSet7.CONTINUUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.NOVA:
                Type = type;
                Image = ImageConstants.MainMenuSet7.NOVA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.PARADOX:
                Type = type;
                Image = ImageConstants.MainMenuSet7.PARADOX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.ABYSSAL:
                Type = type;
                Image = ImageConstants.MainMenuSet7.ABYSSAL;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.ARCANE:
                Type = type;
                Image = ImageConstants.MainMenuSet7.ARCANE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.ETERNUM:
                Type = type;
                Image = ImageConstants.MainMenuSet7.ETERNUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.LUMINARY:
                Type = type;
                Image = ImageConstants.MainMenuSet7.LUMINARY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.COSMOS:
                Type = type;
                Image = ImageConstants.MainMenuSet7.COSMOS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.ASTRION:
                Type = type;
                Image = ImageConstants.MainMenuSet7.ASTRION;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.NEOTERRA:
                Type = type;
                Image = ImageConstants.MainMenuSet7.NEOTERRA;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.HORIZON:
                Type = type;
                Image = ImageConstants.MainMenuSet7.HORIZON;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.NEXARIUM:
                Type = type;
                Image = ImageConstants.MainMenuSet7.NEXARIUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.CHRONYX:
                Type = type;
                Image = ImageConstants.MainMenuSet7.CHRONYX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.FERRUMAX:
                Type = type;
                Image = ImageConstants.MainMenuSet7.FERRUMAX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.COGNITUM:
                Type = type;
                Image = ImageConstants.MainMenuSet7.COGNITUM;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.ASHFRAME:
                Type = type;
                Image = ImageConstants.MainMenuSet7.ASHFRAME;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.THRENODY:
                Type = type;
                Image = ImageConstants.MainMenuSet7.THRENODY;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.MORVANE:
                Type = type;
                Image = ImageConstants.MainMenuSet7.MORVANE;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.VELKRYN:
                Type = type;
                Image = ImageConstants.MainMenuSet7.VELKRYN;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.XARPHIS:
                Type = type;
                Image = ImageConstants.MainMenuSet7.XARPHIS;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.OMNIVEX:
                Type = type;
                Image = ImageConstants.MainMenuSet7.OMNIVEX;
                await CreateRankControllerAsync(stat);
                break;
            case AppConstants.MainMenuSet7.KAELTHRA:
                Type = type;
                Image = ImageConstants.MainMenuSet7.KAELTHRA;
                await CreateRankControllerAsync(stat);
                break;

            default:
                break;
        }
    }
    public async Task CreateRankControllerAsync(IStats stat)
    {
        GameObject currentObject = Instantiate(PopupRankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Content = transform.Find("Scroll View/Viewport/Content");
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);

        });
        Dictionary<string, FeatureRankDTO> uniqueTypes = new Dictionary<string, FeatureRankDTO>();
        uniqueTypes = await FeaturesService.Create().GetRankFeaturesByTypeAsync(Type, stat);
        uniqueTypes = uniqueTypes
            .OrderBy(kvp =>
            {
                var match = Regex.Match(kvp.Value.FeatureName, @"\d+$");
                return match.Success ? int.Parse(match.Value) : 0;
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        FeatureList = uniqueTypes.ToList();
        CurrentPage = 0;
        Stat = stat;
        SetupPagination(currentObject);
        RenderPage();
    }

    private void RenderPage()
    {
        // 1. Dọn dẹp các Prefab UI cũ ở trang trước
        foreach (Transform child in Content)
            Destroy(child.gameObject);

        if (FeatureList == null || FeatureList.Count == 0) return;

        // 2. Tính toán dải Index phân trang Client-side
        int start = CurrentPage * ITEMS_PER_PAGE;
        int end = Mathf.Min(start + ITEMS_PER_PAGE, FeatureList.Count);

        int userLevel = User.CurrentUserLevel; // Cache level để tránh gọi Property nhiều lần trong vòng lặp

        for (int i = start; i < end; i++)
        {
            var kvp = FeatureList[i];

            // Tạo bản copy của giá trị để tránh lỗi bộ nhớ Closure trong Lambda Listener
            string currentSubtype = kvp.Key;
            int currentRequiredLevel = kvp.Value.RequiredLevel;
            string currentFeatureId = kvp.Value.Id;
            int displayIndex = i + 1;
            bool isLocked = currentRequiredLevel > userLevel;

            // Sinh đối tượng Prefab nút bấm
            GameObject button = Instantiate(PopupRankButtonPrefab, Content);
            Transform btnTransform = button.transform; // Cache transform của button

            // 3. Tối ưu tìm kiếm và gán Text (Dùng chuỗi format thay vì Replace trùng lặp)
            string processedText = currentSubtype.Replace("_", " ");

            TextMeshProUGUI buttonText = btnTransform.Find("ContentText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null) buttonText.text = processedText;

            TextMeshProUGUI buttonText2 = btnTransform.Find("MainTitleText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText2 != null) buttonText2.text = processedText;

            TextMeshProUGUI quantityText = btnTransform.Find("QuantityText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (quantityText != null) quantityText.text = displayIndex.ToString();

            // 4. Xử lý trạng thái khóa/mở khóa cấp độ
            Transform warningLevel = btnTransform.Find("WarningLevel");
            if (warningLevel != null)
            {
                warningLevel.gameObject.SetActive(isLocked);
                if (isLocked)
                {
                    TextMeshProUGUI levelText = warningLevel.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
                    if (levelText != null) levelText.text = currentRequiredLevel.ToString();
                }
            }

            // 5. Gán sự kiện click chuột an toàn (Safe Event Binding)
            Button btn = button.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners(); // Đảm bảo sạch listener
                btn.onClick.AddListener(async () =>
                {
                    if (isLocked)
                    {
                        AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                        return;
                    }

                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                    await CreateMainRankPanelAsync(currentFeatureId, currentSubtype);
                });
            }
        }
    }

    private void SetupPagination(GameObject currentObject)
    {
        PaginationManager = currentObject.transform.Find("PaginationPanelPrefab")?.GetComponent<PaginationManager>();

        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;
            PaginationManager.OnPageChanged += OnPageSelected;

            // Vẽ dải nút số phân trang trên UI
            PaginationManager.InitPagination(FeatureList.Count, ITEMS_PER_PAGE, CurrentPage + 1);

            // QUAN TRỌNG: Vẽ luôn dữ liệu trang hiện tại lên khung Content khi vừa setup xong
            RenderPage();
        }
        else
        {
            Debug.LogError("Không tìm thấy component PaginationManager trong 'Pagination'!");
        }
    }

    private void OnPageSelected(int pageNumber)
    {
        CurrentPage = pageNumber - 1;
        RenderPage();
    }

    private void ResetOrUpdatePagination()
    {
        if (PaginationManager != null)
        {
            PaginationManager.OnPageChanged -= OnPageSelected;

            CurrentPage = 0;

            PaginationManager.InitPagination(FeatureList.Count, ITEMS_PER_PAGE, 1);

            // Vẽ lại dữ liệu của Trang 1 sau khi thực hiện filter/search thành công
            RenderPage();

            PaginationManager.OnPageChanged += OnPageSelected;
        }
    }

    public async Task CreateMainRankPanelAsync(string featureId, string featureName)
    {
        GameObject currentObject = Instantiate(MainRankPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Button upgradeLevelButton = transform.Find("UpgradeLevelButton").GetComponent<Button>();
        Transform leftSideContent = transform.Find("LeftSideContent");
        Transform rightSideContent = transform.Find("RightSideContent");
        TextMeshProUGUI levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Button closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            Destroy(currentObject);
        });
        Button homeButton = transform.Find("HomeButton").GetComponent<Button>();
        homeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            ButtonEvent.Instance.Close(MainPanel);

        });

        RawImage mapImage = transform.Find("MapImage").GetComponent<RawImage>();
        Texture mapTexture = TextureHelper.LoadTexture2DCached("UI/Background2/Chapter_14");
        mapImage.texture = mapTexture;
        RawImage rankImage = transform.Find("GroupBackground/RankImage").GetComponent<RawImage>();
        // Texture rankTexture = TextureHelper.LoadTexture2DCached($"UI/Rank_Research/{AppConstants.MainMenuSet7.MASTER_OF_ATOMIC}");
        // rankImage.texture = rankTexture;
        RawImage background = transform.Find("Background").GetComponent<RawImage>();
        background.texture = TextureHelper.LoadTexture2DCached(Image);

        AnimationController.Instance.CreateRankAnimation(currentObject);
        Ranks rank = await RanksService.Create().GetRankByIdAsync(featureId);
        UserRanks userRank = await UserRanksService.Create().GetUserRanksAsync(User.CurrentUserId, featureId, Stat);
        List<RecipeItemDto> recipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userRank.Level, User.CurrentUserId);

        if (recipeItems == null || recipeItems.Count == 0)
            return;

        // Xoá item cũ nếu có
        foreach (Transform child in leftSideContent)
            Destroy(child.gameObject);

        foreach (Transform child in rightSideContent)
            Destroy(child.gameObject);

        int total = recipeItems.Count;
        int leftCount = Mathf.CeilToInt(total / 2f);

        for (int i = 0; i < total; i++)
        {
            Transform parent = (i < leftCount)
                ? leftSideContent
                : rightSideContent;

            GameObject itemGO = Instantiate(RankItemPrefab, parent);

            SetupRankItemUI(itemGO, recipeItems[i]);
        }

        int currentLevel = userRank?.Level ?? 0;
        levelText.text = currentLevel.ToString();
        async Task RefreshPanelAsync()
        {
            userRank = await UserRanksService.Create().GetUserRanksAsync(User.CurrentUserId, featureId, Stat);
            currentLevel = userRank?.Level ?? 0;
            levelText.text = currentLevel.ToString();

            List<RecipeItemDto> refreshedRecipeItems = await RecipeService.Create().GetRecipeItemsAsync(featureName, userRank.Level, User.CurrentUserId);
            if (refreshedRecipeItems == null)
                return;

            foreach (Transform child in leftSideContent)
                Destroy(child.gameObject);
            foreach (Transform child in rightSideContent)
                Destroy(child.gameObject);

            int refreshedTotal = refreshedRecipeItems.Count;
            int refreshedLeftCount = Mathf.CeilToInt(refreshedTotal / 2f);

            for (int i = 0; i < refreshedTotal; i++)
            {
                Transform parent = (i < refreshedLeftCount)
                    ? leftSideContent
                    : rightSideContent;

                GameObject itemGO = Instantiate(RankItemPrefab, parent);
                SetupRankItemUI(itemGO, refreshedRecipeItems[i]);
            }
        }


        // Popup that allows upgrading multiple levels (wired to UpgradeLevelButton)
        void CreatePopupUpgradePanelAsync()
        {
            GameObject gameObject =
                Instantiate(PopupRankQuantityPanelPrefab, MainPanel);

            Transform panelTransform = gameObject.transform;

            TextMeshProUGUI currentLevelText = panelTransform.Find("CurrentLevel").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI nextLevelText = panelTransform.Find("NextLevel").GetComponent<TextMeshProUGUI>();
            Slider quantitySlider = panelTransform.Find("QuantitySlider").GetComponent<Slider>();
            // TextMeshProUGUI userItemQuantityText = panelTransform.Find("UserItemQuantityText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemUsedQuantityText = panelTransform.Find("ItemUsedQuantityText").GetComponent<TextMeshProUGUI>();
            // RawImage userItemImage = panelTransform.Find("UserItemImage").GetComponent<RawImage>();
            // RawImage itemUsedImage = panelTransform.Find("ItemUsedImage").GetComponent<RawImage>();
            TextMeshProUGUI notificationText = panelTransform.Find("Notification/ContentText").GetComponent<TextMeshProUGUI>();
            Button increaseOneButton = panelTransform.Find("IncreaseOneButton").GetComponent<Button>();
            Button increaseTenButton = panelTransform.Find("IncreaseTenButton").GetComponent<Button>();
            Button increaseMaxButton = panelTransform.Find("IncreaseMaxButton").GetComponent<Button>();
            Button decreaseOneButton = panelTransform.Find("DecreaseOneButton").GetComponent<Button>();
            Button decreaseTenButton = panelTransform.Find("DecreaseTenButton").GetComponent<Button>();
            Button decreaseMaxButton = panelTransform.Find("DecreaseMaxButton").GetComponent<Button>();
            Button confirmButton = panelTransform.Find("ConfirmButton").GetComponent<Button>();
            Button closeButton = panelTransform.Find("CloseButton").GetComponent<Button>();
            Transform currentStatsContent = panelTransform.Find("Scroll View/Viewport/Content/CurrentStats");
            Transform nextStatsContent = panelTransform.Find("Scroll View/Viewport/Content/NextStats");

            int popupCurrentLevel = currentLevel;
            int maxLevel = rank != null ? rank.MaxLevel : popupCurrentLevel;
            int maxPossible = Mathf.Max(0, maxLevel - popupCurrentLevel);

            currentLevelText.text = popupCurrentLevel.ToString();
            nextLevelText.text = (popupCurrentLevel + 1).ToString();

            if (userRank != null)
            {
                StatsManager.Instance.CreateStatsManager(userRank, currentStatsContent);
                StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);
            }

            quantitySlider.minValue = 1;
            quantitySlider.maxValue = Mathf.Max(1, maxPossible);
            quantitySlider.wholeNumbers = true;
            quantitySlider.value = 1;

            void SetPreviewNotification(string value, Color color)
            {
                notificationText.text = LocalizationManager.Get(value);
                notificationText.color = color;
            }

            async void UpdatePreview()
            {
                await UpdatePreviewAsync();
            }

            async Task UpdatePreviewAsync()
            {
                int requested = (int)quantitySlider.value;

                if (maxPossible <= 0)
                {
                    var backgroundImage = confirmButton.transform.Find("Background2")?.GetComponent<RawImage>();
                    if (backgroundImage != null)
                        backgroundImage.color = Color.gray;

                    SetPreviewNotification(MessageConstants.UPGRADE_ALREADY_MAX, Color.red);
                    nextLevelText.text = "MAX";
                    confirmButton.interactable = true;
                    itemUsedQuantityText.text = "0";

                    if (userRank != null)
                        StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);

                    return;
                }

                var preview = await UpgradeFunctionHelper.PreviewUpgradeAsync(
                    featureName,
                    popupCurrentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (!preview.Success)
                {
                    SetPreviewNotification(preview.Message, Color.red);
                    confirmButton.interactable = false;
                    nextLevelText.text = preview.TargetLevel.ToString();
                    itemUsedQuantityText.text = "0";
                    // userItemQuantityText.text = "0";
                    return;
                }

                nextLevelText.text = preview.TargetLevel.ToString();
                confirmButton.interactable = preview.UpgradedLevels > 0;

                if (preview.UpgradedLevels > 0)
                {
                    UserRanks previewRank = userRank.CloneUserRank(userRank);
                    EnhanceHelper.EnhanceRanks(previewRank, preview.UpgradedLevels, rank.BaseMultiplier);
                    StatsManager.Instance.CreateStatsManager(previewRank, nextStatsContent);
                }
                else if (userRank != null)
                {
                    StatsManager.Instance.CreateStatsManager(userRank, nextStatsContent);
                }

                bool hasEnough = true;
                if (preview.RequiredItems != null && preview.RequiredItems.Count > 0)
                {
                    var first = preview.RequiredItems.First();
                    string firstItemId = first.Key;
                    double requiredQty = first.Value;

                    var recipeLevelItems = await RecipeService.Create()
                        .GetRecipeItemsAsync(featureName, popupCurrentLevel + 1, User.CurrentUserId);

                    double owned = 0;
                    string imagePath = null;
                    if (recipeLevelItems != null)
                    {
                        var match = recipeLevelItems.FirstOrDefault(x => x.ItemId == firstItemId);
                        if (match != null)
                        {
                            owned = match.UserQuantity;
                            imagePath = match.ItemImage;
                        }
                    }

                    itemUsedQuantityText.text = requiredQty.ToString();
                    // userItemQuantityText.text = owned.ToString();

                    if (owned < requiredQty)
                    {
                        hasEnough = false;
                    }

                    Texture tex = null;
                    if (!string.IsNullOrEmpty(imagePath))
                        tex = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(imagePath));

                    if (tex != null)
                    {
                        // itemUsedImage.texture = tex;
                        // userItemImage.texture = tex;
                    }
                }
                else
                {
                    itemUsedQuantityText.text = "0";
                    // userItemQuantityText.text = "0";
                }

                if (preview.UpgradedLevels > 0 && hasEnough)
                {
                    SetPreviewNotification(MessageConstants.READY_TO_UPGRADE, Color.green);
                }
                else
                {
                    SetPreviewNotification(MessageConstants.NOT_ENOUGH_MATERIALS, Color.red);
                    confirmButton.interactable = false;
                }
            }

            quantitySlider.onValueChanged.AddListener(_ => UpdatePreview());

            increaseOneButton.onClick.AddListener(() =>
            {
                quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 1);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            });
            increaseTenButton.onClick.AddListener(() =>
            {
                quantitySlider.value = Mathf.Min(quantitySlider.maxValue, quantitySlider.value + 10);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            });
            increaseMaxButton.onClick.AddListener(async () =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                quantitySlider.SetValueWithoutNotify(quantitySlider.maxValue);
                await UpdatePreviewAsync();
            });

            decreaseOneButton.onClick.AddListener(() =>
            {
                quantitySlider.value = Mathf.Max(quantitySlider.minValue, quantitySlider.value - 1);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            });
            decreaseTenButton.onClick.AddListener(() =>
            {
                quantitySlider.value = Mathf.Max(quantitySlider.minValue, quantitySlider.value - 10);
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            });
            decreaseMaxButton.onClick.AddListener(() =>
            {
                quantitySlider.value = quantitySlider.minValue;
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            });

            UpdatePreview();

            confirmButton.onClick.AddListener(async () =>
            {
                if (popupCurrentLevel >= maxLevel)
                {
                    notificationText.text = MessageConstants.UPGRADE_ALREADY_MAX;
                    notificationText.color = Color.red;
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.REJECT_SOUND);
                    return;
                }

                AudioManager.Instance.PlaySFX(AudioConstants.SFX.LEVEL_UP_SOUND);

                int requested = (int)quantitySlider.value;
                var result = await UpgradeFunctionHelper.UpgradeLevelAsync(
                    featureName,
                    popupCurrentLevel,
                    maxLevel,
                    requested,
                    User.CurrentUserId);

                if (result.Success)
                {
                    userRank = EnhanceHelper.EnhanceRanks(userRank, result.UpgradedLevels, rank.BaseMultiplier);
                    var insertOrUpdateResult = await UserRanksService.Create().InsertOrUpdateUserRanksAsync(User.CurrentUserId, userRank, Stat);

                    if (insertOrUpdateResult.Data && insertOrUpdateResult.IsChangePower
                    && (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated || insertOrUpdateResult.OperationType == DatabaseOperationType.Inserted))
                    {
                        PowerResultDTO powerResult = await UserService.Create().UpdateUserPowerAsync();

                        if (powerResult.HasChanged)
                        {
                            PowerController.Instance.ShowPower(
                                powerResult.CurrentPower,
                                powerResult.Difference,
                                1
                            );
                        }
                    }

                    Destroy(gameObject);
                    await RefreshPanelAsync();
                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioConstants.SFX.ALERT_SOUND);
                    notificationText.text = result.Message;
                }
            });

            closeButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
                Destroy(gameObject);
            });
        }

        upgradeLevelButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX(AudioConstants.SFX.BUTTON_CLICK_SOUND);
            CreatePopupUpgradePanelAsync();
        });
    }

    private void SetupRankItemUI(GameObject itemGO, RecipeItemDto data)
    {
        // TextMeshProUGUI nameText =
        //     itemGO.transform.Find("ItemName")
        //     .GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI requiredText =
            itemGO.transform.Find("RequiredText")
            .GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI ownedText =
            itemGO.transform.Find("AvailableText")
            .GetComponent<TextMeshProUGUI>();

        RawImage image =
            itemGO.transform.Find("Image")
            .GetComponent<RawImage>();

        // nameText.text = data.ItemId;

        requiredText.text = data.RequiredQuantity.ToString();
        ownedText.text = data.UserQuantity.ToString();

        // Nếu thiếu nguyên liệu -> đổi màu
        if (data.UserQuantity < data.RequiredQuantity)
            ownedText.color = Color.red;
        else
            ownedText.color = Color.green;

        // Load icon nếu có
        Texture texture = TextureHelper.LoadTexture2DCached(ImageHelper.RemoveImageExtension(data.ItemImage));
        if (texture != null)
            image.texture = texture;
    }
}
