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
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
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
                        .IsRequired();

                    b.HasKey("EquipamentoId");

                    b.HasIndex("BlocoId");

                    b.ToTable("Equipamento");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cargo");

                    b.Property<string>("Email");

                    b.Property<int>("NIF");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Notas");

                    b.Property<int>("Telefone");

                    b.HasKey("FuncionarioId");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Requisicao", b =>
                {
                    b.Property<int>("RequisicaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprovado");

                    b.Property<int>("DepartamentoId");

                    b.Property<DateTime>("HoraDeFim");

                    b.Property<DateTime>("HoraDeInicio");

                    b.Property<string>("RequisicoesAdicionais");

                    b.HasKey("RequisicaoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Requisicao");
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

                    b.HasKey("RequisicaoEquipamentoId");

                    b.HasIndex("BlocoId");

                    b.HasIndex("EquipamentoId");

                    b.ToTable("RequisicaoEquipamento");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Equipamento", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Bloco", "Bloco")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Requisicao", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.RequisicaoEquipamento", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Bloco", "Bloco")
                        .WithMany()
                        .HasForeignKey("BlocoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorHorarioG6.Models.Equipamento", "Equipamento")
                        .WithMany()
                        .HasForeignKey("EquipamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
