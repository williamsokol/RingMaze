
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RingGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ringFab;
    private GameObject parent;
    public int ringAmount;
    public bool autoGenerate;
    public int blockDensity;

    [SerializeField] public int[][] maze;

    struct path{
        public int[][] pathing;
        public bool isValid;

    }

    void Start()
    {
        parent = GameObject.Find("Environment");
        maze = DesignMaze();
        System.Console.Write("testing");
        for(int i=0; i< ringAmount;i++){
            GameObject ring = Instantiate(ringFab,new Vector3(0f,i*1.3225f,0f),Quaternion.identity,parent.transform);
            
            for(int j=0;j<ring.transform.childCount;j++){
                GameObject brick = ring.transform.GetChild(j).gameObject;
//               print(maze[i][j] +" part: "+i+" "+j);
                if(maze[i][j] == 1){
                    brick.SetActive(true);
                    //Time.timeScale = 0.1f;
                }else{
                    brick.SetActive(false);
                }
            }
        }
    }
    int Increment(int input){
        if(input > 0 ){
                return -1;
        }else{  return 1;}
    }
    bool RuleOne(int x, int y){
        if(maze[y][x] == 0){
            return true;
        }else{ return false;}
    }

    bool MakePath(int x,int y){
    
        // Rule number two
        int[,] list = new int[,]
        {
            {-2,-1},{-2,0},{-2,1},
            {-1,-1},{-1,0},{-1,1},
            {2,-1},{2,0},{2,1},
            {1,-1},{1,0},{1,1}
        };
        List<int[]> numbers = new List<int[]>();
        //it's retarded how you can't use rows on a C# 2d array
        for(int i=0;i<list.Length/2;i++){
             numbers.Add(new int[]{list[i,1],list[i,0]});
        }

        for(int i=0;i<numbers.Count;i++){
            int pickedNum = Random.Range(0,numbers.Count);

//            print(y+numbers[pickedNum][0] >=0 && y+numbers[pickedNum][0] < maze.Length);
            //Debug.Log((y+numbers[pickedNum][0]) +" "+(x+numbers[pickedNum][1]))
            if( y+numbers[pickedNum][0] < 0 || 
                y+numbers[pickedNum][0] >= maze.Length || 
                x+numbers[pickedNum][1] < 0 ||
                x+numbers[pickedNum][1] >= maze[0].Length )     
            {
//                print("test-1");
                numbers.RemoveAt(pickedNum);
                continue;
            }
            
            if(maze[y+numbers[pickedNum][0]][x+(numbers[pickedNum][1])] == 1)
                {
                    
//                print("test0: " + maze[y+numbers[pickedNum][0]][x+(numbers[pickedNum][1])]);
                bool valid = true;
                int axis = numbers[pickedNum][0] <0? 0:1;
                int count = Mathf.Abs(numbers[pickedNum][0]) + Mathf.Abs(numbers[pickedNum][1]);

                for(int j=0; j<=count; j++){
                    
//                    print("count: " + count);
                    //print(maze[y+numbers[pickedNum][0]+1]);
                    if(y+numbers[pickedNum][0]+1 < maze.Length){
                        
                        if(maze[y+numbers[pickedNum][0]+1][x+numbers[pickedNum][1]] == 1){
        //                      print("test1");
                                valid = false;
                                break;
                        }
       //             
                        maze[y+numbers[pickedNum][0]+1][x+numbers[pickedNum][1]] = 2;
                        print("thing x="+ x+" y="+y+" from:"+(x+numbers[pickedNum][1])+","+(y+numbers[pickedNum][0]));
                    }
                    axis = numbers[pickedNum][axis]==0? 1-axis:axis; // sees if we should maove directions

                    if(numbers[pickedNum][axis] != 0){
                            numbers[pickedNum][axis] += Increment(numbers[pickedNum][axis]);
                    }
                }
                if(valid){
                    print("thing x="+ x+" y="+y+" from:"+(numbers[pickedNum][1])+","+(numbers[pickedNum][0]));
                    return true;
                }
            }    
            
            numbers.RemoveAt(pickedNum);
        
        }
//    print("test4");
    return false;


    }

    // Update is called once per frame
    int[][] DesignMaze()
    {

        int ringBlockDensity = (int)((blockDensity)*  ringAmount * ringFab.transform.childCount)/100;
        print("qwerty " + ringBlockDensity);

        maze = new int[ringAmount][];
        for(int i=0; i<ringAmount; i++){
            maze[i] = new int[ringFab.transform.childCount];
        }
        maze[0][0] = 1;

        int blocks=0;
        while( blocks< ringBlockDensity){
            int x = Random.Range(0,(ringFab.transform.childCount));
            int y = Random.Range(0,(ringAmount));

            //path path = MakePath(x,y); 
            if(RuleOne(x,y)){
                if(MakePath(x,y) == true)
                {
//                    print("did somthing");
                    maze[y][x] = 1;
                    blocks++;
                }
            }
        }
        //maze[0][1] = 1;
        
        // int[][] path = new int[pathLength][];
        // for(int i=0; i<pathLength; i++){
        //     path[i] = new int[2];
        //     path[i][0] = Random.Range(-1,1+1);

        //     path[i][1] = Random.Range(-3,3+1);
        //     if( path[i][0]+grid.y < 0)
        //     {
        //         path[i][0] = (int)grid.y;
        //     }
        //     grid += new Vector2(path[i][1],path[i][0]);

        //     print(i+" | "+grid);
        //     // for(int j =0;j<path[i][1];j++){
        //     //     maze[][]
        //     // }    
        // }

        

        // for(int i=0; i< maze.Length; i++)
        // {
            
            
        //     for(int j=0; j<maze[i].Length; j++)
        //     {
                
                
        //         if(Random.value > .5){
        //             maze[i][j]=true;
        //         }
        //     }
            

        // }

        

        return maze;
    }
}
