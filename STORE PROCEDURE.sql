CREATE PROC AgregarUsuarios
@nombre varchar(50), @contraseña varchar(50), @Id int
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM Usuario WHERE Id=@Id
BEGIN
INSERT INTO Usuario (NombreUsuario, Contraseña, Id) VALUES (@nombre, @contraseña, @Id)
PRINT 'El usuario a sido registrado con exito'
END
ELSE
BEGIN
PRINT 'Este usuario ya existe'
END
RETURN @@ROWCOUNT
END
