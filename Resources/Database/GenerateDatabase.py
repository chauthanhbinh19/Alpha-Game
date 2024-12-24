import json
import os
import random

def calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                    speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality):
    weight_health = 1
    weight_physical_attack = 1.5
    weight_physical_defense = 1.2
    weight_magical_attack = 1.5
    weight_magical_defense = 1.2
    weight_chemical_attack = 1.5
    weight_chemical_defense = 1.2
    weight_atomic_attack = 1.5
    weight_atomic_defense = 1.2
    weight_mental_attack = 1.5
    weight_mental_defense = 1.2
    weight_speed = 1.5
    weight_critical_rate = 1.2
    weight_critical_damage = 1.5
    weight_armor_penetration = 1.2
    weight_avoid = 1.2
    weight_absorbs_damage = 1.2
    weight_regenerate_vitality = 1.2

    power=(
        health*weight_health+
        physical_attack*weight_physical_attack+
        physical_defense*weight_physical_defense+
        magical_attack*weight_magical_attack+
        magical_defense*weight_magical_defense+
        chemical_attack*weight_chemical_attack+
        chemical_defense*weight_chemical_defense+
        atomic_attack*weight_atomic_attack+
        atomic_defense*weight_atomic_defense+
        mental_attack*weight_mental_attack+
        mental_defense*weight_mental_defense+
        speed*weight_speed+
        critical_rate*weight_critical_rate+
        critical_damage*weight_critical_damage+
        armor_penetration*weight_armor_penetration+
        avoid*weight_avoid+
        absorbs_damage*weight_absorbs_damage+
        regenerate_vitality*weight_regenerate_vitality
    )
    return power

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
    speed=10000
    critical_rate=0
    critical_damage=40000
    armor_penetration=10000
    avoid=0
    absorbs_damage=50000
    regenerate_vitality=3000
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
                        name=name.replace("_"," ")
                        
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into cards values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")

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
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into cards values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
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
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into cards values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
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
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into cards values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                        id=id+1

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
    speed=50000
    critical_rate=0
    critical_damage=1000000
    armor_penetration=1500000
    avoid=0
    absorbs_damage=500000
    regenerate_vitality=500000
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
                    name=name.replace("_"," ")
                
                    power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                    print("insert into books values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                    id=id+1


def create_army_database():
    cards_dir="Army"
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
    speed=50000
    critical_rate=0
    critical_damage=1000000
    armor_penetration=1500000
    avoid=0
    absorbs_damage=500000
    regenerate_vitality=500000
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
                    name=name.replace("_"," ")
                    
                    power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                    print("insert into army values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + dir_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                    id=id+1


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
                    name=name.replace("_"," ")
                    
                    power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                    print("insert into skills values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + dir_name + "',"  + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                    id=id+1

def create_collaboration_equipments_database():
    cards_dir="CollborationEquipment"
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
    speed=10000
    critical_rate=0
    critical_damage=40000
    armor_penetration=10000
    avoid=0
    absorbs_damage=50000
    regenerate_vitality=3000
    mana=100
    rare="SR"
    clan=""
    price=100000
    price_unit="Chasmic_Blue_Crystalyte"
    path=""
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=""
        if current_dir not in ["MR","LG", "UR", "SSR", "SR"]:
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
                        name=name.replace("_"," ")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into collaboration_equipments values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")

                        id=id+1
            if "SSR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        health=2000000
                        physical_attack=500000
                        physical_defense=200000
                        magical_attack=500000
                        magical_defense=200000
                        chemical_attack=500000
                        chemical_defense=200000
                        atomic_attack=500000
                        atomic_defense=200000
                        mental_attack=500000
                        mental_defense=200000
                        mana=200
                        rare="SSR"
                        price=500000
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into collaboration_equipments values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                        id=id+1
            if "UR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name,extension=os.path.splitext(file_name)
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        health=5000000
                        physical_attack=1000000
                        physical_defense=500000
                        magical_attack=1000000
                        magical_defense=500000
                        chemical_attack=1000000
                        chemical_defense=500000
                        atomic_attack=1000000
                        atomic_defense=500000
                        mental_attack=1000000
                        mental_defense=500000
                        mana=500
                        rare="UR"
                        price=1000000
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into collaboration_equipments values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                        id=id+1
            if "LG" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        health=10000000
                        physical_attack=2000000
                        physical_defense=1500000
                        magical_attack=2000000
                        magical_defense=1500000
                        chemical_attack=2000000
                        chemical_defense=1500000
                        atomic_attack=2000000
                        atomic_defense=1500000
                        mental_attack=2000000
                        mental_defense=1500000
                        mana=1000
                        rare="LG"
                        price=5000000
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into collaboration_equipments values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                        id=id+1
            if "MR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        health=20000000
                        physical_attack=3000000
                        physical_defense=2000000
                        magical_attack=3000000
                        magical_defense=2000000
                        chemical_attack=3000000
                        chemical_defense=2000000
                        atomic_attack=3000000
                        atomic_defense=2000000
                        mental_attack=3000000
                        mental_defense=2000000
                        mana=1000
                        rare="LG"
                        price=5000000
                        name=name.replace("_"," ")
                        power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                        print("insert into collaboration_equipments values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + current_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                        id=id+1

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
                    name=name.replace("_"," ")
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
                    name=name.replace("_"," ")
                    
                    power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                    print("insert into pets values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + dir_name + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")

                    id=id+1

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
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
            dir_name = os.path.basename(current_dir)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
            
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    name=name.replace("_"," ")
                    
                    power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                    print("insert into symbols values (" + str(id) + ",'" + name + "','" + path + "','"+dir_name+"'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")

                    id=id+1

def create_medals_database():
    cards_dir="Medal"
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
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
                name=name.replace("_"," ")
                
                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into medals values (" + str(id) + ",'" + name + "','" + path + "'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")

                id=id+1
    
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
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
                name=name.replace("_"," ")
                
                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into achievements values (" + str(id) + ",'" + name + "','" + path + "'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")
                id=id+1
    
def create_titles_database():
    cards_dir="Title"
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
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
                name=name.replace("_"," ")

                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into titles values (" + str(id) + ",'" + name + "','" + path + "'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")
                id=id+1

def create_monster_database():
    cards_dir="Monster"
    card_list = []
    id=1
    card_name=""
    health=100000000
    physical_attack=30000000
    physical_defense=20000000
    magical_attack=30000000
    magical_defense=20000000
    chemical_attack=30000000
    chemical_defense=20000000
    atomic_attack=30000000
    atomic_defense=20000000
    mental_attack=30000000
    mental_defense=20000000
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
                name=name.replace("_"," ")

                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into monsters values (" + str(id) + ",'" + name + "','" + path + "','" + rare + "','" + "none" + "'," + str(0) + "," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ",'');")
                id=id+1

def create_borders_database():
    cards_dir="Border"
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
    per_health=10
    per_physical_attack=10
    per_physical_defense=10
    per_magical_attack=10
    per_magical_defense=10
    per_chemical_attack=10
    per_chemical_defense=10
    per_atomic_attack=10
    per_atomic_defense=10
    per_mental_attack=10
    per_mental_defense=10
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
                name=name.replace("_"," ")
            
                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into borders values (" + str(id) + ",'" + name + "','" + path + "'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")
                id=id+1

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
                name=name.replace("_"," ")
                print("insert into currency values (" + str(id) + ",'" + name + "','" + path + "');")
                id=id+1


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
                    name=name.replace("_"," ")
                    print("insert into items values (" + str(id) + ",'" + name + "','" + path + "','" + dir_name + "'," + str(1000) + ",'');")
                    id=id+1

def create_equipments_database():
    cards_dir="Equipment"
    card_list = []
    id=1
    card_name=""
    health=10000000000
    physical_attack=200000000
    physical_defense=150000000
    magical_attack=200000000
    magical_defense=150000000
    chemical_attack=200000000
    chemical_defense=150000000
    atomic_attack=200000000
    atomic_defense=150000000
    mental_attack=200000000
    mental_defense=150000000
    speed=1000000
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
    special_health=10000000000
    special_physical_attack=200000000
    special_physical_defense=150000000
    special_magical_attack=200000000
    special_magical_defense=150000000
    special_chemical_attack=200000000
    special_chemical_defense=150000000
    special_atomic_attack=200000000
    special_atomic_defense=150000000
    special_mental_attack=200000000
    special_mental_defense=150000000
    special_speed=1000000
    for root, dirs, files in os.walk(cards_dir):
        current_dir = os.path.basename(root)
        current_name=""
        current_name = os.path.basename(os.path.dirname(root))
        for dir_name in dirs:
            current_dir = os.path.join(root, dir_name)
            for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith(".png"):
                        name, extension = os.path.splitext(file_name)
                        path=os.path.join(current_dir,file_name)
                        path=path.replace("\\","/")
                        set1_folder_name = os.path.basename(os.path.dirname(current_dir))
                        name=name.replace("_"," ")
                        name=name.encode('latin1', 'ignore').decode('latin1')
                        if "SR" in dir_name:
                            health=20000000
                            physical_attack=1000000
                            physical_defense=500000
                            magical_attack=1000000
                            magical_defense=500000
                            chemical_attack=1000000
                            chemical_defense=500000
                            atomic_attack=1000000
                            atomic_defense=500000
                            mental_attack=1000000
                            mental_defense=500000
                            variables = {
                                'special_health': 20000000,
                                'special_physical_attack': 1000000,
                                'special_physical_defense': 500000,
                                'special_magical_attack': 1000000,
                                'special_magical_defense': 500000,
                                'special_chemical_attack': 1000000,
                                'special_chemical_defense': 500000,
                                'special_atomic_attack': 1000000,
                                'special_atomic_defense': 500000,
                                'special_mental_attack': 1000000,
                                'special_mental_defense': 500000,
                                'special_speed': 5000000
                            }
                            selected_variables = random.sample(list(variables.keys()), 2)
                            for var in variables:
                                if var not in selected_variables:
                                    variables[var] = 0
                            power = calculate_power(health, physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                                                    speed, critical_rate, critical_damage, armor_penetration, avoid, absorbs_damage, regenerate_vitality)
                            print("insert into equipments values (" + str(id) + ",'" + name + "','" + path + "','" + current_name + "','" + dir_name + "','"+set1_folder_name +"'," + str(0) + "," + str(power) + "," + str(health) +
                                "," + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense)
                                + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration)
                                + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + 
                                "," + str(list(variables.values())[0]) + ","+ str(list(variables.values())[1]) + "," + str(list(variables.values())[2]) + "," + str(list(variables.values())[3]) + "," + str(list(variables.values())[4]) + "," + str(list(variables.values())[5]) + "," + str(list(variables.values())[6])
                                + "," + str(list(variables.values())[7]) + "," + str(list(variables.values())[8]) + "," + str(list(variables.values())[9]) + "," + str(list(variables.values())[10]) + "," + str(list(variables.values())[11]) + ",'');")
                            id += 1
                        elif "SSR" in dir_name:
                            health=50000000
                            physical_attack=2500000
                            physical_defense=1000000
                            magical_attack=2500000
                            magical_defense=1000000
                            chemical_attack=2500000
                            chemical_defense=1000000
                            atomic_attack=2500000
                            atomic_defense=1000000
                            mental_attack=2500000
                            mental_defense=1000000
                            variables = {
                                'special_health': 50000000,
                                'special_physical_attack': 2500000,
                                'special_physical_defense': 1000000,
                                'special_magical_attack': 2500000,
                                'special_magical_defense': 1000000,
                                'special_chemical_attack': 2500000,
                                'special_chemical_defense': 1000000,
                                'special_atomic_attack': 2500000,
                                'special_atomic_defense': 1000000,
                                'special_mental_attack': 2500000,
                                'special_mental_defense': 1000000,
                                'special_speed': 10000000
                            }
                            selected_variables = random.sample(list(variables.keys()), 2)
                            for var in variables:
                                if var not in selected_variables:
                                    variables[var] = 0
                            power = calculate_power(health, physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                                                    speed, critical_rate, critical_damage, armor_penetration, avoid, absorbs_damage, regenerate_vitality)
                            print("insert into equipments values (" + str(id) + ",'" + name + "','" + path + "','" + current_name + "','" + dir_name + "','"+set1_folder_name +"'," + str(0) + "," + str(power) + "," + str(health) +
                                "," + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense)
                                + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration)
                                + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + 
                                "," + str(list(variables.values())[0]) + ","+ str(list(variables.values())[1]) + "," + str(list(variables.values())[2]) + "," + str(list(variables.values())[3]) + "," + str(list(variables.values())[4]) + "," + str(list(variables.values())[5]) + "," + str(list(variables.values())[6])
                                + "," + str(list(variables.values())[7]) + "," + str(list(variables.values())[8]) + "," + str(list(variables.values())[9]) + "," + str(list(variables.values())[10]) + "," + str(list(variables.values())[11]) + ",'');")
                            id += 1
                        elif "UR" in dir_name:
                            health=100000000
                            physical_attack=5000000
                            physical_defense=2500000
                            magical_attack=5000000
                            magical_defense=2500000
                            chemical_attack=5000000
                            chemical_defense=2500000
                            atomic_attack=5000000
                            atomic_defense=2500000
                            mental_attack=5000000
                            mental_defense=2500000
                            variables = {
                                'special_health': 100000000,
                                'special_physical_attack': 5000000,
                                'special_physical_defense': 2500000,
                                'special_magical_attack': 5000000,
                                'special_magical_defense': 2500000,
                                'special_chemical_attack': 5000000,
                                'special_chemical_defense': 2500000,
                                'special_atomic_attack': 5000000,
                                'special_atomic_defense': 2500000,
                                'special_mental_attack': 5000000,
                                'special_mental_defense': 2500000,
                                'special_speed': 50000000
                            }
                            selected_variables = random.sample(list(variables.keys()), 2)
                            for var in variables:
                                if var not in selected_variables:
                                    variables[var] = 0
                            power = calculate_power(health, physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                                                    speed, critical_rate, critical_damage, armor_penetration, avoid, absorbs_damage, regenerate_vitality)
                            print("insert into equipments values (" + str(id) + ",'" + name + "','" + path + "','" + current_name + "','" + dir_name + "','"+set1_folder_name +"'," + str(0) + "," + str(power) + "," + str(health) +
                                "," + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense)
                                + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration)
                                + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + 
                                "," + str(list(variables.values())[0]) + ","+ str(list(variables.values())[1]) + "," + str(list(variables.values())[2]) + "," + str(list(variables.values())[3]) + "," + str(list(variables.values())[4]) + "," + str(list(variables.values())[5]) + "," + str(list(variables.values())[6])
                                + "," + str(list(variables.values())[7]) + "," + str(list(variables.values())[8]) + "," + str(list(variables.values())[9]) + "," + str(list(variables.values())[10]) + "," + str(list(variables.values())[11]) + ",'');")
                            id += 1
                        elif "LG" in dir_name:
                            health=500000000
                            physical_attack=10000000
                            physical_defense=5000000
                            magical_attack=10000000
                            magical_defense=5000000
                            chemical_attack=10000000
                            chemical_defense=5000000
                            atomic_attack=10000000
                            atomic_defense=5000000
                            mental_attack=10000000
                            mental_defense=5000000
                            variables = {
                                'special_health': 500000000,
                                'special_physical_attack': 10000000,
                                'special_physical_defense': 5000000,
                                'special_magical_attack': 10000000,
                                'special_magical_defense': 5000000,
                                'special_chemical_attack': 10000000,
                                'special_chemical_defense': 5000000,
                                'special_atomic_attack': 10000000,
                                'special_atomic_defense': 5000000,
                                'special_mental_attack': 10000000,
                                'special_mental_defense': 5000000,
                                'special_speed': 100000000
                            }
                            selected_variables = random.sample(list(variables.keys()), 2)
                            for var in variables:
                                if var not in selected_variables:
                                    variables[var] = 0
                            if "Souls_Equipment" in current_name or "Gem_Equipment" in current_name:
                                if "lv1" in name:
                                    health=50000000
                                    physical_attack=1000000
                                    physical_defense=500000
                                    magical_attack=1000000
                                    magical_defense=500000
                                    chemical_attack=1000000
                                    chemical_defense=500000
                                    atomic_attack=1000000
                                    atomic_defense=500000
                                    mental_attack=1000000
                                    mental_defense=500000
                                elif "lv2" in name:
                                    health=80000000
                                    physical_attack=3000000
                                    physical_defense=1500000
                                    magical_attack=3000000
                                    magical_defense=1500000
                                    chemical_attack=3000000
                                    chemical_defense=1500000
                                    atomic_attack=3000000
                                    atomic_defense=1500000
                                    mental_attack=3000000
                                    mental_defense=1500000
                                elif "lv3" in name:
                                    health=150000000
                                    physical_attack=5000000
                                    physical_defense=2500000
                                    magical_attack=5000000
                                    magical_defense=2500000
                                    chemical_attack=5000000
                                    chemical_defense=2500000
                                    atomic_attack=5000000
                                    atomic_defense=2500000
                                    mental_attack=5000000
                                    mental_defense=2500000
                                elif "lv4" in name:
                                    health=350000000
                                    physical_attack=8000000
                                    physical_defense=5000000
                                    magical_attack=8000000
                                    magical_defense=5000000
                                    chemical_attack=8000000
                                    chemical_defense=5000000
                                    atomic_attack=8000000
                                    atomic_defense=5000000
                                    mental_attack=8000000
                                    mental_defense=5000000
                                elif "lv5" in name:
                                    health=600000000
                                    physical_attack=10000000
                                    physical_defense=5000000
                                    magical_attack=10000000
                                    magical_defense=5000000
                                    chemical_attack=10000000
                                    chemical_defense=5000000
                                    atomic_attack=10000000
                                    atomic_defense=5000000
                                    mental_attack=10000000
                                    mental_defense=5000000
                                elif "lv6" in name:
                                    health=1000000000
                                    physical_attack=40000000
                                    physical_defense=25000000
                                    magical_attack=40000000
                                    magical_defense=25000000
                                    chemical_attack=40000000
                                    chemical_defense=25000000
                                    atomic_attack=40000000
                                    atomic_defense=25000000
                                    mental_attack=40000000
                                    mental_defense=25000000
                                elif "lv7" in name:
                                    health=5000000000
                                    physical_attack=90000000
                                    physical_defense=50000000
                                    magical_attack=90000000
                                    magical_defense=50000000
                                    chemical_attack=90000000
                                    chemical_defense=50000000
                                    atomic_attack=90000000
                                    atomic_defense=50000000
                                    mental_attack=90000000
                                    mental_defense=50000000
                            power = calculate_power(health, physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                                                    speed, critical_rate, critical_damage, armor_penetration, avoid, absorbs_damage, regenerate_vitality)
                            print("insert into equipments values (" + str(id) + ",'" + name + "','" + path + "','" + current_name + "','" + dir_name + "','"+set1_folder_name +"'," + str(0) + "," + str(power) + "," + str(health) +
                                "," + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense)
                                + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration)
                                + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + 
                                "," + str(list(variables.values())[0]) + ","+ str(list(variables.values())[1]) + "," + str(list(variables.values())[2]) + "," + str(list(variables.values())[3]) + "," + str(list(variables.values())[4]) + "," + str(list(variables.values())[5]) + "," + str(list(variables.values())[6])
                                + "," + str(list(variables.values())[7]) + "," + str(list(variables.values())[8]) + "," + str(list(variables.values())[9]) + "," + str(list(variables.values())[10]) + "," + str(list(variables.values())[11]) + ",'');")
                            id += 1
                        elif "MR" in dir_name:
                            health=1000000000
                            physical_attack=20000000
                            physical_defense=15000000
                            magical_attack=20000000
                            magical_defense=15000000
                            chemical_attack=20000000
                            chemical_defense=15000000
                            atomic_attack=20000000
                            atomic_defense=15000000
                            mental_attack=20000000
                            mental_defense=15000000
                            variables = {
                                'special_health': 1000000000,
                                'special_physical_attack': 20000000,
                                'special_physical_defense': 15000000,
                                'special_magical_attack': 20000000,
                                'special_magical_defense': 15000000,
                                'special_chemical_attack': 20000000,
                                'special_chemical_defense': 15000000,
                                'special_atomic_attack': 20000000,
                                'special_atomic_defense': 15000000,
                                'special_mental_attack': 20000000,
                                'special_mental_defense': 15000000,
                                'special_speed': 200000000
                            }
                            selected_variables = random.sample(list(variables.keys()), 2)
                            for var in variables:
                                if var not in selected_variables:
                                    variables[var] = 0
                            power = calculate_power(health, physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                                                    speed, critical_rate, critical_damage, armor_penetration, avoid, absorbs_damage, regenerate_vitality)
                            print("insert into equipments values (" + str(id) + ",'" + name + "','" + path + "','" + current_name + "','" + dir_name + "','"+set1_folder_name +"'," + str(0) + "," + str(power) + "," + str(health) +
                                "," + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense)
                                + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration)
                                + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + 
                                "," + str(list(variables.values())[0]) + ","+ str(list(variables.values())[1]) + "," + str(list(variables.values())[2]) + "," + str(list(variables.values())[3]) + "," + str(list(variables.values())[4]) + "," + str(list(variables.values())[5]) + "," + str(list(variables.values())[6])
                                + "," + str(list(variables.values())[7]) + "," + str(list(variables.values())[8]) + "," + str(list(variables.values())[9]) + "," + str(list(variables.values())[10]) + "," + str(list(variables.values())[11]) + ",'');")
                            id += 1

