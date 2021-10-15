using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrays : MonoBehaviour
{
    public static Vector2[/*Length*/] FruitsArray1;
    public static Vector2[/*Length*/] PitfallsArray1;
    public static Vector2[/*Length*/] WallsArray1;
    public static Vector2[/*Length*/] FruitsArray2;
    public static Vector2[/*Length*/] PitfallsArray2;
    public static Vector2[/*Length*/] WallsArray2;
    public static Vector2[/*Length*/] FruitsArray3;
    public static Vector2[/*Length*/] PitfallsArray3;
    public static Vector2[/*Length*/] WallsArray3;
    public static Vector2[/*Length*/] FruitsArray4;
    public static Vector2[/*Length*/] PitfallsArray4;
    public static Vector2[/*Length*/] WallsArray4;
    public static Vector2[/*Length*/] FruitsArray5;
    public static Vector2[/*Length*/] PitfallsArray5;
    public static Vector2[/*Length*/] WallsArray5;
    //All of the arrays are instantiated above, where I've commented in /*Length*/, put the total number of that thing.
    //For example, if level 1 has 11 fruit, replace the /*Length*/ with 11 where FruitsArray1 is instantiated above.
    //Now is the fun part, filling the arrays! Since we have an even grid (30x30), we'll just say the bottom left corner is (0,0).
    //I'll start by doing the first few fruits of map 1 as an example. Keep in mind that the first element of an array is at position [0].
    private void Start()
    {
        Fruit1();
        Pitfall1();
        Wall1();
        Fruit2();
        Pitfall2();
        Wall2();
        Fruit3();
        Pitfall3();
        Wall3();
        Fruit4();
        Pitfall4();
        Wall4();
        Fruit5();
        Pitfall5();
        Wall5();

    }
    private void Fruit1()
    {
        FruitsArray1[0] = new Vector2(0, 0);
        FruitsArray1[1] = new Vector2(0, 3);
        FruitsArray1[2] = new Vector2(0, 5); // This'll do the 3 leftmost fruits on the first row of map 1, this same process can be used to build all the other arrays.
    }
    private void Pitfall1()
    {

    }
    private void Wall1()
    {

    }
    private void Fruit2()
    {

    }
    private void Pitfall2()
    {

    }
    private void Wall2()
    {

    }
    private void Fruit3()
    {

    }
    private void Pitfall3()
    {

    }
    private void Wall3()
    {

    }
    private void Fruit4()
    {

    }
    private void Pitfall4()
    {

    }
    private void Wall4()
    {

    }
    private void Fruit5()
    {

    }
    private void Pitfall5()
    {

    }
    private void Wall5()
    {

    }
}

