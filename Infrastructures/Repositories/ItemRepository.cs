using Microsoft.EntityFrameworkCore;
using CS_DB_Sample.Domains.Repositories;
using CS_DB_Sample.Domains.Adapters;
using CS_DB_Sample.Domains.Models;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample.Infrastructures.Repositories;
/// <summary>
/// 商品をCRUD操作するリポジトリインターフェイスの実装
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-23</date>
/// <version>1.0.0</version>
public class ItemRepository : IItemRepository
{
    // ItemEntityとItemの変換を行うアダプター
    private readonly IItemAdapter<ItemEntity> _adapter;
    // DbContext継承クラス
    private readonly AppDbContext _appDbContext;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="adapter">ItemEntityとItemの変換を行うアダプター</param>
    /// <param name="appDbContext">アプリケーション用DbContext</param>
    public ItemRepository(
        IItemAdapter<ItemEntity> adapter, AppDbContext appDbContext)
    {
        _adapter = adapter;
        _appDbContext = appDbContext;
    }

    /// <summary>
    /// すべての商品を取得する
    /// </summary>
    /// <returns>商品のリスト</returns>
    public List<Item> FindAll()
    {
        var itemEntities = _appDbContext.Items
            .AsNoTracking()
            .ToList();
        return _adapter.ToDomainList(itemEntities);
    }

    /// <summary>
    /// 指定された商品Idの商品を取得する
    /// </summary>
    /// <param name="id">商品Id</param>
    /// <returns>存在しない場合nullを返す</returns>
    public Item? FindById(int id)
    {
        var entity = _appDbContext.Items
            .Include(i => i.Category)
            .AsNoTracking()
            .SingleOrDefault(i => i.Id == id);
        return _adapter.ToDomain(entity!);
    }

    /// <summary>
    /// 指定された商品名の存在を確認する
    /// </summary>
    /// <param name="name">商品名</param>
    /// <returns>true:存在する false:存在しない</returns>
    public bool Exists(string name)
    {
        var result = _appDbContext.Items
            .Any(i => i.Name == name);
        return result;
    }

    /// <summary>
    /// 商品を永続化する
    /// </summary>
    /// <param name="item">永続化する商品</param>
    public void Create(Item item)
    {
        var entity = _adapter.FromDomain(item);
        _appDbContext.Items.Add(entity);
        _appDbContext.SaveChanges();
    }

    /// <summary>
    /// 指定された商品Idの商品を変更する
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool UpdateById(Item item)
    {
        var entity = _appDbContext.Items
            .SingleOrDefault(i => i.Id == item.Id);
        if (item == null)
        {
            return false;
        }
        entity!.Name = item.Name!;
        entity!.Price = item.Price;
        _appDbContext.Items.Update(entity);
        _appDbContext.SaveChanges();
        return true;
    }

    /// <summary>
    /// 指定された商品Idの商品を削除する
    /// </summary>
    /// <param name="id">商品名</param>
    /// <returns></returns>
    public bool DeleteById(int id)
    {
        var entity = _appDbContext.Items
            .SingleOrDefault(i => i.Id == id);
        if (entity == null)
        {
            return false;
        }
        _appDbContext.Items.Remove(entity);
        _appDbContext.SaveChanges();
        return true;
    }
}