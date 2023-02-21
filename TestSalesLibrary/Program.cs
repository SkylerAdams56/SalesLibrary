using SalesLibrary;

CustomersController custCtrl= new ("localhost", "sqlexpress");

var customers = custCtrl.GetBySales(20000, 50000);

foreach(var cust in customers)
{
    Console.WriteLine($"{cust.Id} | {cust.Name} | {cust.City}, {cust.State} | {cust.Sales} | {cust.Active}");
}

//var max = custCtrl.GetById(36);

//if (max == null)
//{
//    Console.WriteLine("Not Found");
//    return;
//}

//max.Name = "MAX Technical training";

//var success = custCtrl.Update(max);

//if (!success)
//{
//    Console.WriteLine("Update Failed");
//    return;
//}

//var newCustomer = new Customer
//{
//    Id = 0, Name = "MAX", City = "Mason", State = "OH", Sales = 1000, Active = true
//};
//var success = custCtrl.Add(newCustomer);
//if(!success)
//{
//    Console.WriteLine("Add failed");
//}


//var customers = custCtrl.GetAll();

//foreach(var customer in customers)
//{
//    Console.WriteLine($"The customer name is {customer.Name} Very Cool!");
//    //var intro = "The customer name is ";
//    //var outro = "Very cool!";
//    //Console.WriteLine(intro + customer.Name + outro);
//}


//var success = custCtrl.Delete(36);

//var cust = custCtrl.GetById(36);
//if (cust is null)
//{
//    Console.WriteLine("Not Found!!");
//}
//else
//{
//    Console.WriteLine($"{cust.Id} | {cust.Name} | {cust.City}, {cust.State} | {cust.Sales} | {cust.Active}");
//}


custCtrl.CloseConnection();