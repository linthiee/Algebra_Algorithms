using UnityEngine;
using static Algorithms;

public class Test : MonoBehaviour
{
    private void Start()
    {
        int[] a = { -1, 4, -3, 8, 5, 9, 0, 22, 90, 88, 11 };
        GnomeSort(a);
    }
}