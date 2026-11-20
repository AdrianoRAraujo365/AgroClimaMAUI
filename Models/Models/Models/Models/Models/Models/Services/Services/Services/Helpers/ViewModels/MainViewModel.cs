💻 Visualizador de Arquivos

ViewModels/MainViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AgroClimaMaui.Models;
using AgroClimaMaui.Services;

namespace AgroClimaMaui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IBGEApiService _ibgeService;
        private readonly ClimaApiService _climaService;
        private readonly USDAFoodDataService _usdaService;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Estado> _estados;
        public ObservableCollection<Estado> Estados { get => _estados; set { _estados = value; OnPropertyChanged(); } }

        private ObservableCollection<Municipio> _municipios;
        public ObservableCollection<Municipio> Municipios { get => _municipios; set { _municipios = value; OnPropertyChanged(); } }

        private ObservableCollection<ProdutoAgricola> _produtos;
        public ObservableCollection<ProdutoAgricola> Produtos { get => _produtos; set { _produtos = value; OnPropertyChanged(); } }

        private DadosClima _dadosClima;
        public DadosClima DadosClima { get => _dadosClima; set { _dadosClima = value; OnPropertyChanged(); } }

        private AlimentoUsda _alimentoUsda;
        public AlimentoUsda AlimentoUsda { get => _alimentoUsda; set { _alimentoUsda = value; OnPropertyChanged(); } }

        private ProdutoAgricola _produtoSelecionado;
        public ProdutoAgricola ProdutoSelecionado { get => _produtoSelecionado; set { _produtoSelecionado = value; OnPropertyChanged(); } }

        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(); } }

        public ICommand CarregarEstadosCommand { get; }
        public ICommand CarregarMunicipiosCommand { get; }
        public ICommand CarregarProdutosCommand { get; }
        public ICommand ConsultarDadosCommand { get; }

        public MainViewModel()
        {
            _ibgeService = new IBGEApiService();
            _climaService = new ClimaApiService();
            _usdaService = new USDAFoodDataService();

            Estados = new ObservableCollection<Estado>();
            Municipios = new ObservableCollection<Municipio>();
            Produtos = new ObservableCollection<ProdutoAgricola>();

            CarregarEstadosCommand = new Command(async () => await CarregarEstados());
            CarregarMunicipiosCommand = new Command<string>(async (sigla) => await CarregarMunicipios(sigla));
            CarregarProdutosCommand = new Command<int>(async (codMun) => await CarregarProdutos(codMun));
            ConsultarDadosCommand = new Command(async () => await ConsultarDados());
        }

        private async Task CarregarEstados()
        {
            IsLoading = true;
            var estados = await _ibgeService.GetEstadosAsync();
            Estados.Clear();
            foreach (var estado in estados) Estados.Add(estado);
            IsLoading = false;
        }

        private async Task CarregarMunicipios(string siglaEstado)
        {
            if (string.IsNullOrEmpty(siglaEstado)) return;
            IsLoading = true;
            var municipios = await _ibgeService.GetMunicipiosAsync(siglaEstado);
            Municipios.Clear();
            foreach (var m in municipios) Municipios.Add(m);
            IsLoading = false;
        }

        private async Task CarregarProdutos(int codigoMunicipio)
        {
            IsLoading = true;
            var produtos = await _ibgeService.GetProdutosAgricolasAsync(codigoMunicipio);
            Produtos.Clear();
            foreach (var p in produtos) Produtos.Add(p);
            IsLoading = false;
        }

        private async Task ConsultarDados()
        {
            if (ProdutoSelecionado == null) return;
            IsLoading = true;
            var lat = -15.7801;
            var lon = -47.9292;
            DadosClima = await _climaService.GetClimaAsync(lat, lon);
            AlimentoUsda = await _usdaService.GetAlimentoAsync(ProdutoSelecionado.Nome);
            IsLoading = false;
            await Shell.Current.GoToAsync("///resultados");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
