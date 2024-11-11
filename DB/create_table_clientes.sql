-- create a table named Companies with different columns

CREATE TABLE Clientes (
  id int IDENTITY(1, 1) not null,
  nombres varchar(250) not null,
  apellidos varchar(250) not null,
  fechanacimiento date not null,
  direccion varchar(250) not null,
  [password] varchar(250) not null,
  telefono varchar(250) not null,
  email varchar(250) not null,
  fechamodificacion  date
);



