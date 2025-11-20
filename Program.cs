using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        var accessor = new ItemAccessor(new AppDbContext());
        var item = new Item { Name = "消しゴム", Price = 120, CategoryId = 1 };


        // 商品を登録する
        item = accessor.Create(item);
        Console.WriteLine($"登録された商品 {item}");
        // 登録する商品リスト
        var items = new List<Item>
        {
            new Item { Name = "砂消しゴム", Price = 120, CategoryId = 1 },
            new Item { Name = "定規", Price = 150, CategoryId = 1 }
        };
        // 商品を一括登録する
        accessor.CreateRange(items);
        foreach (var i in items)
        {
            Console.WriteLine($"登録された商品 {i}");
        }
    }
}
