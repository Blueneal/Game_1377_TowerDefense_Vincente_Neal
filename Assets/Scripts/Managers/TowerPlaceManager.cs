using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public int startingMoney;
    public Camera mainCamera;
    public LayerMask tileLayer;
    public InputAction placeTowerAction;

    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private float placementHeightOffset = 0.2f;
    [SerializeField] private TextMeshProUGUI moneyText;
    private GameObject currentTowerToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPos;
    private int currentMoney;

    void Start()
    {
        currentMoney = startingMoney;
        moneyText.text = "Money: $" + currentMoney;
    }

    
    void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, tileLayer))
            {
                towerPlacementPos = hitInfo.transform.position + Vector3.up * placementHeightOffset;
                towerPreview.transform.position = towerPlacementPos;
                towerPreview.SetActive(true);
            }
            else
            {
                towerPreview.SetActive(false);
            }
        }
        moneyText.text = "Money: $" + currentMoney;
    }

    private void OnEnable()
    {
        placeTowerAction.Enable();
        placeTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        placeTowerAction.performed -= OnPlaceTower;
        placeTowerAction.Disable();
    }

    public void StartPlacingTower(GameObject towerPrefab)
    {
        if (currentTowerToSpawn != towerPrefab)
        {
            isPlacingTower = true;
            currentTowerToSpawn = towerPrefab;
            if (towerPreview != null)
            {
                Destroy(towerPreview);
            }
            towerPreview = Instantiate(currentTowerToSpawn);
        }
    }

    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (!isPlacingTower)
        {
            return;
        }
        Instantiate (currentTowerToSpawn, towerPlacementPos, Quaternion.identity);
        Destroy(towerPreview);
        currentTowerToSpawn = null;
        isPlacingTower = false;
    }
}
