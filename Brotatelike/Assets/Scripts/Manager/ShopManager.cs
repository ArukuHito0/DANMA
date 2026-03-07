using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ProductBaseData> productDataList;
    [SerializeField] private ProductCard[] products;

    private HashSet<ProductBaseData> lineup = new HashSet<ProductBaseData>();

    public event Action OnEndShopping;

    private void OnEnable()
    {
        EnemyGenerator eg = FindObjectOfType<EnemyGenerator>();
        if(eg != null)
            eg.OnEndWave += UpdateProducts;
    }

    private void OnDisable()
    {
        EnemyGenerator eg = FindObjectOfType<EnemyGenerator>();
        if(eg != null)
            eg.OnEndWave -= UpdateProducts;
    }

    public void UpdateProducts()
    {
        foreach (ProductCard product in products)
        {
            if (product.isLocked) continue;
            else lineup.Remove(product.productData);
        }

        List<ProductBaseData> list = productDataList.Where(k => !lineup.Contains(k)).OrderBy(x => Guid.NewGuid()).ToList();
        
        int idx = 0;

        foreach (ProductCard product in products)
        {
            if(product.isLocked) continue;

            lineup.Add(list[idx]);
            product.SetProductData(list[idx]);
            product.Initialize();

            idx++;
        }
    }

    public void GoToNextWave()
    {
        OnEndShopping?.Invoke();
    }
}
