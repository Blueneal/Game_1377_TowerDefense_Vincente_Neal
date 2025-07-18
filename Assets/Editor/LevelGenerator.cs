using UnityEngine;
using UnityEditor;
using System;

public class LevelGenerator : EditorWindow
{
    public int gridSizeX;
    public int gridSizeZ;
    private GameObject tilePrefab;
    private Transform gridParent;
    private int sectionSpacing = 10;
    private GameObject[,] gridTiles;

    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("LevelGenerator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridSizeX = EditorGUILayout.IntField("Grid Size X", gridSizeX);
        gridSizeZ = EditorGUILayout.IntField("Grid Size Z", gridSizeZ);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Tile Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform)EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        GUILayout.Space(sectionSpacing);

        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }
    }

    private void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab is not assigned");
            return;
        }
        gridTiles = new GameObject[gridSizeX, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                gridTiles[x, z] = (GameObject) PrefabUtility.InstantiatePrefab(tilePrefab, gridParent);
                gridTiles[x, z].transform.position = position;
            }
        }
    }

    private void ClearGrid()
    {
        foreach (var tile in gridTiles)
        {
            DestroyImmediate(tile.gameObject);
        }
    }
}
