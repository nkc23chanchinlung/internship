using UnityEngine;
using System.Collections.Generic;

public class MapCreaterTest : MonoBehaviour
{
    [SerializeField]int w, h;
    Array2D data;
    Array2D array2d;
    int sw, sh;
    int minAera = 10;
    Array2D room;
   GameObject cube;
    List<GameObject> cubes = new List<GameObject>();
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
         array2d= new Array2D(w, 0, h, 0);
         room=new Array2D(minAera, 0, minAera, 0); //部屋のサイズを設定
        
        
    }
    void Start()
    {
       
    }
    private void FixedUpdate()
    {
        PlayingGround();
    }
    //部屋を作成するメソッド
    void createroom(int roomquanity)
    {
       
    }
   public class Array2D
    {
        public int right, left, top, botton;
        public Array2D(int r, int l, int t, int b)
        {
            right = r-1;
            left = l;
            top = t-1;
            botton = b;
        }
    }
    void PlayingGround()
    {
        if (sw != w || sh != h)
        {
            // 前のキューブを削除
            foreach (GameObject c in cubes)
            {
                Destroy(c); // 生成したキューブを削除
            }
            // 新しいキューブを生成
            for (int i = array2d.botton; i < w; i++)
            {
                for (int j = array2d.left; j < h; j++)
                {
                    Vector3 pos = new Vector3(i * 1, 0, j * 1);
                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = pos;
                    cube.transform.localScale = new Vector3(1, 1, 1);
                    cubes.Add(cube); // 生成したキューブをリストに追加


                }
            }
            sw = w;
            sh = h;
        }
    }

 }
