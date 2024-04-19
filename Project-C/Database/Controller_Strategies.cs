using Microsoft.EntityFrameworkCore;
using Model;

public static class Controller_Strategies
{
    public static List<TodoItem> GetStrategies(OGSM current_OGSM)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        var Query = from o in myData.OGSM
                join sio in myData.Strategies_In_OGSM on o.ID equals sio.OGSM_Id
                join s in myData.Strategies on sio.Strategie_Id equals s.ID
                where o.ID == current_OGSM.ID
                select s;

        List<Strategies> strategies = Query.ToList();
        List<TodoItem> strategies_as_TodoItem = new List<TodoItem>();

        if (strategies is not null && strategies.Count > 0)
        {
            foreach (Strategies strategie in strategies)
            {
                strategies_as_TodoItem.Add(new TodoItem(){ ID = strategie.ID, Title = strategie.Name});
            }
        }

        return strategies_as_TodoItem;

        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }

    public static async void AddStrategie(Guid OGSM_ID, Strategies new_strategie)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        if (new_strategie is not null)
        {
            OGSM ogsm = myData.OGSM.Where(O => O.ID == OGSM_ID).FirstOrDefault();

        if (ogsm is not null)
        {
            myData.Add(new_strategie);
            myData.Add(new Strategies_In_OGSM(Guid.NewGuid(), OGSM_ID, new_strategie.ID));

            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }
        
        else
        {
            Console.WriteLine("OGSM not found");
        }

        }

        }
        
        }

        catch (Exception e)
        {

        }

    }

    public static async void UpdateStrategie(Guid Strategie_ID, string new_strategie)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        Strategies Current_strategie = myData.Strategies.AsNoTracking().Where(S => S.ID == Strategie_ID).FirstOrDefault();

        if (Current_strategie is not null && new_strategie is not null && new_strategie != "")
        {
            var update = Current_strategie with {Name = new_strategie};
            myData.Strategies.Update(update);

            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        else
        {
            Console.WriteLine("Strategie not found");
        }

        }

        }

        catch (Exception e)
        {

        }
    }
}