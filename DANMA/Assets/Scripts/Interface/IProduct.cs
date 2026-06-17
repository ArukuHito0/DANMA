using System;
using UnityEngine;

public interface IProduct
{
    TierType Tier { get; }
    Sprite Icon { get; }
    string Name { get; }
    int Price { get; }

    void PayProduct();
    string GetDescriptionText();
}

