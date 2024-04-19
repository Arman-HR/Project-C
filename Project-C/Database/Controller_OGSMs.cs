using Microsoft.EntityFrameworkCore;
using Model;

public static class Controller_OGSMs
{
    public static List<TodoItem> GetOGSMs(Users current_user)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here
        Guid Manager_id = myData.Manager_and_User.Where(MAU => MAU.User_Id == current_user.ID).Select(M => M.Manager_Id).FirstOrDefault();

        if (Manager_id == default)
        {
            Console.WriteLine("No Manager found");
            return new List<TodoItem>();
        }

        var Query = from mau in myData.Manager_and_User
                join u in myData.Users on mau.User_Id equals u.ID
                join opu in myData.OGSMs_Per_User on u.ID equals opu.User_Id
                join o in myData.OGSM on opu.OGSM_Id equals o.ID
                where mau.Manager_Id == Manager_id
                select o;

        List<OGSM> ogsms = Query.ToList();
        List<TodoItem> ogsms_as_TodoItem = new List<TodoItem>();

        if (ogsms is not null && ogsms.Count > 0)
        {
            foreach (OGSM ogsm in ogsms)
            {
                ogsms_as_TodoItem.Add(new TodoItem(){ ID = ogsm.ID, Title = ogsm.Name});
            }
        }

        return ogsms_as_TodoItem;
        }
        
        }

        catch (Exception e)
        {
            return new List<TodoItem>();
        }
    }

    public static OGSM GetOGSM_byID(Guid Id)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        OGSM OGSM = myData.OGSM.Where(O => O.ID == Id).FirstOrDefault();

        return OGSM;

        }
        
        }

        catch (Exception e)
        {

        }
        return null;
    }

    public static async void AddOGSM(Guid UserID, OGSM new_ogsm)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        if (new_ogsm is not null)
        {
            Users user = myData.Users.Where(U => U.ID == UserID).FirstOrDefault();

        if (user is not null)
        {
            myData.Add(new_ogsm);
            myData.Add(new OGSMs_Per_User(Guid.NewGuid(), user.ID, new_ogsm.ID));
            
            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        else
        {
            Console.WriteLine("User is not found");
        }

        }

        }
        
        }

        catch (Exception e)
        {

        }

    }

    public static async void SetNewAmbition(Guid OGSM_ID, string new_ambition)
    {
        try
        {
        using (var myData = new MyContext()) {

        // write queries here

        OGSM Current_OGSM = myData.OGSM.AsNoTracking().Where(O => O.ID == OGSM_ID).FirstOrDefault();

        if (Current_OGSM is not null && new_ambition is not null && new_ambition != "")
        {
            var update = Current_OGSM with {Ambition = new_ambition};
            myData.OGSM.Update(update);
            
            var changes = await myData.SaveChangesAsync();
            Console.WriteLine(changes);
        }

        else
        {
            Console.WriteLine("OGSM not found");
        }

        }
        
        }

        catch (Exception e)
        {

        }
    }
}