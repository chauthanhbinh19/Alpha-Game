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
        Transform research1Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research1/Content");
        Transform research2Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research2/Content");
        Transform research3Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research3/Content");
        Transform research4Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research4/Content");
        Transform research5Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research5/Content");
        Transform research6Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research6/Content");
        Transform research7Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research7/Content");
        Transform research8Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research8/Content");
        Transform research9Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research9/Content");
        Transform research10Panel = currentObject.transform.Find("Scroll View/Viewport/Content/Research10/Content");

        CreateResearchButtonUI(1, AppDisplayConstants.Research.HOUSING, Resources.Load<Texture2D>(ImageConstants.Research.HOUSING_URL), research1Panel);
        CreateResearchButtonUI(2, AppDisplayConstants.Research.INFRASTRUCTURE, Resources.Load<Texture2D>(ImageConstants.Research.INFRASTRUCTURE_URL), research1Panel);
        CreateResearchButtonUI(3, AppDisplayConstants.Research.LOGISTICS, Resources.Load<Texture2D>(ImageConstants.Research.LOGISTICS_URL), research1Panel);
        CreateResearchButtonUI(4, AppDisplayConstants.Research.SANITATION, Resources.Load<Texture2D>(ImageConstants.Research.SANITATION_URL), research1Panel);
        CreateResearchButtonUI(5, AppDisplayConstants.Research.TRANSPORTATION, Resources.Load<Texture2D>(ImageConstants.Research.TRANSPORTATION_URL), research1Panel);
        CreateResearchButtonUI(6, AppDisplayConstants.Research.URBANIZATION, Resources.Load<Texture2D>(ImageConstants.Research.URBANIZATION_URL), research1Panel);
        CreateResearchButtonUI(7, AppDisplayConstants.Research.UTILITIES, Resources.Load<Texture2D>(ImageConstants.Research.UTILITIES_URL), research1Panel);
        CreateResearchButtonUI(8, AppDisplayConstants.Research.WASTE, Resources.Load<Texture2D>(ImageConstants.Research.WASTE_URL), research1Panel);
        CreateResearchButtonUI(9, AppDisplayConstants.Research.WATER, Resources.Load<Texture2D>(ImageConstants.Research.WATER_URL), research1Panel);

        CreateResearchButtonUI(10, AppDisplayConstants.Research.CONSTRUCTION, Resources.Load<Texture2D>(ImageConstants.Research.CONSTRUCTION_URL), research2Panel);
        CreateResearchButtonUI(11, AppDisplayConstants.Research.ENERGY, Resources.Load<Texture2D>(ImageConstants.Research.ENERGY_URL), research2Panel);
        CreateResearchButtonUI(12, AppDisplayConstants.Research.ENGINEERING, Resources.Load<Texture2D>(ImageConstants.Research.ENGINEERING_URL), research2Panel);
        CreateResearchButtonUI(13, AppDisplayConstants.Research.INDUSTRY, Resources.Load<Texture2D>(ImageConstants.Research.INDUSTRY_URL), research2Panel);
        CreateResearchButtonUI(14, AppDisplayConstants.Research.MANUFACTURING, Resources.Load<Texture2D>(ImageConstants.Research.MANUFACTURING_URL), research2Panel);
        CreateResearchButtonUI(15, AppDisplayConstants.Research.MATERIALS, Resources.Load<Texture2D>(ImageConstants.Research.MATERIALS_URL), research2Panel);
        CreateResearchButtonUI(16, AppDisplayConstants.Research.POWER, Resources.Load<Texture2D>(ImageConstants.Research.POWER_URL), research2Panel);

        CreateResearchButtonUI(17, AppDisplayConstants.Research.ARMOR, Resources.Load<Texture2D>(ImageConstants.Research.ARMOR_URL), research3Panel);
        CreateResearchButtonUI(18, AppDisplayConstants.Research.DEFENSE, Resources.Load<Texture2D>(ImageConstants.Research.DEFENSE_URL), research3Panel);
        CreateResearchButtonUI(19, AppDisplayConstants.Research.DISASTER, Resources.Load<Texture2D>(ImageConstants.Research.DISASTER_URL), research3Panel);
        CreateResearchButtonUI(20, AppDisplayConstants.Research.EMERGENCY, Resources.Load<Texture2D>(ImageConstants.Research.EMERGENCY_URL), research3Panel);
        CreateResearchButtonUI(21, AppDisplayConstants.Research.MILITARY, Resources.Load<Texture2D>(ImageConstants.Research.MILITARY_URL), research3Panel);
        CreateResearchButtonUI(22, AppDisplayConstants.Research.SAFETY, Resources.Load<Texture2D>(ImageConstants.Research.SAFETY_URL), research3Panel);
        CreateResearchButtonUI(23, AppDisplayConstants.Research.SHIELDING, Resources.Load<Texture2D>(ImageConstants.Research.SHIELDING_URL), research3Panel);
        CreateResearchButtonUI(24, AppDisplayConstants.Research.WEAPONS, Resources.Load<Texture2D>(ImageConstants.Research.WEAPONS_URL), research3Panel);

        CreateResearchButtonUI(25, AppDisplayConstants.Research.COMMERCE, Resources.Load<Texture2D>(ImageConstants.Research.COMMERCE_URL), research4Panel);
        CreateResearchButtonUI(26, AppDisplayConstants.Research.ECONOMY, Resources.Load<Texture2D>(ImageConstants.Research.ECONOMY_URL), research4Panel);
        CreateResearchButtonUI(27, AppDisplayConstants.Research.FINANCE, Resources.Load<Texture2D>(ImageConstants.Research.FINANCE_URL), research4Panel);
        CreateResearchButtonUI(28, AppDisplayConstants.Research.INVESTMENT, Resources.Load<Texture2D>(ImageConstants.Research.INVESTMENT_URL), research4Panel);
        CreateResearchButtonUI(29, AppDisplayConstants.Research.PRODUCTIVITY, Resources.Load<Texture2D>(ImageConstants.Research.PRODUCTIVITY_URL), research4Panel);
        CreateResearchButtonUI(30, AppDisplayConstants.Research.TRADE, Resources.Load<Texture2D>(ImageConstants.Research.TRADE_URL), research4Panel);

        CreateResearchButtonUI(31, AppDisplayConstants.Research.CLIMATE, Resources.Load<Texture2D>(ImageConstants.Research.CLIMATE_URL), research5Panel);
        CreateResearchButtonUI(32, AppDisplayConstants.Research.CONSERVATION, Resources.Load<Texture2D>(ImageConstants.Research.CONSERVATION_URL), research5Panel);
        CreateResearchButtonUI(33, AppDisplayConstants.Research.ECOLOGY, Resources.Load<Texture2D>(ImageConstants.Research.ECOLOGY_URL), research5Panel);
        CreateResearchButtonUI(34, AppDisplayConstants.Research.ENVIRONMENT, Resources.Load<Texture2D>(ImageConstants.Research.ENVIRONMENT_URL), research5Panel);
        CreateResearchButtonUI(35, AppDisplayConstants.Research.POLLUTION, Resources.Load<Texture2D>(ImageConstants.Research.POLLUTION_URL), research5Panel);
        CreateResearchButtonUI(36, AppDisplayConstants.Research.RECYCLING, Resources.Load<Texture2D>(ImageConstants.Research.RECYCLING_URL), research5Panel);
        CreateResearchButtonUI(37, AppDisplayConstants.Research.SUSTAINABILITY, Resources.Load<Texture2D>(ImageConstants.Research.SUSTAINABILITY_URL), research5Panel);

        CreateResearchButtonUI(38, AppDisplayConstants.Research.ASCENSION, Resources.Load<Texture2D>(ImageConstants.Research.ASCENSION_URL), research6Panel);
        CreateResearchButtonUI(39, AppDisplayConstants.Research.COLONIZATION, Resources.Load<Texture2D>(ImageConstants.Research.COLONIZATION_URL), research6Panel);
        CreateResearchButtonUI(40, AppDisplayConstants.Research.DIMENSIONAL, Resources.Load<Texture2D>(ImageConstants.Research.DIMENSIONAL_URL), research6Panel);
        CreateResearchButtonUI(41, AppDisplayConstants.Research.EXPANSION, Resources.Load<Texture2D>(ImageConstants.Research.EXPANSION_URL), research6Panel);
        CreateResearchButtonUI(42, AppDisplayConstants.Research.EXPLORATION, Resources.Load<Texture2D>(ImageConstants.Research.EXPLORATION_URL), research6Panel);
        CreateResearchButtonUI(43, AppDisplayConstants.Research.MEGASTRUCTURE, Resources.Load<Texture2D>(ImageConstants.Research.MEGASTRUCTURE_URL), research6Panel);
        CreateResearchButtonUI(44, AppDisplayConstants.Research.SINGULARITY, Resources.Load<Texture2D>(ImageConstants.Research.SINGULARITY_URL), research6Panel);
        CreateResearchButtonUI(45, AppDisplayConstants.Research.TERRAFORMING, Resources.Load<Texture2D>(ImageConstants.Research.TERRAFORMING_URL), research6Panel);
        CreateResearchButtonUI(46, AppDisplayConstants.Research.TIME, Resources.Load<Texture2D>(ImageConstants.Research.TIME_URL), research6Panel);

        CreateResearchButtonUI(47, AppDisplayConstants.Research.EPIDEMIOLOGY, Resources.Load<Texture2D>(ImageConstants.Research.EPIDEMIOLOGY_URL), research7Panel);
        CreateResearchButtonUI(48, AppDisplayConstants.Research.GENETICS, Resources.Load<Texture2D>(ImageConstants.Research.GENETICS_URL), research7Panel);
        CreateResearchButtonUI(49, AppDisplayConstants.Research.HEALTH, Resources.Load<Texture2D>(ImageConstants.Research.HEALTH_URL), research7Panel);
        CreateResearchButtonUI(50, AppDisplayConstants.Research.LONGEVITY, Resources.Load<Texture2D>(ImageConstants.Research.LONGEVITY_URL), research7Panel);
        CreateResearchButtonUI(51, AppDisplayConstants.Research.MEDICINE, Resources.Load<Texture2D>(ImageConstants.Research.MEDICINE_URL), research7Panel);

        CreateResearchButtonUI(52, AppDisplayConstants.Research.AI, Resources.Load<Texture2D>(ImageConstants.Research.AI_URL), research8Panel);
        CreateResearchButtonUI(53, AppDisplayConstants.Research.COMMUNICATION, Resources.Load<Texture2D>(ImageConstants.Research.COMMUNICATION_URL), research8Panel);
        CreateResearchButtonUI(54, AppDisplayConstants.Research.CYBERSECURITY, Resources.Load<Texture2D>(ImageConstants.Research.CYBERSECURITY_URL), research8Panel);
        CreateResearchButtonUI(55, AppDisplayConstants.Research.DATA, Resources.Load<Texture2D>(ImageConstants.Research.DATA_URL), research8Panel);
        CreateResearchButtonUI(56, AppDisplayConstants.Research.INFORMATION, Resources.Load<Texture2D>(ImageConstants.Research.INFORMATION_URL), research8Panel);
        CreateResearchButtonUI(57, AppDisplayConstants.Research.NETWORKING, Resources.Load<Texture2D>(ImageConstants.Research.NETWORKING_URL), research8Panel);
        CreateResearchButtonUI(58, AppDisplayConstants.Research.SECURITY, Resources.Load<Texture2D>(ImageConstants.Research.SECURITY_URL), research8Panel);
        CreateResearchButtonUI(59, AppDisplayConstants.Research.SURVEILLANCE, Resources.Load<Texture2D>(ImageConstants.Research.SURVEILLANCE_URL), research8Panel);

        CreateResearchButtonUI(60, AppDisplayConstants.Research.AUTOMATION, Resources.Load<Texture2D>(ImageConstants.Research.AUTOMATION_URL), research9Panel);
        CreateResearchButtonUI(61, AppDisplayConstants.Research.BIOLOGY, Resources.Load<Texture2D>(ImageConstants.Research.BIOLOGY_URL), research9Panel);
        CreateResearchButtonUI(62, AppDisplayConstants.Research.CHEMISTRY, Resources.Load<Texture2D>(ImageConstants.Research.CHEMISTRY_URL), research9Panel);
        CreateResearchButtonUI(63, AppDisplayConstants.Research.COMPUTING, Resources.Load<Texture2D>(ImageConstants.Research.COMPUTING_URL), research9Panel);
        CreateResearchButtonUI(64, AppDisplayConstants.Research.NANOTECHNOLOGY, Resources.Load<Texture2D>(ImageConstants.Research.NANOTECHNOLOGY_URL), research9Panel);
        CreateResearchButtonUI(65, AppDisplayConstants.Research.PHYSICS, Resources.Load<Texture2D>(ImageConstants.Research.PHYSICS_URL), research9Panel);
        CreateResearchButtonUI(66, AppDisplayConstants.Research.QUANTUM, Resources.Load<Texture2D>(ImageConstants.Research.QUANTUM_URL), research9Panel);
        CreateResearchButtonUI(67, AppDisplayConstants.Research.ROBOTICS, Resources.Load<Texture2D>(ImageConstants.Research.ROBOTICS_URL), research9Panel);
        CreateResearchButtonUI(68, AppDisplayConstants.Research.SCIENCE, Resources.Load<Texture2D>(ImageConstants.Research.SCIENCE_URL), research9Panel);

        CreateResearchButtonUI(69, AppDisplayConstants.Research.CULTURE, Resources.Load<Texture2D>(ImageConstants.Research.CULTURE_URL), research10Panel);
        CreateResearchButtonUI(70, AppDisplayConstants.Research.DEMOGRAPHY, Resources.Load<Texture2D>(ImageConstants.Research.DEMOGRAPHY_URL), research10Panel);
        CreateResearchButtonUI(71, AppDisplayConstants.Research.EDUCATION, Resources.Load<Texture2D>(ImageConstants.Research.EDUCATION_URL), research10Panel);
        CreateResearchButtonUI(72, AppDisplayConstants.Research.GOVERNANCE, Resources.Load<Texture2D>(ImageConstants.Research.GOVERNANCE_URL), research10Panel);
        CreateResearchButtonUI(73, AppDisplayConstants.Research.HAPPINESS, Resources.Load<Texture2D>(ImageConstants.Research.HAPPINESS_URL), research10Panel);
        CreateResearchButtonUI(74, AppDisplayConstants.Research.LAW, Resources.Load<Texture2D>(ImageConstants.Research.LAW_URL), research10Panel);
        CreateResearchButtonUI(75, AppDisplayConstants.Research.POLICY, Resources.Load<Texture2D>(ImageConstants.Research.POLICY_URL), research10Panel);
        CreateResearchButtonUI(76, AppDisplayConstants.Research.POPULATION, Resources.Load<Texture2D>(ImageConstants.Research.POPULATION_URL), research10Panel);
        CreateResearchButtonUI(77, AppDisplayConstants.Research.SOCIETY, Resources.Load<Texture2D>(ImageConstants.Research.SOCIETY_URL), research10Panel);

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
        ButtonEvent.Instance.AssignButtonEvent("Button_2", panel, () => InfrastructureManager.Instance.CreateInfrastructureManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_3", panel, () => LogisticsManager.Instance.CreateLogisticsManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_4", panel, () => SanitationManager.Instance.CreateSanitationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_5", panel, () => TransportationManager.Instance.CreateTransportationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_6", panel, () => UrbanizationManager.Instance.CreateUrbanizationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_7", panel, () => UtilitiesManager.Instance.CreateUtilitiesManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_8", panel, () => WasteManager.Instance.CreateWasteManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_9", panel, () => WaterManager.Instance.CreateWaterManager());
    }
    public void CreateCoreSystems(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_10", panel, () => ConstructionManager.Instance.CreateConstructionManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_11", panel, () => EnergyManager.Instance.CreateEnergyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_12", panel, () => EngineeringManager.Instance.CreateEngineeringManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_13", panel, () => IndustryManager.Instance.CreateIndustryManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_14", panel, () => ManufacturingManager.Instance.CreateManufacturingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_15", panel, () => MaterialsManager.Instance.CreateMaterialsManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_16", panel, () => PowerResearchManager.Instance.CreatePowerManager());
    }
    public void CreateDefenseSafety(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_17", panel, () => ArmorManager.Instance.CreateArmorManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_18", panel, () => DefenseManager.Instance.CreateDefenseManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_19", panel, () => DisasterManager.Instance.CreateDisasterManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_20", panel, () => EmergencyManager.Instance.CreateEmergencyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_21", panel, () => MilitaryManager.Instance.CreateMilitaryManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_22", panel, () => SafetyManager.Instance.CreateSafetyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_23", panel, () => ShieldingManager.Instance.CreateShieldingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_24", panel, () => WeaponsManager.Instance.CreateWeaponsManager());
    }
    public void CreateEconomyProduction(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_25", panel, () => CommerceManager.Instance.CreateCommerceManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_26", panel, () => EconomyManager.Instance.CreateEconomyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_27", panel, () => FinanceManager.Instance.CreateFinanceManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_28", panel, () => InvestmentManager.Instance.CreateInvestmentManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_29", panel, () => ProductivityManager.Instance.CreateProductivityManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_30", panel, () => TradeManager.Instance.CreateTradeManager());
    }
    public void CreateEnvironmentSustainability(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_31", panel, () => ClimateManager.Instance.CreateClimateManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_32", panel, () => ConservationManager.Instance.CreateConservationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_33", panel, () => EcologyManager.Instance.CreateEcologyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_34", panel, () => EnvironmentManager.Instance.CreateEnvironmentManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_35", panel, () => PollutionManager.Instance.CreatePollutionManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_36", panel, () => RecyclingManager.Instance.CreateRecyclingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_37", panel, () => SustainabilityManager.Instance.CreateSustainabilityManager());
    }
    public void CreateExpansionExploration(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_38", panel, () => AscensionManager.Instance.CreateAscensionManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_39", panel, () => ColonizationManager.Instance.CreateColonizationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_40", panel, () => DimensionalManager.Instance.CreateDimensionalManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_41", panel, () => ExpansionManager.Instance.CreateExpansionManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_42", panel, () => ExplorationManager.Instance.CreateExplorationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_43", panel, () => MegastructureManager.Instance.CreateMegastructureManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_44", panel, () => SingularityManager.Instance.CreateSingularityManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_45", panel, () => TerraformingManager.Instance.CreateTerraformingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_46", panel, () => TimeManager.Instance.CreateTimeManager());
    }
    public void CreateHealthLife(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_47", panel, () => EpidemiologyManager.Instance.CreateEpidemiologyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_48", panel, () => GeneticsManager.Instance.CreateGeneticsManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_49", panel, () => HealthManager.Instance.CreateHealthManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_50", panel, () => LongevityManager.Instance.CreateLongevityManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_51", panel, () => MedicineManager.Instance.CreateMedicineManager());
    }
    public void CreateInformationControl(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_52", panel, () => AIManager.Instance.CreateAIManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_53", panel, () => CommunicationManager.Instance.CreateCommunicationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_54", panel, () => CybersecurityManager.Instance.CreateCybersecurityManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_55", panel, () => DataManager.Instance.CreateDataManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_56", panel, () => InformationManager.Instance.CreateInformationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_57", panel, () => NetworkingManager.Instance.CreateNetworkingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_58", panel, () => SecurityManager.Instance.CreateSecurityManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_59", panel, () => SurveillanceManager.Instance.CreateSurveillanceManager());
    }
    public void CreateScienceTechnology(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_60", panel, () => AutomationManager.Instance.CreateAutomationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_61", panel, () => BiologyManager.Instance.CreateBiologyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_62", panel, () => ChemistryManager.Instance.CreateChemistryManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_63", panel, () => ComputingManager.Instance.CreateComputingManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_64", panel, () => NanotechnologyManager.Instance.CreateNanotechnologyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_65", panel, () => PhysicsManager.Instance.CreatePhysicsManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_66", panel, () => QuantumManager.Instance.CreateQuantumManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_67", panel, () => RoboticsManager.Instance.CreateRoboticsManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_68", panel, () => ScienceManager.Instance.CreateScienceManager());
    }
    public void CreateSocietyPopulation(Transform panel)
    {
        ButtonEvent.Instance.AssignButtonEvent("Button_69", panel, () => CultureManager.Instance.CreateCultureManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_70", panel, () => DemographyManager.Instance.CreateDemographyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_71", panel, () => EducationManager.Instance.CreateEducationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_72", panel, () => GovernanceManager.Instance.CreateGovernanceManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_73", panel, () => HappinessManager.Instance.CreateHappinessManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_74", panel, () => LawManager.Instance.CreateLawManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_75", panel, () => PolicyManager.Instance.CreatePolicyManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_76", panel, () => PopulationManager.Instance.CreatePopulationManager());
        ButtonEvent.Instance.AssignButtonEvent("Button_77", panel, () => SocietyManager.Instance.CreateSocietyManager());
    }
}
