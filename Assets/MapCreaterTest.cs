using UnityEngine;

public class MapCreaterTest : MonoBehaviour
{
    [SerializeField]int w, h;
    Array2D data;
    Array2D array2d;
    int sw, sh;
    int minAera = 10;
    Array2D room;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
         array2d= new Array2D(w, 0, h, 0);
         room=new Array2D(10, 0, 10, 0); //部屋のサイズを設定
    }
    void Start()
    {
        //for(int i = array2d.botton; i < array2d.top; i++) {
        //    for (int j = array2d.left; j < array2d.right; j++)
        //    {
        //        Vector3 pos = new Vector3(i * 9, 0, j * 9);
        //        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
        //        cube.transform.position = pos;
        //        cube.transform.localScale = new Vector3(9, 1, 9);

               
        //    }
        //}
        //sw = w;
        //sw = h;
    }
    private void FixedUpdate()
    {
        if (sw != w || sh != h)
        {
            
            for (int i = array2d.botton; i < w; i++)
            {
                for (int j = array2d.left; j < h; j++)
                {
                    Vector3 pos = new Vector3(i * 1, 0, j * 1);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = pos;
                    cube.transform.localScale = new Vector3(1, 1, 1);


                }
            }
            sw = w;
            sh = h;
        }
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

    }
