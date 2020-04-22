using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDropSolver : MonoBehaviour
{
    public int numberToCheck;
    public int numberToDrop;

    List<int> listOfInts = new List<int>();
    List<int> removedInts = new List<int>();
    int[] optimalValues;

    void Start() {

        GetIntArray(numberToCheck);
        SolveCode();
    }

    void GetIntArray(int num) {
        
        while (num > 0) {
            listOfInts.Add(num % 10);
            num = num / 10;
        }
        listOfInts.Reverse();
    }

    void SolveCode() {

        int firstLarge = -1;
        int firstPos = -1;

        for(int i = 0; i <= numberToDrop; i++) {
            Debug.Log(listOfInts[i]);
            if (listOfInts[i] > firstLarge) {
                firstLarge = listOfInts[i];
                DropInt(firstPos);
                firstPos = i;
            }
        }

    }

    void DropInt(int intDrop) {
        if (intDrop < 0 || intDrop >= listOfInts.Count) return;
        removedInts.Add(listOfInts[intDrop]);
        listOfInts.Remove(intDrop);
    }
}
