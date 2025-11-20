using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        var accessor = new ItemAccessor(new AppDbContext());
        var item = new Item { Id = 29};
        // 商品:消しゴムの単価を変更する
        item = accessor.DeleteById(item);
        Console.WriteLine($"削除された商品 {item}");
        
        // 削除する商品リスト
        var items = new List<Item>
        {
            new Item { Id = 30 },
            new Item { Id = 31 }
        };
        // 商品を一括変更する
        accessor.DeleteRange(items);
        foreach (var i in items)
        {
            Console.WriteLine($"削除された商品 {i}");
        }
    }
}
