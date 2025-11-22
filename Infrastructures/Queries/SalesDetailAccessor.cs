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

    /// <summary>
    /// 売上明細を登録する
    /// </summary>
    /// <param name="salesDetail">登録する売上明細</param>
    /// <returns>登録結果</returns>
    public SalesDetail Create(SalesDetail salesDetail)
    {
        var result = _context.SalesDetails.Add(salesDetail);
        // 変更を保存する
        _context.SaveChanges();
        return result.Entity;
    }

    /// <summary>
    /// 商品Idでグループ化して、売上数量と小計金額を集計する
    /// </summary>
    /// <returns></returns>
    public List<ItemSalesSummary> FindAllGroupByItemId()
    {
        var groupedDetails = _context.SalesDetails
            .GroupBy(sd => sd.ItemId)
            .Select(g => new ItemSalesSummary
            {
                ItemId = g.Key,
                TotalQuantity = g.Sum(sd => sd.Quantity),
                TotalSubtotal = g.Sum(sd => sd.Subtotal)
            })
            .ToList();
        return groupedDetails;
    }
}