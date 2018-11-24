using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteHabilidade;

namespace TesteHabilidade.rotinas
{

    static class ConstantsRelatorio
    {

        //Constantes de definição de Colunas do relatório. 
        public const  int colunasNome    = 32;
        public const  int colunasDep     = 14;
        public const  int colunasSalario = 10;
        public const  int colunasCPF     = 14;
        public const  int colunasIR      = 9;

  }

    public class Rotinas
    {

        //Método para formatação do relatório.
        public String imprimeIfensFaltantes(int size)
        {
            String text = "";
            for(int i = 0; i<= size; i++)
            {
                text += " ";
            }
            text += "|";
            return text;
        }

        //Método para formatar o CPF para apresentar no Relatório.
        public String formataCPF(String CPF)
        {
            char[] chCpf= CPF.ToCharArray();
            String sCpfFormatado = "";

            validaCPF(CPF);

            for (int i = 0; i < CPF.Length; i++)
            {
                if (i == 3)
                    sCpfFormatado += ".";
                else if (i == 6)
                    sCpfFormatado += ".";
                else if (i == 9)
                    sCpfFormatado += "-";

                sCpfFormatado += chCpf[i];
            }

            return sCpfFormatado;
        }

        public int validaCPF(String CPF)
        {
            String sFormat  = removeCaracteres('.', CPF); ;
            sFormat         = removeCaracteres('.', sFormat);
            sFormat         = removeCaracteres('-', sFormat);
            sFormat         = removeCaracteres('/', sFormat);

            //Condição de saída do programa
            if (CPF.Equals("0")) 
                return 1;

            //Retorna 0 caso o CPF seja válido
            if (sFormat.Length == 11)
                return 0;
            //Retorna -1 caso o  CPF não seja válido
            else 
                return -1;

        }

        //Rotina para remover caracteres indesejados em uma String.
        public String removeCaracteres(char c /*Caractere a ser removido*/, String buffer /*Texto a ser formatado*/)
        {

            char[] chBuffer = buffer.ToCharArray();

            String sFormatada = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                if (chBuffer[i].Equals(c))
                    continue;
                else
                    sFormatada += chBuffer[i];
            }

            return sFormatada;
        }
    }


}
