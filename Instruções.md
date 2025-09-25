Sistema de Gestão de Eventos Corporativos
Requisitos

Windows 10/11

Visual Studio 2022 (ou superior) com suporte a WPF e .NET 6/7

SQL Server (pode ser Express)

.NET SDK correspondente ao projeto

1. Configurando o Banco de Dados

Abra o SQL Server Management Studio (SSMS) ou qualquer ferramenta de SQL.

Crie um banco de dados chamado EventosDB.

Abra o arquivo SQL/EventosDB_Script.sql do repositório.

Execute o script para criar todas as tabelas e relações.

Verifique se o banco está populado corretamente (opcional).

⚠️ Certifique-se de que a string de conexão no arquivo AppDbContext.cs esteja correta:

optionsBuilder.UseSqlServer("Server=localhost;Database=EventosDB;Trusted_Connection=True;TrustServerCertificate=True;");


Se você usa outro servidor ou usuário, atualize a conexão.

2. Rodando o Projeto

Abra a solução SistemaEventosCorporativos.sln no Visual Studio.

Defina o projeto SistemaEventosCorporativos.UI como projeto inicial.

Pressione F5 para compilar e executar.

A janela de login aparecerá. Clique no SR-71 para entrar na tela de login (mock).

3. Funcionalidades Principais

Cadastro de eventos, participantes e fornecedores.

Consulta de agenda dos participantes.

Relatórios de fornecedores mais utilizados e tipos de participantes mais frequentes.

Controle de lotação e orçamento dos eventos.

4. Observações

O login é um mock, apenas para demonstração. Usuário: admin, senha: 123.

Todas as regras de negócio estão implementadas no projeto.

Caso queira refazer as migrations, use o Package Manager Console:

Update-Database