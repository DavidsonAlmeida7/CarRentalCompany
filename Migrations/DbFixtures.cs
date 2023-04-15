using CarRentalCompany.Models.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalCompany.Migrations
{
    public class DbFixtures
    {
        public static void seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "Nome", "Email", "DataNascimento", "SalarioBase", "DataCadastro", "DataAtualizado" },
                values: new object[,]
                {
                    { 1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, DateTime.Now, null },
                    { 2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 3100.0, DateTime.Now, null },
                    { 3, "Alex Grey", "alex@gmail.com", new DateTime(1998, 1, 15), 2200.0, DateTime.Now, DateTime.Now },
                    { 4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 3000.0, DateTime.Now, null },
                    { 5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 5300.0, DateTime.Now, null },
                    { 6, "Davidson Almeida", "davidson@gmail.com", new DateTime(1992, 5, 29), 3000.0, DateTime.Now, null }
                }
            );

            migrationBuilder.InsertData(
                table: "Veiculo",
                columns: new[] { "Id", "Tipo", "Modelo", "Ano", "Cor", "Valor", "DescricaoDetalhes", "Imagem", "DataCadastro", "DataAtualizado", "Alugado" },
                values: new object[,]
                {
                    { 1, 1, "BMW Z4", new DateTime(2020, 05, 21), 4, 445500.50, "4 portas, ar condicionado, air-bague", "bmw-z4.jpg", DateTime.Now, null, false },
                    { 2, 4, "Mercedes-Benz", new DateTime(2021, 09, 21), 5, 560500.50, "4 portas, ar condicionado, air-bague, GLA 200 ff Advance.", "mercedes-benz-gla-200.jpg", DateTime.Now, null, false },
                    { 3, 3, "Volkswagen Gol G5", new DateTime(2010, 09, 21), 5, 17500.50, "4 portas, ar condicionado, air-bague, 1.6 Mi total flex 8v", "gol-g5.jpg", DateTime.Now, null, false},
                }
            );

            migrationBuilder.InsertData(
                table: "Locacao",
                columns: new[] { "Id", "Status", "Valor", "TempoAluguelDias", "DataRetirada", "DataDevolucao", "DataAtiva", "DataFinalizada", "DataCancelada", "ClienteId", "VeiculoId" },
                values: new object[,]
                {
                    { 1, 1, 1547.25, 30, DateTime.Now.AddMonths(-1), DateTime.Now, DateTime.Now.AddMonths(-1), DateTime.Now, null, 1, 1 },
                    { 2, 1, 2113.25, 31, DateTime.Now.AddMonths(-1), DateTime.Now, DateTime.Now.AddMonths(-1), DateTime.Now, null, 6, 2 },
                }
            );
        }
    }
}
