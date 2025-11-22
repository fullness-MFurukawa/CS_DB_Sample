using CS_DB_Sample.Domains.Models;
namespace CS_DB_Sample.Domains.Adapters;
/// <summary>
/// ドメインオブジェクトItemと他の形式のオブジェクトとの
/// 相互変換を行うアダプターインターフェイス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public interface IItemAdapter<T>
{
    /// <summary>
    /// 外部オブジェクトからドメインオブジェクトItemへ変換する
    /// </summary>
    /// <param name="source">外部オブジェクト</param>
    /// <returns>ドメインオブジェクトItem</returns>
    Item ToDomain(T source);

    /// <summary>
    /// 外部オブジェクトのリストからドメインオブジェクトItemのリストへ変換する
    /// </summary>
    /// <param name="sources">外部オブジェクトのリスト</param>
    /// <returns>ドメインオブジェクトItemのリスト</returns>
    List<Item> ToDomainList(List<T> sources);

    /// <summary>
    /// ドメインオブジェクトItemから外部オブジェクトへ変換する
    /// </summary>
    /// <param name="item">ドメインオブジェクトItem</param>
    /// <returns>外部オブジェクト</returns>
    T FromDomain(Item item);

    /// <summary>
    /// ドメインオブジェクトItemのリストから外部オブジェクトのリストへ変換する
    /// </summary>
    /// <param name="items">ドメインオブジェクトItemのリスト</param>
    /// <returns>外部オブジェクト</returns>
    List<T> FromDomainList(List<Item> items);
}