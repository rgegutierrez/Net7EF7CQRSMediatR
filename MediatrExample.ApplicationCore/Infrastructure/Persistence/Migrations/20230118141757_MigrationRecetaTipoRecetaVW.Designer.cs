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
    [Migration("20230118141757_MigrationRecetaTipoRecetaVW")]
    partial class MigrationRecetaTipoRecetaVW
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Formacion", b =>
                {
                    b.Property<int>("FormacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormacionId"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<decimal>("RangoAlturaMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RangoAlturaMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RangoAnguloMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RangoAnguloMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UnidadMedidaAltura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnidadMedidaAngulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FormacionId");

                    b.ToTable("Formacion", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.LineaProduccion", b =>
                {
                    b.Property<int>("LineaProduccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaProduccionId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LineaProduccionId");

                    b.ToTable("LineaProduccion", "trzreceta");
                });

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

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.ProductoQuimico", b =>
                {
                    b.Property<int>("ProductoQuimicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoQuimicoId"));

                    b.Property<bool>("Certificacion")
                        .HasColumnType("bit");

                    b.Property<string>("CodigoSap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Funcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasKey("ProductoQuimicoId");

                    b.ToTable("ProductoQuimico", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaFabricacion", b =>
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

                    b.Property<int?>("TipoRecetaId")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("RecetaFabricacionId");

                    b.HasIndex("TipoRecetaId");

                    b.ToTable("RecetaFabricacion", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaMaquina", b =>
                {
                    b.Property<int>("RecetaLineaMaquinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaLineaMaquinaId"));

                    b.Property<int>("LineaProduccionId")
                        .HasColumnType("int");

                    b.Property<string>("LineaProduccionNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecetaFabricacionId")
                        .HasColumnType("int");

                    b.HasKey("RecetaLineaMaquinaId");

                    b.HasIndex("LineaProduccionId");

                    b.HasIndex("RecetaFabricacionId");

                    b.ToTable("RecetaLineaMaquina", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaPreparacion", b =>
                {
                    b.Property<int>("RecetaLineaPreparacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaLineaPreparacionId"));

                    b.Property<int>("LineaProduccionId")
                        .HasColumnType("int");

                    b.Property<string>("LineaProduccionNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecetaFabricacionId")
                        .HasColumnType("int");

                    b.HasKey("RecetaLineaPreparacionId");

                    b.HasIndex("LineaProduccionId");

                    b.HasIndex("RecetaFabricacionId");

                    b.ToTable("RecetaLineaPreparacion", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaProduccion", b =>
                {
                    b.Property<int>("RecetaLineaProduccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaLineaProduccionId"));

                    b.Property<int>("LineaProduccionId")
                        .HasColumnType("int");

                    b.Property<string>("LineaProduccionNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecetaFabricacionId")
                        .HasColumnType("int");

                    b.HasKey("RecetaLineaProduccionId");

                    b.HasIndex("LineaProduccionId");

                    b.HasIndex("RecetaFabricacionId");

                    b.ToTable("RecetaLineaProduccion", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaMaquinaPapelera", b =>
                {
                    b.Property<int>("RecetaMaquinaPapeleraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaMaquinaPapeleraId"));

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormulaCalculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaquinaPapeleraId")
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

                    b.Property<int>("RecetaLineaMaquinaId")
                        .HasColumnType("int");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RecetaMaquinaPapeleraId");

                    b.HasIndex("MaquinaPapeleraId");

                    b.HasIndex("RecetaLineaMaquinaId");

                    b.ToTable("RecetaMaquinaPapelera", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaMateriaPrima", b =>
                {
                    b.Property<int>("RecetaMateriaPrimaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaMateriaPrimaId"));

                    b.Property<string>("CodigoSap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MateriaPrimaId")
                        .HasColumnType("int");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<int>("RecetaLineaProduccionId")
                        .HasColumnType("int");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RecetaMateriaPrimaId");

                    b.HasIndex("MateriaPrimaId");

                    b.HasIndex("RecetaLineaProduccionId");

                    b.ToTable("RecetaMateriaPrima", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaPreparacionPasta", b =>
                {
                    b.Property<int>("RecetaPreparacionPastaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaPreparacionPastaId"));

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<int>("PreparacionPastaId")
                        .HasColumnType("int");

                    b.Property<int>("RecetaLineaPreparacionId")
                        .HasColumnType("int");

                    b.Property<string>("UnidadMedida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMaximo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorMinimo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RecetaPreparacionPastaId");

                    b.HasIndex("PreparacionPastaId");

                    b.HasIndex("RecetaLineaPreparacionId");

                    b.ToTable("RecetaPreparacionPasta", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaVariableFormula", b =>
                {
                    b.Property<int>("RecetaVariableFormulaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecetaVariableFormulaId"));

                    b.Property<string>("Letra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecetaMaquinaPapeleraId")
                        .HasColumnType("int");

                    b.HasKey("RecetaVariableFormulaId");

                    b.HasIndex("RecetaMaquinaPapeleraId");

                    b.ToTable("RecetaVariableFormula", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.TipoReceta", b =>
                {
                    b.Property<int>("TipoRecetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoRecetaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreVariable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoRecetaId");

                    b.ToTable("TipoReceta", "trzreceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.TiroMaquina", b =>
                {
                    b.Property<int>("TiroMaquinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TiroMaquinaId"));

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

                    b.HasKey("TiroMaquinaId");

                    b.ToTable("TiroMaquina", "trzreceta");
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

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.View.RecetaFabricacionVW", b =>
                {
                    b.Property<int>("RecetaFabricacionId")
                        .HasColumnType("int");

                    b.Property<string>("AprobacionGerencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AprobacionJefatura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteCodigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClienteNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoReceta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diametro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EspecificacionTecnicaId")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("Gramaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InicioVigencia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TerminoVigencia")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoEspecificacionDsc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoEspecificacionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoEspecificacionNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPapelCodigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPapelId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPapelNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TipoRecetaId")
                        .HasColumnType("int");

                    b.Property<string>("TipoRecetaNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tolerancia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tubete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("RecetaFabricacionId");

                    b.ToTable((string)null);

                    b.ToView("RecetaFabricacionVW", (string)null);
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaFabricacion", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.TipoReceta", "TipoReceta")
                        .WithMany()
                        .HasForeignKey("TipoRecetaId");

                    b.Navigation("TipoReceta");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaMaquina", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.LineaProduccion", "LineaProduccion")
                        .WithMany()
                        .HasForeignKey("LineaProduccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaFabricacion", "RecetaFabricacion")
                        .WithMany()
                        .HasForeignKey("RecetaFabricacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LineaProduccion");

                    b.Navigation("RecetaFabricacion");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaPreparacion", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.LineaProduccion", "LineaProduccion")
                        .WithMany()
                        .HasForeignKey("LineaProduccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaFabricacion", "RecetaFabricacion")
                        .WithMany()
                        .HasForeignKey("RecetaFabricacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LineaProduccion");

                    b.Navigation("RecetaFabricacion");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaProduccion", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.LineaProduccion", "LineaProduccion")
                        .WithMany()
                        .HasForeignKey("LineaProduccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaFabricacion", "RecetaFabricacion")
                        .WithMany()
                        .HasForeignKey("RecetaFabricacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LineaProduccion");

                    b.Navigation("RecetaFabricacion");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaMaquinaPapelera", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.MaquinaPapelera", "MaquinaPapelera")
                        .WithMany()
                        .HasForeignKey("MaquinaPapeleraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaMaquina", "RecetaLineaMaquina")
                        .WithMany()
                        .HasForeignKey("RecetaLineaMaquinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaquinaPapelera");

                    b.Navigation("RecetaLineaMaquina");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaMateriaPrima", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.MateriaPrima", "MateriaPrima")
                        .WithMany()
                        .HasForeignKey("MateriaPrimaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaProduccion", "RecetaLineaProduccion")
                        .WithMany()
                        .HasForeignKey("RecetaLineaProduccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MateriaPrima");

                    b.Navigation("RecetaLineaProduccion");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaPreparacionPasta", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.PreparacionPasta", "PreparacionPasta")
                        .WithMany()
                        .HasForeignKey("PreparacionPastaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaLineaPreparacion", "RecetaLineaPreparacion")
                        .WithMany()
                        .HasForeignKey("RecetaLineaPreparacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PreparacionPasta");

                    b.Navigation("RecetaLineaPreparacion");
                });

            modelBuilder.Entity("MediatrExample.ApplicationCore.Domain.Receta.RecetaVariableFormula", b =>
                {
                    b.HasOne("MediatrExample.ApplicationCore.Domain.Receta.RecetaMaquinaPapelera", "RecetaMaquinaPapelera")
                        .WithMany()
                        .HasForeignKey("RecetaMaquinaPapeleraId");

                    b.Navigation("RecetaMaquinaPapelera");
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
