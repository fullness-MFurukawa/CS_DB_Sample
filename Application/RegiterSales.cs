using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample.Application;
/// <summary>
/// 売上を登録サービスクラス
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class RegiterSales
{
    //  アプリケーション用DbContext
    private readonly AppDbContext _context;
    //  売上テーブルにアクセスするクラス
    private readonly SalesAccessor _salesAccessor;
    //  売上明細テーブルにアクセスするクラス
    private readonly SalesDetailAccessor _salesDetailAccessor;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    public RegiterSales(AppDbContext context)
    {
        _context = context;
        _salesAccessor = new SalesAccessor(_context);
        _salesDetailAccessor = new SalesDetailAccessor(_context);
    }

    /// <summary>
    /// 売上と売上明細を登録する
    /// </summary>
    /// <param name="sale">売上</param>
    /// <param name="salesDetails">売上明細</param>
    public void Register(SalesEntity sales, List<SalesDetailEntity> salesDetails)
    {
        // トランザクションを開始する
        // usingステートメントを使うことで、トランザクションが自動的に破棄される
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            // 売上を登録する
            var salesResult = _salesAccessor.Create(sales);
            Console.WriteLine("売上の登録に成功しました。");
            Console.WriteLine(salesResult);
            // 売上明細を登録する
            foreach (var salesDetail in salesDetails)
            {
                salesDetail.SalesId = salesResult.Id;
                var salesDetailResult = _salesDetailAccessor.Create(salesDetail);
                Console.WriteLine("売上明細の登録に成功しました。");
                Console.WriteLine(salesDetailResult);
            }
            // トランザクションをコミットする
            transaction.Commit();
            Console.WriteLine("コミット:売上、売上明細の登録に成功しました。");
        }
        catch (Exception ex)
        {
            // 例外が発生した場合はロールバックする
            transaction.Rollback();
            Console.WriteLine("ロールバック:売上、売上明細の登録に失敗しました。");
            Console.WriteLine(ex.Message);
        }
    }
}