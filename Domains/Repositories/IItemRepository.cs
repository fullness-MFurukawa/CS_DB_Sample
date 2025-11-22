using CS_DB_Sample.Domains.Models;
namespace CS_DB_Sample.Domains.Repositories;
/// <summary>
/// 商品をCRUD操作するRepositoryインターフェイス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-22</date>
/// <version>1.0.0</version>
public interface IItemRepository
{
    /// <summary>
    /// すべての商品を取得する
    /// </summary>
    /// <returns>商品のリスト</returns>
    List<Item> FindAll();
    /// <summary>
    /// 指定された商品Idの商品を取得する
    /// </summary>
    /// <param name="id">商品Id</param>
    /// <returns>存在しない場合nullを返す</returns>
    Item? FindById(int id);
    /// <summary>
    /// 商品を永続化する
    /// </summary>
    /// <param name="item">永続化する商品</param>
    void Create(Item item);
    /// <summary>
    /// 指定された商品Idの商品を変更する
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool UpdateById(Item item);
    /// <summary>
    /// 指定された商品Idの商品を削除する
    /// </summary>
    /// <param name="id">商品名</param>
    /// <returns></returns>
    bool DeleteById(int id);
    /// <summary>
    /// 指定された商品名の存在を確認する
    /// </summary>
    /// <param name="name">商品名</param>
    /// <returns>true:存在する false:存在しない</returns>
    bool Exists(string name);
}