def create_collaboration_database():
    cards_dir="Collaboration"
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
    per_health=40
    per_physical_attack=40
    per_physical_defense=40
    per_magical_attack=40
    per_magical_defense=40
    per_chemical_attack=40
    per_chemical_defense=40
    per_atomic_attack=40
    per_atomic_defense=40
    per_mental_attack=40
    per_mental_defense=40
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
                name=name.replace("_"," ")
            
                power=calculate_power(health,physical_attack,physical_defense,magical_attack,magical_defense,chemical_attack,chemical_defense,atomic_attack,atomic_defense,mental_attack,mental_defense,
                                              speed,critical_rate,critical_damage,armor_penetration,avoid,absorbs_damage,regenerate_vitality)
                print("insert into collaborations values (" + str(id) + ",'" + name + "','" + path + "'," + str(power) + "," + str(health) + "," 
                              + str(physical_attack) + "," + str(physical_defense) + "," + str(magical_attack) + "," + str(magical_defense) + "," + str(chemical_attack) + "," + str(chemical_defense) 
                              + "," + str(atomic_attack) + "," + str(atomic_defense) + "," + str(mental_attack) + "," + str(mental_defense) + "," + str(speed) + "," + str(critical_damage) + "," + str(critical_rate) + "," + str(armor_penetration) 
                              + "," + str(avoid) + "," + str(absorbs_damage) + "," + str(regenerate_vitality) + "," + str(mana) + ","+ str(per_health)+","+str(per_physical_attack) +","+ str(per_physical_defense)
                              + ","+str(per_magical_attack) +","+ str(per_magical_defense) + ","+str(per_chemical_attack) +","+ str(per_chemical_defense) + ","+str(per_atomic_attack) +","+ str(per_atomic_defense) + ","+str(per_mental_attack) +","+ str(per_mental_defense)+",'');")
                id=id+1      

