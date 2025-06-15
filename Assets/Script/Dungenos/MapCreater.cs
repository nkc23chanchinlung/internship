using UnityEngine;

public class MapCreater : MonoBehaviour
{
    [SerializeField] int MapSize;
    [SerializeField] GameObject[] MapTiles;// 0:Ground,1:Wall 2:90度Wall
    [SerializeField] GameObject PlayerPrefab; // プレイヤーのプレハブ
    int[,] MapData; // 0:Ground,1:Wall
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapData = new int[,]        //マップデータ
        {
            {3,2,2,2,2,2,2,2,2,3},
            {1,0,0,0,0,0,0,0,0,1},
            {1,0,2,2,2,2,2,0,0,1},
            {1,0,0,0,0,0,1,0,0,1},
            {1,0,2,2,2,2,1,0,0,1},
            {1,0,0,0,0,0,0,0,0,1},
            {1,0,2,2,2,2,2,2,0,1},
            {1,0,0,0,0,0,0,0,0,1},
            {1,2,2,2,0,2,2,2,2,1},
            {1,0,0,0,0,0,0,0,0,1},
            {3,2,2,2,2,2,2,2,2,3}
        };
        


        for (int x = 0; x < MapSize+1; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                
                Instantiate(MapTiles[0], new Vector3(z * 4, 0, x * 4), Quaternion.identity);//Ground 生成
                if (MapData[x, z] == 1)
                {
                    Instantiate(MapTiles[1], new Vector3(x * 4, 0, (z) * 4), Quaternion.identity);//Wall 生成
                }
                else if (MapData[x, z] == 2)
                {
                    Instantiate(MapTiles[1], new Vector3(x * 4, 0, (z +1)* 4), Quaternion.Euler(new Vector3(0, 90, 0)));//90度Wall 生成
                }
                else if (MapData[x, z] == 3)
                {
                    Instantiate(MapTiles[3], new Vector3(x * 4, 0, (z + 1) * 4), Quaternion.Euler(new Vector3(0, 90, 0)));//90度Wall 生成
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
