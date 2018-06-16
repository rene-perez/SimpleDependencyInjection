using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDependencyInjection
{
	class Program
	{
		private static IContainer Container { get; set; }

		static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<ConsoleOutput>().As<IOutput>();
			builder.RegisterType<TodayWriter>().As<IDateWriter>();
			Container = builder.Build();

			// The WriteDate method is where we'll make use
			// of our dependency injection. We'll define that
			// in a bit.
			WriteDate();
		}

		public static void WriteDate()
		{
			// Create the scope, resolve your IDateWriter,
			// use it, then dispose of the scope.
			using (var scope = Container.BeginLifetimeScope())
			{
				var writer = scope.Resolve<IDateWriter>();
				writer.WriteDate();
				Console.ReadLine();
			}
		}
	}
}
