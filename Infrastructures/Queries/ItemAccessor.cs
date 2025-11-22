using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;
namespace CS_DB_Sample.Infrastructures.Queries;
/// <summary>
/// itemテーブルにアクセスするクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
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

    /// <summary>
    /// 指定された商品Idの商品とカテゴリを取得する
    /// </summary>
    /// <param name="id">商品Id(主キー)</param>
    /// <returns></returns>
    public Item FindByIdJoinItemCategory(int id)
    {
        var item = _context.Items
            .Where(i => i.Id == id)
            .Include(i => i.Category) // カテゴリを結合して取得する
            .Single();
        return item;
    }

    /// <summary>
    /// すべての商品を商品名で昇順に並び替えて取得する
    /// </summary>
    /// <returns></returns>
    public List<Item> FindAllOrderByName()
    {
        var items = _context.Items
            .OrderBy(i => i.Name)
            .ToList();
        return items;
    }

    /// <summary>
    /// すべての商品を単価で降順に並び替ええて取得する
    /// </summary>
    /// <returns></returns>
    public List<Item> FindAllOrderByDecPrice()
    {
        var items = _context.Items
            .OrderByDescending(i => i.Price)
            .ToList();
        return items;
    }

    /// <summary>
    /// 指定された在庫数を持つ商品が存在するかを判定する
    /// </summary>
    /// <param name="stockThreshold">判定対象とする在庫数の下限値</param>
    /// <returns>在庫数が条件以上の商品の存在有無(true/false)</returns>
    public bool ExistsItemWithStock(int stock)
    {
        return _context.Items
            .Any(i => _context.ItemStocks
                .Any(s => s.ItemId == i.Id && s.Stock >= stock));
    }

    /// <summary>
    /// 商品名に引数keywordが含まれ、指定された在庫数を持つ商品を取得する
    /// </summary>
    /// <param name="keyword">商品検索キーワード</param>
    /// <param name="stock">一致させたい在庫数</param>
    /// <returns>条件に一致する商品の一覧</returns>
    public List<Item> FindItemsByNameAndStockContains(string keyword,int stock)
    {
        var items = _context.Items
            .Where(i =>
                // 商品名がkeywordが含まれ、かつ指定在庫数の商品Idに一致する
                i.Name.Contains(keyword) &&
                _context.ItemStocks
                    .Where(s => s.Stock == stock) // 条件に合う在庫
                    .Select(s => s.ItemId)        // 対象商品のIdを抽出
                    .Contains(i.Id)               // 商品Idが一致するか
            )
            .ToList();  // クエリ実行（SQLに変換される）

        return items;
    }

    /// <summary>
    /// 指定されたカテゴリIdの商品の平均単価を取得する(小数部切り捨て)
    /// </summary>
    /// <param name="categoryId">カテゴリId</param>
    /// <returns>平均単価</returns>
    public int GetAveragePriceByCategoryId(int categoryId)
    {
        var average = _context.Items
            .Where(i => i.CategoryId == categoryId)
            .Select(i => i.Price) // 商品の単価を取得する
            .Average(); // 単価の平均を取得する
        return (int)Math.Floor(average);
    }

    /// <summary>
    /// 指定されたカテゴリIdの商品の合計単価を取得する
    /// </summary>
    /// <param name="categoryId">カテゴリId</param>
    /// <returns>合計金額</returns>
    public int GetTotalPriceByCategoryId(int categoryId)
    {
        var sum = _context.Items
            .Where(i => i.CategoryId == categoryId)
            .Select(i => i.Price) // 商品の単価を取得する
            .Sum(); // 単価の合計を取得する
        return sum;
    }
}