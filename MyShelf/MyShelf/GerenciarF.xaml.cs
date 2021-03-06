﻿using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Modelo;
using Negocio;

namespace MyShelf
{
    /// <summary>
    /// Lógica interna para GerenciarF.xaml
    /// </summary>
    public partial class GerenciarF : Window
    {
        NFuncionario f = new NFuncionario();
        public GerenciarF()
        {
            InitializeComponent();
            funcionarios.ItemsSource = f.Listar();
            if(funcionarios.Items.Count == 0)
            {
                but_atualizar.IsEnabled = false;
                but_Excluir.IsEnabled = false;
            }
        }
        private void Adicionar(object sender, RoutedEventArgs e)
        {
            Window n = new AddF();
            bool no_errors = true;
            if (n.ShowDialog().Value)
            {
                try
                {
                    Funcionario l = (n as AddF).GetFuncionario();
                    f.Adicionar(l);
                }
                catch (Exception k)
                {
                    MessageBox.Show(k.Message);
                    no_errors = false;
                }
                if (no_errors)
                {
                    funcionarios.ItemsSource = f.Listar();
                    if (funcionarios.Items.Count == 0)
                    {
                        but_atualizar.IsEnabled = false;
                        but_Excluir.IsEnabled = false;
                    }
                    else
                    {
                        but_atualizar.IsEnabled = true;
                        but_Excluir.IsEnabled = true;
                    }
                }
            }
        }

        private void Excluir(object sender, RoutedEventArgs e)
        {
            if (funcionarios.SelectedItem != null) {
                f.Excluir((funcionarios.SelectedItem) as Funcionario);
                funcionarios.ItemsSource = f.Listar();
                if (funcionarios.Items.Count == 0)
                {
                    but_atualizar.IsEnabled = false;
                    but_Excluir.IsEnabled = false;
                }
                else
                {
                    but_atualizar.IsEnabled = true;
                    but_Excluir.IsEnabled = true;
                }
            }
        }

        private void Atualizar(object sender, RoutedEventArgs e)
        {
            if (funcionarios.SelectedItem != null)
            {
                Window n = new AttF(funcionarios.SelectedItem as Funcionario);
                if (n.ShowDialog().Value)
                {
                    f.Atualizar((n as AttF).GetFuncionario());
                    funcionarios.ItemsSource = f.Listar();
                    if (funcionarios.Items.Count == 0)
                    {
                        but_atualizar.IsEnabled = false;
                        but_Excluir.IsEnabled = false;
                    }
                    else
                    {
                        but_atualizar.IsEnabled = true;
                        but_Excluir.IsEnabled = true;
                    }
                }
            }
        }
    }
}
