using Model;

public static class Controller_Users
{
    public static Users GetUser(string Email, string password)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        Users user = myData.Users.Where(U => U.Email == Email && U.Password == password).FirstOrDefault();
    
        return user;

        }
        
        }

        catch (Exception e)
        {

        }
        return null;
    }

    public static async void AddUser(Users new_user, Guid Manager_ID = default)
    {
        Console.WriteLine("Part1");
        try
        {
        using (var myData = new MyContext()) {

        // write queries here
        Console.WriteLine("Part2");

        if (new_user is not null)
        {
            Console.WriteLine("Part3");
            Users Check_If_Email_Is_Free = myData.Users.Where(U => U.Email == new_user.Email).FirstOrDefault();
            
            if (Check_If_Email_Is_Free is not null)
            {
                Console.WriteLine("Error: Email already exist");
                return; 
            }

            myData.Users.Add(new_user);
            if (Manager_ID != default)
            {
                myData.Manager_and_User.Add(new Manager_and_User(Guid.NewGuid(), Manager_ID, new_user.ID));
                Console.WriteLine($"Employee {new_user.ID} added to Manager {Manager_ID}.");
            }

            if (Manager_ID == default)
            {
                Console.WriteLine($"Regular User {new_user.ID} added.");
            }
    
            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
            
        }

        }
        
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

    public static Users GetUser_byID(Guid Id)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        Users user = myData.Users.Where(U => U.ID == Id).FirstOrDefault();
    
        return user;

        }
        
        }

        catch (Exception e)
        {

        }
        return null;
    }

    public static List<TodoItem> GetAllUsers(Guid Manager_ID)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        var Query = from MAU in myData.Manager_and_User
                join u in myData.Users on MAU.User_Id equals u.ID
                where MAU.Manager_Id == Manager_ID
                select u;

        List<Users> users = Query.ToList();
        List<TodoItem> users_as_TodoItem = new List<TodoItem>();

        if (users is not null && users.Count > 0)
        {
            foreach (Users user in users)
            {
                users_as_TodoItem.Add(new TodoItem(){ ID = user.ID, Title = $"{user.First_name} {user.Last_name}"});
            }
        }

         if (users is null || users.Count == 0)
        {
            Console.WriteLine("No users found");
        }

        return users_as_TodoItem;

        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }

    public static async void AddMockUser(Guid Manager_ID)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here
        int number_of_employees_for_this_manager = myData.Manager_and_User.Where(MAU => MAU.Manager_Id == Manager_ID).Select(U => U.User_Id).Count();

        Guid mock_employee_id = Guid.NewGuid();
        string Email = "Employee" + Convert.ToString(number_of_employees_for_this_manager + 1) + "@outlook.com";
        string First_name = "Employee" + Convert.ToString(number_of_employees_for_this_manager + 1);
        string Last_name = "Doe";
        string Password = First_name;
        string User_type = "User";
        Users mock_employee = new Users(mock_employee_id, Email, First_name, Last_name, Password, User_type);

        myData.Users.Add(mock_employee);
        myData.Manager_and_User.Add(new Manager_and_User(Guid.NewGuid(), Manager_ID, mock_employee.ID));

        var changes = await myData.SaveChangesAsync();
        Console.WriteLine(changes);
        Console.WriteLine($"Mock employee {First_name} added.");

        }
        
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

}