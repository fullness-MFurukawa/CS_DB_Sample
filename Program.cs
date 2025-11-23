using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Adapters;
using CS_DB_Sample.Infrastructures.Repositories;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        // DbContext継承クラスのインスタンスを生成
        using var context = new AppDbContext();
        // ItemエンティティとドメインオブジェクトItemModelの変換を行うアダプター
        var adapter = new ItemEntityAdapter();
        // 商品リポジトリのインスタンスを生成
        var itemRepository = new ItemRepository(adapter, context);
        // 商品Idで問合せ
        var item = itemRepository.FindById(2);
        Console.WriteLine(item);
        // 商品の存在有無
        var result = itemRepository.Exists("防水スプレー");
        Console.WriteLine($"データの有無:{result}");
    }
}
