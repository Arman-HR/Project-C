using Microsoft.EntityFrameworkCore;
using Model;

public static class Controller_Goals
{
    public static List<TodoItem> GetGoals(OGSM current_OGSM)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        var Query = from o in myData.OGSM
                join gio in myData.Goals_In_OGSM on o.ID equals gio.OGSM_Id
                join g in myData.Goals on gio.Goal_Id equals g.ID
                where o.ID == current_OGSM.ID
                select g;

        List<Goals> goals = Query.ToList();
        List<TodoItem> goals_as_TodoItem = new List<TodoItem>();

        if (goals is not null && goals.Count > 0)
        {
            foreach (Goals goal in goals)
            {
                goals_as_TodoItem.Add(new TodoItem(){ ID = goal.ID, Title = goal.Name});
            }
        }

        return goals_as_TodoItem;

        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }

    public static async void AddGoal(Guid OGSM_ID, Goals new_goal)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        if (new_goal is not null)
        {
            OGSM ogsm = myData.OGSM.Where(O => O.ID == OGSM_ID).FirstOrDefault();

        if (ogsm is not null)
        {
            myData.Add(new_goal);
            myData.Add(new Goals_In_OGSM(Guid.NewGuid(), OGSM_ID, new_goal.ID));

            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
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

    public static async void UpdateGoal(Guid Goal_ID, string new_goal)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        Goals Current_goal = myData.Goals.AsNoTracking().Where(G => G.ID == Goal_ID).FirstOrDefault();

        if (Current_goal is not null && new_goal is not null && new_goal != "")
        {
            var update = Current_goal with {Name = new_goal};
            myData.Goals.Update(update);
            
            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        else
        {
            Console.WriteLine("Goal not found");
        }

        }
        
        }

        catch (Exception e)
        {

        }
    }
}