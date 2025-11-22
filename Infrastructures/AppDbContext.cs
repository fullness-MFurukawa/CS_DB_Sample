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
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class AppDbContext : DbContext
{

    /// <summary>
    /// itemテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<Item> Items { get; set; } = null!;
    /// <summary>
    /// item_categoryテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<ItemCategory> ItemCategories { get; set; } = null!;
    /// <summary>
    /// item_stockテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<ItemStock> ItemStocks { get; set; } = null!;
    /// <summary>
    /// salesテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<Sale> Sales { get; set; } = null!;
    /// <summary>
    /// sales_detailテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<SalesDetail> SalesDetails { get; set; } = null!;
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
    /// マッピング情報が初期化されるタイミングで一度だけ呼び出さる
    /// </summary>
    /// <param name="modelBuilder">
    /// モデルの構成を行うための <see cref="ModelBuilder"/> オブジェクト
    /// 主キー、複合キー、リレーションなどのマッピング情報を定義する
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Itemエンティティのモデル構成を定義    
        modelBuilder.Entity<Item>()
            // itemテーブルにマッピング
            .ToTable("item")       
            // category_id列とプロパティをマッピング
            .Property(i => i.CategoryId)
            .HasColumnName("category_id");
           
        // ItemCategoryエンティティのモデル構成を定義 
        modelBuilder.Entity<ItemCategory>()
            // item_categoryテーブルにマッピング
            .ToTable("item_category");
        // ItemCategory(1)とItem(多)のリレーションを定義
        modelBuilder.Entity<Item>()
            // 1側のプロパティ名
            .HasOne(i => i.Category)
            // 多側のプロパティ名
            .WithMany(c => c.Items)
            // itemテーブルの外部キー
            .HasForeignKey(i => i.CategoryId)
            // 子が存在する限り、親を削除できない
            .OnDelete(DeleteBehavior.Restrict);

        // Saleエンティティのモデル構成を定義
        // ラムダ式の記述例
        modelBuilder.Entity<Sale>(entity =>
        {
            // sales テーブルにマッピング
            entity.ToTable("sales");
            // 主キー
            entity.HasKey(s => s.Id);
            // 列マッピング
            entity.Property(s => s.Id)
                .HasColumnName("id");
            entity.Property(s => s.SalesDate)
                .HasColumnName("sales_date");
            entity.Property(s => s.Total)
                .HasColumnName("total");
            entity.Property(s => s.AccountId)
                .HasColumnName("account_id");
        });

        // SalesDetailエンティティのモデル構成を定義
        // ラムダ式の記述例
        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.ToTable("sales_detail");
            // 主キーは id のみ（AUTO_INCREMENT）
            entity.HasKey(sd => sd.Id);
            entity.Property(sd => sd.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd(); // 明示的に自動採番を設定

            entity.Property(sd => sd.SalesId)
                .HasColumnName("sales_id");
            entity.Property(sd => sd.ItemId)
                .HasColumnName("item_id");
            entity.Property(sd => sd.Quantity)
                .HasColumnName("quantity");
            entity.Property(sd => sd.Subtotal)
                .HasColumnName("subtotal");
            // リレーション設定
            entity.HasOne(sd => sd.Item)
                .WithMany()
                .HasForeignKey(sd => sd.ItemId);
        });
    }
}
