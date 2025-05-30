using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadTeams : MonoBehaviour
{
    private float currentHealth;
    public Transform playerCardField;
    public Transform enemyCardField;
    public GameObject CardModelPrefab;
    public GameObject healthBarPrefab;
    private HealthBar healthBar;
    private string type;
    void Start()
    {
        // currentHealth = maxHealth;

        // // Instantiate thanh máu
        // GameObject hb = Instantiate(healthBarPrefab, card.transform);
        // hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
        // healthBar = hb.GetComponent<HealthBar>();
        // if (healthBar == null)
        //     healthBar = hb.GetComponentInChildren<HealthBar>();

        // if (healthBar == null)
        // {
        //     Debug.LogError("Không tìm thấy script HealthBar trong prefab!");
        // }
        // else
        // {
        //     healthBar.SetMaxHealth(maxHealth);
        // }
        type = "cardHeroes";
        LoadPlayerTeamCard("1");
        LoadEnemyTeamCard("1");

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 1);
        healthBar.SetHealth(currentHealth);
    }
    public void LoadPlayerTeamCard(string team_id)
    {
        List<CardHeroes> cardHeroesList = new List<CardHeroes>();
        List<CardCaptains> cardCaptainsList = new List<CardCaptains>();
        List<CardColonels> cardColonelsList = new List<CardColonels>();
        List<CardGenerals> cardGeneralsList = new List<CardGenerals>();
        List<CardAdmirals> cardAdmiralsList = new List<CardAdmirals>();
        List<CardMonsters> cardMonstersList = new List<CardMonsters>();
        List<CardMilitary> cardMilitaryList = new List<CardMilitary>();
        List<CardSpell> cardSpellList = new List<CardSpell>();
        for (int i = 1; i <= 10; i++)
        {
            string position = i.ToString();
            if (type == "cardHeroes")
            {
                cardHeroesList.AddRange(UserCardHeroesService.Create().GetUserCardHeroesTeam("1", team_id, position));
            }
            else if (type == "cardCaptains")
            {
                CardCaptains c = new CardCaptains();
                cardCaptainsList.AddRange(UserCardCaptainsService.Create().GetUserCardCaptainsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardColonels")
            {
                CardColonels c = new CardColonels();
                cardColonelsList.AddRange(UserCardColonelsService.Create().GetUserCardColonelsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardGenerals")
            {
                CardGenerals c = new CardGenerals();
                cardGeneralsList.AddRange(UserCardGeneralsService.Create().GetUserCardGeneralsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardAdmirals")
            {
                CardAdmirals c = new CardAdmirals();
                cardAdmiralsList.AddRange(UserCardAdmiralsService.Create().GetUserCardAdmiralsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardMonsters")
            {
                CardMonsters c = new CardMonsters();
                cardMonstersList.AddRange(UserCardMonstersService.Create().GetUserCardMonstersTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardMilitary")
            {
                CardMilitary c = new CardMilitary();
                cardMilitaryList.AddRange(UserCardMilitaryService.Create().GetUserCardMilitaryTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardSpell")
            {
                CardSpell c = new CardSpell();
                cardSpellList.AddRange(UserCardSpellService.Create().GetUserCardSpellTeam(User.CurrentUserId, team_id, position));
            }
        }

        LoadPlayerCardHeroes(cardHeroesList);
    }
    public void LoadEnemyTeamCard(string team_id)
    {
        List<CardHeroes> cardHeroesList = new List<CardHeroes>();
        List<CardCaptains> cardCaptainsList = new List<CardCaptains>();
        List<CardColonels> cardColonelsList = new List<CardColonels>();
        List<CardGenerals> cardGeneralsList = new List<CardGenerals>();
        List<CardAdmirals> cardAdmiralsList = new List<CardAdmirals>();
        List<CardMonsters> cardMonstersList = new List<CardMonsters>();
        List<CardMilitary> cardMilitaryList = new List<CardMilitary>();
        List<CardSpell> cardSpellList = new List<CardSpell>();
        for (int i = 1; i <= 10; i++)
        {
            string position = i.ToString();
            if (type == "cardHeroes")
            {
                CardHeroes c = new CardHeroes();
                cardHeroesList.AddRange(UserCardHeroesService.Create().GetUserCardHeroesTeam("2", team_id, position));
            }
            else if (type == "cardCaptains")
            {
                CardCaptains c = new CardCaptains();
                cardCaptainsList.AddRange(UserCardCaptainsService.Create().GetUserCardCaptainsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardColonels")
            {
                CardColonels c = new CardColonels();
                cardColonelsList.AddRange(UserCardColonelsService.Create().GetUserCardColonelsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardGenerals")
            {
                CardGenerals c = new CardGenerals();
                cardGeneralsList.AddRange(UserCardGeneralsService.Create().GetUserCardGeneralsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardAdmirals")
            {
                CardAdmirals c = new CardAdmirals();
                cardAdmiralsList.AddRange(UserCardAdmiralsService.Create().GetUserCardAdmiralsTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardMonsters")
            {
                CardMonsters c = new CardMonsters();
                cardMonstersList.AddRange(UserCardMonstersService.Create().GetUserCardMonstersTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardMilitary")
            {
                CardMilitary c = new CardMilitary();
                cardMilitaryList.AddRange(UserCardMilitaryService.Create().GetUserCardMilitaryTeam(User.CurrentUserId, team_id, position));
            }
            else if (type == "CardSpell")
            {
                CardSpell c = new CardSpell();
                cardSpellList.AddRange(UserCardSpellService.Create().GetUserCardSpellTeam(User.CurrentUserId, team_id, position));
            }
        }

        LoadEnemyCardHeroes(cardHeroesList);
    }
    public void LoadPlayerCardHeroes(List<CardHeroes> cardHeroesList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardHeroes card = cardHeroesList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>(); 
            }
            healthBar.SetMaxHealth(card.all_health);

            // Gắn dữ liệu vào script CardHeroesBattle
            CardHeroesBattle battleScript = cardInstance.GetComponent<CardHeroesBattle>();
            if (battleScript != null)
            {
                battleScript.name = card.name;
                battleScript.type = card.type;
                battleScript.power = card.all_power;
                battleScript.health = card.all_health;
                battleScript.physical_attack = card.all_physical_attack;
                battleScript.physical_defense = card.all_physical_defense;
                battleScript.magical_attack = card.all_magical_attack;
                battleScript.magical_defense = card.all_magical_defense;
                battleScript.chemical_attack = card.all_chemical_attack;
                battleScript.chemical_defense = card.all_chemical_defense;
                battleScript.atomic_attack = card.all_atomic_attack;
                battleScript.atomic_defense = card.all_atomic_defense;
                battleScript.mental_attack = card.all_mental_attack;
                battleScript.mental_defense = card.all_mental_defense;
                battleScript.speed = card.all_speed;
                battleScript.critical_damage_rate = card.all_critical_damage_rate;
                battleScript.critical_rate = card.all_critical_rate;
                battleScript.penetration_rate = card.all_penetration_rate;
                battleScript.evasion_rate = card.all_evasion_rate;
                battleScript.damage_absorption_rate = card.all_damage_absorption_rate;
                battleScript.vitality_regeneration_rate = card.all_vitality_regeneration_rate;
                battleScript.accuracy_rate = card.all_accuracy_rate;
                battleScript.lifesteal_rate = card.all_lifesteal_rate;
                battleScript.mana = card.all_mana;
                battleScript.mana_regeneration_rate = card.all_mana_regeneration_rate;
                battleScript.shield_strength = card.all_shield_strength;
                battleScript.tenacity = card.all_tenacity;
                battleScript.resistance_rate = card.all_resistance_rate;
                battleScript.combo_rate = card.all_combo_rate;
                battleScript.reflection_rate = card.all_reflection_rate;
                battleScript.damage_to_different_faction_rate = card.all_damage_to_different_faction_rate;
                battleScript.resistance_to_different_faction_rate = card.all_resistance_to_different_faction_rate;
                battleScript.damage_to_same_faction_rate = card.all_damage_to_same_faction_rate;
                battleScript.resistance_to_same_faction_rate = card.all_resistance_to_same_faction_rate;
                battleScript.position = card.position;
                battleScript.healthBar = healthBar;
                // Gán thêm các thuộc tính khác nếu có
                foreach (var script in cardInstance.GetComponents<CardBase>())
                {
                    if (script != battleScript) Destroy(script);;
                }
            }
            else
            {
                Debug.LogError("CardModelPrefab chưa gắn script CardHeroesBattle!");
            }
        }
    }
    public void LoadPlayerCardCaptains(List<CardCaptains> cardCaptainsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardCaptains card = cardCaptainsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardColonels(List<CardColonels> cardColonelsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardColonels card = cardColonelsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardGenerals(List<CardGenerals> cardGeneralsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardGenerals card = cardGeneralsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardAdmirals(List<CardAdmirals> cardAdmiralsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardAdmirals card = cardAdmiralsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardMonsters(List<CardMonsters> cardMonstersList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardMonsters card = cardMonstersList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardMilitary(List<CardMilitary> cardMilitaryList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardMilitary card = cardMilitaryList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadPlayerCardSpell(List<CardSpell> cardSpellList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardSpell card = cardSpellList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = playerCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardHeroes(List<CardHeroes> cardHeroesList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardHeroes card = cardHeroesList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }
            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0.5f, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>(); 
            }
            healthBar.SetMaxHealth(card.all_health);

            // Gắn dữ liệu vào script CardHeroesBattle
            CardHeroesBattle battleScript = cardInstance.GetComponent<CardHeroesBattle>();
            if (battleScript != null)
            {
                battleScript.name = card.name;
                battleScript.type = card.type;
                battleScript.power = card.all_power;
                battleScript.health = card.all_health;
                battleScript.physical_attack = card.all_physical_attack;
                battleScript.physical_defense = card.all_physical_defense;
                battleScript.magical_attack = card.all_magical_attack;
                battleScript.magical_defense = card.all_magical_defense;
                battleScript.chemical_attack = card.all_chemical_attack;
                battleScript.chemical_defense = card.all_chemical_defense;
                battleScript.atomic_attack = card.all_atomic_attack;
                battleScript.atomic_defense = card.all_atomic_defense;
                battleScript.mental_attack = card.all_mental_attack;
                battleScript.mental_defense = card.all_mental_defense;
                battleScript.speed = card.all_speed;
                battleScript.critical_damage_rate = card.all_critical_damage_rate;
                battleScript.critical_rate = card.all_critical_rate;
                battleScript.penetration_rate = card.all_penetration_rate;
                battleScript.evasion_rate = card.all_evasion_rate;
                battleScript.damage_absorption_rate = card.all_damage_absorption_rate;
                battleScript.vitality_regeneration_rate = card.all_vitality_regeneration_rate;
                battleScript.accuracy_rate = card.all_accuracy_rate;
                battleScript.lifesteal_rate = card.all_lifesteal_rate;
                battleScript.mana = card.all_mana;
                battleScript.mana_regeneration_rate = card.all_mana_regeneration_rate;
                battleScript.shield_strength = card.all_shield_strength;
                battleScript.tenacity = card.all_tenacity;
                battleScript.resistance_rate = card.all_resistance_rate;
                battleScript.combo_rate = card.all_combo_rate;
                battleScript.reflection_rate = card.all_reflection_rate;
                battleScript.damage_to_different_faction_rate = card.all_damage_to_different_faction_rate;
                battleScript.resistance_to_different_faction_rate = card.all_resistance_to_different_faction_rate;
                battleScript.damage_to_same_faction_rate = card.all_damage_to_same_faction_rate;
                battleScript.resistance_to_same_faction_rate = card.all_resistance_to_same_faction_rate;
                battleScript.position = card.position;
                battleScript.healthBar = healthBar;
                // Gán thêm các thuộc tính khác nếu có
                foreach (var script in cardInstance.GetComponents<CardBase>())
                {
                    if (script != battleScript) Destroy(script);;
                }
            }
            else
            {
                Debug.LogError("CardModelPrefab chưa gắn script CardHeroesBattle!");
            }
        }
    }
    public void LoadEnemyCardCaptains(List<CardCaptains> cardCaptainsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardCaptains card = cardCaptainsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardColonels(List<CardColonels> cardColonelsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardColonels card = cardColonelsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardGenerals(List<CardGenerals> cardGeneralsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardGenerals card = cardGeneralsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardAdmirals(List<CardAdmirals> cardAdmiralsList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardAdmirals card = cardAdmiralsList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardMonsters(List<CardMonsters> cardMonstersList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardMonsters card = cardMonstersList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardMilitary(List<CardMilitary> cardMilitaryList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardMilitary card = cardMilitaryList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
    public void LoadEnemyCardSpell(List<CardSpell> cardSpellList)
    {
        for (int i = 1; i <= 10; i++)
        {
            // Tìm lá bài có position là "i-1"
            string targetPosition = i + "-1";
            CardSpell card = cardSpellList.FirstOrDefault(c => c.position == targetPosition);
            if (card == null) continue;

            Transform cardTransform = enemyCardField.Find("CardPosition" + i);
            if (cardTransform == null) continue;

            GameObject cardInstance = Instantiate(CardModelPrefab, cardTransform);
            cardInstance.transform.localPosition = Vector3.zero;

            // Lấy Renderer của Plane (MeshRenderer)
            Renderer rend = cardInstance.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Không tìm thấy Renderer trên CardModelPrefab");
                return;
            }

            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");

            if (texture != null)
            {
                Material newMat = new Material(rend.sharedMaterial);
                newMat.mainTexture = texture;
                rend.material = newMat;
            }

            GameObject hb = Instantiate(healthBarPrefab, cardInstance.transform);
            hb.transform.localPosition = new Vector3(0, 0, 5.5f); // chỉnh vị trí nằm trên đầu
            healthBar = hb.GetComponent<HealthBar>();
            if (healthBar == null)
            {
                healthBar = hb.GetComponentInChildren<HealthBar>();
                healthBar.SetMaxHealth(card.health);
            }

        }
    }
}
