using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    //public int width = 10;
   // public int height = 10;
    public float roomSpacing = 15f;
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

            // 重複を避ける
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

    /// <summary>
    /// roomPrefabを指定のグリッド位置に配置します。
    /// </summary>
    /// <param name="gridPos"></param>
    void PlaceRoom(Vector2Int gridPos)
    {
        Vector3 worldPos = new Vector3(gridPos.x * roomSpacing, 0, gridPos.y * roomSpacing);
        Instantiate(roomPrefab, worldPos, Quaternion.Euler(GetRoomRot(90)), transform);
    }
    /// <summary>
    /// 次の部屋の位置をランダムに決定します。
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    Vector2Int GetNextPosition(Vector2Int current)
    {
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };
        Vector2Int dir = directions[Random.Range(0, directions.Length)];
        return current + dir;
    }
    /// <summary>
    /// 部屋の回転をランダムに決定します。
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    Vector3 GetRoomRot(float angle)
    {
        Vector3[] rot =
        {
            Vector3.up, Vector3.down,
            Vector3.zero
        };
        Vector3 vector3 = rot[Random.Range(0, rot.Length)];
        return vector3* angle;
    }
}