def create_cards_trade():
    cards_dir="Cards"
    id=1
    
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
                        name=name.replace("_"," ")
                        
                        
                        print("insert into card_trade values (" + str(id) + "," + str(7) + "," + str(200) + ");")

                        id=id+1
            if "SSR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        
                        print("insert into card_trade values (" + str(id) + "," + str(7) + "," + str(500) + ");")
                        id=id+1
            if "UR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name,extension=os.path.splitext(file_name)
                        
                        print("insert into card_trade values (" + str(id) + "," + str(7) + "," + str(1000) + ");")
                        id=id+1
            if "LG" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        
                        print("insert into card_trade values (" + str(id) + "," + str(7) + "," + str(2000) + ");")
                        id=id+1

def create_book_trade():
    cards_dir="Book"
    id=1
    
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
                    name=name.replace("_"," ")
                
                    print("insert into book_trade values (" + str(id) + "," + str(37) + "," + str(2000) + ");")
                    id=id+1

def create_army_trade():
    cards_dir="Army"
    card_list = []
    id=1
    
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
                    name=name.replace("_"," ")
                    
                    print("insert into army_trade values (" + str(id) + "," + str(48) + "," + str(2000) + ");")
                    id=id+1

def create_skills_trade():
    cards_dir="Skill"
    card_list = []
    id=1
    
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
                    name=name.replace("_"," ")
                    
                    print("insert into skill_trade values (" + str(id) + "," + str(8) + "," + str(2000) + ");")
                    id=id+1

