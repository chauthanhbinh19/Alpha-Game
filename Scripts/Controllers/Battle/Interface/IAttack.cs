using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public interface IAttack
{
    void PhysicalAttack(CardBase cardEnemy);
    void MagicalAttack(CardBase cardEnemy);
    void ChemicalAttack(CardBase cardEnemy);
    void AtomicAttack(CardBase cardEnemy);
    void MentalAttack(CardBase cardEnemy);
}