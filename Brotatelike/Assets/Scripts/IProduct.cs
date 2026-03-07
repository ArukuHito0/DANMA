using System;
using UnityEngine;

public interface IProduct
{
    TierType Tier { get; }
    Sprite Icon { get; }
    string Name { get; }
    uint Price { get; }

    void PayProduct();
    string GetDescriptionText();
}

