💻 Visualizador de Arquivos

Helpers/FormHelper.cs
using AgroClimaMaui.Models;

namespace AgroClimaMaui.Helpers
{
    /// <summary>
    /// Helper POO com métodos: GetForm, SetForm, ClearForm, ValidateForm
    /// </summary>
    public static class FormHelper
    {
        public static FormularioModel GetForm(Picker pickerEstado, Picker pickerMunicipio, Picker pickerProduto)
        {
            return new FormularioModel
            {
                EstadoSelecionado = pickerEstado.SelectedItem as Estado,
                MunicipioSelecionado = pickerMunicipio.SelectedItem as Municipio,
                ProdutoSelecionado = pickerProduto.SelectedItem as ProdutoAgricola,
                DataConsulta = DateTime.Now
            };
        }

        public static void SetForm(Picker pickerEstado, Picker pickerMunicipio, Picker pickerProduto, FormularioModel dados)
        {
            if (dados == null) return;
            if (dados.EstadoSelecionado != null) pickerEstado.SelectedItem = dados.EstadoSelecionado;
            if (dados.MunicipioSelecionado != null) pickerMunicipio.SelectedItem = dados.MunicipioSelecionado;
            if (dados.ProdutoSelecionado != null) pickerProduto.SelectedItem = dados.ProdutoSelecionado;
        }

        public static void ClearForm(Picker pickerEstado, Picker pickerMunicipio, Picker pickerProduto)
        {
            pickerEstado.SelectedIndex = -1;
            pickerMunicipio.SelectedIndex = -1;
            pickerProduto.SelectedIndex = -1;
            pickerMunicipio.ItemsSource = null;
            pickerProduto.ItemsSource = null;
        }

        public static (bool IsValid, string Mensagem) ValidateForm(Picker pickerEstado, Picker pickerMunicipio)
        {
            if (pickerEstado.SelectedItem == null) return (false, "Por favor, selecione um estado.");
            if (pickerMunicipio.SelectedItem == null) return (false, "Por favor, selecione um município.");
            return (true, "Formulário válido.");
        }
    }
