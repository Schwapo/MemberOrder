#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using UnityEngine;

public class MemberOrderPropertyProcessor : OdinPropertyProcessor
{
    private MemberOrderAttribute memberOrderAttribute;
    private Dictionary<string, float> memberOrder = new Dictionary<string, float>();

    public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
    {
        memberOrderAttribute = Property.GetAttribute<MemberOrderAttribute>();

        if (memberOrderAttribute == null)
            return;

        var matches = Regex.Matches(memberOrderAttribute.Order, @"(?<order>.+)=(?<memberName>.+)");

        if (matches.Count == 0)
            return;

        foreach (Match match in matches)
        {
            var orderValue = match.Groups["order"].Value.Trim();
            var orderResolvedString = orderValue.StartsWith("@") ? orderValue : $"@{orderValue}";
            var orderResolver = ValueResolver.Get<float>(Property, orderResolvedString);

            var memberValue = match.Groups["memberName"].Value.Trim();
            var memberResolver = ValueResolver.GetForString(Property, memberValue);

            if (orderResolver.HasError)
                Debug.Log(orderResolver.ErrorMessage);

            if (memberResolver.HasError)
                Debug.Log(memberResolver.ErrorMessage);

            memberOrder[memberResolver.GetValue()] = orderResolver.GetValue();
        }

        foreach (var propertyInfo in propertyInfos)
        {
            propertyInfo.Order = memberOrder.ContainsKey(propertyInfo.PropertyName)
                ? memberOrder[propertyInfo.PropertyName]
                : memberOrderAttribute.BaseOrder;
        }
    }
}
#endif