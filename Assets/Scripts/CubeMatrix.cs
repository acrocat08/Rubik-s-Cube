using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMatrix : MonoBehaviour {

    Cube[,,] cubes;
    [SerializeField] GameObject cubePrefab;

    [SerializeField] int size;
    [SerializeField] List<Material> colors;

    public void Start() {
        cubes = new Cube[size, size, size];
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                for(int k = 0; k < size; k++) {
                    var obj = GameObject.Instantiate(cubePrefab);
                    obj.transform.SetParent(transform);
                    obj.transform.localPosition = new Vector3(i, j, k) - Vector3.one * (size - 1) / 2f;
                    cubes[i, j, k] = obj.GetComponent<Cube>();
                    SetColor(cubes[i, j, k], i, j, k);
                }  
            }
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) RotateXPlus(0);
        if(Input.GetKeyDown(KeyCode.W)) RotateXMinus(0);
        if(Input.GetKeyDown(KeyCode.E)) RotateYPlus(0);
        if(Input.GetKeyDown(KeyCode.R)) RotateYMinus(0);
        if(Input.GetKeyDown(KeyCode.T)) RotateZPlus(0);
        if(Input.GetKeyDown(KeyCode.Y)) RotateZMinus(0);
    }

    void SetColor(Cube cube, int x, int y, int z) {
        cube.xp = x == size - 1 ? 0 : 6;
        cube.xm = x == 0        ? 1 : 6;
        cube.yp = y == size - 1 ? 2 : 6;
        cube.ym = y == 0        ? 3 : 6;
        cube.zp = z == size - 1 ? 4 : 6;
        cube.zm = z == 0        ? 5 : 6;
        cube.Draw(colors);
    }

    Cube[,] GetRow(int x, int y, int z) {
        Cube[,] ret = new Cube[size, size];
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                int xi = x >= 0 ? x : z >= 0 ? i : j;
                int yi = y >= 0 ? y : x >= 0 ? i : j;
                int zi = z >= 0 ? z : y >= 0 ? i : j;
                ret[i, j] = cubes[xi, yi, zi];
            }
        }
        return ret;
    }

    void RotateMatrix(int x, int y, int z, int dir) {
        var tmp = GetRow(x, y, z);
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                int xi = x >= 0 ? x : z >= 0 ? i : j;
                int yi = y >= 0 ? y : x >= 0 ? i : j;
                int zi = z >= 0 ? z : y >= 0 ? i : j;
                if(dir == 1) cubes[xi, yi, zi] = tmp[j, size - i - 1];
                else         cubes[xi, yi, zi] = tmp[size - j - 1, i];
            }
        }
    }

    void RotateXPlus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[row, i, j].RotateXPlus();
            }
        }
        RotateMatrix(row, -1, -1, 1);
    }

    void RotateXMinus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[row, i, j].RotateXMinus();
            }
        }
        RotateMatrix(row, -1, -1, -1);
    }

    void RotateYPlus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[j, row, i].RotateYPlus();
            }
        }
        RotateMatrix(-1, row, -1, 1);
    }

    void RotateYMinus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[j, row, i].RotateYMinus();
            }
        }
        RotateMatrix(-1, row, -1, -1);
    }

    void RotateZPlus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[i, j, row].RotateZPlus();
            }
        }
        RotateMatrix(-1, -1, row, 1);
    }

    void RotateZMinus(int row) {
        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                cubes[i, j, row].RotateZMinus();
            }
        }
        RotateMatrix(-1, -1, row, -1);
    }


}
