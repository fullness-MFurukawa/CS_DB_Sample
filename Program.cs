using CS_DB_Sample.Application;
using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();
        var registerSales = new RegiterSales(context);
        // 登録する売上データ
        var sale = new SalesEntity
        { SalesDate = DateTime.UtcNow, Total = 240, AccountId = 1 };
        // 登録する売上明細データ
        var salesDetails = new List<SalesDetailEntity>
        {
            new SalesDetailEntity
                { Quantity = 1, Subtotal = 120, ItemId = 1 },
            new SalesDetailEntity
                { Quantity = 1, Subtotal = 120, ItemId = 2 }
        };
        // 売上と売上明細を登録する
        registerSales.Register(sale, salesDetails);
    }
}
