using UnityEngine;

public class MapCreater : MonoBehaviour
{
    [SerializeField] int MapSize;
    [SerializeField] GameObject[] MapTiles;// 0:Ground,1:Wall 2:90度Wall 3:90度Wall(fire) 4:Wall(fire)
    [SerializeField] GameObject PlayerPrefab; // プレイヤーのプレハブ
    [SerializeField] Transform Map;
    int[,] MapData; // 0:Ground,1:Wall
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapData = new int[MapSize, MapSize]; // マップデータの初期化
        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                MapData[x, z] = Random.Range(0, 2); // 0:Ground,1:Wall

            }
        }
        for (int x = 0; x < MapSize*4 ; x++)
        {
            for (int z = 0; z < MapSize*4 ; z++)
            {
                Instantiate(MapTiles[0], new Vector3(z * 4, 0, x * 4), Quaternion.identity, Map);//Ground 生成
            }
        }

                for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                
               
                if (MapData[x, z] == 1)
                {
                    Instantiate(MapTiles[1], new Vector3(x * 12, 0, z * 12)+new Vector3(10,0,10), Quaternion.Euler(new Vector3(0, 90*Random.Range(0,4), 0)), Map);//Wall 生成
                }
                //else if (MapData[x, z] == 2)
                //{
                //    Instantiate(MapTiles[1], new Vector3(x * 4, 0, z * 4), Quaternion.Euler(new Vector3(0, 90, 0)), Map);//90度Wall 生成
                //}
                //else if (MapData[x, z] == 3)
                //{
                //    Instantiate(MapTiles[3], new Vector3(x * 4, 0, z * 4), Quaternion.Euler(new Vector3(0, 90, 0)), Map);//90度Wall 生成
                //}
                //else if (MapData[x, z] == 4)
                //{
                //    Instantiate(MapTiles[4], new Vector3(x * 4, 0, z * 4), Quaternion.Euler(new Vector3(0, 90, 0)),Map);//90度Wall(fire) 生成
                //}
                //else if (MapData[x, z] == 5)
                //{
                //    Instantiate(MapTiles[4], new Vector3(x * 4, 0, z * 4), Quaternion.Euler(new Vector3(0, 0, 0)), Map);//Wall(fire) 生成
                //}

            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
