-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-02-2024 a las 21:54:10
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `anteproyecto`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `entradas`
--

CREATE TABLE `entradas` (
  `id_entrada` int(11) NOT NULL,
  `id_usuario` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `contenido` varchar(10000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `entradas`
--

INSERT INTO `entradas` (`id_entrada`, `id_usuario`, `fecha`, `contenido`) VALUES
(1, 1, '2024-02-16', 'Hoy empecé mi viaje hacia las montañas de Suiza. El paisaje es impresionante, con picos nevados que se elevan hacia el cielo azul y lagos cristalinos reflejando la majestuosidad de los Alpes.'),
(2, 1, '2024-02-17', 'Caminé por senderos sinuosos y bosques frondosos en Canadá. El sonido de los pájaros y el viento me acompañaban en mi camino hacia la inmensidad de la naturaleza.'),
(3, 1, '2024-02-18', 'Alcanzé la cima de una montaña en Nepal y contemplé la vista panorámica del Himalaya. Me sentí pequeño ante tanta grandeza y agradecido por presenciar tal maravilla natural.'),
(4, 1, '2024-02-19', 'Descendí hacia un valle en Nueva Zelanda lleno de flores silvestres y arroyos cristalinos. El aire fresco revitalizaba mis sentidos mientras me sumergía en la belleza de este país.'),
(5, 1, '2024-02-20', 'Exploré ruinas antiguas en Perú, en el corazón de la selva amazónica. Cada piedra contaba una historia perdida en el tiempo, recordándome la grandeza de las civilizaciones antiguas.'),
(6, 1, '2024-02-21', 'Conocí a lugareños amables en Japón que compartieron sus leyendas y tradiciones. Me sentí bienvenido en este país lleno de contrastes entre lo moderno y lo tradicional.'),
(7, 1, '2024-02-22', 'Seguí mi viaje hacia un río serpenteante en Brasil. Pasé horas observando la danza del agua y los reflejos del sol en la exuberante selva tropical.'),
(8, 1, '2024-02-23', 'Acampé bajo un cielo estrellado en Australia y me dormí con el sonido de la naturaleza como melodía de fondo. La grandeza del Outback me dejó sin aliento y con el deseo de explorar más.'),
(9, 1, '2024-02-24', 'En mi último día, caminé de regreso en España con el corazón lleno de recuerdos y el alma renovada. La diversidad de paisajes y la riqueza cultural de este país me dejaron una huella imborrable.'),
(10, 1, '2024-02-25', 'Este viaje ha sido una experiencia inolvidable. Me llevo conmigo la belleza de estos paisajes y la calidez de su gente en Italia, un país lleno de historia, arte y pasión.');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `entradas`
--
ALTER TABLE `entradas`
  ADD PRIMARY KEY (`id_entrada`),
  ADD KEY `fk_idUsuario` (`id_usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `entradas`
--
ALTER TABLE `entradas`
  MODIFY `id_entrada` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `entradas`
--
ALTER TABLE `entradas`
  ADD CONSTRAINT `fk_idUsuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuarios` (`id_usuario`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
