﻿// <auto-generated />
using System;
using GestorHorarioG6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestorHorarioG6.Migrations
{
    [DbContext(typeof(GestorHorarioG6Context))]
    partial class GestorHorarioG6ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestorHorarioG6.Models.Bloco", b =>
                {
                    b.Property<int>("BlocoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("BlocoId");

                    b.ToTable("Bloco");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Cargo", b =>
                {
                    b.Property<int>("CargoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("CargoId");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("DepartamentoId");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Equipamento", b =>
                {
                    b.Property<int>("EquipamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlocoId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("EquipamentoId");

                    b.HasIndex("BlocoId");

                    b.ToTable("Equipamento");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CargoId");

                    b.Property<string>("Email");

                    b.Property<string>("NIF")
                        .IsRequired();

                    b.Property<DateTime>("Nascimento");

                    b.Property<DateTime>("NascimentoFilho");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Notas");

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.HasKey("FuncionarioId");

                    b.HasIndex("CargoId");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Requisicao", b =>
                {
                    b.Property<int>("RequisicaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartamentoId");

                    b.Property<DateTime>("Dia");

                    b.HasKey("RequisicaoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Requisicao");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.RequisicaoDetalhe", b =>
                {
                    b.Property<int>("RequisicaoDetalheId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprovado");

                    b.Property<DateTime>("DuraçãoEstimada");

                    b.Property<DateTime>("HoraConcluido");

                    b.Property<DateTime>("HoraCriticaAte");

                    b.Property<DateTime>("HoraCriticaDe");

                    b.Property<DateTime>("HoraDeFim");

                    b.Property<DateTime>("HoraDeInicio");

                    b.Property<string>("Notas");

                    b.Property<int>("RequisicaoId");

                    b.Property<int>("ServicoId");

                    b.HasKey("RequisicaoDetalheId");

                    b.HasIndex("RequisicaoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("RequisicaoDetalhe");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.RequisicaoEquipamento", b =>
                {
                    b.Property<int>("RequisicaoEquipamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlocoId");

                    b.Property<int>("EquipamentoId");

                    b.Property<DateTime>("HoraDeFim");

                    b.Property<DateTime>("HoraDeInicio");

                    b.Property<int>("On Delete No Action");

                    b.HasKey("RequisicaoEquipamentoId");

                    b.HasIndex("BlocoId");

                    b.HasIndex("On Delete No Action");

                    b.ToTable("RequisicaoEquipamento");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Servico", b =>
                {
                    b.Property<int>("ServicoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descrição")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("ServicoId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Equipamento", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Bloco", "Bloco")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Funcionario", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Requisicao", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.RequisicaoDetalhe", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Requisicao", "Requisicao")
                        .WithMany("Detalhes")
                        .HasForeignKey("RequisicaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestorHorarioG6.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.RequisicaoEquipamento", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Bloco", "Bloco")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GestorHorarioG6.Models.Equipamento", "Equipamento")
                        .WithMany()
                        .HasForeignKey("On Delete No Action")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
