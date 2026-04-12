using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum TargetType
{
    None,
    Ground,
    Enemy,
    Item
}
[SerializeField]
public class Target
{
    public TargetType type;
    public Transform transform;
}
