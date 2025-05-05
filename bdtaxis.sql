-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Servidor: localhost:3306
-- Tiempo de generación: 25-04-2025 a las 01:48:03
-- Versión del servidor: 5.7.24
-- Versión de PHP: 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `bdtaxis`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `table_clientes`
--

CREATE TABLE `table_clientes` (
  `idSitio` int(11) NOT NULL,
  `idCliente` int(11) NOT NULL,
  `NombreCompleto` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `table_clientes`
--

INSERT INTO `table_clientes` (`idSitio`, `idCliente`, `NombreCompleto`) VALUES
(0, 1, 'Hernan Cancino Robles'),
(0, 2, 'Charly Aquino Vazquez');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `table_operadores`
--

CREATE TABLE `table_operadores` (
  `idSitio` int(11) NOT NULL,
  `idOperador` int(11) NOT NULL,
  `UsuarioOpe` varchar(30) NOT NULL,
  `passOpe` varchar(30) NOT NULL,
  `idTipoOpe` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `table_operadores`
--

INSERT INTO `table_operadores` (`idSitio`, `idOperador`, `UsuarioOpe`, `passOpe`, `idTipoOpe`) VALUES
(0, 1, 'Cazadora1', 'Caz1234', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `table_servicios`
--

CREATE TABLE `table_servicios` (
  `idSitio` int(11) NOT NULL,
  `idServicio` int(11) NOT NULL,
  `partida` varchar(50) NOT NULL,
  `numero_cliente` bigint(10) NOT NULL,
  `numero_taxista` int(4) NOT NULL,
  `llegada` varchar(50) NOT NULL,
  `fecha` date NOT NULL,
  `hora` time NOT NULL,
  `costo` int(10) NOT NULL,
  `referencias` varchar(100) NOT NULL,
  `status` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `table_servicios`
--

INSERT INTO `table_servicios` (`idSitio`, `idServicio`, `partida`, `numero_cliente`, `numero_taxista`, `llegada`, `fecha`, `hora`, `costo`, `referencias`, `status`) VALUES
(0, 1, 'villa de san marcos', 9612384068, 2345, 'parque de la marimba', '2024-11-26', '22:52:00', 60, 'jhvjhv', 2),
(0, 2, 'villa de san marcos', 9611429700, 4818, 'parque central', '2024-11-26', '23:22:00', 50, 'casa verde', 3),
(0, 3, 'parque central', 9615802227, 2345, 'parque 5 de mayo', '2024-11-26', '23:24:00', 40, 'enfrente de la catedral', 2),
(0, 4, 'ggggggg', 9612384068, 4818, 'trtt', '2025-01-23', '18:14:00', 30, 'erererre', 1),
(0, 5, 'villa de san marcos', 9611429700, 2345, 'parque de la marimba', '2025-01-28', '15:31:00', 70, 'kasjbxkaj', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `table_sitios`
--

CREATE TABLE `table_sitios` (
  `idSitio` int(11) NOT NULL,
  `nombreSitio` varchar(30) NOT NULL,
  `Status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `table_sitios`
--

INSERT INTO `table_sitios` (`idSitio`, `nombreSitio`, `Status`) VALUES
(0, 'Jaguar', 1),
(1, 'Diana Cazadora', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `taxistas`
--

CREATE TABLE `taxistas` (
  `idSitio` int(11) NOT NULL,
  `Telefono` bigint(10) NOT NULL,
  `Nombre_taxista` varchar(30) NOT NULL,
  `apellidoP_taxista` varchar(20) NOT NULL,
  `apellidoM_taxista` varchar(20) NOT NULL,
  `num_eco` int(4) NOT NULL,
  `placas` varchar(10) NOT NULL,
  `year_auto` int(4) NOT NULL,
  `modelo` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `taxistas`
--

INSERT INTO `taxistas` (`idSitio`, `Telefono`, `Nombre_taxista`, `apellidoP_taxista`, `apellidoM_taxista`, `num_eco`, `placas`, `year_auto`, `modelo`) VALUES
(0, 2143123434, 'reyme', 'cano', 'roblero', 4818, '31-HBD-1', 2017, 'Tsuru'),
(0, 9612384068, 'Hernan', 'Cancino', 'Robles', 2345, 'hc123l', 2012, 'Aveo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_operador`
--

CREATE TABLE `tipo_operador` (
  `idTipoOpe` int(5) NOT NULL,
  `descrOpe` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `tipo_operador`
--

INSERT INTO `tipo_operador` (`idTipoOpe`, `descrOpe`) VALUES
(1, 'ADMON'),
(2, 'OPERADOR');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_status`
--

CREATE TABLE `tipo_status` (
  `id` int(1) NOT NULL,
  `status` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `tipo_status`
--

INSERT INTO `tipo_status` (`id`, `status`) VALUES
(1, 'EMISION'),
(2, 'FINALIZADO'),
(3, 'CANCELADO');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `table_clientes`
--
ALTER TABLE `table_clientes`
  ADD PRIMARY KEY (`idCliente`),
  ADD KEY `idSitio` (`idSitio`);

--
-- Indices de la tabla `table_operadores`
--
ALTER TABLE `table_operadores`
  ADD PRIMARY KEY (`idOperador`),
  ADD KEY `idSitio` (`idSitio`),
  ADD KEY `idTipoOpe` (`idTipoOpe`);

--
-- Indices de la tabla `table_servicios`
--
ALTER TABLE `table_servicios`
  ADD PRIMARY KEY (`idServicio`),
  ADD KEY `relacion_status` (`status`),
  ADD KEY `relacion_cliente` (`numero_cliente`),
  ADD KEY `numero_taxista` (`numero_taxista`),
  ADD KEY `idSitio` (`idSitio`);

--
-- Indices de la tabla `table_sitios`
--
ALTER TABLE `table_sitios`
  ADD PRIMARY KEY (`idSitio`);

--
-- Indices de la tabla `taxistas`
--
ALTER TABLE `taxistas`
  ADD UNIQUE KEY `Telefono` (`Telefono`),
  ADD KEY `num_eco` (`num_eco`),
  ADD KEY `idSitio` (`idSitio`);

--
-- Indices de la tabla `tipo_operador`
--
ALTER TABLE `tipo_operador`
  ADD UNIQUE KEY `idTipoOpe` (`idTipoOpe`);

--
-- Indices de la tabla `tipo_status`
--
ALTER TABLE `tipo_status`
  ADD UNIQUE KEY `id` (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `table_clientes`
--
ALTER TABLE `table_clientes`
  MODIFY `idCliente` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `table_operadores`
--
ALTER TABLE `table_operadores`
  MODIFY `idOperador` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `table_servicios`
--
ALTER TABLE `table_servicios`
  MODIFY `idServicio` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `table_sitios`
--
ALTER TABLE `table_sitios`
  MODIFY `idSitio` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `table_clientes`
--
ALTER TABLE `table_clientes`
  ADD CONSTRAINT `table_clientes_ibfk_1` FOREIGN KEY (`idSitio`) REFERENCES `table_sitios` (`idSitio`);

--
-- Filtros para la tabla `table_operadores`
--
ALTER TABLE `table_operadores`
  ADD CONSTRAINT `table_operadores_ibfk_1` FOREIGN KEY (`idSitio`) REFERENCES `table_sitios` (`idSitio`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `table_operadores_ibfk_2` FOREIGN KEY (`idTipoOpe`) REFERENCES `tipo_operador` (`idTipoOpe`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `table_servicios`
--
ALTER TABLE `table_servicios`
  ADD CONSTRAINT `relacion_status` FOREIGN KEY (`status`) REFERENCES `tipo_status` (`id`),
  ADD CONSTRAINT `table_servicios_ibfk_1` FOREIGN KEY (`numero_taxista`) REFERENCES `taxistas` (`num_eco`),
  ADD CONSTRAINT `table_servicios_ibfk_2` FOREIGN KEY (`idSitio`) REFERENCES `table_sitios` (`idSitio`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `taxistas`
--
ALTER TABLE `taxistas`
  ADD CONSTRAINT `taxistas_ibfk_1` FOREIGN KEY (`idSitio`) REFERENCES `table_sitios` (`idSitio`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
