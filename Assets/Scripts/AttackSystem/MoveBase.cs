using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase
{
    public string Name { get; set;}
    public string Description { get; set;}
    public int Power { get; set;}
    public int Accuracy { get; set;}
    public Target Target { get; set;}
    public Category Category { get; set;}
}


public enum Target
{
    SELF,OTHER
}

public enum Category
{
    STATUS,DIRECT
}