using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    //public int width = 10;
   // public int height = 10;
    public float roomSpacing = 10f;
    public int maxRooms = 20;

    private HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        Vector2Int currentPos = Vector2Int.zero;
        roomPositions.Add(currentPos);
        PlaceRoom(currentPos);

        for (int i = 0; i < maxRooms - 1; i++)
        {
            Vector2Int nextPos = GetNextPosition(currentPos);

            // d•¡‚ð”ð‚¯‚é
            int attempts = 0;
            while (roomPositions.Contains(nextPos) && attempts < 10)
            {
                nextPos = GetNextPosition(currentPos);
                attempts++;
            }

            if (attempts >= 10)
                break;

            currentPos = nextPos;
            roomPositions.Add(currentPos);
            PlaceRoom(currentPos);
        }
    }

    void PlaceRoom(Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(gridPos.x * roomSpacing, 0, gridPos.y * roomSpacing);
        Instantiate(roomPrefab, worldPos, Quaternion.identity, transform);
    }

    Vector2Int GetNextPosition(Vector2Int current)
    {
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };
        Vector2Int dir = directions[Random.Range(0, directions.Length)];
        return current + dir;
    }
}
