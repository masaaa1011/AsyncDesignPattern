using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Immutable.Classes
{
    public class PrintPersonThread
    {
		Person _person;

		public PrintPersonThread(Person person)
		{
			_person = person;
		}

		public Task Run()
		{
			while (true)
			{
				Console.WriteLine(Thread.CurrentThread.Name + " prints " + _person.ToString());
			}
		}
	}
}
