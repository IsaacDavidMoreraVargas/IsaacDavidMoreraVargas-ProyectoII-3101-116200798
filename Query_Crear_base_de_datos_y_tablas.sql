Create database ProyectoII
GO
Use ProyectoII
GO
Create table libro(
        Codigo_Libro int NOT NULL,
        Nombre_Libro varchar(30) NOT NULL,
        Nombre_Empresa varchar(30) NOT NULL,
		PRIMARY KEY (Codigo_Libro)
);

Create table cliente(
        Codigo_Cliente  int NOT NULL,
		Numero_Identificacion int NOT NULL,
        Nombre_Cliente varchar(30) NOT NULL,
        Fecha_Nacimiento varchar(20) NOT NULL,
		Correo_Electronico varchar(100) NOT NULL,
		PRIMARY KEY (Codigo_Cliente)
);

Create table stock(
		 Llave_Libro int NOT NULL IDENTITY,
         Codigo_Libro int NOT NULL,
		 Descripcion_Articulo varchar(30) NOT NULL,
		 Precio_Articulo float NOT NULL,
		 Codigo_Cliente int NOT NULL,
		 Fecha_Ingreso  varchar(20) NOT NULL,
		 PRIMARY KEY (Llave_Libro)
);

Create table retirados(
         Llave_retiro int NOT NULL,
         Codigo_Libro int NOT NULL,
         Nombre_Libro varchar(30) NOT NULL,
         Descripcion_Articulo varchar(30) NOT NULL,
         Codigo_Cliente int NOT NULL,
         Fecha_Retiro varchar(20) NOT NULL,
		 PRIMARY KEY (Llave_retiro)
);

