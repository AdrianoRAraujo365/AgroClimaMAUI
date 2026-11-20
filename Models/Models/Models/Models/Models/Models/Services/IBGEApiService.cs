💻 Visualizador de Arquivos

Services/IBGEApiService.cs
using System.Net.Http.Json;
using AgroClimaMaui.Models;

namespace AgroClimaMaui.Services
{
    public class IBGEApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://servicodados.ibge.gov.br/api/v1";

        public IBGEApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Estado>> GetEstadosAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<EstadoIBGE>>($"{BaseUrl}/localidade/estados?orderBy=nome");
                return response?.Select(e => new Estado
                {
                    Id = e.Id,
                    Sigla = e.Sigla,
                    Nome = e.Nome,
                    Regiao = e.Regiao?.Nome
                }).ToList() ?? new List<Estado>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar estados: {ex.Message}");
                return new List<Estado>();
            }
        }

        public async Task<List<Municipio>> GetMunicipiosAsync(string siglaEstado)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MunicipioIBGE>>($"{BaseUrl}/localidade/estados/{siglaEstado}/municipios");
                return response?.Select(m => new Municipio
                {
                    Id = m.Id,
                    Nome = m.Nome,
                    Microrregiao = m.Microrregiao?.Nome,
                    Mesorregiao = m.Microrregiao?.Mesorregiao?.Nome
                }).ToList() ?? new List<Municipio>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar municípios: {ex.Message}");
                return new List<Municipio>();
            }
        }

        public async Task<List<ProdutoAgricola>> GetProdutosAgricolasAsync(int codigoMunicipio)
        {
            await Task.Delay(500);
            return new List<ProdutoAgricola>
            {
                new ProdutoAgricola { Id = 1, Nome = "Soja", ProducaoToneladas = 15000, ValorProducao = 45000000, Categoria = "Grãos" },
                new ProdutoAgricola { Id = 2, Nome = "Milho", ProducaoToneladas = 12000, ValorProducao = 30000000, Categoria = "Grãos" },
                new ProdutoAgricola { Id = 3, Nome = "Café", ProducaoToneladas = 8000, ValorProducao = 60000000, Categoria = "Bebidas" },
                new ProdutoAgricola { Id = 4, Nome = "Cana-de-açúcar", ProducaoToneladas = 50000, ValorProducao = 25000000, Categoria = "Industrial" }
            };
        }

        private class EstadoIBGE { public int Id { get; set; } public string Sigla { get; set; } public string Nome { get; set; } public RegiaoIBGE Regiao { get; set; } }
        private class RegiaoIBGE { public int Id { get; set; } public string Sigla { get; set; } public string Nome { get; set; } }
        private class MunicipioIBGE { public int Id { get; set; } public string Nome { get; set; } public MicrorregiaoIBGE Microrregiao { get; set; } }
        private class MicrorregiaoIBGE { public int Id { get; set; } public string Nome { get; set; } public MesorregiaoIBGE Mesorregiao { get; set; } }
        private class MesorregiaoIBGE { public int Id { get; set; } public string Nome { get; set; } }
    }