def create_collaboration_equipments_trade():
    cards_dir="CollborationEquipment"
    card_list = []
    id=1
    
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=""
        if current_dir not in ["MR","LG", "UR", "SSR", "SR"]:
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
                        name=name.replace("_"," ")
                        
                        print("insert into collaboration_equipment_trade values (" + str(id) + "," + str(36) + "," + str(200) + ");")

                        id=id+1
            if "SSR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        
                        print("insert into collaboration_equipment_trade values (" + str(id) + "," + str(36) + "," + str(500) + ");")
                        id=id+1
            if "UR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name,extension=os.path.splitext(file_name)
                        print("insert into collaboration_equipment_trade values (" + str(id) + "," + str(36) + "," + str(1000) + ");")
                        id=id+1
            if "LG" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        print("insert into collaboration_equipment_trade values (" + str(id) + "," + str(36) + "," + str(2000) + ");")
                        id=id+1
            if "MR" in dir_name:
                current_dir =os.path.join(root,dir_name)
                for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith("png"):
                        name, extension=os.path.splitext(file_name)
                        print("insert into collaboration_equipment_trade values (" + str(id) + "," + str(36) + "," + str(5000) + ");")
                        id=id+1

def create_pets_trade():
    cards_dir="Pet"
    card_list = []
    id=1
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
                    name=name.replace("_"," ")
                    
                    print("insert into pet_trade values (" + str(id) + "," + str(51) + "," + str(5000) + ");")

                    id=id+1

