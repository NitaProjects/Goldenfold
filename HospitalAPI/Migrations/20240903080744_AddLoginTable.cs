using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Camas_Ubicacion",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Pacientes_IdPaciente",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Usuarios_AsignadoPor",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_HistorialesAltas_Pacientes_IdPaciente",
                table: "HistorialesAltas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habitaciones",
                table: "Habitaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Camas",
                table: "Camas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignaciones",
                table: "Asignaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialesAltas",
                table: "HistorialesAltas");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "Pacientes",
                newName: "pacientes");

            migrationBuilder.RenameTable(
                name: "Habitaciones",
                newName: "habitaciones");

            migrationBuilder.RenameTable(
                name: "Camas",
                newName: "camas");

            migrationBuilder.RenameTable(
                name: "Asignaciones",
                newName: "asignaciones");

            migrationBuilder.RenameTable(
                name: "HistorialesAltas",
                newName: "historialaltas");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "usuarios",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Contrasenya",
                table: "usuarios",
                newName: "contrasenya");

            migrationBuilder.RenameColumn(
                name: "NombreUsuario",
                table: "usuarios",
                newName: "nombre_usuario");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "usuarios",
                newName: "id_rol");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "usuarios",
                newName: "id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "usuarios",
                newName: "IX_usuarios_nombre_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_IdRol",
                table: "usuarios",
                newName: "IX_usuarios_id_rol");

            migrationBuilder.RenameColumn(
                name: "NombreRol",
                table: "roles",
                newName: "nombre_rol");

            migrationBuilder.RenameColumn(
                name: "IdRol",
                table: "roles",
                newName: "id_rol");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "pacientes",
                newName: "telefono");

            migrationBuilder.RenameColumn(
                name: "Sintomas",
                table: "pacientes",
                newName: "sintomas");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "pacientes",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "pacientes",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "pacientes",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Edad",
                table: "pacientes",
                newName: "edad");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "pacientes",
                newName: "direccion");

            migrationBuilder.RenameColumn(
                name: "SeguridadSocial",
                table: "pacientes",
                newName: "seguridad_social");

            migrationBuilder.RenameColumn(
                name: "HistorialMedico",
                table: "pacientes",
                newName: "historial_medico");

            migrationBuilder.RenameColumn(
                name: "FechaRegistro",
                table: "pacientes",
                newName: "fecha_registro");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "pacientes",
                newName: "fecha_nacimiento");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "pacientes",
                newName: "id_paciente");

            migrationBuilder.RenameIndex(
                name: "IX_Pacientes_SeguridadSocial",
                table: "pacientes",
                newName: "IX_pacientes_seguridad_social");

            migrationBuilder.RenameColumn(
                name: "Planta",
                table: "habitaciones",
                newName: "planta");

            migrationBuilder.RenameColumn(
                name: "Edificio",
                table: "habitaciones",
                newName: "edificio");

            migrationBuilder.RenameColumn(
                name: "NumeroHabitacion",
                table: "habitaciones",
                newName: "numero_habitacion");

            migrationBuilder.RenameColumn(
                name: "IdHabitacion",
                table: "habitaciones",
                newName: "id_habitacion");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "camas",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "camas",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "asignaciones",
                newName: "ubicacion");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "asignaciones",
                newName: "id_paciente");

            migrationBuilder.RenameColumn(
                name: "FechaLiberacion",
                table: "asignaciones",
                newName: "fecha_liberacion");

            migrationBuilder.RenameColumn(
                name: "FechaAsignacion",
                table: "asignaciones",
                newName: "fecha_asignacion");

            migrationBuilder.RenameColumn(
                name: "AsignadoPor",
                table: "asignaciones",
                newName: "asignado_por");

            migrationBuilder.RenameColumn(
                name: "IdAsignacion",
                table: "asignaciones",
                newName: "id_asignacion");

            migrationBuilder.RenameIndex(
                name: "IX_Asignaciones_Ubicacion",
                table: "asignaciones",
                newName: "IX_asignaciones_ubicacion");

            migrationBuilder.RenameIndex(
                name: "IX_Asignaciones_IdPaciente",
                table: "asignaciones",
                newName: "IX_asignaciones_id_paciente");

            migrationBuilder.RenameIndex(
                name: "IX_Asignaciones_AsignadoPor",
                table: "asignaciones",
                newName: "IX_asignaciones_asignado_por");

            migrationBuilder.RenameColumn(
                name: "Tratamiento",
                table: "historialaltas",
                newName: "tratamiento");

            migrationBuilder.RenameColumn(
                name: "Diagnostico",
                table: "historialaltas",
                newName: "diagnostico");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "historialaltas",
                newName: "id_paciente");

            migrationBuilder.RenameColumn(
                name: "FechaAlta",
                table: "historialaltas",
                newName: "fecha_alta");

            migrationBuilder.RenameColumn(
                name: "IdHistorial",
                table: "historialaltas",
                newName: "id_historial");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialesAltas_IdPaciente",
                table: "historialaltas",
                newName: "IX_historialaltas_id_paciente");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_rol",
                table: "roles",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "telefono",
                table: "pacientes",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "pacientes",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha_nacimiento",
                table: "pacientes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "numero_habitacion",
                table: "habitaciones",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 2)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "tipo",
                table: "camas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "camas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "id_usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id_rol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pacientes",
                table: "pacientes",
                column: "id_paciente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_habitaciones",
                table: "habitaciones",
                column: "id_habitacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_camas",
                table: "camas",
                column: "Ubicacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asignaciones",
                table: "asignaciones",
                column: "id_asignacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_historialaltas",
                table: "historialaltas",
                column: "id_historial");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id_rol",
                keyValue: 1,
                column: "nombre_rol",
                value: "Administrativo");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id_rol",
                keyValue: 2,
                column: "nombre_rol",
                value: "Medico");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id_rol",
                keyValue: 3,
                column: "nombre_rol",
                value: "ControladorCamas");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id_rol",
                keyValue: 4,
                column: "nombre_rol",
                value: "AdministradorSistemas");

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_camas_ubicacion",
                table: "asignaciones",
                column: "ubicacion",
                principalTable: "camas",
                principalColumn: "Ubicacion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_pacientes_id_paciente",
                table: "asignaciones",
                column: "id_paciente",
                principalTable: "pacientes",
                principalColumn: "id_paciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asignaciones_usuarios_asignado_por",
                table: "asignaciones",
                column: "asignado_por",
                principalTable: "usuarios",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historialaltas_pacientes_id_paciente",
                table: "historialaltas",
                column: "id_paciente",
                principalTable: "pacientes",
                principalColumn: "id_paciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usuarios_roles_id_rol",
                table: "usuarios",
                column: "id_rol",
                principalTable: "roles",
                principalColumn: "id_rol",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_camas_ubicacion",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_pacientes_id_paciente",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_asignaciones_usuarios_asignado_por",
                table: "asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_historialaltas_pacientes_id_paciente",
                table: "historialaltas");

            migrationBuilder.DropForeignKey(
                name: "FK_usuarios_roles_id_rol",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pacientes",
                table: "pacientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_habitaciones",
                table: "habitaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_camas",
                table: "camas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asignaciones",
                table: "asignaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_historialaltas",
                table: "historialaltas");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "pacientes",
                newName: "Pacientes");

            migrationBuilder.RenameTable(
                name: "habitaciones",
                newName: "Habitaciones");

            migrationBuilder.RenameTable(
                name: "camas",
                newName: "Camas");

            migrationBuilder.RenameTable(
                name: "asignaciones",
                newName: "Asignaciones");

            migrationBuilder.RenameTable(
                name: "historialaltas",
                newName: "HistorialesAltas");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Usuarios",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "contrasenya",
                table: "Usuarios",
                newName: "Contrasenya");

            migrationBuilder.RenameColumn(
                name: "nombre_usuario",
                table: "Usuarios",
                newName: "NombreUsuario");

            migrationBuilder.RenameColumn(
                name: "id_rol",
                table: "Usuarios",
                newName: "IdRol");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "Usuarios",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_nombre_usuario",
                table: "Usuarios",
                newName: "IX_Usuarios_NombreUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_id_rol",
                table: "Usuarios",
                newName: "IX_Usuarios_IdRol");

            migrationBuilder.RenameColumn(
                name: "nombre_rol",
                table: "Roles",
                newName: "NombreRol");

            migrationBuilder.RenameColumn(
                name: "id_rol",
                table: "Roles",
                newName: "IdRol");

            migrationBuilder.RenameColumn(
                name: "telefono",
                table: "Pacientes",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "sintomas",
                table: "Pacientes",
                newName: "Sintomas");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Pacientes",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Pacientes",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Pacientes",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "edad",
                table: "Pacientes",
                newName: "Edad");

            migrationBuilder.RenameColumn(
                name: "direccion",
                table: "Pacientes",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "seguridad_social",
                table: "Pacientes",
                newName: "SeguridadSocial");

            migrationBuilder.RenameColumn(
                name: "historial_medico",
                table: "Pacientes",
                newName: "HistorialMedico");

            migrationBuilder.RenameColumn(
                name: "fecha_registro",
                table: "Pacientes",
                newName: "FechaRegistro");

            migrationBuilder.RenameColumn(
                name: "fecha_nacimiento",
                table: "Pacientes",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "id_paciente",
                table: "Pacientes",
                newName: "IdPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_pacientes_seguridad_social",
                table: "Pacientes",
                newName: "IX_Pacientes_SeguridadSocial");

            migrationBuilder.RenameColumn(
                name: "planta",
                table: "Habitaciones",
                newName: "Planta");

            migrationBuilder.RenameColumn(
                name: "edificio",
                table: "Habitaciones",
                newName: "Edificio");

            migrationBuilder.RenameColumn(
                name: "numero_habitacion",
                table: "Habitaciones",
                newName: "NumeroHabitacion");

            migrationBuilder.RenameColumn(
                name: "id_habitacion",
                table: "Habitaciones",
                newName: "IdHabitacion");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Camas",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Camas",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "ubicacion",
                table: "Asignaciones",
                newName: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "id_paciente",
                table: "Asignaciones",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "fecha_liberacion",
                table: "Asignaciones",
                newName: "FechaLiberacion");

            migrationBuilder.RenameColumn(
                name: "fecha_asignacion",
                table: "Asignaciones",
                newName: "FechaAsignacion");

            migrationBuilder.RenameColumn(
                name: "asignado_por",
                table: "Asignaciones",
                newName: "AsignadoPor");

            migrationBuilder.RenameColumn(
                name: "id_asignacion",
                table: "Asignaciones",
                newName: "IdAsignacion");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_ubicacion",
                table: "Asignaciones",
                newName: "IX_Asignaciones_Ubicacion");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_id_paciente",
                table: "Asignaciones",
                newName: "IX_Asignaciones_IdPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_asignaciones_asignado_por",
                table: "Asignaciones",
                newName: "IX_Asignaciones_AsignadoPor");

            migrationBuilder.RenameColumn(
                name: "tratamiento",
                table: "HistorialesAltas",
                newName: "Tratamiento");

            migrationBuilder.RenameColumn(
                name: "diagnostico",
                table: "HistorialesAltas",
                newName: "Diagnostico");

            migrationBuilder.RenameColumn(
                name: "id_paciente",
                table: "HistorialesAltas",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "fecha_alta",
                table: "HistorialesAltas",
                newName: "FechaAlta");

            migrationBuilder.RenameColumn(
                name: "id_historial",
                table: "HistorialesAltas",
                newName: "IdHistorial");

            migrationBuilder.RenameIndex(
                name: "IX_historialaltas_id_paciente",
                table: "HistorialesAltas",
                newName: "IX_HistorialesAltas_IdPaciente");

            migrationBuilder.AlterColumn<int>(
                name: "NombreRol",
                table: "Roles",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Pacientes",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 9)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Pacientes",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Pacientes",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroHabitacion",
                table: "Habitaciones",
                type: "int",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Tipo",
                table: "Camas",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Camas",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pacientes",
                table: "Pacientes",
                column: "IdPaciente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habitaciones",
                table: "Habitaciones",
                column: "IdHabitacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Camas",
                table: "Camas",
                column: "Ubicacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignaciones",
                table: "Asignaciones",
                column: "IdAsignacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialesAltas",
                table: "HistorialesAltas",
                column: "IdHistorial");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 1,
                column: "NombreRol",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 2,
                column: "NombreRol",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 3,
                column: "NombreRol",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 4,
                column: "NombreRol",
                value: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Camas_Ubicacion",
                table: "Asignaciones",
                column: "Ubicacion",
                principalTable: "Camas",
                principalColumn: "Ubicacion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Pacientes_IdPaciente",
                table: "Asignaciones",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Usuarios_AsignadoPor",
                table: "Asignaciones",
                column: "AsignadoPor",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialesAltas_Pacientes_IdPaciente",
                table: "HistorialesAltas",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_IdRol",
                table: "Usuarios",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
