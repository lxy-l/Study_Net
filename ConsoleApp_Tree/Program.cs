
using System.Text.Json;

Dictionary<int, TreeModel> keyValuePairs = new Dictionary<int, TreeModel>()
{
    { 1, new TreeModel(){Id=1,Name="A",ParentId=0 } },
    { 2, new TreeModel(){Id=2,Name="B",ParentId=0 } },
    { 3, new TreeModel(){Id=3,Name="C",ParentId=0 } },
    { 4, new TreeModel(){Id=4,Name="A1",ParentId=1 } },
    { 5, new TreeModel(){Id=5,Name="A2",ParentId=1 } },
    { 6, new TreeModel(){Id=6,Name="B1",ParentId=2 } },
    { 7, new TreeModel(){Id=7,Name="B2",ParentId=2 } }
};

var tree = GetTree(keyValuePairs);

string json=JsonSerializer.Serialize(tree);
Console.WriteLine(json);
Console.ReadKey();


/// <summary>
/// 委托生成树形结构
/// </summary>
/// <param name="keyValues"></param>
/// <returns></returns>
List<TreeModel> GetTree(Dictionary<int, TreeModel> keyValues)
{
    Func<int, List<TreeModel>> func = null;
    func = m =>
    {
        List<TreeModel> list = new List<TreeModel>();
        var kv = keyValues.Where(kv => kv.Value.ParentId == m);
        foreach (var item in kv)
        {
            var childs = func(item.Value.Id);
            item.Value.Children.AddRange(childs);
            list.Add(item.Value);
        }
        return list;
    };
    return func(0);
}
public class TreeModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }

    public List<TreeModel> Children { get; set; }

    public TreeModel() => Children = new List<TreeModel>();


    //public override string ToString()
    //{
    //    return $"Id={Id};Name={Name};ParentId={ParentId}"; 
    //}
}