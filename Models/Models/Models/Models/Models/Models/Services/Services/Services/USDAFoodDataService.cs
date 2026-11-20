using System.Net.Http.Json;
using AgroClimaMaui.Models;

namespace AgroClimaMaui.Services
{
    public class USDAFoodDataService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "SUA_API_KEY_USDA";
        private const string BaseUrl = "https://api.nal.usda.gov/fdc/v1";

        public USDAFoodDataService() { _httpClient = new HttpClient(); }

        public async Task<AlimentoUsda> GetAlimentoAsync(string nomeProduto)
        {
            try
            {
                var searchUrl = $"{BaseUrl}/foods/search?query={Uri.EscapeDataString(nomeProduto)}&pageSize=1&api_key={ApiKey}";
                var searchResponse = await _httpClient.GetFromJsonAsync<SearchResponse>(searchUrl);
                if (searchResponse?.Foods == null || !searchResponse.Foods.Any())
                    return ObterAlimentoSimulado(nomeProduto);

                var foodId = searchResponse.Foods.First().FdcId;
                var detailUrl = $"{BaseUrl}/food/{foodId}?api_key={ApiKey}";
                var detailResponse = await _httpClient.GetFromJsonAsync<FoodDetailResponse>(detailUrl);
                return MapearParaAlimentoUsda(detailResponse);
            }
            catch { return ObterAlimentoSimulado(nomeProduto); }
        }

        private AlimentoUsda ObterAlimentoSimulado(string nomeProduto)
        {
            return new AlimentoUsda
            {
                Id = 1,
                Descricao = nomeProduto,
                Categoria = "Produto Agrícola",
                Nutrientes = GerarNutrientesSimulados(),
                FichaTecnica = GerarFichaTecnicaSimulada()
            };
        }

        private List<Nutriente> GerarNutrientesSimulados()
        {
            return new List<Nutriente>
            {
                new Nutriente { Id = 1, Nome = "Energia", Quantidade = 365, Unidade = "kcal", Categoria = "Geral" },
                new Nutriente { Id = 2, Nome = "Proteína", Quantidade = 35.2, Unidade = "g", Categoria = "Macronutrientes" },
                new Nutriente { Id = 3, Nome = "Gordura Total", Quantidade = 18.5, Unidade = "g", Categoria = "Macronutrientes" },
                new Nutriente { Id = 4, Nome = "Carboidrato", Quantidade = 30.1, Unidade = "g", Categoria = "Macronutrientes" },
                new Nutriente { Id = 5, Nome = "Fibra Alimentar", Quantidade = 9.3, Unidade = "g", Categoria = "Macronutrientes" },
                new Nutriente { Id = 6, Nome = "Cálcio", Quantidade = 277, Unidade = "mg", Categoria = "Minerais" },
                new Nutriente { Id = 7, Nome = "Ferro", Quantidade = 15.7, Unidade = "mg", Categoria = "Minerais" },
                new Nutriente { Id = 8, Nome = "Potássio", Quantidade = 1804, Unidade = "mg", Categoria = "Minerais" }
            };
        }

        private FichaTecnica GerarFichaTecnicaSimulada()
        {
            return new FichaTecnica
            {
                Calorias = 365, Proteina = 35.2, Gordura = 18.5,
                Carboidrato = 30.1, Fibra = 9.3, Acucar = 2.5, Sodio = 15, Porcao = "100g"
            };
        }

        private AlimentoUsda MapearParaAlimentoUsda(FoodDetailResponse response)
        {
            var nutrientes = response.FoodNutrients?.Select(n => new Nutriente
            {
                Id = n.Id, Nome = n.Nutrient?.Name,
                Quantidade = n.Amount, Unidade = n.Nutrient?.UnitName,
                Categoria = n.Nutrient?.NutrientCategory?.Name
            }).ToList() ?? new List<Nutriente>();

            return new AlimentoUsda
            {
                Id = response.FdcId, Descricao = response.Description,
                Categoria = response.FoodCategory?.Name,
                Nutrientes = nutrientes,
                FichaTecnica = ExtrairFichaTecnica(nutrientes)
            };
        }

        private FichaTecnica ExtrairFichaTecnica(List<Nutriente> nutrientes)
        {
            return new FichaTecnica
            {
                Calorias = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Energy", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Proteina = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Protein", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Gordura = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Total lipid", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Carboidrato = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Carbohydrate", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Fibra = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Fiber", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Acucar = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Sugars", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Sodio = nutrientes.FirstOrDefault(n => n.Nome?.Contains("Sodium", StringComparison.OrdinalIgnoreCase) == true)?.Quantidade ?? 0,
                Porcao = "100g"
            };
        }

        private class SearchResponse { public List<FoodSearch> Foods { get; set; } }
        private class FoodSearch { public int FdcId { get; set; } public string Description { get; set; } }
        private class FoodDetailResponse { public int FdcId { get; set; } public string Description { get; set; } public FoodCategory FoodCategory { get; set; } public List<FoodNutrient> FoodNutrients { get; set; } }
        private class FoodCategory { public string Name { get; set; } }
        private class FoodNutrient { public int Id { get; set; } public double Amount { get; set; } public NutrientInfo Nutrient { get; set; } }
        private class NutrientInfo { public int Id { get; set; } public string Name { get; set; } public string UnitName { get; set; } public NutrientCategory NutrientCategory { get; set; } }
        private class NutrientCategory { public string Name { get; set; } }
    }
