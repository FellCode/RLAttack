using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Moves/New Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string moveName;
    [TextArea] [SerializeField] private string description;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int critChance;
    [SerializeField] private Target target;
    [SerializeField] private Category category;
    [SerializeField] private MoveEffects effects;

    public string MoveName()
    {
        return moveName;
    }
    
    public string Description()
    {
        return description;
    }
    
    public int Power()
    {
        return power;
    }
    
    public int Accuracy()
    {
        return accuracy;
    }

    public int CritChance()
    {
        return critChance;
    }
    
    public Target Target()
    {
        return target;
    }
    
    public Category Category()
    {
        return category;
    }

    public MoveEffects Effects()
    {
        return effects;
    }
}



public enum Target
{
    SELF,OTHER
}

public enum Category
{
    STATUS,DIRECT
}

[System.Serializable]
public class MoveEffects
{
    [SerializeField] ConditionID status;

    public ConditionID Status => status;
}