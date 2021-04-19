using System;

//desafio: fazer uma classe de filme e uma de filmerepositorio

namespace DIO.Series
{
    class Program
    {
        
		static SerieRepositorio repositorio = new SerieRepositorio();

		static FilmeRepositorio repositorio2 = new FilmeRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			string opcaoUsuarioItem = FilmesOuSeries();

			while (opcaoUsuario.ToUpper() != "X")
			{

				//string opcaoUsuario = ObterOpcaoUsuario();

				//string opcaoUsuarioItem = FilmesOuSeries();


				switch (opcaoUsuario)
				{
					case "1":
						if (opcaoUsuarioItem == "1")
						{
							ListarSeries();

						}
						else if(opcaoUsuarioItem == "2")
						{
							ListarFilmes();
						}
						break;

					case "2":
						if (opcaoUsuarioItem == "1")
						{
							InserirSerie();

						}
						else if(opcaoUsuarioItem == "2")
						{
							InserirFilme();
						}
						
						break;

					case "3":

						if (opcaoUsuarioItem == "1")
						{
							AtualizarSerie();

						}
						else if(opcaoUsuarioItem == "2")
						{
							AtualizarFilme();
						}
						break;

					case "4":

						if (opcaoUsuarioItem == "1")
						{
							ExcluirSerie();

						}
						else if(opcaoUsuarioItem == "2")
						{
							ExcluirFilme();
						}
						break;

					case "5":
						
						if (opcaoUsuarioItem == "1")
						{
							VisualizarSerie();

						}
						else if(opcaoUsuarioItem == "2")
						{
							VisualizarFilme();
						}
						break;

					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
				
				opcaoUsuario = ObterOpcaoUsuario();
				opcaoUsuarioItem = FilmesOuSeries();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
			//Console.Clear();
        }

        private static void ExcluirSerie()
		{
			string confirmacao = "N";
			bool opcaoCerta = false;

			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			while (opcaoCerta == false)
			{

				Console.WriteLine();
				Console.Write("Você realmente quer excluir este item dos registros ativos? (S/N)");
			
				confirmacao = Console.ReadLine();

				if (confirmacao.ToUpper()=="S")
				{
					repositorio.Exclui(indiceSerie);
					Console.WriteLine("Item excluído com sucesso dos registros ativos.");
					opcaoCerta = true;
				}
				else if (confirmacao.ToUpper()=="N")
				{
					Console.WriteLine();
					Console.WriteLine("Item mantido nos registros ativos.");
					opcaoCerta = true;
				}
				else 
				{
					Console.WriteLine("Opção Inválida, por favor tente novamente.");
				}

			}

			
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine(); //tentar melhoria para aproveitar trecho de código tanto na adição quanto na atualização - fonte do ID é diferente

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine();
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar itens");
			Console.WriteLine("2- Inserir novo item");
			Console.WriteLine("3- Atualizar item");
			Console.WriteLine("4- Excluir item");
			Console.WriteLine("5- Visualizar item");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

		private static string FilmesOuSeries()
		{
			Console.WriteLine();
			Console.WriteLine("O item é uma série ou um filme?");
			Console.WriteLine("1 - Série");
			Console.WriteLine("2 - Filme");

			string opcaoItem = Console.ReadLine();

			return opcaoItem;
		}
		//-----------------------------------------------------------------------------------------------------------------------
		private static void ExcluirFilme()
		{
			string confirmacao = "N";
			bool opcaoCerta = false;

			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			while (opcaoCerta == false)
			{

				Console.WriteLine();
				Console.Write("Você realmente quer excluir este item dos registros ativos? (S/N)");
			
				confirmacao = Console.ReadLine();

				if (confirmacao.ToUpper()=="S")
				{
					repositorio.Exclui(indiceSerie);
					Console.WriteLine("Item excluído com sucesso dos registros ativos.");
					opcaoCerta = true;
				}
				else if (confirmacao.ToUpper()=="N")
				{
					Console.WriteLine();
					Console.WriteLine("Item mantido nos registros ativos.");
					opcaoCerta = true;
				}
				else 
				{
					Console.WriteLine("Opção Inválida, por favor tente novamente.");
				}

			}

			
		}

        private static void VisualizarFilme()
		{
			Console.Write("Digite o id do filme: ");
			int indiceFilme = int.Parse(Console.ReadLine());

			var filme = repositorio2.RetornaPorId(indiceFilme);

			Console.WriteLine(filme);
		}

        private static void AtualizarFilme()
		{
			Console.Write("Digite o id do filme: ");
			int indiceFilme = int.Parse(Console.ReadLine());

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Filme: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Filme: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Filme: ");
			string entradaDescricao = Console.ReadLine(); //tentar melhoria para aproveitar trecho de código tanto na adição quanto na atualização - fonte do ID é diferente

			Filme atualizaFilme= new Filme(id: indiceFilme,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio2.Atualiza(indiceFilme, atualizaFilme);
		}
        private static void ListarFilmes()
		{
			Console.WriteLine("Listar filmes");

			var lista = repositorio2.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhum filme cadastrada.");
				return;
			}

			foreach (var filme in lista)
			{
                var excluido = filme.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirFilme()
		{
			Console.WriteLine("Inserir novo filme");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título do Filme: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início do Filme: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição do Filme: ");
			string entradaDescricao = Console.ReadLine();

			Filme novoFilme = new Filme(id: repositorio2.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio2.Insere(novoFilme);
		}

    }
}
