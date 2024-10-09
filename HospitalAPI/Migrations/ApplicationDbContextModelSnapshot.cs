﻿// <auto-generated />
using System;
using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("HospitalApi.Asignacion", b =>
                {
                    b.Property<int>("IdAsignacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_asignacion");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdAsignacion"));

                    b.Property<int>("AsignadoPor")
                        .HasColumnType("int")
                        .HasColumnName("asignado_por");

                    b.Property<DateTime>("FechaAsignacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_asignacion");

                    b.Property<DateTime?>("FechaLiberacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_liberacion");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int")
                        .HasColumnName("id_paciente");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("ubicacion");

                    b.HasKey("IdAsignacion");

                    b.HasIndex("AsignadoPor");

                    b.HasIndex("IdPaciente");

                    b.HasIndex("Ubicacion");

                    b.ToTable("asignaciones", (string)null);
                });

            modelBuilder.Entity("HospitalApi.Cama", b =>
                {
                    b.Property<string>("Ubicacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Ubicacion");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("estado");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tipo");

                    b.HasKey("Ubicacion");

                    b.ToTable("camas", (string)null);
                });

            modelBuilder.Entity("HospitalApi.Habitacion", b =>
                {
                    b.Property<int>("IdHabitacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_habitacion");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdHabitacion"));

                    b.Property<string>("Edificio")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("edificio");

                    b.Property<string>("NumeroHabitacion")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("numero_habitacion");

                    b.Property<string>("Planta")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("planta");

                    b.HasKey("IdHabitacion");

                    b.ToTable("habitaciones", (string)null);
                });

            modelBuilder.Entity("HospitalApi.HistorialAlta", b =>
                {
                    b.Property<int>("IdHistorial")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_historial");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdHistorial"));

                    b.Property<string>("Diagnostico")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("diagnostico");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_alta");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int")
                        .HasColumnName("id_paciente");

                    b.Property<string>("Tratamiento")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("tratamiento");

                    b.HasKey("IdHistorial");

                    b.HasIndex("IdPaciente");

                    b.ToTable("historialaltas", (string)null);
                });

            modelBuilder.Entity("HospitalApi.Paciente", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_paciente");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPaciente"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("direccion");

                    b.Property<int>("Edad")
                        .HasColumnType("int")
                        .HasColumnName("edad");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("estado");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_nacimiento");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_registro");

                    b.Property<string>("HistorialMedico")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("historial_medico");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<string>("SeguridadSocial")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("seguridad_social");

                    b.Property<string>("Sintomas")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("sintomas");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)")
                        .HasColumnName("telefono");

                    b.HasKey("IdPaciente");

                    b.HasIndex("SeguridadSocial")
                        .IsUnique();

                    b.ToTable("pacientes", (string)null);
                });

            modelBuilder.Entity("HospitalApi.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre_rol");

                    b.HasKey("IdRol");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            IdRol = 1,
                            NombreRol = "Administrativo"
                        },
                        new
                        {
                            IdRol = 2,
                            NombreRol = "Medico"
                        },
                        new
                        {
                            IdRol = 3,
                            NombreRol = "ControladorCamas"
                        },
                        new
                        {
                            IdRol = 4,
                            NombreRol = "AdministradorSistemas"
                        });
                });

            modelBuilder.Entity("HospitalApi.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Contrasenya")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contrasenya");

                    b.Property<int>("IdRol")
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nombre_usuario");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdRol");

                    b.HasIndex("NombreUsuario")
                        .IsUnique();

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("HospitalApi.Asignacion", b =>
                {
                    b.HasOne("HospitalApi.Usuario", "Usuario")
                        .WithMany("Asignaciones")
                        .HasForeignKey("AsignadoPor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalApi.Paciente", "Paciente")
                        .WithMany("Asignaciones")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalApi.Cama", "Cama")
                        .WithMany("Asignaciones")
                        .HasForeignKey("Ubicacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cama");

                    b.Navigation("Paciente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HospitalApi.HistorialAlta", b =>
                {
                    b.HasOne("HospitalApi.Paciente", "Paciente")
                        .WithMany("HistorialAltas")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("HospitalApi.Usuario", b =>
                {
                    b.HasOne("HospitalApi.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("HospitalApi.Cama", b =>
                {
                    b.Navigation("Asignaciones");
                });

            modelBuilder.Entity("HospitalApi.Paciente", b =>
                {
                    b.Navigation("Asignaciones");

                    b.Navigation("HistorialAltas");
                });

            modelBuilder.Entity("HospitalApi.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("HospitalApi.Usuario", b =>
                {
                    b.Navigation("Asignaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
