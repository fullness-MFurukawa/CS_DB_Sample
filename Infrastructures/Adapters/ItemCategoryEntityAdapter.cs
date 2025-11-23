using CS_DB_Sample.Domains.Adapters;
using CS_DB_Sample.Domains.Models;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample.Infrastructures.Adapters;
/// <summary>
/// ItemCategoryEntityとドメインオブジェクトItemCategoryの変換を行うアダプター
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-23</date>
/// <version>1.0.0</version>
public class ItemCategoryEntityAdapter : IItemCategoryAdapter<ItemCategoryEntity>
{
    // ItemEntityとドメインオブジェクトItemの変換を行うアダプター
    private readonly IItemAdapter<ItemEntity> _itemAdapter;
    /// <summary>
    /// コンストラクタ(商品アダプターの注入)
    /// </summary>
    /// <param name="itemAdapter"></param>
    public ItemCategoryEntityAdapter(IItemAdapter<ItemEntity> itemAdapter)
    {
        _itemAdapter = itemAdapter;
    }

    /// <summary>
    /// ItemCategoryEntityからドメインオブジェクトItemCategoryを復元する
    /// </summary>
    /// <param name="source">ItemCategoryEntity</param>
    /// <returns>ドメインオブジェクトItemCategory</returns>
    public ItemCategory ToDomain(ItemCategoryEntity source)
    {
        if (source == null)
            throw new ArgumentNullException("引数がnullのため復元できません。");
        var itemCategory =  new ItemCategory(source.Id, source.Name!);
        // カテゴリに属する商品がある場合
        if (source.Items != null)
        {
            // ItemEntityをItemに変換して、ItemCategoryのItemsプロパティに設定する
            var items = _itemAdapter.ToDomainList(source.Items);
            itemCategory.ChangeItems(items);
        }
        return itemCategory; // 生成したItemCategoryを返す
    }

    /// <summary>
    /// ItemCategoryEntityのリストからドメインオブジェクトItemCategoryのリストを復元する
    /// </summary>
    /// <param name="sources">ItemCategoryEntityのリスト</param>
    /// <returns>ドメインオブジェクトItemCategtoryのリスト</returns>
    public List<ItemCategory> ToDomainList(List<ItemCategoryEntity> sources)
    {
        if (sources == null)
            throw new ArgumentNullException("引数がnullのため復元できません。");
        // ItemCategoryのリストを生成する
        var itemCategories = new List<ItemCategory>();
        // ItemCategoryEntityのリストをItemCategoryのリストを復元する
        foreach (var source in sources)
        {
            itemCategories.Add(ToDomain(source));
        }
        return itemCategories; // ItemCategoryのリストを返す
    }

    /// <summary>
    /// ドメインオブジェクトItemCategoryからItemCategoryEntityへ変換する
    /// </summary>
    /// <param name="itemCategory">ドメインオブジェクトItemCategory</param>
    /// <returns>ItemCategoryEntity</returns>
    public ItemCategoryEntity FromDomain(ItemCategory itemCategory)
    {
        if (itemCategory == null)
            throw new ArgumentNullException("引数がnullのため変換できません。");
        // ItemCategoryEntityを生成して返す
        return new ItemCategoryEntity
        {
            Id = itemCategory.Id,
            Name = itemCategory.Name
        };
    }

    /// <summary>
    /// ドメインオブジェクトItemCategoryのリストからItemCategoryEntityのリストへ変換する
    /// </summary>
    /// <param name="itemCategories">ドメインオブジェクトItemCategoryのリスト</param>
    /// <returns>List<ItemCategoryEntity></returns>
    public List<ItemCategoryEntity> FromDomainList(List<ItemCategory> itemCategories)
    {
        if (itemCategories == null)
            throw new ArgumentNullException("引数がnullのため変換できません。");
        // ItemCategoryEntityのリストを生成する
        var itemCategoryEntities = new List<ItemCategoryEntity>();
        // ItemCategoryのリストからItemCategoryEntityのリストに変換する
        foreach (var itemCategory in itemCategories)
        {
            itemCategoryEntities.Add(FromDomain(itemCategory));
        }
        return itemCategoryEntities; // ItemCategoryEntityのリストを返す
    }
}