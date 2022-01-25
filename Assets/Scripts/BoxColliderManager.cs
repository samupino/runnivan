using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxColliderManager : MonoBehaviour {

    public NamedBox[] boxes;
    Dictionary<string, NamedBox> map = new Dictionary<string, NamedBox>();

    void Awake() {
        foreach (NamedBox box in boxes) {
            map[box.name] = box;
        }
    }

    public void SetColliderDimensionsTo(BoxCollider2D collider, string boxName) {
        NamedBox box = map[boxName];
        collider.size = box.size;
        collider.offset = box.offset;
    }
}

[System.Serializable]
public struct NamedBox {
    public string name;
    public Vector2 offset;
    public Vector2 size;
}