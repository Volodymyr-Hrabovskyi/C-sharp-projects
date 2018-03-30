using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_SoftTest
{
    abstract class Pet
    {
        public string Name;
        public string Breed;

        public void Introduce()
        {
            Console.WriteLine("I'm {0} of {1}.I'm a {2}", Name, Breed, GetType().Name);
        }
    }
    class PetShop
    {
        List<Pet> pets = new List<Pet>();

        public void AddPet(Pet animal)
        {
            pets.Add(animal);
        }

        public void IntroduceAll()
        {
            foreach (var p in pets)
                p.Introduce();
        }
    }
    class Dog : Pet
    {
        public Dog(string n, string b) : base()
        {
            Name = n;
            Breed = b;
        }

    }
    class Cat : Pet
    {
        public Cat(string n, string b) : base()
        {
            Name = n;
            Breed = b;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            PetShop petshop = new PetShop();
            petshop.AddPet(new Dog("Rex", "Fex"));
            petshop.AddPet(new Cat("Mura", "Home"));
            petshop.AddPet(new Dog("Bim", "Jef"));
            petshop.IntroduceAll();
            Console.ReadLine();
        }
    }
}
