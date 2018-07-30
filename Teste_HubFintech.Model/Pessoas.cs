using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Teste_HubFintech.Model
{
    public class Pessoas
    {
        public int PessoaId { get; set; }
        public string CpfCnpj { get; set; }
        public string NomeCompleto { get; set; }
        public string DataNascimento { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
    }
}