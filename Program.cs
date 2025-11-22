using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;
namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
        using var context = new AppDbContext();
        var accessor = new ItemAccessor(context);

        var average = accessor.GetAveragePriceByCategoryId(1);
        Console.WriteLine($"平均単価: {average}");
        var sum = accessor.GetTotalPriceByCategoryId(1);
        Console.WriteLine($"合計単価: {sum}");
    }
}
