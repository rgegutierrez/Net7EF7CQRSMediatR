﻿// <auto-generated />
using System;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(MyAppDbContext))]
    [Migration("20221220141004_RecetaFabricacion3")]
    partial class RecetaFabricacion3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.MaquinaPapelera", b =>
                {
                    b.Property<int>("MaquinaPapeleraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaquinaPapeleraId"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("FormulaCalculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LineaProduccion")
                        .HasColumnType("int");

                    b.Property<bool>("ModoIngreso")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MaquinaPapeleraId");

                    b.ToTable("MaquinaPapelera", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.MateriaPrima", b =>
                {
                    b.Property<int>("MateriaPrimaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MateriaPrimaId"));

                    b.Property<string>("CodigoSap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MateriaPrimaId");

                    b.ToTable("MateriaPrima", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.PreparacionPasta", b =>
                {
                    b.Property<int>("PreparacionPastaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PreparacionPastaId"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PreparacionPastaId");

                    b.ToTable("PreparacionPasta", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Producto", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.RecetaFabricacion", b =>
                {
                    b.Property<int>("RecetaFabricacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaFabricacionId"));

                    b.Property<string>("AprobacionGerencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AprobacionJefatura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoReceta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("EspecificacionTecnicaId")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InicioVigencia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TerminoVigencia")
                        .HasColumnType("datetime2");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("RecetaFabricacionId");

                    b.ToTable("RecetaFabricacion", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.VariableFormula", b =>
                {
                    b.Property<int>("VariableFormulaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VariableFormulaId"));

                    b.Property<string>("Letra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaquinaPapeleraId")
                        .HasColumnType("int");

                    b.Property<int?>("VariableId")
                        .HasColumnType("int");

                    b.HasKey("VariableFormulaId");

                    b.HasIndex("MaquinaPapeleraId");

                    b.HasIndex("VariableId");

                    b.ToTable("VariableFormula", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.VariableFormula", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.MaquinaPapelera", "MaquinaPapelera")
                        .WithMany("MaquinasPapeleras")
                        .HasForeignKey("MaquinaPapeleraId");

                    b.HasOne("MediatrExample.ApplicationCore.Domain.MaquinaPapelera", "Variable")
                        .WithMany("Variables")
                        .HasForeignKey("VariableId");

                    b.Navigation("MaquinaPapelera");

                    b.Navigation("Variable");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.MaquinaPapelera", b =>
                {
                    b.Navigation("MaquinasPapeleras");

                    b.Navigation("Variables");
                });
#pragma warning restore 612, 618
        }
    }
}
