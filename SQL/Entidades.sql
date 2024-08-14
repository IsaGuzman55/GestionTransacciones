-- Active: 1723584257790@@bd28w0h20amjfgpv9llz-mysql.services.clever-cloud.com@3306
CREATE TABLE Transacciones(
    Id VARCHAR(45) PRIMARY KEY NOT NULL,
    FechaHora DATETIME NOT NULL,
    Monto INT NOT NULL,
    Estado ENUM("Pendiente", "Fallida", "Completada"),
    TipoTransaccion VARCHAR(100) NOT NULL,
    Plataforma VARCHAR(70) NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

CREATE TABLE Clientes(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    Identificacion VARCHAR(200) NOT NULL,
    Direccion VARCHAR(255) NOT NULL,
    Telefono VARCHAR(100) NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
    Rol ENUM("Admin", "User")
);

CREATE TABLE Facturas(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PeriodoFactura VARCHAR(50) NOT NULL,
    MontoFacturado INT,
    NumeroFactura VARCHAR(50) NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

CREATE TABLE Pagos(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    TransaccionId VARCHAR(45),
    FacturaId INT NOT NULL,
    MontoPagado INT,
    FOREIGN KEY (FacturaId) REFERENCES Facturas(Id),
    FOREIGN KEY (TransaccionId) REFERENCES Transacciones(Id)
);


ALTER TABLE Clientes AUTO_INCREMENT = 1;
