using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sisat.Models
{
    public partial class SisatContext : DbContext
    {
        public SisatContext()
        {
        }

        public SisatContext(DbContextOptions<SisatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conveniados> Conveniados { get; set; } = null!;
        public virtual DbSet<ConvenioProjeto> ConvenioProjeto { get; set; } = null!;
        public virtual DbSet<Forum> Forum { get; set; } = null!;
        public virtual DbSet<NivelDeAcesso> NivelDeAcesso { get; set; } = null!;
        public virtual DbSet<PacotesAtualizacoes> PacotesAtualizacoes { get; set; } = null!;
        public virtual DbSet<Projetos> Projetos { get; set; } = null!;
        public virtual DbSet<RespostasForum> RespostasForum { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data Source=HOME_PC\\SQLEXPRESS;Initial Catalog=Teste002; Integrated Security=True;");
                //optionsBuilder.UseSqlServer("data Source=CPADSI39\\sqlexpress;Initial Catalog=Teste115; Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conveniados>(entity =>
            {
                entity.HasKey(e => e.IdConveniado)
                    .HasName("PK__Convenia__24F606D1AD79651A");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Conveniados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Conveniad__ID_Us__45F365D3");
            });

            modelBuilder.Entity<ConvenioProjeto>(entity =>
            {
                entity.HasKey(e => new { e.IdCon, e.IdProj })
                    .HasName("PK__Convenio__1D45BDBD778EE64B");

                entity.Property(e => e.IdConProj).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdConNavigation)
                    .WithMany(p => p.ConvenioProjeto)
                    .HasForeignKey(d => d.IdCon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_CONVENIADO");
            });

            modelBuilder.Entity<NivelDeAcesso>(entity =>
            {
                entity.HasKey(e => e.IdNivelAcesso)
                    .HasName("PK__NivelDeA__6CF663574C169F98");

                entity.Property(e => e.IdNivelAcesso).ValueGeneratedNever();
            });

            modelBuilder.Entity<PacotesAtualizacoes>(entity =>
            {
                entity.HasKey(e => e.IdPacote)
                    .HasName("PK__Pacotes___B2C7136C44F19D13");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProjNavigation)
                    .WithMany(p => p.PacotesAtualizacoes)
                    .HasForeignKey(d => d.IdProj)
                    .HasConstraintName("FK_ID_PROJETO");
            });

            modelBuilder.Entity<Projetos>(entity =>
            {
                entity.HasKey(e => e.IdProjeto)
                    .HasName("PK__Projetos__6701DEA982A41A27");
            });

            modelBuilder.Entity<RespostasForum>(entity =>
            {
                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.RespostasForum)
                    .HasForeignKey(d => d.IdAutorResposta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Respostas_Forum_Autor");

                entity.HasOne(d => d.IdTopicoNavigation)
                    .WithMany(p => p.RespostasForum)
                    .HasForeignKey(d => d.IdTopico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Respostas_Forum_Topico");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne(d => d.IdNivAcessoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdNivAcesso)
                    .HasConstraintName("FK__Usuario__Id_NivA__48CFD27E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
