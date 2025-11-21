using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;
namespace CS_DB_Sample.Infrastructures.Queries;
/// <summary>
/// sales_detailテーブルにアクセスするクラス    
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class SalesDetailAccessor
{
    //  アプリケーション用DbContext
    private readonly AppDbContext _context;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public SalesDetailAccessor(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 指定された売上Idの売上、商品とそのカテゴリを取得する
    /// sales_detail->item->item_category
    /// </summary>
    /// <param name="salesId">売上Id(主キー)</param>
    /// <returns></returns>
    public List<SalesDetail> FindBySalesIdJoinItemAndItemCategory(int salesId)
    {
        var salesDetails = _context.SalesDetails
            .Where(s => s.SalesId == salesId)
            // 商品を結合で取得する
            .Include(s => s.Item)
            // 結合で取得した商品のカテゴリを取得する
            .ThenInclude(item => item!.Category) 
            .ToList();
        return salesDetails;
    }
}