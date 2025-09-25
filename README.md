Sistema de Gestão de Eventos Corporativos
Descrição

Sistema desktop desenvolvido em C# com WPF e SQL Server, voltado para o gerenciamento de eventos corporativos, participantes e fornecedores.

O projeto atende aos requisitos de CRUD com relacionamentos, regras de negócio básicas, relatórios e interface desktop funcional.

Funcionalidades Implementadas (Obrigatórias)
Cadastro de Tipos de Eventos

Campo: Descrição

Cadastro de Eventos

Nome

Data de início e fim

Local (Endereço)

Observações

Lotação máxima

Orçamento máximo

Tipo do evento

Cadastro de Participantes

Nome completo

CPF

Telefone

Tipo (VIP, Interno, Externo)

Cadastro de Fornecedores

Nome do serviço

Valor

CNPJ

Regras de Negócio Implementadas

Cálculo do valor total do evento a partir dos fornecedores cadastrados.

Bloqueio de fornecedores que ultrapassem o orçamento do evento.

Evita que um participante esteja em eventos que ocorram nas mesmas datas.

Limita a inclusão de participantes à lotação máxima do evento.

Relatórios / Dashboards

Agenda dos participantes (filtragem por participante e exibição de eventos/datas).

Fornecedores mais utilizados (quantidade e valores gastos).

Tipos de participantes mais frequentes.

Saldo de orçamento dos eventos.

Estrutura do Projeto

Camadas:

CORE / Core: Entidades do sistema (Participante, Evento, Fornecedor, etc.)

DATA: Contexto do Entity Framework e migrations

UI: Telas WPF, UserControls e navegação

Banco de dados: SQL Server (migrations criadas para todas as tabelas)

Diferenciais Implementados (Opcional)

Mock de login com UserControl (simula autenticação).

Relatórios e dashboards em WPF.

Nota: Funcionalidades como SQL manual, JWT real e testes unitários xUnit não foram implementadas, pois são diferenciais opcionais.

Como Executar

Clone o repositório.

Abra a solução no Visual Studio 2022 ou superior.

Restaure os pacotes NuGet.

Configure o SQL Server local (ou ajuste a string de conexão em AppDbContext).

Aplique as migrations ou use o script SQL fornecido (EventosDB.sql).

Compile e execute o projeto.

Entregáveis

Código-fonte completo no repositório.

Scripts SQL de criação de banco ou migrations.

Instruções de execução (este README).
