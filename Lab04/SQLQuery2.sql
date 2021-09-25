create proc BuscarCursoTitulo
	@Title nvarchar(100)
as
	select * from Course where Title like '%'+@Title+'%';