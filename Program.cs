using CS_DB_Sample.Infrastructures;
using CS_DB_Sample.Infrastructures.Queries;

namespace CS_DB_Sample;
class Program
{
    static void Main(string[] args)
    {
         var accessor = new ItemAccessor(new AppDbContext());
        var items = accessor.FindByNameContains("ペン");
        Console.WriteLine("商品名に指定語句を「含む」商品を検索（中間一致）");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        items = accessor.FindByNameStartsWith("水性");
        Console.WriteLine("商品名が指定語句で「始まる」商品を検索（前方一致）");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
        items = accessor.FindByNameEndsWith("キーボード");
        Console.WriteLine("商品名が指定語句で「終わる」商品を検索（後方一致）");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
