using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;

namespace CS_DB_Sample.Infrastructures.Queries;
/// <summary>
/// itemテーブルにアクセスするクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-16</date>
/// <version>1.0.0</version>
public class ItemAccessor
{
    //  アプリケーション用DbContext
    private readonly AppDbContext _context;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public ItemAccessor(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 指定された単価のすべての商品を取得する
    /// </summary>
    /// <param name="price">単価</param>
    /// <returns></returns>
    public List<Item> FindByPrice(int price)
    {
        var items = _context.Items
        // 引数priceと同じ価格のすべて商品を取得する
        .Where(i => i.Price == price)
        .ToList();
        return items;
    }


    /// <summary>
    /// 指定された商品名の商品名と単価を取得する
    /// </summary>
    /// <param name="name">商品名</param>
    /// <returns></returns>
    public Item FindByNameTypeA(string name)
    {
        // 引数nameと同じ名前の商品を取得する
        var item = _context.Items
            .Where(i => i.Name == name)
            .Select(i => new Item
            {
                // 商品名と単価を取得する
                Name = i.Name,
                Price = i.Price
            })
            // Where()メソッドで条件指定して場合引数は不要
            .Single();
        return item;
    }

    /// <summary>
    /// 指定された商品名の商品名と単価を取得する
    /// </summary>
    /// <param name="name">商品名</param>
    /// <returns></returns>
    public Item FindByNameTypeB(string name)
    {
        // 引数nameと同じ名前の商品を取得する
        var item = _context.Items
            .Select(i => new Item
            {
                // 商品名と単価を取得する
                Name = i.Name,
                Price = i.Price
            })
            // Where()メソッドを利用していないので、条件を指定
            .Single(i => i.Name == name);
        return item;
    }

    /// <summary>
    /// 商品名に指定語句を「含む」商品を検索（中間一致）
    /// </summary>
    public List<Item> FindByNameContains(string keyword)
    {
        return _context.Items
            .Where(i => i.Name!.Contains(keyword))
            .ToList();
    }

    /// <summary>
    /// 商品名が指定語句で「始まる」商品を検索（前方一致）
    /// </summary>
    public List<Item> FindByNameStartsWith(string prefix)
    {
        return _context.Items
            .Where(i => i.Name!.StartsWith(prefix))
            .ToList();
    }

    /// <summary>
    /// 商品名が指定語句で「終わる」商品を検索（後方一致）
    /// </summary>
    public List<Item> FindByNameEndsWith(string suffix)
    {
        return _context.Items
            .Where(i => i.Name!.EndsWith(suffix))
            .ToList();
    }

    /// <summary>
    /// 指定された単価のすべての商品を取得する
    /// ノントラッキングモード
    /// </summary>
    /// <param name="price">単価</param>
    /// <returns></returns>
    public List<Item> FindByPriceNoTracking(int price)
    {
        var items = _context.Items
            .Where(i => i.Price == price)
            .AsNoTracking()
            .ToList();
        return items;
    }

    /// <summary>
    /// 商品を登録する
    /// </summary>
    /// <param name="item">登録データを保持するEntity</param>
    /// <returns></returns>
    public Item Create(Item item)
    {
        // 新規商品を追加する
        var result = _context.Items.Add(item);
        // 変更を永続化する
        _context.SaveChanges();
        return result.Entity;
    }

    /// <summary>
    /// 複数の商品を登録する
    /// </summary>
    /// <param name="items">登録データを保持するEntityのリスト</param>
    public void CreateRange(List<Item> items)
    {
        // 新規商品を追加する
        _context.Items.AddRange(items);
        // 変更を永続化する
        _context.SaveChanges();
    }   

    /// <summary>
    /// 商品を変更する
    /// </summary>
    /// <param name="id">変更対象の商品の主キー値</param>
    /// <returns></returns>
    public Item? UpdateById(Item item)
    {
        // 商品Idを指定して商品を取得する
        var result = _context.Items.Find(item.Id);
        if (item == null)
        {
            return null; // 商品が見つからない場合はnullを返す
        }
        // 商品名と単価を変更する
        result!.Name = item.Name;
        result.Price = item.Price;
        // 変更を永続化する
        _context.SaveChanges();
        return item;
    }

    /// <summary>
    /// 複数の商品を更新する
    /// </summary>
    /// <param name="items">変更対象の商品</param>
    public void UpdateRange(List<Item> items)
    {
        // 商品を更新する
        _context.Items.UpdateRange(items);
        // 変更を永続化する
        _context.SaveChanges();
    }

    /// <summary>
    /// 商品を削除する
    /// </summary>
    /// <param name="item">削除対象の商品</param>
    /// <returns>削除したエンティティ</returns>
    public Item? DeleteById(Item item)
    {
        var result = _context.Items.Find(item.Id);
        if (result == null)
        {
            return null;// 商品が見つからない場合はnullを返す
        }
        // 商品を削除する
        var delResult = _context.Items.Remove(result);
        // 削除を永続化する
        _context.SaveChanges();
        return delResult.Entity;
    }

    /// <summary>
    /// 複数の商品を削除する
    /// </summary>
    /// <param name="items">削除対象の商品</param>
    public void DeleteRange(List<Item> items)
    {
        // 商品を削除する
        _context.Items.RemoveRange(items);
        // 削除を永続化する
        _context.SaveChanges();
    }
}