# 🌱 AgroClima MAUI

Aplicação .NET MAUI que integra três APIs:
- **IBGE** - Estados, municípios e produção agrícola
- **OpenWeatherMap** - Dados climáticos e mapa de calor
- **USDA FoodData Central** - Informações nutricionais

## 📋 Requisitos
- .NET 8 SDK
- Visual Studio 2022 com workload MAUI

## 🚀 Como executar
```bash
dotnet restore
dotnet build
dotnet maui run
```

## 🏗️ Estrutura
- **Models/** - Entidades de domínio
- **Services/** - Serviços de API
- **ViewModels/** - Lógica de apresentação (MVVM)
- **Views/** - 4 telas XAML
- **Helpers/** - Métodos POO (GetForm, SetForm, ClearForm, ValidateForm)

## 🔑 Configurar APIs
Substitua as chaves nos services:
- ClimaApiService.cs → ApiKey OpenWeatherMap
- USDAFoodDataService.cs → ApiKey USDA
