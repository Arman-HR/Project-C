using Microsoft.EntityFrameworkCore;
using Model;

public static class Controller_Actions
{
    public static List<TodoItem> GetActions(OGSM current_OGSM)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        var Query = from o in myData.OGSM
                join aio in myData.Actions_In_OGSM on o.ID equals aio.OGSM_Id
                join a in myData.Actions on aio.Action_Id equals a.ID
                where o.ID == current_OGSM.ID
                select a;

        List<Actions> actions = Query.ToList();
        List<TodoItem> actions_as_TodoItem = new List<TodoItem>();

        if (actions is not null && actions.Count > 0)
        {
            foreach (Actions action in actions)
            {
                actions_as_TodoItem.Add(new TodoItem(){ ID = action.ID, Title = action.Name, Done = action.Done});
            }
        }

        return actions_as_TodoItem;

        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }

    public static async void AddAction(Guid OGSM_ID, Actions new_action)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        if (new_action is not null)
        {
            OGSM ogsm = myData.OGSM.Where(O => O.ID == OGSM_ID).FirstOrDefault();

        if (ogsm is not null)
        {
            myData.Add(new_action);
            myData.Add(new Actions_In_OGSM(Guid.NewGuid(), OGSM_ID, new_action.ID));

            var changes = await myData.SaveChangesAsync();
        }

        else
        {
            Console.WriteLine("OGSM is not found");
        }

        }

        }
        
        }

        catch (Exception e)
        {

        }

    }

    public static async void UpdateAction(Guid Action_ID, string new_action)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        Actions Current_action = myData.Actions.AsNoTracking().Where(A => A.ID == Action_ID).FirstOrDefault();

        if (Current_action is not null && new_action is not null && new_action == "g9f31rg97gf3uoufaf9af7uef79hudosf9979w")
        {
            if (Current_action.Done)
            {
                var update = Current_action with {Done = false};
                myData.Actions.Update(update);
                Console.WriteLine("Action changed to Unfinished");
            }
            else
            {
                var update = Current_action with {Done = true};
                myData.Actions.Update(update);
                Console.WriteLine("Action changed to Finished");
            }

            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        if (Current_action is not null && new_action is not null && new_action != "" && new_action != "g9f31rg97gf3uoufaf9af7uef79hudosf9979w")
        {
            var update = Current_action with {Name = new_action};
            myData.Actions.Update(update);
            
            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        if (Current_action is null || new_action is null)
        {
            Console.WriteLine("Action not found");
        }

        }
        
        }

        catch (Exception e)
        {

        }
    }

    public static List<TodoItem> GetAllActions(Guid Manager_ID)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        var Query = from MAU in myData.Manager_and_User
                join u in myData.Users on MAU.User_Id equals u.ID
                join opu in myData.OGSMs_Per_User on u.ID equals opu.User_Id
                join o in myData.OGSM on opu.OGSM_Id equals o.ID
                join aio in myData.Actions_In_OGSM on o.ID equals aio.OGSM_Id
                join a in myData.Actions on aio.Action_Id equals a.ID
                where MAU.Manager_Id == Manager_ID
                select a;

        List<Actions> actions = Query.ToList();
        List<TodoItem> actions_as_TodoItem = new List<TodoItem>();

        if (actions is not null && actions.Count > 0)
        {
            foreach (Actions action in actions)
            {
                actions_as_TodoItem.Add(new TodoItem(){ ID = action.ID, Title = action.Name, Done = action.Done});
            }
        }

         if (actions is null || actions.Count == 0)
        {
            Console.WriteLine("No Actions found");
        }

        return actions_as_TodoItem;

        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }
}