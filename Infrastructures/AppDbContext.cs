using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace CS_DB_Sample.Infrastructures;
/// <summary>
/// DbContext継承クラス
/// WSL(Ubutu24.0.4)
/// MYSQL8.0.43
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-15</date>
/// <version>1.0.0</version>
public class AppDbContext : DbContext
{

    /// <summary>
    /// itemテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<Item> Items { get; set; } = null!;
    /// <summary>
    /// departmentテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<Department> Departments{ get; set; } = null!;


    /// <summary>
    /// DbContextの構成処理をする
    /// データベース接続情報の設定や、SQLログ出力の有効化する
    /// </summary>
    /// <param name="optionsBuilder">
    /// DbContextの動作設定を構築するためのオプションビルダーオブジェクト
    /// 接続先データベースやログ設定などを定義
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 接続文字列（サーバー名、DB名、ユーザー名、パスワード）
        string connectionString =
        "Server=localhost;Database=cs_db_exercise;User=root;Password=root;";

        optionsBuilder
        // MySQLデータベースに接続する設定
        // - connectionString：接続文字列（サーバー名、DB名、ユーザー名、パスワード）
        // - MySqlServerVersion：利用するMySQLのバージョンを指定（8.0.43）
        .UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 43)))
        // 実行されたSQLをコンソールに表示する
        // - SQL文が目に見えるので「どんなSQLが発行されているか」が分かる
        // - デバッグや学習に非常に便利
        .LogTo(Console.WriteLine, LogLevel.Information)
        // パラメーターの値もログに表示する
        // - SQLに渡された値（例: "商品名 = '鉛筆'"）も確認できる
        // - デバッグ時は便利だが、個人情報などを扱う本番環境では使わない
        .EnableSensitiveDataLogging();
    }

    /// <summary>
    /// モデルの構成の定義
    /// このメソッドは、Entity Framework Core によってエンティティとテーブルの
    ///  マッピング情報）が初期化されるタイミングで一度だけ呼び出さる
    /// </summary>
    /// <param name="modelBuilder">
    /// モデルの構成を行うための <see cref="ModelBuilder"/> オブジェクト
    /// 主キー、複合キー、リレーションなどのマッピング情報を定義する
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // sales_detail エンティティのモデル構成を定義
        // 複合主キー（id, sales_id）を指定する
        modelBuilder.Entity<SalesDetailEntity>()
            //new { ... } の構文で、2つのプロパティをまとめて1つのキーとして指定
            .HasKey(sd => new { sd.Id, sd.SalesId });
    }
}
