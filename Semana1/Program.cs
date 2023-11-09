// See https://aka.ms/new-console-template for more information
//Console.WriteLine("RONALDO JOGA MUITO NO CORINTHIANS");
//Console.WriteLine("DIGITA ALGUMA COISA AÍ PVTÃO");
//Console.ReadLine();


#region Teste de tipos de dados

int tipoInteiro;
double tipoDouble;
string tipoString;
long tipoLong;

tipoInteiro=int.MaxValue;
tipoLong=long.MaxValue;

tipoDouble = Double.MaxValue;

tipoString = "100";

tipoInteiro = int.Parse(tipoString);

Console.WriteLine("O máximo inteiro é "+tipoInteiro);

Console.WriteLine("O máximo long é "+tipoLong);

Console.WriteLine("O máximo do Double "+tipoDouble);

Console.WriteLine("O máximo inteiro é "+tipoString+" convertido para int com parse ="+tipoInteiro);


#endregion

#region Operadores
tipoDouble = tipoInteiro + tipoLong;

//tipoInteiro = tipoDouble + tipoLong;

//tipoLong = tipoDouble + tipoInteiro;

tipoInteiro = 10 > 5 ? 1 : 0;
Console.WriteLine("Valor do inteniro após verificação de maior : "+tipoInteiro);


#endregion