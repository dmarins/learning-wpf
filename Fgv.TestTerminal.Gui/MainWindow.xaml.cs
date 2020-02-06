using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fgv.TestTerminal.Gui.Domain.Contracts.Request;
using Fgv.TestTerminal.Gui.Infrastructure.Clients;

namespace Fgv.TestTerminal.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            var contract = new AddressRequest
            {
                Cep = Cep.Text
            };

            var addressClient = new AddressClient();
            var addressInfo = Task.Run(() => addressClient.GetAddressInfo(contract)).Result;

            if (addressInfo != null && string.IsNullOrWhiteSpace(addressInfo.Uf))
            {
                AddressInfo.Text = "Erro ao recuperar os dados do endereço";
                return;
            }

            AddressInfo.Text = $"{addressInfo.Logradouro}\n{addressInfo.Bairro} / {addressInfo.Localidade} / {addressInfo.Uf}\n{addressInfo.Cep}";
        }
    }
}
