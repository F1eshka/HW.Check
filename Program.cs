using System.Collections;

namespace HW.Check
{

    struct Product
    {
        private string productName;
        private double price;

        public double GetPrice()
        {
            return price;
        }

        public void SetPrice(double price)
        {
            this.price = price;
        }

        public string GetProductName()
        {
            return productName;
        }

        public void SetProductName(string productName)
        {
            this.productName = productName;
        }
    }

    struct Shop
    {
        private string name;
        private string address;

        public string GetAddress()
        {
            return address;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
    }

    struct Check
    {
        private List<(Product, int)> products; 
        private Shop shop;
        private double clientMoney;
        private int discount;
        private double surrenders;
        private DateTime dataTime;
        private float itog;

        public void SetShop(Shop shop)
        {
            this.shop = shop;
        }

        public void AddProduct(Product product, int quantity)
        {
            if (products == null)
            {
                products = new List<(Product, int)>();
            }
            products.Add((product, quantity));
        }

        public List<(Product, int)> GetProducts()
        {
            return products;
        }

        public Shop GetShop()
        {
            return shop;
        }

        public void SetItog(float itog)
        {
            this.itog = itog;
        }

        public float GetItog()
        {
            return itog;
        }
    
        public DateTime GetDataTime()
        {
            return dataTime;
        }

        public void SetDataTime(DateTime dataTime)
        {
            this.dataTime = dataTime;
        }


        public double GetSurrenders()
        {
            return surrenders;
        }

        public void SetSurrenders(double surrenders)
        {
            this.surrenders = surrenders;
        }


        public int GetDiscount()
        {
            return discount;
        }

        public void SetDiscount(int discount)
        {
            this.discount = discount;
        }

        public double GetMyMoney()
        {
            return clientMoney;
        }

        public void SetMyMoney(double clientMoney)
        {
            this.clientMoney = clientMoney;
        }

    }

    struct CalculateCheck
    {
        public void Calculate(ref Check check)
        {
            double totalPrice = 0;
            foreach (var (product, quantity) in check.GetProducts())
            {
                totalPrice += product.GetPrice() * quantity;
            }

            float discountAmount = (float)(totalPrice * check.GetDiscount() / 100);

            check.SetItog((float)(totalPrice - discountAmount));

            check.SetSurrenders(check.GetMyMoney() - check.GetItog());

        }
    }


    struct ConsoleInputInformtion
    {
        private bool operation;

        public void Input(ref Check check)
        {
            Product product = new Product();
            Shop shop = new Shop();

            operation = true;
            while (operation)
            {
                try
                {
                    Console.Write("Enter shop name: ");
                    String shopName = Console.ReadLine();
                    shop.SetName(shopName);

                    Console.Write("Enter address: ");
                    String address = Console.ReadLine();
                    shop.SetAddress(address);

                    check.SetShop(shop);
                    check.SetDataTime(DateTime.Now);
                                      
                    Console.Write("Enter discount in %: ");
                    int discount = Convert.ToInt32(Console.ReadLine());
                    check.SetDiscount(discount);

                    bool addMoreProducts = true;
                    while (addMoreProducts)
                    {


                        Console.Write("Enter product name: ");
                        String productName = Console.ReadLine();
                        product.SetProductName(productName);

                        Console.Write("Enter price: ");
                        double price = Convert.ToDouble(Console.ReadLine());
                        product.SetPrice(price);

                        Console.Write("Enter quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        check.AddProduct(product, quantity);

                        Console.Write("Do you want to add another product? (y/n): ");
                        string response = Console.ReadLine().ToLower();
                        addMoreProducts = response == "y";
                    }
                    Console.Write("Enter your money: ");
                    double myMoney = Convert.ToDouble(Console.ReadLine());
                    check.SetMyMoney(myMoney);

                    operation = false;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }



    struct ConsolePrinter
    {
        public void Print(ref Check check)
        {
            Console.WriteLine();
            Console.WriteLine(check.GetShop().GetName());
            Console.WriteLine(check.GetShop().GetAddress());

            Console.WriteLine("-------------------------------------");

            foreach (var (product, quantity) in check.GetProducts())
            {
                Console.WriteLine($"{product.GetProductName()} (x{quantity})");
                Console.WriteLine(String.Format("Price:          {0:F2} $ \n\t", product.GetPrice()));
            }

            Console.WriteLine("-------------------------------------");

            Console.WriteLine(String.Format("Your money:     {0:F2} $ \n\t", check.GetMyMoney()));

            Console.WriteLine(String.Format("Total price:     {0:F2} $ \n\t", check.GetItog()));

            Console.WriteLine(String.Format("Surrenders:     {0:F2} $ \n\t", check.GetSurrenders()));

            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Discount: " + check.GetDiscount() + "%");

            Console.WriteLine(String.Format("With discount:  {0:F2} $\n", check.GetItog()));

            Console.Write("Total: ");
            Console.WriteLine(String.Format("      {0:F2} $\n", check.GetItog()));

            Console.Write("-------------------------------------\n");

            Console.WriteLine(check.GetDataTime());
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Check check = new Check();
            ConsoleInputInformtion input = new ConsoleInputInformtion();
            CalculateCheck calculateCheck = new CalculateCheck();
            ConsolePrinter consolePrinter = new ConsolePrinter();

            input.Input(ref check);
            calculateCheck.Calculate(ref check);
            consolePrinter.Print(ref check);
        }
    }
}