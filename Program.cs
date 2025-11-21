using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;
using CS_DB_Sample.Infrastructures.Entities;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        var context = new AppDbContext();
        var salesDetailAccessor = new SalesDetailAccessor(context);
        // 指定された売上Idの売上、商品とそのカテゴリを取得する
        var salesDetails = salesDetailAccessor
            .FindBySalesIdJoinItemAndItemCategory(1);

        foreach(var salesDetail in salesDetails)
        {
            Console.WriteLine(salesDetail);
            Console.WriteLine(salesDetail.Item);
            Console.WriteLine(salesDetail.Item!.Category);

        }
    }
}
