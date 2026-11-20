using System.Net.Http.Json;
using AgroClimaMaui.Models;

namespace AgroClimaMaui.Services
{
    public class ClimaApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "SUA_API_KEY_OPENWEATHERMAP";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

        public ClimaApiService() { _httpClient = new HttpClient(); }

        public async Task<DadosClima> GetClimaAsync(double latitude, double longitude)
        {
            try
            {
                var url = $"{BaseUrl}/weather?lat={latitude}&lon={longitude}&appid={ApiKey}&units=metric&lang=pt_br";
                var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
                if (response == null) return ObterClimaSimulado(latitude, longitude);

                return new DadosClima
                {
                    TemperaturaMedia = response.Main.Temp,
                    TemperaturaMaxima = response.Main.TempMax,
                    TemperaturaMinima = response.Main.TempMin,
                    Umidade = response.Main.Humidity,
                    PressaoAtmosferica = response.Main.Pressure,
                    Descricao = response.Weather.FirstOrDefault()?.Description,
                    Icone = response.Weather.FirstOrDefault()?.Icon,
                    DataAtualizacao = DateTime.Now,
                    MapaCalor = GerarMapaCalor(latitude, longitude)
                };
            }
            catch { return ObterClimaSimulado(latitude, longitude); }
        }

        private DadosClima ObterClimaSimulado(double lat, double lon)
        {
            var random = new Random((int)(lat * 100 + lon * 100));
            return new DadosClima
            {
                TemperaturaMedia = 15 + random.NextDouble() * 15,
                TemperaturaMaxima = 25 + random.NextDouble() * 10,
                TemperaturaMinima = 10 + random.NextDouble() * 10,
                Umidade = 40 + random.NextDouble() * 50,
                PressaoAtmosferica = 1000 + random.NextDouble() * 30,
                Descricao = "Parcialmente nublado",
                Icone = "02d",
                DataAtualizacao = DateTime.Now,
                MapaCalor = GerarMapaCalor(lat, lon)
            };
        }

        private List<double[,]> GerarMapaCalor(double lat, double lon)
        {
            var mapa = new List<double[,]>();
            var random = new Random((int)(lat * 1000 + lon * 1000));
            for (int dia = 0; dia < 7; dia++)
            {
                var dados = new double[10, 10];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                        dados[i, j] = 15 + random.NextDouble() * 20;
                mapa.Add(dados);
            }
            return mapa;
        }

        private class WeatherResponse { public MainData Main { get; set; } public List<WeatherInfo> Weather { get; set; } }
        private class MainData { public double Temp { get; set; } public double TempMax { get; set; } public double TempMin { get; set; } public double Humidity { get; set; } public double Pressure { get; set; } }
        private class WeatherInfo { public string Description { get; set; } public string Icon { get; set; } }
    }
