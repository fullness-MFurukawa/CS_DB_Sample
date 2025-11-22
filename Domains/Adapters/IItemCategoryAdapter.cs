using CS_DB_Sample.Domains.Models;
namespace CS_DB_Sample.Domains.Adapters;
/// <summary>
/// ドメインオブジェクトItemCategoryと他の形式のオブジェクトとの
/// 相互変換を行うアダプターインターフェイス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public interface IItemCategoryAdapter<T>
{
    /// <summary>
    /// 外部オブジェクトからドメインオブジェクトItemCategoryへ変換する
    /// </summary>
    /// <param name="source">外部オブジェクト</param>
    /// <returns>ドメインオブジェクトItemCategory</returns>
    ItemCategory ToDomain(T source);

    /// <summary>
    /// 外部オブジェクトのリストからドメインオブジェクトItemCategoryのリストへ変換する
    /// </summary>
    /// <param name="sources">外部オブジェクトのリスト</param>
    /// <returns>ドメインオブジェクトItemCategtoryのリスト</returns>
    List<ItemCategory> ToDomainList(List<T> sources);

    /// <summary>
    /// ドメインオブジェクトItemCategoryから外部オブジェクトへ変換する
    /// </summary>
    /// <param name="itemCategory">ドメインオブジェクトItemCategory</param>
    /// <returns>外部オブジェクト</returns>
    T FromDomain(ItemCategory itemCategory);

    /// <summary>
    /// ドメインオブジェクトItemCategoryのリストから外部オブジェクトのリストへ変換する
    /// </summary>
    /// <param name="itemCategories">ドメインオブジェクトItemCategoryのリスト</param>
    /// <returns>外部オブジェクト</returns>
    List<T> FromDomainList(List<ItemCategory> itemCategoryies);
}