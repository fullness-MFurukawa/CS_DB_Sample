using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample.Infrastructures.Queries;
/// <summary>
/// salesテーブルにアクセスするクラス    
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class SalesAccessor
{
    //  アプリケーション用DbContext
    private readonly AppDbContext _context;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public SalesAccessor(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// 売上を登録する
    /// </summary>
    /// <param name="sales">登録する売上</param>
    /// <returns>登録結果</returns>
    public Sale Create(Sale sale)
    {
        var result = _context.Sales.Add(sale);
        // 変更を保存する
        _context.SaveChanges();
        return result.Entity;
    }
}