def create_symbols_trade():
    cards_dir="Symbol"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        current_name=current_dir
        for dir_name in dirs:
            current_dir =os.path.join(root,dir_name)
            dir_name = os.path.basename(current_dir)
            for file_name in os.listdir(current_dir):
                if file_name.endswith(".jpg") or file_name.endswith("png"):
                    name, extension=os.path.splitext(file_name)
            
                    path=os.path.join(current_dir,file_name)
                    path=path.replace("\\","/")
                    name=name.replace("_"," ")
                    
                    print("insert into symbol_trade values (" + str(id) + "," + str(52) + "," + str(2000) + ");")

                    id=id+1

def create_medals_trade():
    cards_dir="Medal"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")
                
                print("insert into medal_trade values (" + str(id) + "," + str(36) + "," + str(2000) + ");")

                id=id+1

def create_achievements_trade():
    cards_dir="Achievement"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")
                
                print("insert into achievement_trade values (" + str(id) + "," + str(33) + "," + str(2000) + ");")
                id=id+1

def create_titles_trade():
    cards_dir="Title"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")

                print("insert into title_trade values (" + str(id) + "," + str(6) + "," + str(2000) + ");")
                id=id+1

def create_borders_trade():
    cards_dir="Border"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")
            
                print("insert into border_trade values (" + str(id) + "," + str(35) + "," + str(2000) + ");")
                id=id+1

