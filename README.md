# Sistema de Gerenciamento de Veículos

## Visão Geral
O Sistema de Gerenciamento de Veículos é uma plataforma onde o usuário, que é o próprio dono dos veículos, pode gerenciar todas as informações sobre seus veículos, manutenções e despesas. O sistema permite acompanhar o histórico de manutenção e controlar os gastos, tudo em um só lugar.

### Exemplo de Uso Completo
Imagine que o usuário acabou de comprar um carro novo. Ele acessa o sistema, adiciona o veículo com as informações básicas e, depois de algum tempo, registra uma revisão. No fim do mês, ele adiciona uma despesa de combustível e vê o histórico de serviços e gastos no carro. Dessa forma, ele mantém um controle organizado e tem uma visão clara de todas as despesas e manutenções feitas no veículo.

## Funcionalidades
- **Cadastro de Veículos**: Adicione, edite ou remova veículos com informações como marca, modelo, ano, quilometragem, placa e observações.
- **Gerenciamento de Serviços**: Registre e controle serviços de manutenção realizados nos veículos.
- **Histórico de Serviços**: Veja o histórico completo de manutenções e consertos para cada veículo.
- **Gerenciamento de Despesas**: Registre despesas associadas aos veículos, como custos de manutenção, combustível e peças.
- **Histórico de Despesas**: Veja o histórico completo de despesas para cada veículo.
- **Oficinas**: Gerencie as oficinas cadastradas.
- **Login Social**: Autenticação via Google e Facebook.
- **Confirmação por Email**: Registro de usuários com confirmação de email.
- **Controle de Acesso**: Telas públicas e privadas, acessíveis somente para usuários logados.

## Estrutura do Projeto
A aplicação é estruturada em três camadas principais: Model, View e Controller (MVC).

### Models
Contêm a lógica de dados, definindo as entidades da aplicação:
- **Veículo**: Marca, Modelo, Ano, Quilometragem, Placa, Observações.
- **Serviço**: Categoria de Serviço, Data, Status, Observações.
- **Despesa**: Tipo de Despesa, Valor, Data, Observações.
- **Categoria de Serviço**: Tipos de serviços (ex.: troca de óleo, alinhamento).
- **Tipo de Despesa**: Categorias de gastos (ex.: combustível, manutenção).
- **Oficina**: Nome, Responsável, Telefone.

### Controllers
Controlam as operações das principais entidades do sistema:
- **VeiculosController**: Gerencia as operações relacionadas a veículos.
- **ServicosController**: Controla as operações de criação e edição de serviços.
- **DespesasController**: Gerencia as despesas registradas.
- **OficinasController**: Controla as operações relacionadas às oficinas.

### Views
Exibe a interface para o usuário interagir com as funcionalidades da aplicação, incluindo telas públicas e privadas.

## Tecnologias Utilizadas
- **.NET**: Framework principal para o desenvolvimento do backend.
- **JavaScript**: Aumenta a interatividade da interface.
- **C#**: Linguagem utilizada para construir o backend e a lógica do sistema.
- **Entity Framework**: ORM para interação com o banco de dados.
- **Unity**: para interação do HomerController com o DbContext.
- **SQL Server**: Banco de dados utilizado na aplicação.

## Estrutura de Telas

### Públicas:
- **Página inicial**: Exibe uma visão geral dos benefícios da aplicação e oferece opções de login e cadastro.
- **Página de Explicação**: Introdução à aplicação, destacando seus benefícios.

### Privadas:
- **Dashboard**: Visão geral de veículos, serviços e despesas.
- **Cadastro de Veículos**: Adição, edição e remoção de veículos.
- **Gerenciamento de Serviços**: Registro e controle de serviços de manutenção.
- **Histórico de Serviços**: Histórico completo de manutenções e consertos.
- **Gerenciamento de Despesas**: Registro de despesas associadas aos veículos.
- **Histórico de Despesas**: Histórico completo de despesas.
- **Oficinas**: Gestão das oficinas cadastradas.
- **Configurações de Usuário**: Atualização de informações pessoais.

## Validações
- **Validação de placa única por usuário**: Uma mesma placa só pode ser cadastrada uma vez por usuário.
- **Validação de formato de placa**: Segue o padrão atual do Brasil.
- **Validação de quilometragem crescente**: A quilometragem não pode ser menor que a anterior.
- **Validação de data de serviço**: Garantia de que não seja futura.
- **Validação de data de despesa**: Garantia de que não seja futura.
- **Validação de valor de despesa**: Impede valores negativos.

## Segurança e Controle de Acesso
A aplicação possui autenticação via Google e Facebook para controle de acesso. Algumas páginas e funcionalidades são protegidas e acessíveis apenas para usuários logados, incluindo a criação de novos veículos e o registro de despesas e serviços.
**Nota:** Não se esqueça de substituir as chaves de autenticação do Google e Facebook:
```csharp
//app.UseFacebookAuthentication(
//   appId: "1827322141132429",
//   appSecret: "44f0122308ffc32ee4ccc3b8688fc4e8");

//app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
//{
//    ClientId = "158463186876-ap7fnlcspui68kkscgk4b2rko5ev744c.apps.googleusercontent.com",
//    Client
```
## Como Rodar a Aplicação

1. Clone o repositório: `git clone https://github.com/brina-malaquias/TP3_A1.git`
2. Configure o banco de dados no arquivo `Web.config`
3. Execute as migrações do Entity Framework: `dotnet ef database update`
4. Inicie a aplicação: `dotnet run`
