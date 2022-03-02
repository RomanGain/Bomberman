using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class OnLoad : MonoBehaviour
{
    const int MAX_BRICK_BLOCKS_PER_COLUMN = 5;
    const int MAX_ENEMIES_PER_GAMEFIELD = 10;

    [SerializeField]
    private int fieldWidth;
    [SerializeField]
    private int fieldHeight;
    [SerializeField]
    private int enemiesMaxAmount;

    private GameObject solidBlock;
    private GameObject brickBlock;

    private GameObject mrStrawberry;
    private GameObject player;
    private Grid grid;
    
    private bool isSetBrickBlock;

    List<FieldColumn> fieldCells; // here we will store info about internal cells, except borders

    void Awake()
    {
        fieldWidth = 31;
        fieldHeight = 15;

        var random = Random.value;

        fieldCells = new List<FieldColumn>();

        solidBlock = (GameObject)Resources.Load("Prefabs/Environment/SolidBlock", typeof(GameObject));
        brickBlock = (GameObject)Resources.Load("Prefabs/Environment/BrickBlock", typeof(GameObject));

        mrStrawberry = (GameObject)Resources.Load("Prefabs/Enemies/MrStrawberry-1", typeof(GameObject));
        player = GameObject.Find("Bomberman-1");
        grid = transform.parent.GetComponent<Grid>();

        GenerateBlocks();
        SpawnPlayer();
        SpawnEnemies();
        //SpawnExitDoor();
        //SpawnBonuses();
    }

    private bool IsBorder(int column, int row)
    {
        if (column == 0 || column == fieldWidth - 1 || row == 0 || row == fieldHeight - 1)
            return true;
        else
            return false;
    }

    private bool IsChessOrder(int column, int row)
    {
        if ((column % 2 == 0  && column != 0 && column != fieldWidth - 1)  && 
            (row % 2 == 0 && row != 0 && row != fieldHeight - 1))
            return true;
        else
            return false;
    }

    private bool IsBrickBlock(int column, int row)
    {
        if (column == 0 || column == fieldWidth - 1 || row == 0 || row == fieldHeight - 1)
            return true;
        else
            return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Generate Level
    private void GenerateBlocks()
    {
        Vector3 cellCenterPosition;
        int brickBlocksInCurrentColumn;

        FieldColumn fieldCellColumn;

        for (int i = 0; i < fieldWidth; i++)
        {
            brickBlocksInCurrentColumn = 0;
            fieldCellColumn = new FieldColumn();
            for (int j = 0; j < fieldHeight; j++)
            {
                FieldCell fieldCell = new FieldCell();
                fieldCell.CellCenterPosition = grid.GetCellCenterWorld(new Vector3Int(i, j, 0));

                if (IsBorder(i, j) || IsChessOrder(i, j))
                {
                    GenerateSolidBlock(fieldCell.CellCenterPosition);
                    fieldCell.CellType = CellType.SolidBlock;
                }
                else
                {
                    if (brickBlocksInCurrentColumn < MAX_BRICK_BLOCKS_PER_COLUMN)
                    {
                        GenerateBrickBlock(fieldCell.CellCenterPosition, fieldCell, ref brickBlocksInCurrentColumn);
                    }
                    else
                    {
                        fieldCell.CellType = CellType.Empty;
                    }
                }
                fieldCellColumn.FieldCells.Add(fieldCell);
            }
            fieldCells.Add(fieldCellColumn);
        }
    }

    private void GenerateSolidBlock(Vector3 cellCenterPosition)
    {
        Instantiate(solidBlock, cellCenterPosition, Quaternion.identity);
    }

    private void GenerateBrickBlock(Vector3 cellCenterPosition, FieldCell fieldCell, ref int brickBlocksInCurrentColumn)
    {
        isSetBrickBlock = (Random.value > .5);

        if (isSetBrickBlock)
        {
            Instantiate(brickBlock, cellCenterPosition, Quaternion.identity);
            fieldCell.CellType = CellType.BrickBlock;
            ++brickBlocksInCurrentColumn;
        }
        else
        {
            fieldCell.CellType = CellType.Empty;
        }
    }

    private void SpawnEnemies()
    {
        int currentEnemiesCounter = 0;
        bool isSetEnemyColumn;
        bool isSetEnemyRow;

        for (int i = 3; i < fieldWidth - 1; i += 3)
        {
            if (currentEnemiesCounter < MAX_ENEMIES_PER_GAMEFIELD)
            {
                isSetEnemyColumn = (Random.value > .7);

                if (isSetEnemyColumn)
                {
                    for (int j = 1; j < fieldHeight - 1; j++)
                    {
                        if (fieldCells[i].FieldCells[j].CellType == CellType.Empty)
                        {
                            isSetEnemyRow = (Random.value > .5);
                            if (isSetEnemyRow)
                            {
                                SpawnEnemy(fieldCells[i].FieldCells[j].CellCenterPosition);
                                ++currentEnemiesCounter;
                                continue;
                            }
                        }
                    }
                }
            }
            else
            {
                break;
            }
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition)
    {
        Instantiate(mrStrawberry, spawnPosition, Quaternion.identity);
    }

    private void SpawnPlayer()
    {
        FieldColumn firstFieldColumn = fieldCells[1];
        List<FieldCell> emptyCellsAtFirstFieldColumn = firstFieldColumn.FieldCells.FindAll(f => f.CellType == CellType.Empty);

        int rowCount = firstFieldColumn.FieldCells.Count;

        for (int i = 0; i < rowCount - 2; i++)
        {
            if (firstFieldColumn.FieldCells[i].CellType == CellType.Empty &&
                firstFieldColumn.FieldCells[i + 1].CellType == CellType.Empty &&
                firstFieldColumn.FieldCells[i + 2].CellType == CellType.Empty)
            {
                player.transform.position = firstFieldColumn.FieldCells[i].CellCenterPosition;
                return;
            }
        }
    }

    #endregion
}