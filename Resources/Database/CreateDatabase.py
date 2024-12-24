import json
import os

def create_cards_database():
    cards_dir="Cards"
    card_list = []
    id=1
    card_name=""
    health=1000000
    physical_attack=100000
    physical_defense=100000
    magical_attack=100000
    magical_defense=100000
    chemical_attack=100000
    chemical_defense=100000
    atomic_attack=100000
    atomic_defense=100000
    mental_attack=100000
    mental_defense=100000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="SR"
    clan=""
    price=100000
    price_unit="Chasmic_Blue_Crystalyte"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=""
        if current_dir not in ["LG", "UR", "SSR", "SR"]:
            current_name=current_dir
            # print(current_name)
        for dir_name in dirs:
            if "SR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        card = {
                            "id":id,
                            "CardName":name,
                            "CardNameImage": file_name,
                            "Health": health,
                            "PhysicalAttack": physical_attack,
                            "PhysicalDefense": physical_defense,
                            "MagicalAttack": magical_attack,
                            "MagicalDefense": magical_defense,
                            "ChemicalAttack": chemical_attack,
                            "ChemicalDefense": chemical_defense,
                            "AtomicAttack": atomic_attack,
                            "AtomicDefense": atomic_defense,
                            "MentalAttack": mental_attack,
                            "MentalDefense": mental_defense,
                            "Speed": speed,
                            "CriticalRate": critical_rate,
                            "CriticalDamage": critical_damage,
                            "ArmorPenetration": armor_penetration,
                            "Avoid": avoid,
                            "AbsorbsDamage": absorbs_damage,
                            "RegenerateVitality": regenerate_vitality,
                            "Mana": mana,
                            "Rare": rare,
                            "Clan": current_name,
                            "Price": price,
                            "PriceUnit":price_unit,
                            "Path":path
                        }
                        card_list.append(card)
                        id=id+1
            if "SSR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        health=2000000
                        physical_attack=200000
                        physical_defense=200000
                        magical_attack=200000
                        magical_defense=200000
                        chemical_attack=200000
                        chemical_defense=200000
                        atomic_attack=200000
                        atomic_defense=200000
                        mental_attack=200000
                        mental_defense=200000
                        mana=200
                        rare="SSR"
                        price=500000
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        card = {
                            "id":id,
                            "CardName":name,
                            "CardNameImage": file_name,
                            "Health": health,
                            "PhysicalAttack": physical_attack,
                            "PhysicalDefense": physical_defense,
                            "MagicalAttack": magical_attack,
                            "MagicalDefense": magical_defense,
                            "ChemicalAttack": chemical_attack,
                            "ChemicalDefense": chemical_defense,
                            "AtomicAttack": atomic_attack,
                            "AtomicDefense": atomic_defense,
                            "MentalAttack": mental_attack,
                            "MentalDefense": mental_defense,
                            "Speed": speed,
                            "CriticalRate": critical_rate,
                            "CriticalDamage": critical_damage,
                            "ArmorPenetration": armor_penetration,
                            "Avoid": avoid,
                            "AbsorbsDamage": absorbs_damage,
                            "RegenerateVitality": regenerate_vitality,
                            "Mana": mana,
                            "Rare": rare,
                            "Clan": current_name,
                            "Price": price,
                            "PriceUnit":price_unit,
                            "Path":path
                        }
                        card_list.append(card)
                        id=id+1
            if "UR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name,extension=os.path.splitext(file_name)
                        health=5000000
                        physical_attack=500000
                        physical_defense=500000
                        magical_attack=500000
                        magical_defense=500000
                        chemical_attack=500000
                        chemical_defense=500000
                        atomic_attack=500000
                        atomic_defense=500000
                        mental_attack=500000
                        mental_defense=500000
                        mana=500
                        rare="UR"
                        price=1000000
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        card = {
                            "id":id,
                            "CardName":name,
                            "CardNameImage": file_name,
                            "Health": health,
                            "PhysicalAttack": physical_attack,
                            "PhysicalDefense": physical_defense,
                            "MagicalAttack": magical_attack,
                            "MagicalDefense": magical_defense,
                            "ChemicalAttack": chemical_attack,
                            "ChemicalDefense": chemical_defense,
                            "AtomicAttack": atomic_attack,
                            "AtomicDefense": atomic_defense,
                            "MentalAttack": mental_attack,
                            "MentalDefense": mental_defense,
                            "Speed": speed,
                            "CriticalRate": critical_rate,
                            "CriticalDamage": critical_damage,
                            "ArmorPenetration": armor_penetration,
                            "Avoid": avoid,
                            "AbsorbsDamage": absorbs_damage,
                            "RegenerateVitality": regenerate_vitality,
                            "Mana": mana,
                            "Rare": rare,
                            "Clan": current_name,
                            "Price": price,
                            "PriceUnit":price_unit,
                            "Path":path
                        }
                        card_list.append(card)
                        id=id+1
            if "LG" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        health=10000000
                        physical_attack=1000000
                        physical_defense=1000000
                        magical_attack=1000000
                        magical_defense=1000000
                        chemical_attack=1000000
                        chemical_defense=1000000
                        atomic_attack=1000000
                        atomic_defense=1000000
                        mental_attack=1000000
                        mental_defense=1000000
                        mana=1000
                        rare="LG"
                        price=5000000
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        card = {
                            "id":id,
                            "CardName":name,
                            "CardNameImage": file_name,
                            "Health": health,
                            "PhysicalAttack": physical_attack,
                            "PhysicalDefense": physical_defense,
                            "MagicalAttack": magical_attack,
                            "MagicalDefense": magical_defense,
                            "ChemicalAttack": chemical_attack,
                            "ChemicalDefense": chemical_defense,
                            "AtomicAttack": atomic_attack,
                            "AtomicDefense": atomic_defense,
                            "MentalAttack": mental_attack,
                            "MentalDefense": mental_defense,
                            "Speed": speed,
                            "CriticalRate": critical_rate,
                            "CriticalDamage": critical_damage,
                            "ArmorPenetration": armor_penetration,
                            "Avoid": avoid,
                            "AbsorbsDamage": absorbs_damage,
                            "RegenerateVitality": regenerate_vitality,
                            "Mana": mana,
                            "Rare": rare,
                            "Clan": current_name,
                            "Price": price,
                            "PriceUnit":price_unit,
                            "Path":path
                        }
                        card_list.append(card)
                        id=id+1
    with open("cards_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_books_database():
    cards_dir="Book"
    card_list = []
    id=1
    card_name=""
    health=20000000
    physical_attack=5000000
    physical_defense=5000000
    magical_attack=5000000
    magical_defense=5000000
    chemical_attack=5000000
    chemical_defense=5000000
    atomic_attack=5000000
    atomic_defense=5000000
    mental_attack=5000000
    mental_defense=5000000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=2000000
    price_unit="Chasmic_Blue_Crystalyte"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "CardBook":name,
                        "BookNameImage": file_name,
                        "Health": health,
                        "PhysicalAttack": physical_attack,
                        "PhysicalDefense": physical_defense,
                        "MagicalAttack": magical_attack,
                        "MagicalDefense": magical_defense,
                        "ChemicalAttack": chemical_attack,
                        "ChemicalDefense": chemical_defense,
                        "AtomicAttack": atomic_attack,
                        "AtomicDefense": atomic_defense,
                        "MentalAttack": mental_attack,
                        "MentalDefense": mental_defense,
                        "Speed": speed,
                        "CriticalRate": critical_rate,
                        "CriticalDamage": critical_damage,
                        "ArmorPenetration": armor_penetration,
                        "Avoid": avoid,
                        "AbsorbsDamage": absorbs_damage,
                        "RegenerateVitality": regenerate_vitality,
                        "Mana": mana,
                        "Rare": rare,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("books_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_skills_database():
    cards_dir="Skill"
    card_list = []
    id=1
    card_name=""
    health=10000000
    physical_attack=2000000
    physical_defense=2000000
    magical_attack=2000000
    magical_defense=2000000
    chemical_attack=2000000
    chemical_defense=2000000
    atomic_attack=2000000
    atomic_defense=2000000
    mental_attack=2000000
    mental_defense=2000000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=1000000
    price_unit="Chasmic_Purple_Crystalyte"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "SkillName":name,
                        "SkillNameImage": file_name,
                        "Health": health,
                        "PhysicalAttack": physical_attack,
                        "PhysicalDefense": physical_defense,
                        "MagicalAttack": magical_attack,
                        "MagicalDefense": magical_defense,
                        "ChemicalAttack": chemical_attack,
                        "ChemicalDefense": chemical_defense,
                        "AtomicAttack": atomic_attack,
                        "AtomicDefense": atomic_defense,
                        "MentalAttack": mental_attack,
                        "MentalDefense": mental_defense,
                        "Speed": speed,
                        "CriticalRate": critical_rate,
                        "CriticalDamage": critical_damage,
                        "ArmorPenetration": armor_penetration,
                        "Avoid": avoid,
                        "AbsorbsDamage": absorbs_damage,
                        "RegenerateVitality": regenerate_vitality,
                        "Mana": mana,
                        "Rare": rare,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("skills_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_bosses_database():
    cards_dir="Boss"
    card_list = []
    id=1
    card_name=""
    health=10000000000
    physical_attack=200000000
    physical_defense=200000000
    magical_attack=200000000
    magical_defense=200000000
    chemical_attack=200000000
    chemical_defense=200000000
    atomic_attack=200000000
    atomic_defense=200000000
    mental_attack=200000000
    mental_defense=200000000
    speed=1
    critical_rate=0
    critical_damage=100
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=10000000
    price_unit="Chasmic_Grey_Crystalyte"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "SkillName":name,
                        "SkillNameImage": file_name,
                        "Health": health,
                        "PhysicalAttack": physical_attack,
                        "PhysicalDefense": physical_defense,
                        "MagicalAttack": magical_attack,
                        "MagicalDefense": magical_defense,
                        "ChemicalAttack": chemical_attack,
                        "ChemicalDefense": chemical_defense,
                        "AtomicAttack": atomic_attack,
                        "AtomicDefense": atomic_defense,
                        "MentalAttack": mental_attack,
                        "MentalDefense": mental_defense,
                        "Speed": speed,
                        "CriticalRate": critical_rate,
                        "CriticalDamage": critical_damage,
                        "ArmorPenetration": armor_penetration,
                        "Avoid": avoid,
                        "AbsorbsDamage": absorbs_damage,
                        "RegenerateVitality": regenerate_vitality,
                        "Mana": mana,
                        "Rare": rare,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("bosses_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_pets_database():
    cards_dir="Pet"
    card_list = []
    id=1
    card_name=""
    health=10000000
    physical_attack=1500000
    physical_defense=1500000
    magical_attack=1500000
    magical_defense=1500000
    chemical_attack=1500000
    chemical_defense=1500000
    atomic_attack=1500000
    atomic_defense=1500000
    mental_attack=1500000
    mental_defense=1500000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=10000000
    price_unit="Ancient_Rune_Crystal_Fire"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "PetName":name,
                        "PetNameImage": file_name,
                        "Health": health,
                        "PhysicalAttack": physical_attack,
                        "PhysicalDefense": physical_defense,
                        "MagicalAttack": magical_attack,
                        "MagicalDefense": magical_defense,
                        "ChemicalAttack": chemical_attack,
                        "ChemicalDefense": chemical_defense,
                        "AtomicAttack": atomic_attack,
                        "AtomicDefense": atomic_defense,
                        "MentalAttack": mental_attack,
                        "MentalDefense": mental_defense,
                        "Speed": speed,
                        "CriticalRate": critical_rate,
                        "CriticalDamage": critical_damage,
                        "ArmorPenetration": armor_penetration,
                        "Avoid": avoid,
                        "AbsorbsDamage": absorbs_damage,
                        "RegenerateVitality": regenerate_vitality,
                        "Mana": mana,
                        "Rare": rare,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("pets_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_symbols_database():
    cards_dir="Symbol"
    card_list = []
    id=1
    card_name=""
    health=30000000
    physical_attack=3500000
    physical_defense=3500000
    magical_attack=3500000
    magical_defense=3500000
    chemical_attack=3500000
    chemical_defense=3500000
    atomic_attack=3500000
    atomic_defense=3500000
    mental_attack=3500000
    mental_defense=3500000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=10000000
    price_unit="Ancient_Rune_Crystal_Dark"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "SymbolName":name,
                        "SymbolNameImage": file_name,
                        "Health": health,
                        "PhysicalAttack": physical_attack,
                        "PhysicalDefense": physical_defense,
                        "MagicalAttack": magical_attack,
                        "MagicalDefense": magical_defense,
                        "ChemicalAttack": chemical_attack,
                        "ChemicalDefense": chemical_defense,
                        "AtomicAttack": atomic_attack,
                        "AtomicDefense": atomic_defense,
                        "MentalAttack": mental_attack,
                        "MentalDefense": mental_defense,
                        "Speed": speed,
                        "CriticalRate": critical_rate,
                        "CriticalDamage": critical_damage,
                        "ArmorPenetration": armor_penetration,
                        "Avoid": avoid,
                        "AbsorbsDamage": absorbs_damage,
                        "RegenerateVitality": regenerate_vitality,
                        "Mana": mana,
                        "Rare": rare,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("symbols_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_achievements_database():
    cards_dir="Achievement"
    card_list = []
    id=1
    card_name=""
    health=10000000
    physical_attack=2000000
    physical_defense=2000000
    magical_attack=2000000
    magical_defense=2000000
    chemical_attack=2000000
    chemical_defense=2000000
    atomic_attack=2000000
    atomic_defense=2000000
    mental_attack=2000000
    mental_defense=2000000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                card = {
                    "id":id,
                    "AchievementName":name,
                    "AchievementNameImage": file_name,
                    "Health": health,
                    "PhysicalAttack": physical_attack,
                    "PhysicalDefense": physical_defense,
                    "MagicalAttack": magical_attack,
                    "MagicalDefense": magical_defense,
                    "ChemicalAttack": chemical_attack,
                    "ChemicalDefense": chemical_defense,
                    "AtomicAttack": atomic_attack,
                    "AtomicDefense": atomic_defense,
                    "MentalAttack": mental_attack,
                    "MentalDefense": mental_defense,
                    "Speed": speed,
                    "CriticalRate": critical_rate,
                    "CriticalDamage": critical_damage,
                    "ArmorPenetration": armor_penetration,
                    "Avoid": avoid,
                    "AbsorbsDamage": absorbs_damage,
                    "RegenerateVitality": regenerate_vitality,
                    "Mana": mana,
                    "Rare": rare,
                    "Type": "Achievement",
                    "Path":path
                }
                card_list.append(card)
                id=id+1
    with open("achievements_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_currencies_database():
    cards_dir="Currency"
    card_list = []
    id=1
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                card = {
                    "id":id,
                    "CurrencyName":name,
                    "CurrencyNameImage": file_name,
                    "Type": "Currency",
                    "Path":path
                }
                card_list.append(card)
                id=id+1
    with open("currency_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_items_database():
    cards_dir="Item"
    card_list = []
    id=1
    card_name=""
    price=10000000
    price_unit="Ancient_Rune_Crystal_Light"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    card = {
                        "id":id,
                        "ItemName":name,
                        "ItemNameImage": file_name,
                        "Type": dir_name,
                        "Price": price,
                        "PriceUnit":price_unit,
                        "Path":path
                    }
                    card_list.append(card)
                    id=id+1
    with open("items_database.json", "w") as json_file:
        json.dump(card_list, json_file)

def create_equipments_database():
    cards_dir="Equipment"
    card_list = []
    id=1
    card_name=""
    health=10000000000
    physical_attack=200000000
    physical_defense=200000000
    magical_attack=200000000
    magical_defense=200000000
    chemical_attack=200000000
    chemical_defense=200000000
    atomic_attack=200000000
    atomic_defense=200000000
    mental_attack=200000000
    mental_defense=200000000
    speed=1
    critical_rate=0
    critical_damage=0
    armor_penetration=0
    avoid=0
    absorbs_damage=0
    regenerate_vitality=0
    mana=100
    rare="LG"
    price=100000
    price_unit=""
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            if current_name =="Amnitus_Equipment":
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        card = {
                            "id":id,
                            "SkillName":name,
                            "SkillNameImage": file_name,
                            "Health": health,
                            "PhysicalAttack": physical_attack,
                            "PhysicalDefense": physical_defense,
                            "MagicalAttack": magical_attack,
                            "MagicalDefense": magical_defense,
                            "ChemicalAttack": chemical_attack,
                            "ChemicalDefense": chemical_defense,
                            "AtomicAttack": atomic_attack,
                            "AtomicDefense": atomic_defense,
                            "MentalAttack": mental_attack,
                            "MentalDefense": mental_defense,
                            "Speed": speed,
                            "CriticalRate": critical_rate,
                            "CriticalDamage": critical_damage,
                            "ArmorPenetration": armor_penetration,
                            "Avoid": avoid,
                            "AbsorbsDamage": absorbs_damage,
                            "RegenerateVitality": regenerate_vitality,
                            "Mana": mana,
                            "Rare": rare,
                            "Type": current_name,
                            "Price": price,
                            "PriceUnit":price_unit,
                            "Path":path
                        }
                        card_list.append(card)
                        id=id+1
    with open("equipments_database.json", "w") as json_file:
        json.dump(card_list, json_file)

# create_cards_database()
# create_books_database()
# create_skills_database()
# create_bosses_database()
# create_equipments_database()
# create_items_database()
# create_achievements_database()
# create_currencies_database()
# create_pets_database()
# create_symbols_database()