using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TesteHabilidade;
using TesteHabilidade.rotinas;



namespace TesteHabilidade
{
    class Program
    {
        static void Main(string[] args)
        {

            
            //Instâncias ultilizadas no programa.
            List<Contribuinte> contribuintes = new List<Contribuinte>();
            Contribuinte c;
            Rotinas r = new Rotinas();

            double salarioMinimo = 0.0;
            int index = 0; //Variavel para controlar o indice de inserção na lista.
            int cpfInvalido = 0;

            //Laço para capturar as informação dos contribuintes.
            while (cpfInvalido != 1)
            {

                Console.Clear();
                c = new Contribuinte();
                Console.Write("Informe o CPF do contribuinte : ");
                c.cpf = Console.ReadLine().ToString();

                //Caso CPF seja válido continua a solicitar os dados do usuário.
                if(r.validaCPF(c.cpf) == 0)
                {
                    Console.Write("Informe o nome : ");
                    c.nome = Console.ReadLine().ToString();
                    Console.Write("Informe a quantidade de dependentes :");
                    c.dependentes = Convert.ToInt32(Console.ReadLine());
                    Console.Write("informe o seu salário : ");
                    c.salario = Convert.ToDouble(Console.ReadLine());
                    contribuintes.Insert(index, c);
                }
                else
                {
                    if (r.validaCPF(c.cpf) == 1) //Condição para finalizar cadastros.
                    {
                        cpfInvalido = 1;
                        continue;
                    }

                    Console.Write("Informe um CPF válido. ");
                    Thread.Sleep(2000);
                    continue;
                }
                index++;
            }

            Console.Clear();
            Console.Write("Informe o valor do Salário Mínimo:");
            salarioMinimo = Convert.ToDouble(Console.ReadLine());
            Console.Write("\n");

            //Laço para definir o calculo de IR dos contribuintes.
            foreach(Contribuinte cont in contribuintes)
            {
                cont.CalcularIR(salarioMinimo);
            }

            //Ordenação das Listas.
            var orderListNome = (from a in contribuintes orderby a.nome ascending select a).ToList();
            var orderListIR   = (from a in contribuintes orderby a.IR ascending select a).ToList();

            //Impressão do Relatório.
            ImprimirRelatorio("Lista ordenada por Nome.", orderListNome, salarioMinimo);
            ImprimirRelatorio("Lista ordenada por IR."  , orderListIR  , salarioMinimo);

            //Condição para encerrar a Execução do programa.
            Console.Write("Tecle Enter para sair.");
            while (true)
            {
                ConsoleKeyInfo kPress = Console.ReadKey();
                if (kPress.Key == ConsoleKey.Enter)
                    break;
            }



        }

        //Rotina para impressão do relatório.
        public static void ImprimirRelatorio( String title /* Titulo do Relatório */, List<Contribuinte> contribuintes /* Lista de contribuintes a ser listada*/, double salarioMinimo /*Salario minimo informado.*/)
        {


            //Escreve o cabeçalho do relatório.
            Console.Write("                ================= "+ title + " ===================\n");
            Console.Write("+---------------------------------+---------------+-----------+---------------+----------+ \n" +
                          "|NOME                             | DEPENDENTES   | SALARIO L | CPF           | IR       | \n" +
                          "+---------------------------------+---------------+-----------+---------------+----------+ \n");

            //Váriaveis que iram conter o restante de colunas faltantes para formatação do relatório.
            int szNome = 0;
            int szDep = 0;
            int szSal = 0;
            int szCPF = 0;
            int szIR = 0;

            Rotinas r = new Rotinas(); //Instância da Classe de Rotinas.
            //Laço para listar os dados no relatório.
            foreach (Contribuinte contribuinte in contribuintes)
            {
                szNome = rotinas.ConstantsRelatorio.colunasNome    - contribuinte.nome.Length;
                szDep  = rotinas.ConstantsRelatorio.colunasDep     - contribuinte.dependentes.ToString().Length;
                szSal  = rotinas.ConstantsRelatorio.colunasSalario - contribuinte.salario.ToString().Length;
                szCPF  = rotinas.ConstantsRelatorio.colunasCPF     - r.formataCPF(contribuinte.cpf).Length;
                szIR   = rotinas.ConstantsRelatorio.colunasIR      - contribuinte.CalcularIR(salarioMinimo).ToString().Length;

                Console.Write("|" + contribuinte.nome + r.imprimeIfensFaltantes(szNome) +
                                    contribuinte.dependentes + r.imprimeIfensFaltantes(szDep) +
                                    contribuinte.CalcularRendaLiquida(salarioMinimo) + r.imprimeIfensFaltantes(szSal) +
                                    r.formataCPF(contribuinte.cpf) + r.imprimeIfensFaltantes(szCPF) +
                                    contribuinte.CalcularIR(salarioMinimo) + r.imprimeIfensFaltantes(szIR) + "\n");

                Console.Write("+---------------------------------+---------------+-----------+---------------+----------+   \n");
            }

            Console.Write("\n");
            Console.Write("\n");

        }


    }
}
