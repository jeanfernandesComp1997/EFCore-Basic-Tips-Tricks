# Conceitos Databases e EFCore

# Code First
	
	� a abordagem mais popular e natural para quem ir� utilizar o Entity Framework Core.

	Basicamente o que voc� faz � o seguinte, em vez de criar todo banco de dados primeiro, voc� como
	programador se concentra no dom�nio, e come�a criando suas classes que posteriormente ir�o se 
	materializar atrav�s de m�todos de extens�o, o qual s�o conhecidos como Fluent API que � uma das
	formas de fazer todo mapeamento de entidade utilizando m�todos, ou Data Annotations, que s�o 
	atriburos adicionados a uma classe ou a uma propriedade.

# Database First

	Primeiramente � criado o banco de dados, tabelas, campos e �ndices.

	1� Escrever todas as classes que representa suas entidades e relacionamentos manualmente.
	2� � uma forma mais simples que � fazer engenharia reversa do banco de dados sem a necessidade
	de escrever todo o c�digo do zero, este processo chama-se Scaffold.

# DbContext

	Combina��o dos padr�es de projetos UoW e Repository, que cont�m um conjunto de m�todos respons�veis
	por gravar e ler informa��es do banco de dados.

	O DbContext � a classe principal e mais importante que voc� ter� acesso, o objetivo dela � simplificar
	a intera��o de sua aplica��o com seu banco de dados.

	Reponsabilidades:
		- Configurar modelo de dados;
		- Gerenciar a conexao com o banco de dados;
		- Consultar e persistir dados em seu banco;
		- Fazer toda rastreabilidade de objetos;
		- Materializar resultados de consulta;
		- Cache de primeiro n�vel.

	OnConfiguring:
		� usado para informar qual provider ser� utilizado e informar a string de comex�o, logger,
		servi�os customizados e outros.

	OnModelCreating:
		� usado para configurar todo o modelo de dados que ser�o posteriormente transformados em
		tabelas e comandos SQL's.

	SaveChanges:
		M�todo respons�vel por coletar os dados que sofreram altera��es e persitir no banco de dados.

# Migra��es
	
	A migra��o � um dos recursos mais importantes do entity Framework Core no qual voc� versiona seu modelo
	de dados, neste processo � gerado um arquivo contendo as �ltimas altera��es que voc� fez em seu modelo
	de dados.

	O que � necess�rio para criar uma migra��o?
		- EntityFrameworkCore.Design;
		- EntityFrameworkCore.Tools;

		ou

		Global Tool Entity Framework Core
			dotnet tool install --global dotnet-ef
			dotnet ef

	Utilizando o EFCore Tools:
		
		get-help entityframework

		Adicionando uma migra��o
			Add-Migration

		Remove-Migration
		Script-Migration
		Update-Database

# Criando a primeira migra��o:

	dotnet ef migrations add PrimeiraMigracao -p .\Curso\CursoEFCore.csproj

# Gerando um Script de Migra��o SQL

	dotnet ef migrations script -p .\Curso\CursoEFCore\CursoEFCore.csproj -o .\Curso\PrimeiraMigracao.SQL

# Aplicando a migra��o

	dotnet ef database update -p .\Curso\CursoEFCore\CursoEFCore.csproj -v

# Gerando scripts sql idempotentes
	dotnet ef migrations script -p .\Curso\CursoEFCore\CursoEFCore.csproj -o .\Curso\Idempotente.SQL -i

# Rollback de migra��es
	dotnet ef database update Primeiramigracao -p .\Curso\CursoEFCore\CursoEFCore.csproj -v
	dotnet ef migrations remove -p .\Curso\CursoEFCore\CursoEFCore.csproj -v


# Op��es de carregamento

	**Carregamento adiantado**: Significa que os dados relacionandos s�o carregados do banco de dados em uma 
	�nica consulta.

	**Carregamento expl�cito**: Significa que os dados relacionados s�o explicitamente carregados do banco de dados
	em um momento posterior.

	**Carregamento lento**: Siginifica que os dados relacionados s�o carregados sob demanda do banco de dados
	quando a propriedade de navega��o � acessada.