using System;

[AttributeUsage(AttributeTargets.Class)]
public class MemberOrderAttribute : Attribute
{
    public string Order { get; }
    public float BaseOrder { get; }

    public MemberOrderAttribute(string order, float baseOrder = 0f) 
        => (Order, BaseOrder) = (order, baseOrder);
}