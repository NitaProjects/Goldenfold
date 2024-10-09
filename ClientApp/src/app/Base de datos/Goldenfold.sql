-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         11.4.2-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para db_goldenfold
CREATE DATABASE IF NOT EXISTS `db_goldenfold` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_spanish_ci */;
USE `db_goldenfold`;

-- Volcando estructura para tabla db_goldenfold.asignaciones
CREATE TABLE IF NOT EXISTS `asignaciones` (
  `id_asignacion` int(11) NOT NULL AUTO_INCREMENT,
  `id_paciente` int(11) DEFAULT NULL,
  `id_cama` int(11) DEFAULT NULL,
  `asignado_por` int(11) DEFAULT NULL,
  `fecha_asignacion` datetime DEFAULT current_timestamp(),
  `fecha_liberacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_asignacion`),
  KEY `idx_id_paciente` (`id_paciente`),
  KEY `idx_id_cama` (`id_cama`),
  KEY `idx_asignado_por` (`asignado_por`),
  CONSTRAINT `fk_asignaciones_camas` FOREIGN KEY (`id_cama`) REFERENCES `camas` (`id_cama`),
  CONSTRAINT `fk_asignaciones_pacientes` FOREIGN KEY (`id_paciente`) REFERENCES `pacientes` (`id_paciente`),
  CONSTRAINT `fk_asignaciones_usuarios` FOREIGN KEY (`asignado_por`) REFERENCES `usuarios` (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.asignaciones: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_goldenfold.camas
CREATE TABLE IF NOT EXISTS `camas` (
  `id_cama` int(11) NOT NULL AUTO_INCREMENT,
  `ubicacion` varchar(10) NOT NULL,
  `estado` enum('Disponible','NoDisponible') DEFAULT 'Disponible',
  `tipo` enum('General','UCI','Postoperatorio') DEFAULT 'General',
  `id_habitacion` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_cama`),
  UNIQUE KEY `unique_ubicacion` (`ubicacion`),
  KEY `idx_id_habitacion` (`id_habitacion`),
  CONSTRAINT `fk_camas_habitaciones` FOREIGN KEY (`id_habitacion`) REFERENCES `habitaciones` (`id_habitacion`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.camas: ~40 rows (aproximadamente)
INSERT INTO `camas` (`id_cama`, `ubicacion`, `estado`, `tipo`, `id_habitacion`) VALUES
	(1, 'F1010101', 'Disponible', 'General', 1),
	(2, 'F1010102', 'Disponible', 'General', 1),
	(3, 'F1010201', 'Disponible', 'General', 2),
	(4, 'F1010202', 'Disponible', 'General', 2),
	(5, 'F1010301', 'Disponible', 'General', 3),
	(6, 'F1010302', 'Disponible', 'General', 3),
	(7, 'F1010401', 'Disponible', 'General', 4),
	(8, 'F1010402', 'Disponible', 'General', 4),
	(9, 'F1010501', 'Disponible', 'General', 5),
	(10, 'F1010502', 'Disponible', 'General', 5),
	(11, 'F1010601', 'Disponible', 'General', 6),
	(12, 'F1010602', 'Disponible', 'General', 6),
	(13, 'F1010701', 'Disponible', 'General', 7),
	(14, 'F1010702', 'Disponible', 'General', 7),
	(15, 'F1010801', 'Disponible', 'General', 8),
	(16, 'F1010802', 'Disponible', 'General', 8),
	(17, 'F1010901', 'Disponible', 'General', 9),
	(18, 'F1010902', 'Disponible', 'General', 9),
	(19, 'F1011001', 'Disponible', 'General', 10),
	(20, 'F1011002', 'Disponible', 'General', 10),
	(21, 'F1011101', 'Disponible', 'General', 11),
	(22, 'F1011102', 'Disponible', 'General', 11),
	(23, 'F1011201', 'Disponible', 'General', 12),
	(24, 'F1011202', 'Disponible', 'General', 12),
	(25, 'F1011301', 'Disponible', 'UCI', 13),
	(26, 'F1011302', 'Disponible', 'UCI', 13),
	(27, 'F1011401', 'Disponible', 'UCI', 14),
	(28, 'F1011402', 'Disponible', 'UCI', 14),
	(29, 'F1011501', 'Disponible', 'UCI', 15),
	(30, 'F1011502', 'Disponible', 'UCI', 15),
	(31, 'F1011601', 'Disponible', 'UCI', 16),
	(32, 'F1011602', 'Disponible', 'UCI', 16),
	(33, 'F1011701', 'Disponible', 'Postoperatorio', 17),
	(34, 'F1011702', 'Disponible', 'Postoperatorio', 17),
	(35, 'F1011801', 'Disponible', 'Postoperatorio', 18),
	(36, 'F1011802', 'Disponible', 'Postoperatorio', 18),
	(37, 'F1011901', 'Disponible', 'Postoperatorio', 19),
	(38, 'F1011902', 'Disponible', 'Postoperatorio', 19),
	(39, 'F1012001', 'Disponible', 'Postoperatorio', 20),
	(40, 'F1012002', 'Disponible', 'Postoperatorio', 20);

-- Volcando estructura para tabla db_goldenfold.consultas
CREATE TABLE IF NOT EXISTS `consultas` (
  `id_consulta` int(11) NOT NULL AUTO_INCREMENT,
  `id_paciente` int(11) DEFAULT NULL,
  `id_medico` int(11) DEFAULT NULL,
  `motivo` text DEFAULT NULL,
  `fecha_solicitud` datetime DEFAULT current_timestamp(),
  `fecha_consulta` datetime DEFAULT NULL,
  `estado` enum('pendiente','completada') DEFAULT 'pendiente',
  PRIMARY KEY (`id_consulta`),
  KEY `idx_id_paciente` (`id_paciente`),
  KEY `idx_id_medico` (`id_medico`),
  CONSTRAINT `fk_consultas_medicos` FOREIGN KEY (`id_medico`) REFERENCES `usuarios` (`id_usuario`),
  CONSTRAINT `fk_consultas_pacientes` FOREIGN KEY (`id_paciente`) REFERENCES `pacientes` (`id_paciente`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.consultas: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_goldenfold.habitaciones
CREATE TABLE IF NOT EXISTS `habitaciones` (
  `id_habitacion` int(11) NOT NULL AUTO_INCREMENT,
  `edificio` varchar(2) DEFAULT NULL,
  `planta` varchar(2) DEFAULT NULL,
  `numero_habitacion` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`id_habitacion`),
  KEY `idx_numero_habitacion` (`numero_habitacion`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.habitaciones: ~20 rows (aproximadamente)
INSERT INTO `habitaciones` (`id_habitacion`, `edificio`, `planta`, `numero_habitacion`) VALUES
	(1, 'F1', '01', '01'),
	(2, 'F1', '01', '02'),
	(3, 'F1', '01', '03'),
	(4, 'F1', '01', '04'),
	(5, 'F1', '01', '05'),
	(6, 'F1', '01', '06'),
	(7, 'F1', '01', '07'),
	(8, 'F1', '01', '08'),
	(9, 'F1', '01', '09'),
	(10, 'F1', '01', '10'),
	(11, 'F1', '01', '11'),
	(12, 'F1', '01', '12'),
	(13, 'F1', '01', '13'),
	(14, 'F1', '01', '14'),
	(15, 'F1', '01', '15'),
	(16, 'F1', '01', '16'),
	(17, 'F1', '01', '17'),
	(18, 'F1', '01', '18'),
	(19, 'F1', '01', '19'),
	(20, 'F1', '01', '20');

-- Volcando estructura para tabla db_goldenfold.historialaltas
CREATE TABLE IF NOT EXISTS `historialaltas` (
  `id_historial` int(11) NOT NULL AUTO_INCREMENT,
  `id_paciente` int(11) DEFAULT NULL,
  `id_medico` int(11) DEFAULT NULL,
  `fecha_alta` datetime DEFAULT current_timestamp(),
  `diagnostico` varchar(255) DEFAULT NULL,
  `tratamiento` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id_historial`),
  KEY `idx_id_paciente` (`id_paciente`),
  KEY `idx_id_medico` (`id_medico`),
  CONSTRAINT `fk_historialaltas_medicos` FOREIGN KEY (`id_medico`) REFERENCES `usuarios` (`id_usuario`),
  CONSTRAINT `fk_historialaltas_pacientes` FOREIGN KEY (`id_paciente`) REFERENCES `pacientes` (`id_paciente`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.historialaltas: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_goldenfold.ingresos
CREATE TABLE IF NOT EXISTS `ingresos` (
  `id_ingreso` int(11) NOT NULL AUTO_INCREMENT,
  `id_paciente` int(11) DEFAULT NULL,
  `id_medico` int(11) DEFAULT NULL,
  `motivo` text DEFAULT NULL,
  `fecha_solicitud` datetime DEFAULT current_timestamp(),
  `estado` enum('Pendiente','Ingresado','Rechazado','Alta') DEFAULT 'Pendiente',
  `tipo_cama` enum('General','UCI','Postoperatorio') DEFAULT 'General',
  `fecha_ingreso` datetime DEFAULT NULL,
  `id_asignacion` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_ingreso`),
  KEY `idx_id_paciente` (`id_paciente`),
  KEY `idx_id_medico` (`id_medico`),
  KEY `idx_id_asignacion` (`id_asignacion`),
  CONSTRAINT `fk_ingresos_asignaciones` FOREIGN KEY (`id_asignacion`) REFERENCES `asignaciones` (`id_asignacion`),
  CONSTRAINT `fk_ingresos_medicos` FOREIGN KEY (`id_medico`) REFERENCES `usuarios` (`id_usuario`),
  CONSTRAINT `fk_ingresos_pacientes` FOREIGN KEY (`id_paciente`) REFERENCES `pacientes` (`id_paciente`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.ingresos: ~0 rows (aproximadamente)

-- Volcando estructura para tabla db_goldenfold.pacientes
CREATE TABLE IF NOT EXISTS `pacientes` (
  `id_paciente` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `dni` varchar(9) NOT NULL,
  `fecha_nacimiento` date DEFAULT NULL,
  `seguridad_social` varchar(12) NOT NULL,
  `estado` enum('Registrado','EnConsulta','Ingresado','Alta') DEFAULT 'Registrado',
  `fecha_registro` datetime DEFAULT current_timestamp(),
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `historial_medico` text DEFAULT NULL,
  PRIMARY KEY (`id_paciente`),
  UNIQUE KEY `unique_dni` (`dni`),
  UNIQUE KEY `unique_seguridad_social` (`seguridad_social`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.pacientes: ~20 rows (aproximadamente)
INSERT INTO `pacientes` (`id_paciente`, `nombre`, `dni`, `fecha_nacimiento`, `seguridad_social`, `estado`, `fecha_registro`, `direccion`, `telefono`, `email`, `historial_medico`) VALUES
	(31, 'María Fernández Gomez', '12345678A', '1985-05-12', '123456789012', 'Registrado', '2024-09-16 15:50:04', 'Calle Mayor 12, Madrid', '600123456', 'maria.fernandez@example.com', 'Historial médico de María Fernández.'),
	(32, 'Juan Pérez', '87654321B', '1978-11-23', '987654321098', 'Registrado', '2024-09-16 15:50:04', 'Avenida del Sol 15, Sevilla', '610987654', 'juan.perez@example.com', 'Historial médico de Juan Pérez.'),
	(33, 'Lucía Martínez', '23456789C', '1992-07-30', '234567890123', 'Registrado', '2024-09-16 15:50:04', 'Calle de la Luna 5, Valencia', '620345678', 'lucia.martinez@example.com', 'Historial médico de Lucía Martínez.'),
	(34, 'Pedro Sánchez', '34567890D', '1980-09-18', '345678901234', 'Registrado', '2024-09-16 15:50:04', 'Calle del Río 3, Barcelona', '630987654', 'pedro.sanchez@example.com', 'Historial médico de Pedro Sánchez.'),
	(35, 'Carmen Ramírez', '45678901E', '1965-12-10', '456789012345', 'Registrado', '2024-09-16 15:50:04', 'Avenida de los Pinos 20, Málaga', '640654321', 'carmen.ramirez@example.com', 'Historial médico de Carmen Ramírez.'),
	(36, 'Luis Torres', '56789012F', '1990-03-22', '567890123456', 'Registrado', '2024-09-16 15:50:04', 'Calle del Olmo 7, Zaragoza', '650321987', 'luis.torres@example.com', 'Historial médico de Luis Torres.'),
	(37, 'Ana Gutiérrez Sanchez', '67890123A', '1988-06-05', '678901234567', 'Registrado', '2024-09-16 15:50:04', 'Plaza Mayor 4, Bilbao', '660432567', 'ana.gutierrez@example.com', 'Historial médico de Ana Gutiérrez.'),
	(38, 'Pablo Díaz', '78901234H', '1995-01-13', '789012345678', 'Registrado', '2024-09-16 15:50:04', 'Avenida de la Estrella 11, Valladolid', '670543219', 'pablo.diaz@example.com', 'Historial médico de Pablo Díaz.'),
	(39, 'Laura Moreno', '89012345I', '1977-04-19', '890123456789', 'Registrado', '2024-09-16 15:50:04', 'Calle de los Cipreses 9, Alicante', '680654321', 'laura.moreno@example.com', 'Historial médico de Laura Moreno.'),
	(40, 'Sergio Gómez', '90123456J', '1982-09-01', '901234567890', 'Registrado', '2024-09-16 15:50:04', 'Avenida del Parque 6, Murcia', '690765432', 'sergio.gomez@example.com', 'Historial médico de Sergio Gómez.'),
	(41, 'Isabel Ruiz', '01234567K', '1987-11-07', '012345678901', 'Registrado', '2024-09-16 15:50:04', 'Calle Mayor 33, Salamanca', '600987654', 'isabel.ruiz@example.com', 'Historial médico de Isabel Ruiz.'),
	(42, 'Antonio López', '12345678L', '1990-12-15', '123456789013', 'Registrado', '2024-09-16 15:50:04', 'Calle Gran Vía 50, Bilbao', '611098765', 'antonio.lopez@example.com', 'Historial médico de Antonio López.'),
	(43, 'Marta Sánchez', '23456789M', '1985-03-25', '234567890127', 'Registrado', '2024-09-16 15:50:04', 'Avenida de los Reyes 2, Granada', '622109876', 'marta.sanchez@example.com', 'Historial médico de Marta Sánchez.'),
	(44, 'Rafael Torres', '34567890N', '1993-05-11', '345678901238', 'Registrado', '2024-09-16 15:50:04', 'Calle del Sol 15, Málaga', '633210987', 'rafael.torres@example.com', 'Historial médico de Rafael Torres.'),
	(45, 'Clara Vázquez', '45678901O', '1970-06-14', '456789012355', 'Registrado', '2024-09-16 15:50:04', 'Calle de las Flores 18, León', '644321098', 'clara.vazquez@example.com', 'Historial médico de Clara Vázquez.'),
	(46, 'Daniel', '63980959W', '2024-09-16', '062271692408', 'Registrado', '2024-09-16 20:59:00', 'string', '175249383', 'user@example.com', 'string'),
	(47, 'Daniel Nita', '60078906P', '2024-09-02', '123456789011', 'Registrado', '2024-09-17 09:56:59', 'Calle falsa 123', '123456789', 'daniel.nita@hotmail.com', 'hola'),
	(48, 'maria', '60078906E', '2024-09-09', '123456789885', 'Registrado', '2024-09-18 10:57:20', 'Calle falsa 123', '123456789', 'daniel.nita@hotmail.com', 'asdfs'),
	(50, 'Daniel Nita', '60078906Q', '2024-09-19', '123456789010', 'Registrado', '2024-09-20 09:06:24', 'Calle falsa 123', '123456789', 'daniel.nita@hotmail.com', 'dolor de muelas'),
	(51, 'Alex Gil', '60078908A', '2024-09-02', '123456782343', 'Registrado', '2024-09-20 10:57:35', 'Calle falsa 123', '345678909', 'daniel.nita@hotmail.com', 'historial medico');

-- Volcando estructura para tabla db_goldenfold.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `id_rol` int(11) NOT NULL AUTO_INCREMENT,
  `nombre_rol` varchar(50) NOT NULL,
  PRIMARY KEY (`id_rol`),
  UNIQUE KEY `unique_nombre_rol` (`nombre_rol`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.roles: ~4 rows (aproximadamente)
INSERT INTO `roles` (`id_rol`, `nombre_rol`) VALUES
	(4, 'Administrador de sistema'),
	(1, 'Administrativo'),
	(3, 'Controlador de camas'),
	(2, 'Medico');

-- Volcando estructura para tabla db_goldenfold.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `id_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `nombre_usuario` varchar(50) NOT NULL,
  `contrasenya` varchar(255) NOT NULL,
  `id_rol` int(11) DEFAULT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `unique_nombre_usuario` (`nombre_usuario`),
  KEY `idx_id_rol` (`id_rol`),
  CONSTRAINT `fk_usuarios_roles` FOREIGN KEY (`id_rol`) REFERENCES `roles` (`id_rol`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Volcando datos para la tabla db_goldenfold.usuarios: ~13 rows (aproximadamente)
INSERT INTO `usuarios` (`id_usuario`, `nombre`, `nombre_usuario`, `contrasenya`, `id_rol`) VALUES
	(1, 'Ana López', 'ana.lopez', 'hashed_password_ana', 1),
	(2, 'Carlos Gómez', 'carlos.gomez', 'hashed_password_carlos', 1),
	(3, 'Lucía Pérez', 'lucia.perez', 'hashed_password_lucia', 1),
	(4, 'Dr. Roberto Martínez', 'roberto.martinez', 'hashed_password_roberto', 2),
	(5, 'Dra. Sandra Torres', 'sandra.torres', 'hashed_password_sandra', 2),
	(6, 'Dr. Mario Fernández', 'mario.fernandez', 'hashed_password_mario', 2),
	(7, 'Alberto Ramírez', 'alberto.ramirez', 'hashed_password_alberto', 3),
	(8, 'Marta Sánchez', 'marta.sanchez', 'hashed_password_marta', 3),
	(9, 'Pedro Ortega', 'pedro.ortega', 'hashed_password_pedro', 3),
	(10, 'Laura Gutiérrez', 'laura.gutierrez', 'hashed_password_laura', 4),
	(11, 'Fernando Ruiz', 'fernando.ruiz', 'hashed_password_fernando', 4),
	(12, 'Isabel Vázquez', 'isabel.vazquez', 'hashed_password_isabel', 4),
	(14, 'Daniel nita', 'isabel.vazquezz', 'sdadasdada', 2);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
