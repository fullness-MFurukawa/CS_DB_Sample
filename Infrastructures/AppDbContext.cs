using CS_DB_Sample.Infrastructures.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace CS_DB_Sample.Infrastructures;
/// <summary>
/// DbContext継承クラス
/// WSL(Ubutu24.0.4)
/// PostgreSQL17
/// </summary>
/// <author>Fullness,Inc.</author>
/// <date>2025-11-21</date>
/// <version>1.0.0</version>
public class AppDbContext : DbContext
{
    /// <summary>
    /// itemテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<ItemEntity> Items { get; set; } = null!;
    /// <summary>
    /// item_categoryテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<ItemCategoryEntity> ItemCategories { get; set; } = null!;
    /// <summary>
    /// item_stockテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<ItemStockEntity> ItemStocks { get; set; } = null!;
    /// <summary>
    /// salesテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<SalesEntity> Sales { get; set; } = null!;
    /// <summary>
    /// sales_detailテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<SalesDetailEntity> SalesDetails { get; set; } = null!;
    /// <summary>
    /// departmentテーブルにマッピングされるDbSetプロパティ 
    /// </summary>
    public DbSet<DepartmentEntity> Departments{ get; set; } = null!;


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
            "Host=localhost;Database=cs_db_exercise;Username=postgres;Password=training;";

        optionsBuilder
        // PostgreSQLデータベースに接続する設定
        // - connectionString：接続文字列（サーバー名、DB名、ユーザー名、パスワード）
        .UseNpgsql(connectionString)
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
        // ItemEntityのモデル構成を定義    
        modelBuilder.Entity<ItemEntity>()
            .ToTable("item")    // itemテーブルにマッピング    
            .HasKey(i => i.Id); // 主キー項目の定義
        // テーブルの列とプロパティのマッピング
        modelBuilder.Entity<ItemEntity>()
            .Property(i => i.Id).HasColumnName("id");
        modelBuilder.Entity<ItemEntity>()
            .Property(i => i.Name).HasColumnName("name");
        modelBuilder.Entity<ItemEntity>()
            .Property(i => i.Price).HasColumnName("price");
        modelBuilder.Entity<ItemEntity>()
            .Property(i => i.CategoryId).HasColumnName("category_id");
           
        // ItemCategoryEntityのモデル構成を定義 
        modelBuilder.Entity<ItemCategoryEntity>()
            // item_categoryテーブルにマッピング
            .ToTable("item_category")
            .HasKey(i => i.Id); // 主キー項目の定義
        // テーブルの列とプロパティのマッピング
        modelBuilder.Entity<ItemCategoryEntity>()
            .Property(c => c.Id).HasColumnName("id");
        modelBuilder.Entity<ItemCategoryEntity>()
            .Property(c => c.Name).HasColumnName("name");
        // ItemCategory(1)とItem(多)のリレーションを定義
        modelBuilder.Entity<ItemEntity>()
            // 1側のプロパティ名
            .HasOne(i => i.Category)
            // 多側のプロパティ名
            .WithMany(c => c.Items)
            // itemテーブルの外部キー
            .HasForeignKey(i => i.CategoryId)
            // 子が存在する限り、親を削除できない
            .OnDelete(DeleteBehavior.Restrict);

        // SaleEntityのモデル構成を定義
        // ラムダ式の記述例
        modelBuilder.Entity<SalesEntity>(entity =>
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

        // SalesDetailEntityのモデル構成を定義
        // ラムダ式の記述例
        modelBuilder.Entity<SalesDetailEntity>(entity =>
        {
            entity.ToTable("sales_detail");
            entity.HasKey(sd => sd.Id);
            entity.Property(sd => sd.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
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
