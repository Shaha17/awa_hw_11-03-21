using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using DZ_11_03_21.context;
using DZ_11_03_21.models;
using Microsoft.EntityFrameworkCore;

namespace DZ_11_03_21
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				System.Console.WriteLine("1. Create");
				System.Console.WriteLine("2. Read");
				System.Console.WriteLine("3. Update");
				System.Console.WriteLine("4. Delete");
				System.Console.Write("Выберите действие: ");
				int.TryParse(Console.ReadLine(), out int choose);
				System.Console.WriteLine();
				switch ((CrudAction)choose)
				{
					case CrudAction.Create:
						{
							await CreateCustomerAsync();
							break;
						}
					case CrudAction.Read:
						{
							System.Console.WriteLine("Все клиенты из базы данных:");
							await ShowAllCustomersAsync();
							break;
						}
					case CrudAction.Update:
						{
							if (!int.TryParse(getValue("Введите id для обновления данных: "), out int id))
							{
								System.Console.WriteLine("Введён не верный айди");
								Console.ReadKey();
								continue;
							}
							await UpdateCustomerByIdAsync(id);
							break;
						}
					case CrudAction.Delete:
						{
							if (!int.TryParse(getValue("Введите id для удаления: "), out int id))
							{
								System.Console.WriteLine("Введён не верный айди");
								Console.ReadKey();
								continue;
							}
							await DeleteCustomerByIdAsync(id);

							break;
						}
					default:
						System.Console.WriteLine("Неизвестная операция, нажмите любую кнопку, чтобы попробовать заново...");
						Console.ReadKey();
						break;
				}
			}
		}

		private static string getValue(string text)
		{
			Console.Write(text);
			return Console.ReadLine().Trim();
		}
		public static async Task DeleteCustomerByIdAsync(int id)
		{
			using (var ctx = new dbContext())
			{

				try
				{
					var cst = ctx.Customers.FindAsync(id);
					ctx.Remove(cst);
					await ctx.SaveChangesAsync();
				}
				catch (System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					Console.ReadKey();

				}
			}
		}
		public static async Task UpdateCustomerByIdAsync(int id)
		{
			using (var ctx = new dbContext())
			{

				try
				{
					var cst = await ctx.Customers.FindAsync(id);
					cst.LastName = getValue("Фамилия: ");
					cst.FirstName = getValue("Имя: ");
					cst.MiddleName = getValue("Отчество: ");
					cst.DateOfBirth = DateTime.Parse(getValue("Дата рождения: "));
					await ctx.SaveChangesAsync();
				}
				catch (System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					Console.ReadKey();

				}
			}
		}
		public static async Task ShowAllCustomersAsync()
		{
			using (var ctx = new dbContext())
			{

				try
				{
					var cusotmers = ctx.Customers;
					if (cusotmers.Count() == 0)
					{
						System.Console.WriteLine("База данных пуста...");
						Console.ReadKey();
					}
					else
					{
						await cusotmers.ForEachAsync<Customer>(cst => cst.Show());
					}
					Console.ReadKey();

				}
				catch (System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					Console.ReadKey();

				}
			}
		}
		public static async Task CreateCustomerAsync()
		{
			var newCstmr = new Customer()
			{
				LastName = getValue("Фамилия: "),
				FirstName = getValue("Имя: "),
				MiddleName = getValue("Отчество: "),
				DateOfBirth = DateTime.Parse(getValue("Дата рождения (гггг-мм-дд): ")),
			};
			using (var ctx = new dbContext())
			{
				ctx.Customers.Add(newCstmr);

				try
				{
					await ctx.SaveChangesAsync();
				}
				catch (System.Exception ex)
				{
					System.Console.WriteLine(ex.Message);
					Console.ReadKey();

				}
			}
		}
	}
	enum CrudAction
	{
		Create = 1,
		Read = 2,
		Update = 3,
		Delete = 4
	}
}
