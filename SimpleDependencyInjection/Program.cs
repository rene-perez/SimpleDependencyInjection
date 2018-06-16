using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
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
			builder.RegisterType<TodayWriter>();
			builder.RegisterType<TodayWriter2>();
			builder.Register<IDateWriter>(c =>
			{
				string writerType = ConfigurationManager.AppSettings["Writer"];
				if (writerType.Equals("1", StringComparison.OrdinalIgnoreCase))
				{
					return c.Resolve<TodayWriter>();
				}
				else
					return c.Resolve<TodayWriter2>();
			});
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
