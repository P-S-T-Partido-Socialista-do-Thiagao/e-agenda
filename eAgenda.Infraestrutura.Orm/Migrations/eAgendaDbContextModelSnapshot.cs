﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAgenda.Infraestrutura.Orm.Compartilhado;

#nullable disable

namespace eAgenda.Infraestrutura.Orm.Migrations
{
    [DbContext(typeof(eAgendaDbContext))]
    partial class eAgendaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CategoriaDespesa", b =>
                {
                    b.Property<Guid>("CategoriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DespesasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriasId", "DespesasId");

                    b.HasIndex("DespesasId");

                    b.ToTable("CategoriaDespesa");
                });

            modelBuilder.Entity("EAgenda.Dominio.ModuloCompromisso.Compromisso", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataDeOcorrencia")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("HoraDeInicio")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraDeTermino")
                        .HasColumnType("time");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCompromisso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.ToTable("Compromissos");
                });

            modelBuilder.Entity("EAgenda.Dominio.ModuloContato.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cargo")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Empresa")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloCategoria.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloDespesa.Despesa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataOcorrencia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Despesas");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.ItemTarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Concluido")
                        .HasColumnType("bit");

                    b.Property<Guid>("TarefaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("ItensTarefa");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Concluida")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataConclusao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("CategoriaDespesa", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloCategoria.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAgenda.Dominio.ModuloDespesa.Despesa", null)
                        .WithMany()
                        .HasForeignKey("DespesasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EAgenda.Dominio.ModuloCompromisso.Compromisso", b =>
                {
                    b.HasOne("EAgenda.Dominio.ModuloContato.Contato", "Contato")
                        .WithMany("Compromissos")
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contato");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.ItemTarefa", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloTarefa.Tarefa", "Tarefa")
                        .WithMany("Itens")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("EAgenda.Dominio.ModuloContato.Contato", b =>
                {
                    b.Navigation("Compromissos");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.Tarefa", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