def create_items_trade():
    cards_dir="Item"
    card_list = []
    id=1
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
                    name=name.replace("_"," ")
                    print("insert into item_trade values (" + str(id) + "," + str(35) + "," + str(10) + ");")
                    id=id+1

def create_equipments_trade():
    cards_dir="Equipment"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir = os.path.basename(root)
        current_name=""
        current_name = os.path.basename(os.path.dirname(root))
        for dir_name in dirs:
            current_dir = os.path.join(root, dir_name)
            for file_name in os.listdir(current_dir):
                    if file_name.endswith(".jpg") or file_name.endswith(".png"):
                        name, extension = os.path.splitext(file_name)
                        set1_folder_name = os.path.basename(os.path.dirname(current_dir))
                        name=name.replace("_"," ")
                        if "SR" in dir_name:
                            print("insert into equipment_trade values (" + str(id) + "," + str(47) + "," + str(200) + ");")
                            id += 1
                        elif "SSR" in dir_name:
                            print("insert into equipment_trade values (" + str(id) + "," + str(47) + "," + str(500) + ");")
                            id += 1
                        elif "UR" in dir_name:
                            print("insert into equipment_trade values (" + str(id) + "," + str(47) + "," + str(1000) + ");")
                            id += 1
                        elif "LG" in dir_name:
                            health=500000000
                            physical_attack=10000000
                            physical_defense=5000000
                            magical_attack=10000000
                            magical_defense=5000000
                            chemical_attack=10000000
                            chemical_defense=5000000
                            atomic_attack=10000000
                            atomic_defense=5000000
                            mental_attack=10000000
                            mental_defense=5000000
                            if "Souls_Equipment" in current_name or "Gem_Equipment" in current_name:
                                if "lv1" in name:
                                    health=50000000
                                    physical_attack=1000000
                                    physical_defense=500000
                                    magical_attack=1000000
                                    magical_defense=500000
                                    chemical_attack=1000000
                                    chemical_defense=500000
                                    atomic_attack=1000000
                                    atomic_defense=500000
                                    mental_attack=1000000
                                    mental_defense=500000
                                elif "lv2" in name:
                                    health=80000000
                                    physical_attack=3000000
                                    physical_defense=1500000
                                    magical_attack=3000000
                                    magical_defense=1500000
                                    chemical_attack=3000000
                                    chemical_defense=1500000
                                    atomic_attack=3000000
                                    atomic_defense=1500000
                                    mental_attack=3000000
                                    mental_defense=1500000
                                elif "lv3" in name:
                                    health=150000000
                                    physical_attack=5000000
                                    physical_defense=2500000
                                    magical_attack=5000000
                                    magical_defense=2500000
                                    chemical_attack=5000000
                                    chemical_defense=2500000
                                    atomic_attack=5000000
                                    atomic_defense=2500000
                                    mental_attack=5000000
                                    mental_defense=2500000
                                elif "lv4" in name:
                                    health=350000000
                                    physical_attack=8000000
                                    physical_defense=5000000
                                    magical_attack=8000000
                                    magical_defense=5000000
                                    chemical_attack=8000000
                                    chemical_defense=5000000
                                    atomic_attack=8000000
                                    atomic_defense=5000000
                                    mental_attack=8000000
                                    mental_defense=5000000
                                elif "lv5" in name:
                                    health=600000000
                                    physical_attack=10000000
                                    physical_defense=5000000
                                    magical_attack=10000000
                                    magical_defense=5000000
                                    chemical_attack=10000000
                                    chemical_defense=5000000
                                    atomic_attack=10000000
                                    atomic_defense=5000000
                                    mental_attack=10000000
                                    mental_defense=5000000
                                elif "lv6" in name:
                                    health=1000000000
                                    physical_attack=40000000
                                    physical_defense=25000000
                                    magical_attack=40000000
                                    magical_defense=25000000
                                    chemical_attack=40000000
                                    chemical_defense=25000000
                                    atomic_attack=40000000
                                    atomic_defense=25000000
                                    mental_attack=40000000
                                    mental_defense=25000000
                                elif "lv7" in name:
                                    health=5000000000
                                    physical_attack=90000000
                                    physical_defense=50000000
                                    magical_attack=90000000
                                    magical_defense=50000000
                                    chemical_attack=90000000
                                    chemical_defense=50000000
                                    atomic_attack=90000000
                                    atomic_defense=50000000
                                    mental_attack=90000000
                                    mental_defense=50000000
                            print("insert into equipment_trade values (" + str(id) + "," + str(47) + "," + str(2000) + ");")
                            id += 1
                        elif "MR" in dir_name:
                            print("insert into equipment_trade values (" + str(id) + "," + str(47) + "," + str(5000) + ");")
                            id += 1

