using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private GameObject upgradeIndicator;
    private Tower tower;
    private int currentMoney;

    public int upgradeCost;

    private void Awake()
    {
        currentMoney = GameManager.Instance.towerManager.currentMoney;
        tower = gameObject.GetComponent<Tower>();
        upgradeCost = tower.value * 2;
    }
    private void Update()
    {
        if (currentMoney >= upgradeCost)
        {
            upgradeIndicator.SetActive(true);
        }
    }
}
