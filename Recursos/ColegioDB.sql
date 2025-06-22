create database colegiodb;

use colegiodb;

CREATE TABLE Pagos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EstudianteId INT NOT NULL,
    NombreCliente VARCHAR(255) NOT NULL,
    Matricula DECIMAL(10,2) DEFAULT 0,
    Bus DECIMAL(10,2) DEFAULT 0,
    Mora DECIMAL(10,2) DEFAULT 0,
    FechaVencimiento DATETIME NOT NULL,
    Estado ENUM('pendiente', 'pagado', 'anulada') NOT NULL DEFAULT 'pendiente'
);

INSERT INTO Pagos (EstudianteId, NombreCliente, Matricula, Bus, Mora, FechaVencimiento, Estado)
VALUES
(101, 'Juan Pérez', 500.00, 500.00, 50.00, '2025-05-15 00:00:00', 'pendiente'),
(102, 'María López', 500.00, 0.00, 0.00, '2025-05-10 00:00:00', 'pagado'),
(103, 'Carlos Sánchez', 500.00, 300.00, 25.00, '2025-04-30 00:00:00', 'anulada');

CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(100) NOT NULL,
    Clave VARCHAR(255) NOT NULL,
    role VARCHAR(20) NOT NULL
);