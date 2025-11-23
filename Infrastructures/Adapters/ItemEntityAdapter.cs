using CS_DB_Sample.Domains.Adapters;
using CS_DB_Sample.Domains.Models;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample.Infrastructures.Adapters;
/// <summary>
/// ItemEntityとドメインオブジェクトItemの変換を行うアダプター
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-23</date>
/// <version>1.0.0</version>
public class ItemEntityAdapter : IItemAdapter<ItemEntity>
{
    // ItemCategoryEntityとドメインオブジェクトItemCategoryの変換を行うアダプター
    private readonly IItemCategoryAdapter<ItemCategoryEntity> _categoryAdapter;
    /// <summary>
    /// コンストラクタ(カテゴリアダプターの注入)
    /// </summary>
    public ItemEntityAdapter(IItemCategoryAdapter<ItemCategoryEntity> categoryAdapter)
    {
        _categoryAdapter = categoryAdapter;
    }

    /// <summary>
    /// ItemEntityからドメインオブジェクトItemを復元する
    /// </summary>
    /// <param name="source">ItemEntity</param>
    /// <returns>ドメインオブジェクトItem</returns>
    public Item ToDomain(ItemEntity source)
    {
        if (source == null)
            throw new ArgumentNullException("引数がnullのため復元できません。");
        ItemCategory? itemCatgeory = null;
        if (source.Category != null)
        {
            // カテゴリが存在したら、ItemCategoryEntityをItemCategoryに変換する
            itemCatgeory = _categoryAdapter.ToDomain(source.Category);
        }
        // ドメインオブジェクトItemを生成して返す
        return new Domains.Models.Item(source.Id, source.Name!, source.Price, itemCatgeory);
    }

    /// <summary>
    /// ItemEntityのリストからドメインオブジェクトItemのリストを復元する
    /// </summary>
    /// <param name="sources">ItemEntityのリスト</param>
    /// <returns>ドメインオブジェクトItemのリスト</returns>
    public List<Item> ToDomainList(List<ItemEntity> sources)
    {
        if (sources == null)
            throw new ArgumentNullException("引数がnullのため復元できません。");
        var items = new List<Item>(); // Itemのリストを生成する
        // ItemEntityのリストからItemのリストを復元する
        foreach (var source in sources)
        {
            items.Add(ToDomain(source));
        }
        return items; // Itemのリストを返す
    }

    /// <summary>
    /// ドメインオブジェクトItemからItemEntityへ変換する
    /// </summary>
    /// <param name="item">ドメインオブジェクトItem</param>
    /// <returns>ItemEntity</returns>
    public ItemEntity FromDomain(Item item)
    {
        if (item == null)
            throw new ArgumentNullException("引数がnullのため変換できません。");
        // ItemEntityを生成して返す
        return new ItemEntity
        {
            Id = item.Id,
            Name = item.Name!,
            Price = item.Price,
            CategoryId = item.Category!.Id
        };
    }

    /// <summary>
    /// ドメインオブジェクトItemのリストからItemEntityのリストへ変換する
    /// </summary>
    /// <param name="items">ドメインオブジェクトItemのリスト</param>
    /// <returns>ItemEntityのリスト</returns>
    public List<ItemEntity> FromDomainList(List<Item> items)
    {
        if (items == null)
            throw new ArgumentNullException("引数がnullのため変換できません。");
        // ItemEntityのリストを生成する
        var itemEntities = new List<ItemEntity>();
        // ItemのリストをItemEntityのリストに変換する
        foreach (var item in items)
        {
            itemEntities.Add(FromDomain(item));
        }
        return itemEntities; // ItemEntityのリストを返す
    }
}