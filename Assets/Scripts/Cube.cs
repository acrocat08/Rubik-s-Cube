using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    static readonly int rotateFrame = 10;

    Transform container;

    public int xp, xm, yp, ym, zp, zm;


    public void Draw(List<Material> colors) {
        transform.Find("Panel X+").GetComponent<MeshRenderer>().material = colors[xp];
        transform.Find("Panel X-").GetComponent<MeshRenderer>().material = colors[xm];
        transform.Find("Panel Y+").GetComponent<MeshRenderer>().material = colors[yp];
        transform.Find("Panel Y-").GetComponent<MeshRenderer>().material = colors[ym];
        transform.Find("Panel Z+").GetComponent<MeshRenderer>().material = colors[zp];
        transform.Find("Panel Z-").GetComponent<MeshRenderer>().material = colors[zm];
    }

    public void Rotate(ref int a, ref int b, ref int c, ref int d) {
        int tmp = d;
        d = c;
        c = b;
        b = a;
        a = tmp;
    }

    public void RotateXPlus() {
        Rotate(ref yp, ref zp, ref ym, ref zm);
        StartCoroutine(RotateAnimation(Vector3.right, 1));
    }

    public void RotateXMinus() {
        Rotate(ref yp, ref zm, ref ym, ref zp);
        StartCoroutine(RotateAnimation(Vector3.right, -1));
    }
    public void RotateYPlus() {
        Rotate(ref zp, ref xp, ref zm, ref xm);
        StartCoroutine(RotateAnimation(Vector3.up, 1));
    }

    public void RotateYMinus() {
        Rotate(ref zp, ref xm, ref zm, ref xp);
        StartCoroutine(RotateAnimation(Vector3.up, -1));
    }

    public void RotateZPlus() {
        Rotate(ref xp, ref yp, ref xm, ref ym);
        StartCoroutine(RotateAnimation(Vector3.forward, 1));
    }

    public void RotateZMinus() {
        Rotate(ref xp, ref ym, ref xm, ref yp);
        StartCoroutine(RotateAnimation(Vector3.forward, -1));
    }

    IEnumerator RotateAnimation(Vector3 axis, int dir) {
        float deg = 90f * dir / rotateFrame;
        var angleAxis = Quaternion.AngleAxis(deg, axis);
        for(int i = 0; i < rotateFrame; i++) {
            transform.localPosition = angleAxis * transform.localPosition;
            transform.Rotate(axis * deg, Space.World);
            yield return null;
        }
    }


    public void SetContainer(Transform contaier) {
        this.container = contaier;
    }


}
