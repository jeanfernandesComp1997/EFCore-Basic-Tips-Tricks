# Conceitos Databases e EFCore

# Code First
	
	É a abordagem mais popular e natural para quem irá utilizar o Entity Framework Core.

	Basicamente o que você faz é o seguinte, em vez de criar todo banco de dados primeiro, você como
	programador se concentra no domínio, e começa criando suas classes que posteriormente irão se 
	materializar através de métodos de extensão, o qual são conhecidos como Fluent API que é uma das
	formas de fazer todo mapeamento de entidade utilizando métodos, ou Data Annotations, que são 
	atriburos adicionados a uma classe ou a uma propriedade.

# Database First

	Primeiramente é criado o banco de dados, tabelas, campos e índices.

	1º Escrever todas as classes que representa suas entidades e relacionamentos manualmente.
	2º É uma forma mais simples que é fazer engenharia reversa do banco de dados sem a necessidade
	de escrever todo o código do zero, este processo chama-se Scaffold.

# DbContext

	Combinação dos padrões de projetos UoW e Repository, que contém um conjunto de métodos responsáveis
	por gravar e ler informações do banco de dados.

	O DbContext é a classe principal e mais importante que você terá acesso, o objetivo dela é simplificar
	a interação de sua aplicação com seu banco de dados.

	Reponsabilidades:
		- Configurar modelo de dados;
		- Gerenciar a conexao com o banco de dados;
		- Consultar e persistir dados em seu banco;
		- Fazer toda rastreabilidade de objetos;
		- Materializar resultados de consulta;
		- Cache de primeiro nível.

	OnConfiguring:
		É usado para informar qual provider será utilizado e informar a string de comexão, logger,
		serviços customizados e outros.

	OnModelCreating:
		É usado para configurar todo o modelo de dados que serão posteriormente transformados em
		tabelas e comandos SQL's.

	SaveChanges:
		Método responsável por coletar os dados que sofreram alterações e persitir no banco de dados.

# Migrações
	
	A migração é um dos recursos mais importantes do entity Framework Core no qual você versiona seu modelo
	de dados, neste processo é gerado um arquivo contendo as últimas alterações que você fez em seu modelo
	de dados.

	O que é necessário para criar uma migração?
		- EntityFrameworkCore.Design;
		- EntityFrameworkCore.Tools;

		ou

		Global Tool Entity Framework Core
			dotnet tool install --global dotnet-ef
			dotnet ef

	Utilizando o EFCore Tools:
		
		get-help entityframework

		Adicionando uma migração
			Add-Migration

		Remove-Migration
		Script-Migration
		Update-Database

# Criando a primeira migração:

	dotnet ef migrations add PrimeiraMigracao -p .\Curso\CursoEFCore.csproj

# Gerando um Script de Migração SQL

	dotnet ef migrations script -p .\Curso\CursoEFCore\CursoEFCore.csproj -o .\Curso\PrimeiraMigracao.SQL

# Aplicando a migração

	dotnet ef database update -p .\Curso\CursoEFCore\CursoEFCore.csproj -v

# Gerando scripts sql idempotentes
	dotnet ef migrations script -p .\Curso\CursoEFCore\CursoEFCore.csproj -o .\Curso\Idempotente.SQL -i

# Rollback de migrações
	dotnet ef database update Primeiramigracao -p .\Curso\CursoEFCore\CursoEFCore.csproj -v
	dotnet ef migrations remove -p .\Curso\CursoEFCore\CursoEFCore.csproj -v


# Opções de carregamento

	**Carregamento adiantado**: Significa que os dados relacionandos são carregados do banco de dados em uma 
	única consulta.

	**Carregamento explícito**: Significa que os dados relacionados são explicitamente carregados do banco de dados
	em um momento posterior.

	**Carregamento lento**: Siginifica que os dados relacionados são carregados sob demanda do banco de dados
	quando a propriedade de navegação é acessada.