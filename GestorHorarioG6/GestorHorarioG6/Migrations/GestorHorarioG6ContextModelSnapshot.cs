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

            modelBuilder.Entity("GestorHorarioG6.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cargo")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int>("NIF");

                    b.Property<DateTime>("Nascimento");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

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

                    b.Property<DateTime>("HoraDeFim");

                    b.Property<DateTime>("HoraDeInicio");

                    b.Property<string>("RequisicoesAdicionais");

                    b.Property<int>("ServicoId");

                    b.HasKey("RequisicaoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("Requisicao");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Servico", b =>
                {
                    b.Property<int>("ServicoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("ServicoId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Trocas", b =>
                {
                    b.Property<int>("TrocasID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprovado");

                    b.Property<bool>("Conhecimento");

                    b.Property<int>("IDFuncionario1FuncionarioId");

                    b.Property<int?>("IDFuncionario2FuncionarioId");

                    b.Property<int>("Turno1");

                    b.Property<int>("Turno2");

                    b.HasKey("TrocasID");

                    b.HasIndex("IDFuncionario1FuncionarioId");

                    b.HasIndex("IDFuncionario2FuncionarioId");

                    b.ToTable("Trocas");
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Requisicao", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorHorarioG6.Models.Trocas", b =>
                {
                    b.HasOne("GestorHorarioG6.Models.Funcionario", "IDFuncionario1")
                        .WithMany()
                        .HasForeignKey("IDFuncionario1FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorHorarioG6.Models.Funcionario", "IDFuncionario2")
                        .WithMany()
                        .HasForeignKey("IDFuncionario2FuncionarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
