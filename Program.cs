using CS_DB_Sample.Domains.Repositories;
using CS_DB_Sample.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        // DIコンテナを生成し、IServiceProviderを返す
        var provider = ServiceProviderBuilder.Build();
        // IItemRepositoryインターフェイス実装を取得する
        var repository = provider!.GetService<IItemRepository>();

        var items = repository!.FindAll();
        foreach (var item in items)
        {
            Console.WriteLine($"商品Id:{item.Id}, 商品名:{item.Name}, 価格:{item.Price}");
        }
    }
}
