using System;

namespace DZ_11_03_21.models
{
	public class Customer
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public void Show()
		{
			Console.WriteLine($"Id: {this.Id},\t Фамилия: {this.LastName},\t Имя: {this.FirstName},\t Отчество: {this.MiddleName},\t Дата рожения: {this.DateOfBirth.ToShortDateString()}");
		}
	}
}