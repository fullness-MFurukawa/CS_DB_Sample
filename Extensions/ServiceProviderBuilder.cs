using CS_DB_Sample.Domains.Adapters;
using CS_DB_Sample.Domains.Repositories;
using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Adapters;
using CS_DB_Sample.Infrastructures.Entities;
using CS_DB_Sample.Infrastructures.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace CS_DB_Sample.Extensions;
/// <summary>
/// DIコンテナを生成しServiceProviderを返すクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-23</date>
/// <version>1.0.0</version>
public static class ServiceProviderBuilder
{
    /// <summary>
    /// IServiceProviderを生成して返す
    /// </summary>
    /// <returns></returns>
    public static IServiceProvider Build()
    {
        // IServiceCollectionインターフェイス実装クラスを生成する
        var services = new ServiceCollection();

        // AppDBContextクラスを生成対象として登録する
        services.AddDbContext<AppDbContext>();

        // ItemCategoryEntityAdapterクラスを生成対象として登録する
        services.AddScoped<IItemCategoryAdapter<ItemCategoryEntity>,ItemCategoryEntityAdapter>();
        // ItemEntityAdapterクラスを生成対象として登録する
        services.AddScoped<IItemAdapter<ItemEntity>, ItemEntityAdapter>();

        // IItemRepositioryインターフェイス実装クラスを生成対象として登録する
        // <インターフェイス名 , インターフェイス実装クラス名>
        services.AddScoped<IItemRepository, ItemRepository>();

        // IServiceProviderを生成して返す
        var provider = services.BuildServiceProvider();
        return provider;
    }
}