using CS_DB_Sample.Infrastructures;

namespace CS_DB_Sample;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== MySQL 接続テスト開始 =====");

        try
        {
            // DbContextを生成
            using var db = new AppDbContext();

            // 実際に MySQL に接続できるか確認する（SELECT 1 相当）
            var canConnect = db.Database.CanConnect();

            if (canConnect)
            {
                Console.WriteLine("✅ MySQL (cs_db_exercise) に接続できました。");
            }
            else
            {
                Console.WriteLine("⚠ MySQL に接続できませんでした。");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ 例外が発生しました。");
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("===== MySQL 接続テスト終了 =====");
    }
}
