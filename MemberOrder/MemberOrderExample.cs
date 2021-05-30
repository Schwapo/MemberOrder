using UnityEngine;

[MemberOrder(@"
0 = someMember03
1 = someMember05
@someMember01Order = someMember01
3 = @someMember04Name
4 = @GetMember02Name()
")]
public class MemberOrderExample : MonoBehaviour
{
    public string someMember01;
    public string someMember02;
    public string someMember03;
    public string someMember04;
    public string someMember05;

    private float someMember01Order = 2f;
    private string someMember04Name = "someMember04";

    private string GetMember02Name()
    {
        return "someMember02";
    }
}