def create_collaboration_trade():
    cards_dir="Collaboration"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")
            
                print("insert into collaboration_trade values (" + str(id) + "," + str(46) + "," + str(5000) + ");")
                id=id+1     

def create_monster_trade():
    cards_dir="Monster"
    card_list = []
    id=1
    for root, dirs, files in os.walk(cards_dir):
        current_dir=os.path.basename(root)
        for file_name in os.listdir(current_dir):
            if file_name.endswith(".jpg") or file_name.endswith("png"):
                name, extension=os.path.splitext(file_name)
                path=os.path.join(current_dir,file_name)
                path=path.replace("\\","/")
                name=name.replace("_"," ")
                
                print("insert into monster_trade values (" + str(id) + "," + str(50) + "," + str(2000) + ");")

                id=id+1
# create_cards_database()
# create_books_database()
# create_skills_database()
# create_army_database()
# create_collaboration_equipments_database()
# create_equipments_database()
# create_monster_database()
# create_items_database()
# create_achievements_database()
# create_titles_database()
# create_currencies_database()
# create_pets_database()
# create_symbols_database()
# create_medals_database()
# create_borders_database()
# create_collaboration_database()

create_cards_trade()
create_book_trade()
create_army_trade()
create_skills_trade()
create_collaboration_equipments_trade()
create_pets_trade()
create_symbols_trade()
create_medals_trade()
create_achievements_trade()
create_titles_trade()
create_borders_trade()
create_items_trade()
create_equipments_trade()
create_collaboration_trade()
create_monster_trade()