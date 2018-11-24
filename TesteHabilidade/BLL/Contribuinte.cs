using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TesteHabilidade
{
    static class Constants
    {
        public const double IRPF_PRIMEIRA_FAIXA = 0.0;
        public const double IRPF_SEGUNDA_FAIXA  = 7.5;
        public const double IRPF_TERCEIRA_FAIXA = 15.0;
        public const double IRPF_QUARTA_FAIXA   = 22.5;
        public const double IRPF_QUINTA_FAIXA   = 27.5;

        public const double DESCONTO_DEPENDENTE = 5.0;
    }
    class Contribuinte
    {

        public String nome { get;   set; }
        public double salario { get;  set; }
        public int dependentes { get;  set; }
        public String cpf { get;  set; }
        public double IR { get; set; }

        public double salarioMinimo {  get; private set; }
        

        public Contribuinte()
        {
        }

        //Rotina para calcular o IR do contribuinte com base no Salário mínimo informado.
        public double CalcularIR(double salarioMinimo)
        {

            double IR = 0.0;

            if (FaixaIrpf.EnumFaixaIRPF.PRIMEIRA.ToString().Equals(IdentificarFaixaIR(salarioMinimo)))
                IR = (CalcularRendaLiquida(salarioMinimo) * Constants.IRPF_PRIMEIRA_FAIXA) / 100;
            else if (FaixaIrpf.EnumFaixaIRPF.SEGUNDA.ToString().Equals(IdentificarFaixaIR(salarioMinimo)))
                IR = (CalcularRendaLiquida(salarioMinimo) * Constants.IRPF_SEGUNDA_FAIXA)  / 100;
            else if(FaixaIrpf.EnumFaixaIRPF.TERCEIRA.ToString().Equals(IdentificarFaixaIR(salarioMinimo)))
                IR = (CalcularRendaLiquida(salarioMinimo) * Constants.IRPF_TERCEIRA_FAIXA) / 100;
            else if(FaixaIrpf.EnumFaixaIRPF.QUARTA.ToString().Equals(IdentificarFaixaIR(salarioMinimo)))
                IR = (CalcularRendaLiquida(salarioMinimo) * Constants.IRPF_QUARTA_FAIXA)   / 100;
            else if(FaixaIrpf.EnumFaixaIRPF.QUINTA.ToString().Equals(IdentificarFaixaIR(salarioMinimo)))
                IR = (CalcularRendaLiquida(salarioMinimo) * Constants.IRPF_QUINTA_FAIXA)   / 100;

            this.IR = IR;
            return IR;
        }

        //Rotina para identificar em qual faixa o contribuinte está.
        public String IdentificarFaixaIR(double salarioMinimo)
        {
            String  ret =  (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 2) <= 0) ? FaixaIrpf.EnumFaixaIRPF.PRIMEIRA.ToString() : "";
                    ret =  (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 2) > 0) && (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 4) <= 0) ? FaixaIrpf.EnumFaixaIRPF.SEGUNDA.ToString()  : ret;
                    ret =  (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 4) > 0) && (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 5) <= 0) ? FaixaIrpf.EnumFaixaIRPF.TERCEIRA.ToString() : ret;
                    ret =  (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 5) > 0) && (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 7) <= 0) ? FaixaIrpf.EnumFaixaIRPF.QUARTA.ToString()   : ret;
                    ret =  (CalcularRendaLiquida(salarioMinimo).CompareTo(salarioMinimo * 7) > 0) ? FaixaIrpf.EnumFaixaIRPF.QUINTA.ToString() : ret;

            return ret;
        }

        //Rotina para calcular o Desconto definido para dependentes
        public double CalcularDescontoDependentes(double salarioMinimo)
        {
            double desconto = (salarioMinimo * Constants.DESCONTO_DEPENDENTE) / 100;
            return desconto;
        }

        //Rotina para calcular a renda líquida do Contribuinte.
        public double CalcularRendaLiquida(double salarioMinimo)
        {
            double renda = salario;

            if (dependentes >= 1)
              renda = salario - (CalcularDescontoDependentes(salarioMinimo) * dependentes);

            return renda;
        }

    }
}
