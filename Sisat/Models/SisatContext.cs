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
                optionsBuilder.UseSqlServer("data Source=CPADSI39\\sqlexpress;Initial Catalog=SistemaAtualizacoes02; Integrated Security=True;");
            }
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conveniados>(entity =>
            {
                entity.HasKey(e => e.IdConveniado)
                    .HasName("PK__Convenia__24F606D12ADBD314");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Conveniados)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Conveniad__ID_Us__45F365D3");
            });

            modelBuilder.Entity<ConvenioProjeto>(entity =>
            {
                entity.HasOne(d => d.IdConNavigation)
                    .WithMany(p => p.ConvenioProjeto)
                    .HasForeignKey(d => d.IdCon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Convenio_Projeto_Conveniados");

                entity.HasOne(d => d.IdProjNavigation)
                    .WithMany(p => p.ConvenioProjeto)
                    .HasForeignKey(d => d.IdProj)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Convenio_Projeto_Projetos");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Forum)
                    .HasForeignKey(d => d.IdAutor)
                    .HasConstraintName("FK_Forum_Usuario");
            });

            modelBuilder.Entity<NivelDeAcesso>(entity =>
            {
                entity.HasKey(e => e.IdNivelAcesso)
                    .HasName("PK__NivelDeA__6CF66357A4B8F4E1");

                entity.Property(e => e.IdNivelAcesso).ValueGeneratedNever();
            });

            modelBuilder.Entity<PacotesAtualizacoes>(entity =>
            {
                entity.HasKey(e => e.IdPacote)
                    .HasName("PK__Pacotes___B2C7136CD2625C65");

                entity.Property(e => e.Ativo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdProjNavigation)
                    .WithMany(p => p.PacotesAtualizacoes)
                    .HasForeignKey(d => d.IdProj)
                    .HasConstraintName("FK_ID_PROJETO");
            });

            modelBuilder.Entity<Projetos>(entity =>
            {
                entity.HasKey(e => e.IdProjeto)
                    .HasName("PK__Projetos__6701DEA94750DD65");

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
                    .HasConstraintName("FK__Usuario__Id_NivA__4AB81AF0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
