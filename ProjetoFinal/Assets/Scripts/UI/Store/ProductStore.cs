using UnityEngine;

public class ProductStore : Store<CleaningProduct>
{
    private void Start()
    {
        DisplayList();
    }

    protected override void DisplayList()
    {
        foreach (var product in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            button.GetComponent<ProductButton>().Setup(product.ProductID, product.NumberOfUses, product.Cost);
        }
    }
}