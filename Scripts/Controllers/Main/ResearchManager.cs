using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour
{
    public static ResearchManager Instance { get; private set; }
    private Transform MainPanel;
    private GameObject ResearchPanelPrefab;
    private GameObject ResearchButtonPrefab;
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
        MainPanel = UIManager.Instance.GetTransform("MainPanel");
        ResearchPanelPrefab = UIManager.Instance.Get("ResearchPanelPrefab");
        ResearchButtonPrefab = UIManager.Instance.Get("ResearchButtonPrefab");
    }
    public void CreateResearch()
    {
        GameObject currentObject = Instantiate(ResearchPanelPrefab, MainPanel);
        Transform transform = currentObject.transform;
        Transform research1Panel = transform.Find("Scroll View/Viewport/Content/Research1/Content");
        Transform research2Panel = transform.Find("Scroll View/Viewport/Content/Research2/Content");
        Transform research3Panel = transform.Find("Scroll View/Viewport/Content/Research3/Content");
        Transform research4Panel = transform.Find("Scroll View/Viewport/Content/Research4/Content");
        Transform research5Panel = transform.Find("Scroll View/Viewport/Content/Research5/Content");
        Transform research6Panel = transform.Find("Scroll View/Viewport/Content/Research6/Content");
        Transform research7Panel = transform.Find("Scroll View/Viewport/Content/Research7/Content");
        Transform research8Panel = transform.Find("Scroll View/Viewport/Content/Research8/Content");
        Transform research9Panel = transform.Find("Scroll View/Viewport/Content/Research9/Content");
        Transform research10Panel = transform.Find("Scroll View/Viewport/Content/Research10/Content");

        CreateResearchButtonUI(1, AppDisplayConstants.Research.HOUSING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.HOUSING_URL), research1Panel);
        CreateResearchButtonUI(2, AppDisplayConstants.Research.INFRASTRUCTURE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.INFRASTRUCTURE_URL), research1Panel);
        CreateResearchButtonUI(3, AppDisplayConstants.Research.LOGISTICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.LOGISTICS_URL), research1Panel);
        CreateResearchButtonUI(4, AppDisplayConstants.Research.SANITATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SANITATION_URL), research1Panel);
        CreateResearchButtonUI(5, AppDisplayConstants.Research.TRANSPORTATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.TRANSPORTATION_URL), research1Panel);
        CreateResearchButtonUI(6, AppDisplayConstants.Research.URBANIZATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.URBANIZATION_URL), research1Panel);
        CreateResearchButtonUI(7, AppDisplayConstants.Research.UTILITIES, TextureHelper.LoadTexture2DCached(ImageConstants.Research.UTILITIES_URL), research1Panel);
        CreateResearchButtonUI(8, AppDisplayConstants.Research.WASTE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.WASTE_URL), research1Panel);
        CreateResearchButtonUI(9, AppDisplayConstants.Research.WATER, TextureHelper.LoadTexture2DCached(ImageConstants.Research.WATER_URL), research1Panel);
        CreateResearchButtonUI(10, AppDisplayConstants.Research.FACILITIES, TextureHelper.LoadTexture2DCached(ImageConstants.Research.FACILITIES_URL), research1Panel);

        CreateResearchButtonUI(11, AppDisplayConstants.Research.CONSTRUCTION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CONSTRUCTION_URL), research2Panel);
        CreateResearchButtonUI(12, AppDisplayConstants.Research.ENERGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ENERGY_URL), research2Panel);
        CreateResearchButtonUI(13, AppDisplayConstants.Research.ENGINEERING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ENGINEERING_URL), research2Panel);
        CreateResearchButtonUI(14, AppDisplayConstants.Research.INDUSTRY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.INDUSTRY_URL), research2Panel);
        CreateResearchButtonUI(15, AppDisplayConstants.Research.MANUFACTURING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MANUFACTURING_URL), research2Panel);
        CreateResearchButtonUI(16, AppDisplayConstants.Research.MATERIALS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MATERIALS_URL), research2Panel);
        CreateResearchButtonUI(17, AppDisplayConstants.Research.POWER, TextureHelper.LoadTexture2DCached(ImageConstants.Research.POWER_URL), research2Panel);
        CreateResearchButtonUI(18, AppDisplayConstants.Research.MECHANICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MECHANICS_URL), research2Panel);
        CreateResearchButtonUI(19, AppDisplayConstants.Research.RESOURCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.RESOURCE_URL), research2Panel);
        CreateResearchButtonUI(20, AppDisplayConstants.Research.SYSTEM, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SYSTEM_URL), research2Panel);

        CreateResearchButtonUI(21, AppDisplayConstants.Research.ARMOR, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ARMOR_URL), research3Panel);
        CreateResearchButtonUI(22, AppDisplayConstants.Research.DEFENSE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DEFENSE_URL), research3Panel);
        CreateResearchButtonUI(23, AppDisplayConstants.Research.DISASTER, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DISASTER_URL), research3Panel);
        CreateResearchButtonUI(24, AppDisplayConstants.Research.EMERGENCY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.EMERGENCY_URL), research3Panel);
        CreateResearchButtonUI(25, AppDisplayConstants.Research.MILITARY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MILITARY_URL), research3Panel);
        CreateResearchButtonUI(26, AppDisplayConstants.Research.SAFETY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SAFETY_URL), research3Panel);
        CreateResearchButtonUI(27, AppDisplayConstants.Research.SHIELDING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SHIELDING_URL), research3Panel);
        CreateResearchButtonUI(28, AppDisplayConstants.Research.WEAPONS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.WEAPONS_URL), research3Panel);
        CreateResearchButtonUI(29, AppDisplayConstants.Research.FORTIFICATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.FORTIFICATION_URL), research3Panel);
        CreateResearchButtonUI(30, AppDisplayConstants.Research.TACTICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.TACTICS_URL), research3Panel);

        CreateResearchButtonUI(31, AppDisplayConstants.Research.COMMERCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.COMMERCE_URL), research4Panel);
        CreateResearchButtonUI(32, AppDisplayConstants.Research.ECONOMY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ECONOMY_URL), research4Panel);
        CreateResearchButtonUI(33, AppDisplayConstants.Research.FINANCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.FINANCE_URL), research4Panel);
        CreateResearchButtonUI(34, AppDisplayConstants.Research.INVESTMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Research.INVESTMENT_URL), research4Panel);
        CreateResearchButtonUI(35, AppDisplayConstants.Research.PRODUCTIVITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.PRODUCTIVITY_URL), research4Panel);
        CreateResearchButtonUI(36, AppDisplayConstants.Research.TRADE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.TRADE_URL), research4Panel);
        CreateResearchButtonUI(37, AppDisplayConstants.Research.ENTERPRISE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ENTERPRISE_URL), research4Panel);
        CreateResearchButtonUI(38, AppDisplayConstants.Research.MARKET, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MARKET_URL), research4Panel);
        CreateResearchButtonUI(39, AppDisplayConstants.Research.SUPPLY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SUPPLY_URL), research4Panel);
        CreateResearchButtonUI(40, AppDisplayConstants.Research.DISTRIBUTION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DISTRIBUTION_URL), research4Panel);

        CreateResearchButtonUI(41, AppDisplayConstants.Research.CLIMATE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CLIMATE_URL), research5Panel);
        CreateResearchButtonUI(42, AppDisplayConstants.Research.CONSERVATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CONSERVATION_URL), research5Panel);
        CreateResearchButtonUI(43, AppDisplayConstants.Research.ECOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ECOLOGY_URL), research5Panel);
        CreateResearchButtonUI(44, AppDisplayConstants.Research.ENVIRONMENT, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ENVIRONMENT_URL), research5Panel);
        CreateResearchButtonUI(45, AppDisplayConstants.Research.POLLUTION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.POLLUTION_URL), research5Panel);
        CreateResearchButtonUI(46, AppDisplayConstants.Research.RECYCLING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.RECYCLING_URL), research5Panel);
        CreateResearchButtonUI(47, AppDisplayConstants.Research.SUSTAINABILITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SUSTAINABILITY_URL), research5Panel);
        CreateResearchButtonUI(48, AppDisplayConstants.Research.PRESERVATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.PRESERVATION_URL), research5Panel);
        CreateResearchButtonUI(49, AppDisplayConstants.Research.RENEWABLES, TextureHelper.LoadTexture2DCached(ImageConstants.Research.RENEWABLES_URL), research5Panel);
        CreateResearchButtonUI(50, AppDisplayConstants.Research.RESTORATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.RESTORATION_URL), research5Panel);

        CreateResearchButtonUI(51, AppDisplayConstants.Research.ASCENSION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ASCENSION_URL), research6Panel);
        CreateResearchButtonUI(52, AppDisplayConstants.Research.COLONIZATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.COLONIZATION_URL), research6Panel);
        CreateResearchButtonUI(53, AppDisplayConstants.Research.DIMENSIONAL, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DIMENSIONAL_URL), research6Panel);
        CreateResearchButtonUI(54, AppDisplayConstants.Research.EXPANSION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.EXPANSION_URL), research6Panel);
        CreateResearchButtonUI(55, AppDisplayConstants.Research.EXPLORATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.EXPLORATION_URL), research6Panel);
        CreateResearchButtonUI(56, AppDisplayConstants.Research.MEGASTRUCTURE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MEGASTRUCTURE_URL), research6Panel);
        CreateResearchButtonUI(57, AppDisplayConstants.Research.SINGULARITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SINGULARITY_URL), research6Panel);
        CreateResearchButtonUI(58, AppDisplayConstants.Research.TERRAFORMING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.TERRAFORMING_URL), research6Panel);
        CreateResearchButtonUI(59, AppDisplayConstants.Research.TIME, TextureHelper.LoadTexture2DCached(ImageConstants.Research.TIME_URL), research6Panel);
        CreateResearchButtonUI(60, AppDisplayConstants.Research.COSMOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.COSMOLOGY_URL), research6Panel);

        CreateResearchButtonUI(61, AppDisplayConstants.Research.EPIDEMIOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.EPIDEMIOLOGY_URL), research7Panel);
        CreateResearchButtonUI(62, AppDisplayConstants.Research.GENETICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.GENETICS_URL), research7Panel);
        CreateResearchButtonUI(63, AppDisplayConstants.Research.HEALTH, TextureHelper.LoadTexture2DCached(ImageConstants.Research.HEALTH_URL), research7Panel);
        CreateResearchButtonUI(64, AppDisplayConstants.Research.LONGEVITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.LONGEVITY_URL), research7Panel);
        CreateResearchButtonUI(65, AppDisplayConstants.Research.MEDICINE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.MEDICINE_URL), research7Panel);
        CreateResearchButtonUI(66, AppDisplayConstants.Research.BIOTECH, TextureHelper.LoadTexture2DCached(ImageConstants.Research.BIOTECH_URL), research7Panel);
        CreateResearchButtonUI(67, AppDisplayConstants.Research.IMMUNOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.IMMUNOLOGY_URL), research7Panel);
        CreateResearchButtonUI(68, AppDisplayConstants.Research.NUTRITION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.NUTRITION_URL), research7Panel);
        CreateResearchButtonUI(69, AppDisplayConstants.Research.PHARMACEUTICALS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.PHARMACEUTICALS_URL), research7Panel);
        CreateResearchButtonUI(70, AppDisplayConstants.Research.REGENERATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.REGENERATION_URL), research7Panel);

        CreateResearchButtonUI(71, AppDisplayConstants.Research.AI, TextureHelper.LoadTexture2DCached(ImageConstants.Research.AI_URL), research8Panel);
        CreateResearchButtonUI(72, AppDisplayConstants.Research.COMMUNICATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.COMMUNICATION_URL), research8Panel);
        CreateResearchButtonUI(73, AppDisplayConstants.Research.CYBERSECURITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CYBERSECURITY_URL), research8Panel);
        CreateResearchButtonUI(74, AppDisplayConstants.Research.DATA, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DATA_URL), research8Panel);
        CreateResearchButtonUI(75, AppDisplayConstants.Research.INFORMATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.INFORMATION_URL), research8Panel);
        CreateResearchButtonUI(76, AppDisplayConstants.Research.NETWORKING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.NETWORKING_URL), research8Panel);
        CreateResearchButtonUI(77, AppDisplayConstants.Research.SECURITY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SECURITY_URL), research8Panel);
        CreateResearchButtonUI(78, AppDisplayConstants.Research.SURVEILLANCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SURVEILLANCE_URL), research8Panel);
        CreateResearchButtonUI(79, AppDisplayConstants.Research.ANALYTICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ANALYTICS_URL), research8Panel);
        CreateResearchButtonUI(80, AppDisplayConstants.Research.CONTROL, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CONTROL_URL), research8Panel);

        CreateResearchButtonUI(81, AppDisplayConstants.Research.AUTOMATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.AUTOMATION_URL), research9Panel);
        CreateResearchButtonUI(82, AppDisplayConstants.Research.BIOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.BIOLOGY_URL), research9Panel);
        CreateResearchButtonUI(83, AppDisplayConstants.Research.CHEMISTRY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CHEMISTRY_URL), research9Panel);
        CreateResearchButtonUI(84, AppDisplayConstants.Research.COMPUTING, TextureHelper.LoadTexture2DCached(ImageConstants.Research.COMPUTING_URL), research9Panel);
        CreateResearchButtonUI(85, AppDisplayConstants.Research.NANOTECHNOLOGY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.NANOTECHNOLOGY_URL), research9Panel);
        CreateResearchButtonUI(86, AppDisplayConstants.Research.PHYSICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.PHYSICS_URL), research9Panel);
        CreateResearchButtonUI(87, AppDisplayConstants.Research.QUANTUM, TextureHelper.LoadTexture2DCached(ImageConstants.Research.QUANTUM_URL), research9Panel);
        CreateResearchButtonUI(88, AppDisplayConstants.Research.ROBOTICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.ROBOTICS_URL), research9Panel);
        CreateResearchButtonUI(89, AppDisplayConstants.Research.SCIENCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SCIENCE_URL), research9Panel);
        CreateResearchButtonUI(90, AppDisplayConstants.Research.INNOVATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.INNOVATION_URL), research9Panel);

        CreateResearchButtonUI(91, AppDisplayConstants.Research.CULTURE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CULTURE_URL), research10Panel);
        CreateResearchButtonUI(92, AppDisplayConstants.Research.DEMOGRAPHY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.DEMOGRAPHY_URL), research10Panel);
        CreateResearchButtonUI(93, AppDisplayConstants.Research.EDUCATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.EDUCATION_URL), research10Panel);
        CreateResearchButtonUI(94, AppDisplayConstants.Research.GOVERNANCE, TextureHelper.LoadTexture2DCached(ImageConstants.Research.GOVERNANCE_URL), research10Panel);
        CreateResearchButtonUI(95, AppDisplayConstants.Research.HAPPINESS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.HAPPINESS_URL), research10Panel);
        CreateResearchButtonUI(96, AppDisplayConstants.Research.LAW, TextureHelper.LoadTexture2DCached(ImageConstants.Research.LAW_URL), research10Panel);
        CreateResearchButtonUI(97, AppDisplayConstants.Research.POLICY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.POLICY_URL), research10Panel);
        CreateResearchButtonUI(98, AppDisplayConstants.Research.POPULATION, TextureHelper.LoadTexture2DCached(ImageConstants.Research.POPULATION_URL), research10Panel);
        CreateResearchButtonUI(99, AppDisplayConstants.Research.SOCIETY, TextureHelper.LoadTexture2DCached(ImageConstants.Research.SOCIETY_URL), research10Panel);
        CreateResearchButtonUI(100, AppDisplayConstants.Research.CIVICS, TextureHelper.LoadTexture2DCached(ImageConstants.Research.CIVICS_URL), research10Panel);

        CreateBaseInfrastructure(research1Panel);
        CreateCoreSystems(research2Panel);
        CreateDefenseSafety(research3Panel);
        CreateEconomyProduction(research4Panel);
        CreateEnvironmentSustainability(research5Panel);
        CreateExpansionExploration(research6Panel);
        CreateHealthLife(research7Panel);
        CreateInformationControl(research8Panel);
        CreateScienceTechnology(research9Panel);
        CreateSocietyPopulation(research10Panel);
    }
    private void CreateResearchButtonUI(int index, string itemName, Texture2D _itemImage, Transform panel)
    {
        // Tạo button từ prefab
        GameObject newButton = Instantiate(ResearchButtonPrefab, panel);
        newButton.name = "Button_" + index;

        // Gán hình ảnh cho itemImage
        RawImage image = newButton.transform.Find("Image").GetComponent<RawImage>();
        if (image != null && _itemImage != null)
        {
            image.texture = _itemImage;
        }

        // Gán tên cho itemName
        TextMeshProUGUI nameText = newButton.transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = LocalizationManager.Get(itemName);
        }
    }
    public void CreateBaseInfrastructure(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_1", panel, async () => await HousingManager.Instance.CreateHousingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, async () => await InfrastructureManager.Instance.CreateInfrastructureManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, async () => await LogisticsManager.Instance.CreateLogisticsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, async () => await SanitationManager.Instance.CreateSanitationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, async () => await TransportationManager.Instance.CreateTransportationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, async () => await UrbanizationManager.Instance.CreateUrbanizationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, async () => await UtilitiesManager.Instance.CreateUtilitiesManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, async () => await WasteManager.Instance.CreateWasteManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, async () => await WaterManager.Instance.CreateWaterManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, async () => await FacilitiesManager.Instance.CreateFacilitiesManagerAsync());
    }
    public void CreateCoreSystems(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, async () => await ConstructionManager.Instance.CreateConstructionManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, async () => await EnergyManager.Instance.CreateEnergyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, async () => await EngineeringManager.Instance.CreateEngineeringManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, async () => await IndustryManager.Instance.CreateIndustryManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, async () => await ManufacturingManager.Instance.CreateManufacturingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, async () => await MaterialsManager.Instance.CreateMaterialsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, async () => await PowerResearchManager.Instance.CreatePowerManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, async () => await MechanicsManager.Instance.CreateMechanicsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, async () => await ResourceManager.Instance.CreateResourceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, async () => await SystemManager.Instance.CreateSystemManagerAsync());
    }
    public void CreateDefenseSafety(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_21", panel, async () => await ArmorManager.Instance.CreateArmorManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_22", panel, async () => await DefenseManager.Instance.CreateDefenseManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_23", panel, async () => await DisasterManager.Instance.CreateDisasterManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_24", panel, async () => await EmergencyManager.Instance.CreateEmergencyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_25", panel, async () => await MilitaryManager.Instance.CreateMilitaryManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_26", panel, async () => await SafetyManager.Instance.CreateSafetyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_27", panel, async () => await ShieldingManager.Instance.CreateShieldingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_28", panel, async () => await WeaponsManager.Instance.CreateWeaponsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_29", panel, async () => await FortificationManager.Instance.CreateFortificationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_30", panel, async () => await TacticsManager.Instance.CreateTacticsManagerAsync());
    }
    public void CreateEconomyProduction(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_31", panel, async () => await CommerceManager.Instance.CreateCommerceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_32", panel, async () => await EconomyManager.Instance.CreateEconomyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_33", panel, async () => await FinanceManager.Instance.CreateFinanceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_34", panel, async () => await InvestmentManager.Instance.CreateInvestmentManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_35", panel, async () => await ProductivityManager.Instance.CreateProductivityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_36", panel, async () => await TradeManager.Instance.CreateTradeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_37", panel, async () => await EnterpriseManager.Instance.CreateEnterpriseManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_38", panel, async () => await MarketManager.Instance.CreateMarketManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_39", panel, async () => await SupplyManager.Instance.CreateSupplyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_40", panel, async () => await DistributionManager.Instance.CreateDistributionManagerAsync());
    }
    public void CreateEnvironmentSustainability(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_41", panel, async () => await ClimateManager.Instance.CreateClimateManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_42", panel, async () => await ConservationManager.Instance.CreateConservationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_43", panel, async () => await EcologyManager.Instance.CreateEcologyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_44", panel, async () => await EnvironmentManager.Instance.CreateEnvironmentManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_45", panel, async () => await PollutionManager.Instance.CreatePollutionManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_46", panel, async () => await RecyclingManager.Instance.CreateRecyclingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_47", panel, async () => await SustainabilityManager.Instance.CreateSustainabilityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_48", panel, async () => await PreservationManager.Instance.CreatePreservationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_49", panel, async () => await RenewablesManager.Instance.CreateRenewablesManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_50", panel, async () => await RestorationManager.Instance.CreateRestorationManagerAsync());
    }
    public void CreateExpansionExploration(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_51", panel, async () => await AscensionManager.Instance.CreateAscensionManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_52", panel, async () => await ColonizationManager.Instance.CreateColonizationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_53", panel, async () => await DimensionalManager.Instance.CreateDimensionalManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_54", panel, async () => await ExpansionManager.Instance.CreateExpansionManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_55", panel, async () => await ExplorationManager.Instance.CreateExplorationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_56", panel, async () => await MegastructureManager.Instance.CreateMegastructureManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_57", panel, async () => await SingularityManager.Instance.CreateSingularityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_58", panel, async () => await TerraformingManager.Instance.CreateTerraformingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_59", panel, async () => await TimeManager.Instance.CreateTimeManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_60", panel, async () => await CosmologyManager.Instance.CreateCosmologyManagerAsync());
    }
    public void CreateHealthLife(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_61", panel, async () => await EpidemiologyManager.Instance.CreateEpidemiologyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_62", panel, async () => await GeneticsManager.Instance.CreateGeneticsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_63", panel, async () => await HealthManager.Instance.CreateHealthManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_64", panel, async () => await LongevityManager.Instance.CreateLongevityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_65", panel, async () => await MedicineManager.Instance.CreateMedicineManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_66", panel, async () => await BiotechManager.Instance.CreateBiotechManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_67", panel, async () => await ImmunologyManager.Instance.CreateImmunologyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_68", panel, async () => await NutritionManager.Instance.CreateNutritionManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_69", panel, async () => await PharmaceuticalsManager.Instance.CreatePharmaceuticalsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_70", panel, async () => await RegenerationManager.Instance.CreateRegenerationManagerAsync());
    }
    public void CreateInformationControl(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_71", panel, async () => await AIManager.Instance.CreateAIManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_72", panel, async () => await CommunicationManager.Instance.CreateCommunicationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_73", panel, async () => await CybersecurityManager.Instance.CreateCybersecurityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_74", panel, async () => await DataManager.Instance.CreateDataManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_75", panel, async () => await InformationManager.Instance.CreateInformationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_76", panel, async () => await NetworkingManager.Instance.CreateNetworkingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_77", panel, async () => await SecurityManager.Instance.CreateSecurityManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_78", panel, async () => await SurveillanceManager.Instance.CreateSurveillanceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_79", panel, async () => await AnalyticsManager.Instance.CreateAnalyticsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_80", panel, async () => await ControlManager.Instance.CreateControlManagerAsync());
    }
    public void CreateScienceTechnology(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_81", panel, async () => await AutomationManager.Instance.CreateAutomationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_82", panel, async () => await BiologyManager.Instance.CreateBiologyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_83", panel, async () => await ChemistryManager.Instance.CreateChemistryManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_84", panel, async () => await ComputingManager.Instance.CreateComputingManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_85", panel, async () => await NanotechnologyManager.Instance.CreateNanotechnologyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_86", panel, async () => await PhysicsManager.Instance.CreatePhysicsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_87", panel, async () => await QuantumManager.Instance.CreateQuantumManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_88", panel, async () => await RoboticsManager.Instance.CreateRoboticsManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_89", panel, async () => await ScienceManager.Instance.CreateScienceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_90", panel, async () => await InnovationManager.Instance.CreateInnovationManagerAsync());
    }
    public void CreateSocietyPopulation(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_91", panel, async () => await CultureManager.Instance.CreateCultureManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_92", panel, async () => await DemographyManager.Instance.CreateDemographyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_93", panel, async () => await EducationManager.Instance.CreateEducationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_94", panel, async () => await GovernanceManager.Instance.CreateGovernanceManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_95", panel, async () => await HappinessManager.Instance.CreateHappinessManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_96", panel, async () => await LawManager.Instance.CreateLawManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_97", panel, async () => await PolicyManager.Instance.CreatePolicyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_98", panel, async () => await PopulationManager.Instance.CreatePopulationManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_99", panel, async () => await SocietyManager.Instance.CreateSocietyManagerAsync());
        ButtonEvent.Instance.AssignButtonEvent("Button_100", panel, async () => await CivicsManager.Instance.CreateCivicsManagerAsync());
    }
}
