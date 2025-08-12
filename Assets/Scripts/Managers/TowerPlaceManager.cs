using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public int startingMoney;
    public int currentMoney;
    public Camera mainCamera;
    public LayerMask tileLayer;
    public InputAction placeTowerAction;

    [SerializeField] private bool isPlacingTower = false;
    [SerializeField] private float placementHeightOffset = 0.2f;
    [SerializeField] private TextMeshProUGUI moneyText;
    private GameObject currentTowerToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacementPos;

    void Start()
    {
        currentMoney = startingMoney;
        moneyText.text = "Money: $" + currentMoney;
    }
    
    /// <summary>
    /// Checks to see if the player has enough money, and if so sets the correct button to on
    /// Checks to see if the player is placing a tower, and updates the preview of the tower being placed to where the mouse is on screen
    /// </summary>
    void Update()
    {
        moneyText.text = "Money: $" + currentMoney;

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

        if (currentMoney < 50)
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.crystalButton.interactable = false;
            if (currentMoney < 20)
            {
                gameManager.cannonButton.interactable = false;
                if (currentMoney < 10)
                {
                    gameManager.ballistaButton.interactable = false;
                }
            }
        }
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

    /// <summary>
    /// Instantiates a preview of the selected tower when the player selects any tower
    /// </summary>
    /// <param name="towerPrefab"></param>
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
            currentMoney -= towerPrefab.GetComponent<Tower>().value;
        }
    }

    /// <summary>
    /// Places the tower on to the grid where the player clicks on screen, if in a valid space
    /// </summary>
    /// <param name="context"></param>
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

    /// <summary>
    /// Adds money to the players pool for buying towers
    /// </summary>
    /// <param name="money"></param>
    public void AddMoney(int money)
    {
        currentMoney += money;
    }

}
