using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;
namespace CS_DB_Sample.Infrastructures.Queries;
/// <summary>
/// item_categoryテーブルにアクセスするクラス    
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class ItemCategoryAccessor
{
    //  アプリケーション用DbContext
    private readonly AppDbContext _context;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public ItemCategoryAccessor(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 指定されたカテゴリIdのカテゴリと商品を取得する
    /// </summary>
    /// <param name="id">カテゴリId(主キー)</param>
    /// <returns></returns>
    public ItemCategory FindByIdJoinItems(int id)
    {
        var itemCategory = _context.ItemCategories
            .Where(i => i.Id == id)
            .Include(i => i.Items) // 商品を結合して取得する
            .Single();
        return itemCategory;
    }
}