using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JBook.Web.Data;

public partial class JBookDbContext : DbContext
{
    public JBookDbContext()
    {
    }

    public JBookDbContext(DbContextOptions<JBookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookData> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=JBook_db;Username=postgres;Password=6859");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookData>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Book");

            entity.Property(e => e.Data).HasColumnType("jsonb");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
