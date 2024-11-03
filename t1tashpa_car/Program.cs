using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t1tashpa_car
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Branch branch = new Branch("Main Branch");

            // Add cars to the branch
            branch.AddCar(new Car("C1", 'A'), 0);
            branch.AddCar(new Car("C2", 'B'), 1);
            branch.AddCar(new Car("C3", 'A'), 2);

            // Booking request for cars with specified levels
            char[] requestedLevels = { 'A', 'B', 'C' };
            string[] bookedCars = branch.BookCars(requestedLevels);

            // Print the booking results
            Console.WriteLine("Booking Results:");
            for (int i = 0; i < bookedCars.Length; i++)
            {
                Console.WriteLine($"Requested Level: {requestedLevels[i]}, Booked Car Code: {(bookedCars[i] == "" ? "None" : bookedCars[i])}");
            }
        }
    }

    public class Car
    {
        private string code;
        private char level;
        private bool booked; //booked for tomorrow?

        public Car(string code, char level)
        {
            this.code = code;
            this.level = level;
            this.booked = false; // initially not booked
        }

        //סעיף א
        public string BookCar(char level) 
        {
            if (level != this.level || this.booked) return ""; //if wrong level, or already booked
            this.booked = true; //book car
            return code;
        }
    }

    public class Branch //2 branches
    {
        string name;
        Car[] cars; //up to 100 cars in a branch

        public Branch(string name)
        {
            this.name = name;
            this.cars = new Car[100]; // initialize with space for 100 cars
        }

        public void AddCar(Car car, int index)
        {
            if (index >= 0 && index < cars.Length)
                cars[index] = car;
        }

        //סעיף ב
        public string[] BookCars(char[] levels)
        {
            string[] order = new string[levels.Length];
            for (int level = 0; level < levels.Length; level++)
            {
                int car = 0;
                string code="";
                bool booked = false;
                while (
                        car < this.cars.Length && //first check index
                        this.cars[car] != null && //then check if not null
                        !booked //while not booked, continue looping
                      )
                {
                    code = this.cars[car].BookCar(levels[level]);
                    if (code == "") car++;
                    else booked = true;
                }

                if (booked)
                    order[level] = code;

            }
            return order;
        }

    }
    /*
    //below is the solution to parts A and B of the original question
        public class Car
        {
            private string code;
            private char level;
            private bool booked; //booked for tomorrow?

            public string BookCar (char level, bool book) //if book == true, try to book a car with this level
            {
                if (level != this.level || this.booked) return ""; //if wrong level, or already booked
                if (book) this.booked = true; //book car
                return code;
            }
        }

        public class Branch //2 branches
        {
            string name;
            Car[] cars; //up to 100 cars in a branch

            public string[] Tomorrow(char[] levels)
            {
                string[] order = new string[levels.Length]; 
                for (int level = 0; level < levels.Length; level++)
                {
                    int car = 0;
                    while (
                        car < this.cars.Length && //first check index
                        this.cars[car] != null && //then check if not null
                        this.cars[car].BookCar(levels[level], false) == "" //check car, but don't book
                        )
                        car++;

                    if (car < this.cars.Length) //car can be booked successfully
                        order[level] = this.cars[car].BookCar(levels[level], true);

                }
                return order;
            }

        }
    */

}
